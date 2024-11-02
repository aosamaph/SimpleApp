namespace Taha.SimpleApp.Domain.ValueObjects
{
    public class MoneyAmount
    {
        public MoneyAmount(decimal price, Currency currency)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
            if (currency == Currency.None)
                throw new ArgumentOutOfRangeException(nameof(currency), "Currency can not be None");
            Price = price;
            Currency = currency;
        }

        public decimal Price { get; }
        public Currency Currency { get; }
    }
}