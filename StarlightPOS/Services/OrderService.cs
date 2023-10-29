namespace StarlightPOS.Services
{
    public class OrderService : IOrderService
    {
        public int TableIndex { get; set; } = 0;
    }

    public interface IOrderService
    {
        int TableIndex { get; set; }
    }
}
