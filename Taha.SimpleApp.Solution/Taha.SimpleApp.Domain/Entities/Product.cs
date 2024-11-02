using Taha.SimpleApp.Domain.ValueObjects;

namespace Taha.SimpleApp.Domain.Entities
{
    public class Product
    {
        public const int DESCRIPTION_MAX_LENGTH = 255;
        private string _description;

        public Product(string name, MoneyAmount price, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            ArgumentNullException.ThrowIfNull(price);

            Name = name;
            Price = price;
            _description = ValidDescription(description);
        }

        private static string ValidDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return string.Empty;

            if (description.Length > DESCRIPTION_MAX_LENGTH)
                throw new ArgumentException($"Description should not exceed {DESCRIPTION_MAX_LENGTH} characters.", nameof(description));

            return description;
        }

        public string Name { get; }
        public MoneyAmount Price { get; }
        public string Description
        {
            get => _description;
            set => _description = ValidDescription(value);
        }
        public string Image { get; set; } = string.Empty;
    }
}
