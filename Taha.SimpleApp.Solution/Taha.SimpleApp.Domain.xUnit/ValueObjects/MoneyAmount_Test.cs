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
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => new MoneyAmount(price, Currency.USD));
            Assert.Equal("price", ex.ParamName);
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
    }
}
