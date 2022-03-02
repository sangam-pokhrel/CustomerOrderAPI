namespace CustomerOrder.DTO
{
    /// <summary>
    /// Class that represents a product request
    /// </summary>
    public class ProductRequest
    {
        /// <summary>
        /// The type of Product(Available Values: 'Photobook', 'Calendar', 'Canvas', 'Cards', 'Mug')
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        /// The Quantity of the specific product ordered
        /// </summary>
        public int Quantity { get; set; }
    }
}