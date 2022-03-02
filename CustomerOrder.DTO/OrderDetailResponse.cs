namespace CustomerOrder.DTO
{
    public class OrderDetailResponse
    {
        /// <summary>
        /// The Required Bin Width in mm
        /// </summary>
        public decimal RequiredBinWidth { get; set; }

        /// <summary>
        /// The list of Product Type with their quantity
        /// </summary>
        public IEnumerable<ProductRequest> Products { get; set; }
    }
}