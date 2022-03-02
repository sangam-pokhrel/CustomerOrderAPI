namespace CustomerOrder.Infrastructure.Entity
{
    public class OrderProduct
    {
        public string OrderId { get; set; }

        public short ProductTypeId { get; set; }

        public int Quantity { get; set; }

        public virtual Order Order { get; set; }

        public virtual ProductType ProductType { get; set; }
    }
}