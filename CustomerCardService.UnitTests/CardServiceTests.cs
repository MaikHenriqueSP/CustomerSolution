using CustomerCardService.Domain.Exceptions;
using CustomerCardService.Domain.Models;
using CustomerCardService.Domain.Repository;
using CustomerCardService.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CustomerCardService.UnitTests
{
    public class CardServiceTests
    {
        private readonly CardService cardService;
        private readonly Mock<CardContext> cardContextMock = new Mock<CardContext>();
        public CardServiceTests()
        {
            cardService = new CardService(cardContextMock.Object);
        }

        [Fact]
        public void SaveCard_WhenCardIsValid_ShouldReturnSavedCard()
        {
            //Arrange
            Card card = new()
            {
                CVV = 123,
                CardNumber = 12345,
                CustomerId = 12345,
            };

            var cards = new List<Card>() { card };
            var mockDbSet = DbContextMock.GenerateDbSetFromList(cards);
            cardContextMock.Setup(c => c.Cards).Returns(mockDbSet);

            //Act       
            Card savedCard = cardService.SaveCard(card);

            //Assert
            Assert.Equal(card.CardNumber, savedCard.CardNumber);
            Assert.Equal(card.CVV, savedCard.CVV);
            Assert.Equal(card.CustomerId, savedCard.CustomerId);
        }

        [Fact]
        public void SaveCard_WhenCardAlreadyExistsAndCustomerIdIsDifferent_ShouldThrowInconsistentCardException()
        {
            //Arrange
            Card firstSavedCard = new()
            {
                CVV = 123,
                CardNumber = 12345,
                CustomerId = 12345
            }
            ;

            Card differentCustomerIdCard = new()
            {
                CVV = firstSavedCard.CVV,
                CardNumber = firstSavedCard.CardNumber,
                CustomerId = 11,
            };

            var cards = new List<Card>() { firstSavedCard };
            var mockDbSet = DbContextMock.GenerateDbSetFromList(cards);
            cardContextMock.Setup(c => c.Cards).Returns(mockDbSet);

            //Act       
            cardService.SaveCard(firstSavedCard);
            Action saveDifferentCustomerIdCard = () => cardService.SaveCard(differentCustomerIdCard);

            //Assert
            Assert.Throws<InconsistentCardException>(saveDifferentCustomerIdCard);
        }

        [Fact]
        public void SaveCard_WhenCardAlreadyExistsAndCVVIsDifferent_ShouldThrowInconsistentCardException()
        {
            //Arrange
            Card firstSavedCard = new()
            {
                CVV = 123,
                CardNumber = 12345,
                CustomerId = 12345,
            };

            Card differentCVVCard = new()
            {
                CVV = 444,
                CardNumber = firstSavedCard.CardNumber,
                CustomerId = firstSavedCard.CustomerId,
            };

            var cards = new List<Card>() { firstSavedCard };
            var mockDbSet = DbContextMock.GenerateDbSetFromList(cards);
            cardContextMock.Setup(c => c.Cards).Returns(mockDbSet);

            //Act       
            cardService.SaveCard(firstSavedCard);
            Action saveDifferentCVVCard = () => cardService.SaveCard(differentCVVCard);

            //Assert
            Assert.Throws<InconsistentCardException>(saveDifferentCVVCard);

        }


        [Fact]
        public void ValidateToken_WhenCardNotSaved_ShouldThrowCardNotFoundException()
        {
            //Arrange
            Card randomCard = new()
            {
                CVV = 123,
                CardNumber = 12345,
                CustomerId = 12345,
            };

            var cards = new List<Card>() { randomCard };
            var mockDbSet = DbContextMock.GenerateDbSetFromList(cards);
            cardContextMock.Setup(c => c.Cards).Returns(mockDbSet);

            //Act       
           
            Action validateCardToken = () => cardService.ValidateToken(randomCard);

            //Assert
            Assert.Throws<CardNotFoundException>(validateCardToken);
        }

    }
}
