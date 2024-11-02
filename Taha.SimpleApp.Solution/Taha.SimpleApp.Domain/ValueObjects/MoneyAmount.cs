namespace Taha.SimpleApp.Domain.ValueObjects
{
    public class MoneyAmount
    {
        public MoneyAmount(decimal price, Currency currency)
        {
            if (currency == Currency.None)
                throw new ArgumentOutOfRangeException(nameof(currency), "Currency can not be None");

            Price = price;
            Currency = currency;
        }

        private decimal _price;
        public decimal Price
        {
            get => _price;
            private set
            {
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value);
                _price = value;
            }
        }
        public Currency Currency { get; }

        public MoneyAmount Multiply(decimal amount)
        {
            amount *= Price;
            Price = amount;
            return this;
        }
    }
}