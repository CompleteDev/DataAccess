using DataAccess.Models.OrderLines;

namespace DataAccess.Data.OrderLineHistory;

public interface IOrderLineHistoryData
{
    Task CreateOrderLineHistory(OrderLinesMDL orderline);
}