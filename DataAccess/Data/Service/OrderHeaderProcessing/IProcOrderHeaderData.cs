using DataAccess.Models;

namespace DataAccess.Data.Service.OrderHeaderProcessing;

public interface IProcOrderHeaderData
{
    Task<int> DoesOrderExist(OrderHeaderMDL orderHeaderMDL);
    Task<int> GetCarrierID(long FileID, string Carrier);
    Task<int> GetClientID(string clientName, long fileID);
    Task<string> GetClientContactInfo(int clientId, long fileID);
    Task<int> GetOrderTypeID(long FileID, string OrderType);
    Task<int> GetShippingMethodID(long FileID, string ShipMethod);
    Task InsertHeaderInfo(OrderHeaderMDL orderHeaderMDL);
    Task<long> InsertOrderHeader(OrderHeaderMDL orderHeaderMDL);
    Task<long> InsertShippingInfo(OrderHeaderMDL orderHeader);
    Task UpdateOrderHeaderStatus(OrderHeaderMDL orderHeaderMDL);
}