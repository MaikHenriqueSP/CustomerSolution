using CustomerCardService.Domain.Exceptions;
using CustomerCardService.Domain.Models;
using CustomerCardService.Domain.Repository;
using CustomerCardService.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly Mock<CardContext> cardContextMock = new();
        private readonly Mock<ILogger<CardService>> logger = new();
        public CardServiceTests()
        {
            cardService = new CardService(cardContextMock.Object, logger.Object);
        }

        [Fact]
        public void SaveCard_WhenCardIsValid_ShouldReturnSavedCard()
        {
            //Arrange
            Card card = new()
            {
                CVV = 123,
                CardNumber = 12345,
                Customer = new Customer()
                { CustomerId = 12345 },
            };

            var cards = new List<Card>();
            var mockDbSet = DbContextMock.GenerateDbSetFromList(cards);
            cardContextMock.Setup(c => c.Cards).Returns(mockDbSet);

            //Act       
            Card savedCard = cardService.SaveCard(card).Result;

            //Assert
            Assert.Equal(card.CardNumber, savedCard.CardNumber);
            Assert.Equal(card.CVV, savedCard.CVV);
            Assert.Equal(card.Customer.CustomerId, savedCard.Customer.CustomerId); ;
        }

        [Fact]
        public async void SaveCard_WhenCardAlreadyExistsAndCustomerIdIsDifferent_ShouldThrowInconsistentCardException()
        {
            //Arrange
            Card firstSavedCard = new()
            {
                CVV = 123,
                CardNumber = 12345,
                Customer = new Customer()
                { CustomerId = 12345 },
            }
            ;

            Card differentCustomerIdCard = new()
            {
                CVV = firstSavedCard.CVV,
                CardNumber = firstSavedCard.CardNumber,
                Customer = new Customer()
                { CustomerId = 11 },
            };

            var cards = new List<Card>() { firstSavedCard };
            var mockDbSet = DbContextMock.GenerateDbSetFromList(cards);
            cardContextMock.Setup(c => c.Cards).Returns(mockDbSet);

            //Assert
            await Assert.ThrowsAsync<InconsistentCardException>(() => cardService.SaveCard(differentCustomerIdCard));
        }

        [Fact]
        public async void SaveCard_WhenCardAlreadyExistsAndCVVIsDifferent_ShouldThrowInconsistentCardException()
        {
            //Arrange
            Card firstSavedCard = new()
            {
                CVV = 123,
                CardNumber = 12345,
                Customer = new Customer()
                { CustomerId = 12345 },
            };

            Card differentCVVCard = new()
            {
                CVV = 444,
                CardNumber = firstSavedCard.CardNumber,
                Customer = new Customer()
                { CustomerId = firstSavedCard.Customer.CustomerId },
            };

            var cards = new List<Card>() { firstSavedCard };
            var mockDbSet = DbContextMock.GenerateDbSetFromList(cards);
            cardContextMock.Setup(c => c.Cards).Returns(mockDbSet);

            //Assert
            await Assert.ThrowsAsync<InconsistentCardException>(() => cardService.SaveCard(differentCVVCard));

        }

        [Fact]
        public async void ValidateToken_WhenCardNotSaved_ShouldThrowCardNotFoundException()
        {
            //Arrange
            Card randomCard = new()
            {
                CVV = 123,
                CardNumber = 12345,
                Customer = new Customer()
                { CustomerId = 12345 },
            };

            var cards = new List<Card>() { randomCard };
            var mockDbSet = DbContextMock.GenerateDbSetFromList(cards);
            cardContextMock.Setup(c => c.Cards).Returns(mockDbSet);

            //Assert
            await Assert.ThrowsAsync<CardNotFoundException>(() => cardService.ValidateToken(randomCard));
        }

        [Fact]
        public async void ValidateToken_WhenTokenIsValid_ShouldReturnTrue()
        {
            //Arrange
            Card validCard = new()
            {
                CVV = 620,
                CardNumber = 42345678922,
                Customer = new Customer()
                { CustomerId = 1254 },
                Token = new Token()
                {
                    CreationDate = DateTimeOffset.UtcNow,
                    TokenValue = Guid.Parse("5a751d6a-0b6e-f05c-fe51-b86e5d1458e6"),
                },
            };

            cardContextMock.Setup(t => t.Cards.FindAsync(It.IsAny<object>())).ReturnsAsync(validCard);

            //Act
            bool isTokenValid = await cardService.ValidateToken(validCard);

            //Assert
            Assert.True(isTokenValid);
        }


        [Fact]
        public async void ValidateToken_WhenTokenOutDated_ShouldReturnFalse()
        {
            //Arrange
            Card validCard = new()
            {
                CVV = 620,
                CardNumber = 42345678922,
                Customer = new Customer()
                { CustomerId = 1254 },
                Token = new Token()
                {
                    CreationDate = DateTimeOffset.UtcNow - TimeSpan.FromMinutes(31),
                    TokenValue = Guid.Parse("5a751d6a-0b6e-f05c-fe51-b86e5d1458e6"),
                },
            };

            cardContextMock.Setup(t => t.Cards.FindAsync(It.IsAny<object>())).ReturnsAsync(validCard);

            //Assert
            await Assert.ThrowsAsync<TokenExpiredException>(() => cardService.ValidateToken(validCard));
        }


        [Fact]
        public async void ValidateToken_WhenCardCustomerAreDifferent_ShouldThrowInconsistentCardException()
        {
            //Arrange
            Card validCard = new()
            {
                CVV = 620,
                CardNumber = 42345678922,
                Customer = new Customer()
                { CustomerId = 1254 },
                Token = new Token()
                {
                    CreationDate = DateTimeOffset.UtcNow,
                    TokenValue = Guid.Parse("5a751d6a-0b6e-f05c-fe51-b86e5d1458e6"),
                }

            };

            Card invalidCard = new()
            {
                CVV = validCard.CVV,
                CardNumber = validCard.CardNumber,
                Customer = new Customer()
                { CustomerId = 1111 },
                Token = new Token()
                {
                    CreationDate = validCard.Token.CreationDate,
                    TokenValue = validCard.Token.TokenValue
                }
            };

            cardContextMock.Setup(t => t.Cards.FindAsync(It.IsAny<object>())).ReturnsAsync(validCard);

            //Assert
            await Assert.ThrowsAsync<InconsistentCardException>(() => cardService.ValidateToken(invalidCard));
        }
    }
}
