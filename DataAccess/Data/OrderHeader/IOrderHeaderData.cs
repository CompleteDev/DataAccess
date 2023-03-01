using DataAccess.Models;

namespace DataAccess.Data;

public interface IOrderHeaderData
{
    Task<OrderHeaderMDL?> GetOrderDetails(int Id);
    Task<IEnumerable<OrderHeaderMDL?>> GetOrderEvents(int Id);
    Task<OrderHeaderMDL?> GetOrderHeader(int Id);
    Task<IEnumerable<OrderHeaderMDL>> GetOrderHeaders();
    Task<IEnumerable<OrderHeaderMDL>> GetResetOrders();
    Task UpdateOrderHeaderReset(OrderHeaderMDL orderheader);
}