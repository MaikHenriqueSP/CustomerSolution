using CustomerCardService.Api.Models.Input;
using CustomerCardService.Api.Models.Output;
using Newtonsoft.Json;
using System;
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
        public async Task PostCard_WhenCardIsValid_ReturnsStatusCreated()
        {
            //Arrange
            var validCard = new CardSaveInput()
            {
                CVV = 2313,
                CardNumber = 2313,
                Customer = new CustomerInput()
                { CustomerId = 31 },
            };
            string endpoint = "/api/v1/Card";

            //Act
            HttpResponseMessage response = await TestClient.PostAsync(endpoint, GetStringContentSerialized(validCard));

            //Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task PostCard_WhenCardWhenCardNumberHasMoreThan16Digits_ReturnsStatusBadRequest()
        {
            //Arrange
            var invalidCard = new CardSaveInput()
            {
                CVV = 2313,
                CardNumber = 12345678912345678,
                Customer = new CustomerInput()
                { CustomerId = 31 },
            };
            string endpoint = "/api/v1/Card";

            //Act
            HttpResponseMessage response = await TestClient.PostAsync(endpoint, GetStringContentSerialized(invalidCard));

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostCard_WhenCardWhenCVVHasMoreThan5Digits_ReturnsStatusBadRequest()
        {
            //Arrange
            var invalidCard = new CardSaveInput()
            {
                CVV = 23132321,
                CardNumber = 123456789,
                Customer = new CustomerInput()
                { CustomerId = 31 },
            };
            string endpoint = "/api/v1/card";

            //Act
            HttpResponseMessage response = await TestClient.PostAsync(endpoint, GetStringContentSerialized(invalidCard));

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetTokenValidity_WhenTokenIsValid_ReturnsStatusOk()
        {
            //Arrange
            var validCard = new CardSaveInput()
            {
                CardNumber = 12344566,
                CVV = 1234,
                Customer = new CustomerInput()
                { CustomerId = 12 },
            };
            string endpoint = "/api/v1/card";
            string tokeValidityEndpoint = "/api/v1/card/token/validity";

            //Act
            HttpResponseMessage response = await TestClient.PostAsync(endpoint, GetStringContentSerialized(validCard));
            response.EnsureSuccessStatusCode();

            string responseString = await response.Content.ReadAsStringAsync();
            CardSaveOutput cardSaved = JsonConvert.DeserializeObject<CardSaveOutput>(responseString);

            CardTokenValidationInput validCardTokenValidationInput = new()
            {
                CardId = cardSaved.CardId,
                Customer = new CustomerInput()
                { CustomerId = validCard.Customer.CustomerId },
                CVV = validCard.CVV,
                TokenInput = new TokenInput() { TokenValue = cardSaved.Token.TokenValue, }

            };

            HttpResponseMessage tokenValidationResponse = await TestClient.PostAsync(tokeValidityEndpoint, GetStringContentSerialized(validCardTokenValidationInput));

            //Assert
            Assert.Equal(HttpStatusCode.OK, tokenValidationResponse.StatusCode);
        }


        [Fact]
        public async Task GetTokenValidity_WhenCardBelongsToDifferenCustomers_ReturnsStatusBadRequest()
        {
            //Arrange
            var validCard = new CardSaveInput()
            {
                CardNumber = 12344566,
                CVV = 1234,
                Customer = new CustomerInput()
                { CustomerId = 12 },
            };
            string endpoint = "/api/v1/card";
            string tokeValidityEndpoint = "/api/v1/card/token/validity";

            //Act
            HttpResponseMessage response = await TestClient.PostAsync(endpoint, GetStringContentSerialized(validCard));
            response.EnsureSuccessStatusCode();

            string responseString = await response.Content.ReadAsStringAsync();
            CardSaveOutput cardSaved = JsonConvert.DeserializeObject<CardSaveOutput>(responseString);

            CardTokenValidationInput validCardTokenValidationInput = new()
            {
                CardId = cardSaved.CardId,
                Customer = new CustomerInput()
                { CustomerId = 99 },
                CVV = validCard.CVV,
                TokenInput = new TokenInput() { TokenValue = cardSaved.Token.TokenValue },
            };

            HttpResponseMessage tokenValidationResponse = await TestClient.PostAsync(tokeValidityEndpoint, GetStringContentSerialized(validCardTokenValidationInput));

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, tokenValidationResponse.StatusCode);
        }

        [Fact]
        public async Task GetTokenValidity_WhenTokenIsDifferent_ReturnsStatusOK_AndValidityEqualsFalse()
        {
            //Arrange
            var validCard = new CardSaveInput()
            {
                CardNumber = 12344566,
                CVV = 1234,
                Customer = new CustomerInput()
                { CustomerId = 12 },
            };
            string endpoint = "/api/v1/card";
            string tokeValidityEndpoint = "/api/v1/card/token/validity";

            //Act
            HttpResponseMessage response = await TestClient.PostAsync(endpoint, GetStringContentSerialized(validCard));
            response.EnsureSuccessStatusCode();

            string responseString = await response.Content.ReadAsStringAsync();
            CardSaveOutput cardSaved = JsonConvert.DeserializeObject<CardSaveOutput>(responseString);

            CardTokenValidationInput validCardTokenValidationInput = new()
            {
                CardId = cardSaved.CardId,
                Customer = new CustomerInput()
                { CustomerId = validCard.Customer.CustomerId },
                CVV = validCard.CVV,
                TokenInput = new TokenInput() { TokenValue = Guid.NewGuid() },
            };

            HttpResponseMessage tokenValidationResponse = await TestClient.PostAsync(tokeValidityEndpoint, GetStringContentSerialized(validCardTokenValidationInput));

            //Assert            
            Assert.Equal(HttpStatusCode.OK, tokenValidationResponse.StatusCode);
        }

        private static StringContent GetStringContentSerialized(object obj,
            string mediaType = "application/json")
        {

            return new StringContent(JsonConvert.SerializeObject(obj),
                Encoding.UTF8, mediaType);
        }



    }
}
