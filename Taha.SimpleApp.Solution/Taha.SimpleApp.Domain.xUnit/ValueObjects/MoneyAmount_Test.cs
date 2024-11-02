using Taha.SimpleApp.Domain.ValueObjects;

namespace Taha.SimpleApp.Domain.xUnit.ValueObjects
{
    public class MoneyAmount_Test
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CreateAmountWithZeroOrNegativePrice_ThrowsArgumentOutOfRangeException(decimal price)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MoneyAmount(price, Currency.USD));
        }

        [Fact]
        public void CreateAmountWithNoneCurrency_ThrowArgumentOutOfRangeException()
        {
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => new MoneyAmount(1, Currency.None));
            Assert.Equal("currency", ex.ParamName);
            Assert.StartsWith("Currency can not be None", ex.Message);
        }

        [Fact]
        public void CreateAmount_Success()
        {
            MoneyAmount moneyAmount = new(10, Currency.EUR);
            Assert.Equal(10, moneyAmount.Price);
            Assert.Equal(Currency.EUR, moneyAmount.Currency);
        }

        [Fact]
        public void MultiplyAmount_Success()
        {
            MoneyAmount moneyAmount = new(10, Currency.EUR);
            MoneyAmount result = moneyAmount.Multiply(2);
            Assert.Equal(20, result.Price);
            Assert.Equal(Currency.EUR, result.Currency);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void MultiplyAmountWithZeroOrNegativePrice_ThrowsArgumentOutOfRangeException(decimal value)
        {
            MoneyAmount moneyAmount = new(1, Currency.USD);
            Assert.Throws<ArgumentOutOfRangeException>(() => moneyAmount.Multiply(value));
        }
    }
}
