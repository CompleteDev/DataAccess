using DataAccess.Models.OrderLines;

namespace DataAccess.Data.Service.OrderLineProcessing;

public interface IProcOrderLineData
{
    Task<int> DoesProductExist(OrderLinesMDL orderLinesMDL, long FileID);
    Task<long> GetProductID(OrderLinesMDL orderLinesMDL, long FileID);
    Task<long> InsertOrderLine(OrderLinesMDL orderLinesMDL, long FileID);
}