using AutoMapper;
using CustomerCardService.Api.Models.Input;
using CustomerCardService.Api.Models.Output;
using CustomerCardService.Domain.Models;
using CustomerCardService.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCardService.Domain.Services
{
    public class CardService : ICardService
    {
        private readonly CardContext cardContext;
        private readonly IMapper mapper;
        public CardService(CardContext cardContext, IMapper mapper)
        {
            this.cardContext = cardContext;
            this.mapper = mapper;
        }

        public Guid GenerateToken(CardSaveInput card)
        {
            long cardNumber = card.CardNumber;
            int cardNumberLastFourDigits = GetLastFourDigits(cardNumber);
            int[] rightRotatedDigits = RightRotateNumberToIntArray(cardNumberLastFourDigits, card.CVV);
            string rotatedString = string.Join("", rightRotatedDigits);
            byte[] hashedBytes = CalculateStringMD5Hash(rotatedString);
            string hashedRotatedString = ConvertByteArrayToString(hashedBytes);

            return new Guid(hashedRotatedString);
        }

        public CardSaveOutput SaveCard(CardSaveInput cardInput)
        {
            Card cardOrDefault = cardContext.Cards
                .SingleOrDefault(c => c.CardNumber == cardInput.CardNumber);

            if (cardOrDefault == null)
            {
                Card card = mapper.Map<Card>(cardInput);
                card.Token = GenerateToken(cardInput);

                cardContext.AddAsync(card);
                cardContext.SaveChangesAsync();

                return mapper.Map<CardSaveOutput>(card);
            }

            cardOrDefault.Token = GenerateToken(cardInput);
            cardContext.AddAsync(cardOrDefault);
            cardContext.SaveChangesAsync();

            return mapper.Map<CardSaveOutput>(cardOrDefault);

        }

        public bool ValidateToken(CardTokenValidationInput card)
        {
            throw new NotImplementedException();
        }

        private static int GetLastFourDigits(long number)
        {
            return (int)(number % 10000);
        }

        private byte[] CalculateStringMD5Hash(string target)
        {
            using (MD5 md5 = MD5.Create())
            {
                return md5.ComputeHash(Encoding.UTF8.GetBytes(target));
            }
        }

        private string ConvertByteArrayToString(byte[] hash)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }

            return sb.ToString();
        }

        private int[] RightRotateNumberToIntArray(int number, int rotations)
        {
            int numberLength = (int)Math.Log10(number) + 1;
            int[] rightRotatedVector = new int[numberLength];

            for (int i = numberLength - 1, multiplicationFactor = 10, divisionFactor = 1; i >= 0; i--,
                multiplicationFactor *= 10, divisionFactor *= 10)
            {
                int digit = (number % multiplicationFactor) / divisionFactor;
                rightRotatedVector[(i + rotations) % numberLength] = digit;
            }
            return rightRotatedVector;
        }


    }
}
