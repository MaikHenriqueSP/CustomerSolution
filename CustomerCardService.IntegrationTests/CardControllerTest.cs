using CustomerCardService.Api.Models.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CustomerCardService.IntegrationTests
{
    public class CardControllerTest : BaseIntegrationTest
    {
        [Fact]
        public async Task PostCard_WhenCardIsValid_ReturnsStatusCodeCreated()
        {
            //Arrange
            var validCard = new CardSaveInput()
            {
                CVV = 2313,
                CardNumber = 2313,
                CustomerId = 31
            };
            string endpoint = "/api/v1/Card";

            //Act
            HttpResponseMessage response = await TestClient.PostAsync(endpoint, GetStringContentSerialized(validCard));

            //Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task PostCard_WhenCardWhenCardNumberHasMoreThan16Digits_ReturnsStatusCodeBadRequest()
        {
            //Arrange
            var invalidCard = new CardSaveInput()
            {
                CVV = 2313,
                CardNumber = 12345678912345678,
                CustomerId = 31
            };
            string endpoint = "/api/v1/Card";

            //Act
            HttpResponseMessage response = await TestClient.PostAsync(endpoint, GetStringContentSerialized(invalidCard));

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostCard_WhenCardWhenCVVHasMoreThan5Digits_ReturnsStatusCodeBadRequest()
        {
            //Arrange
            var invalidCard = new CardSaveInput()
            {
                CVV = 23132321,
                CardNumber = 123456789,
                CustomerId = 31
            };
            string endpoint = "/api/v1/card";

            //Act
            HttpResponseMessage response = await TestClient.PostAsync(endpoint, GetStringContentSerialized(invalidCard));

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        private static StringContent GetStringContentSerialized(CardSaveInput invalidCard,
            string mediaType = "application/json")
        {
            return new StringContent(JsonConvert.SerializeObject(invalidCard), 
                Encoding.UTF8, mediaType);
        }

    }
}
