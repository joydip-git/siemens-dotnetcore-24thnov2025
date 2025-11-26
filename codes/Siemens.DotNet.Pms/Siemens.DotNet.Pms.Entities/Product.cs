namespace Siemens.DotNet.Pms.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"Id={Id}, Name={Name}, Price={Price}, Description={Description}";
        }
        public override bool Equals(object? obj)
        {
            ArgumentNullException.ThrowIfNull(obj);

            if (obj == this) return true;

            if (obj is Product other)
            {
                return this.Id == other.Id;
            }
            else                
                throw new ArgumentException($"expected {nameof(Product)}, received {obj.GetType().Name}");
        }
        public override int GetHashCode()
        {
            const int prime = 31;
            return Id.GetHashCode() ^ Name.GetHashCode() ^ Price.GetHashCode() * prime;
        }
    }
}
