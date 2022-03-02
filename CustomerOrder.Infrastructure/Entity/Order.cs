namespace CustomerOrder.Infrastructure.Entity
{
    public class Order
    {
        public string Id { get; set; }

        /// <summary>
        /// Required Bin Width in mm
        /// </summary>
        public decimal RequiredBinWidth { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}