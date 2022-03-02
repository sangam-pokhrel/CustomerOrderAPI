namespace CustomerOrder.Infrastructure.Entity
{
    public class ProductType
    {
        public ProductType(short id, string name, decimal width, short stack)
        {
            Id = id;
            Name = name;
            Width = width;
            Stack = stack;
        }

        public short Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Width in mm
        /// </summary>
        public decimal Width { get; set; }

        /// <summary>
        /// How many times the product can be stacked on top of each other
        /// </summary>
        public short Stack { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}