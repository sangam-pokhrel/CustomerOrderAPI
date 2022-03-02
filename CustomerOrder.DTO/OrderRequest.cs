namespace CustomerOrder.DTO
{
    /// <summary>
    /// The class that represents an order request
    /// </summary>
    public class OrderRequest
    {
        /// <summary>
        /// The id that represents the specific order
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// The list of Product Type with their quantity
        /// </summary>
        public IEnumerable<ProductRequest> Products { get; set; }
    }
}