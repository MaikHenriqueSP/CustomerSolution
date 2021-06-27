using CustomerCardService.Domain.Models;
using CustomerCardService.Domain.Repository;
using CustomerCardService.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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
                CustomerId = 123
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
        public void SaveCard_WhenCardCVVHasMoreThan5Digits_ShouldThrow()
        {
            //Arrange
            Card card = new()
            {
                CVV = 1234567,
                CardNumber = 12345,

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




    }
}
