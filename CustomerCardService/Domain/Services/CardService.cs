using AutoMapper;
using CustomerCardService.Api.Models.Input;
using CustomerCardService.Api.Models.Output;
using CustomerCardService.Domain.Exceptions;
using CustomerCardService.Domain.Models;
using CustomerCardService.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCardService.Domain.Services
{
    /// <summary>
    /// Provides an implementation of the card service to provide services related to 
    /// the Card entity.
    /// </summary>
    public class CardService : ICardService
    {
        private readonly CardContext cardContext;
        private readonly ILogger logger;
        public CardService(CardContext cardContext, ILogger<CardService> logger)
        {
            this.cardContext = cardContext;
            this.logger = logger;
        }

        /// <summary>
        /// It saves in the context a given card, if it already exists then it simply
        /// updates the Token's creation time if the card is consistent with the one that is already
        /// registered.
        /// </summary>
        /// <param name="card"></param>
        /// <returns>Returns the instance of the saved card.</returns>
        public Card SaveCard(Card card)
        {
            Card cardOrDefault = cardContext.Cards
              .SingleOrDefault(c => c.CardNumber == card.CardNumber);

            if (cardOrDefault == null)
            {
                card.Token = new()
                {
                    TokenValue = GenerateToken(card),
                };
                card.Token.CreationDate = DateTimeOffset.UtcNow;

                cardContext.Add(card);
                cardContext.SaveChanges();

                return card;
            }

            if (card.Customer.CustomerId != cardOrDefault.Customer.CustomerId ||
                card.CVV != cardOrDefault.CVV)
            {
                throw new InconsistentCardException();
            }

            cardOrDefault.Token.CreationDate = DateTimeOffset.UtcNow;
            cardContext.Update(cardOrDefault);
            cardContext.SaveChanges();

            return cardOrDefault;
        }

        /// <summary>
        /// Generates a deterministic token based on a Card instance.
        /// </summary>
        /// <param name="card">Card instance</param>
        /// <returns>Returns a deterministic Guid</returns>
        protected Guid GenerateToken(Card card)
        {
            long cardNumber = card.CardNumber;
            int cardNumberLastFourDigits = GetLastFourDigits(cardNumber);
            int rightRotatedNumber = RightRotateNumber(cardNumberLastFourDigits, card.CVV);
            byte[] hashedBytes = CalculateStringMD5Hash(Encoding.UTF8.GetBytes(rightRotatedNumber.ToString()));
            string hashedRotatedString = ConvertByteArrayToString(hashedBytes);

            return new Guid(hashedRotatedString);
        }

        /// <summary>
        /// Performs the validation of token from a given card. 
        /// It checks if  the card exists, if it is within a valid period of time,
        /// if the provided card is consistent with the queried one and finally 
        /// returns if the tokens matches
        /// </summary>
        /// <param name="card">Card instance</param>
        /// <returns>Returns whether the card's token is valid or not</returns>
        public bool ValidateToken(Card card)
        {

            Card queriedCard = cardContext.Cards.Find(card.CardId);

            if (queriedCard == null)
            {
                throw new CardNotFoundException();
            }

            if (!IsTokenCreationTimeStillValid(queriedCard.Token.CreationDate))
            {
                throw new TokenExpiredException();
            }

            if (card.Customer.CustomerId != queriedCard.Customer.CustomerId)
            {
                throw new InconsistentCardException();
            }

            logger.LogInformation($"Card Number: {card.CardNumber}");

            Guid originalToken = GenerateToken(queriedCard);

            return originalToken.Equals(card.Token.TokenValue);
        }


        private static bool IsTokenCreationTimeStillValid(DateTimeOffset creationTime)
        {
            var rangeValidity = DateTimeOffset.UtcNow.Subtract(Card.TokenValiditySpan);
            return creationTime >= rangeValidity;
        }

        private static int GetLastFourDigits(long number)
        {
            return (int)(number % 10000);
        }

        /// <summary>
        /// Simply computes the hash of a given byte array applying the MD5 algorithm
        /// </summary>
        /// <param name="target">Target byte array that is going to be hashed</param>
        /// <returns>Hashed output of the 'target'</returns>
        private static byte[] CalculateStringMD5Hash(byte[] target)
        {
            using (MD5 md5 = MD5.Create())
            {
                return md5.ComputeHash(target);
            }
        }

        private static string ConvertByteArrayToString(byte[] hash)
        {
            StringBuilder sb = new();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Rotate a given number to the right by the rotations number of times.
        /// Starts by calculating the number of digits of the input number, by applying Log10,
        /// then it checks if performing rotations will result in any difference at all, 
        /// if not it just returns the input number.
        /// The it constructs the result by getting each ith-digit of the number, calculating its new
        /// position and adding it to the result.
        /// </summary>
        /// <param name="number">The target of the rotation</param>
        /// <param name="rotations">Number of right rotations that will be performed</param>
        /// <returns>Right rotated number</returns>
        private static int RightRotateNumber(int number, int rotations)
        {
            int numberLength = (int)Math.Log10(number) + 1;

            if (numberLength % rotations == 0)
            {
                return number;
            }

            int result = 0;

            for (int i = numberLength - 1, multiplicationFactor = 10, divisionFactor = 1; i >= 0; i--,
                multiplicationFactor *= 10, divisionFactor *= 10)
            {
                int ithDigit = (number % multiplicationFactor) / divisionFactor;
                int digitNewPosition = (i + rotations) % numberLength;
                result += (int)Math.Pow(10, numberLength - 1 - digitNewPosition) * ithDigit;
            }
            return result;
        }

    }
}
