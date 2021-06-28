using CustomerCardService.Domain.Models;
using System;
using Xunit;

namespace CustomerCardService.UnitTests
{
    public class CardTests
    {

        [Fact]
        public void InstantiateCard_WhenCardNumberHasMoreThan16Digits_ShouldThrowArgumentException()
        {
            Action cardInstantiation = () => new Card()
            {
                CardNumber = 12345678912345678,
            };

            Assert.Throws<ArgumentException>(cardInstantiation);
        }


        [Fact]
        public void InstantiateCard_WhenCardNumberIsNegative_ShouldThrowArgumentException()
        {
            Action cardInstantiation = () => new Card()
            {
                CVV = -123456789,
            };

            Assert.Throws<ArgumentException>(cardInstantiation);
        }

        [Fact]
        public void InstantiateCard_WhenCVVNumberHasMoreThan5Digits_ShouldThrowArgumentException()
        {
            Action cardInstantiation = () => new Card()
            {
                CVV = 123456789,
            };

            Assert.Throws<ArgumentException>(cardInstantiation);
        }

        [Fact]
        public void InstantiateCard_WhenCVVNumberIsNegative_ShouldThrowArgumentException()
        {
            Action cardInstantiation = () => new Card()
            {
                CVV = -123456789,
            };

            Assert.Throws<ArgumentException>(cardInstantiation);
        }

    }
}
