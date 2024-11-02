using Taha.SimpleApp.Domain.ValueObjects;

namespace Taha.SimpleApp.Domain.Entities
{
    public class Product
    {
        public const int DESCRIPTION_MAX_LENGTH = 255;

        private string _description;
        private MoneyAmount _price;

        public int Id { get; set; }
        public int CategoryId { get; set; }

        public Product(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
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

        public MoneyAmount Price
        {
            get => _price;
            set => _price = new(value.Price, value.Currency);
        }
        public string Description
        {
            get => _description;
            set => _description = ValidDescription(value);
        }
        public string Image { get; set; } = string.Empty;
    }
}
