using DataAccess.Models.OrderLines;

namespace DataAccess.Data.OrderLines;

public interface IOrderLinesData
{
    Task<OrderLinesMDL?> GetOrderLine(int Id);
    Task<IEnumerable<OrderLinesMDL?>> GetOrderLineHistory(int orderlineId);
    Task<IEnumerable<OrderLinesMDL?>> GetOrderLinesFromHeaderID(int Id);
    Task<IEnumerable<OrderLinesMDL?>> GetOrderLinesHistory(int orderHeaderId);
}