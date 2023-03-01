using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data;
public class OrderHeaderData : IOrderHeaderData
{
    private readonly ISqlDataAccess _db;
    public OrderHeaderData(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<OrderHeaderMDL>> GetOrderHeaders() => _db.LoadData<OrderHeaderMDL, dynamic>(
        "SELECT oh.HeaderId,ss.ItemStatus,oi.MustShipDate,oh.SentDate,oi.ShipmentRetryCount,cli.Name AS ClientName " +
        "FROM OrderHeader oh " +
        "INNER JOIN OrderHeaderInfo oi ON oi.OrderHeaderId = oh.HeaderId " +
        "INNER JOIN ItemStatus ss ON ss.StatusId = oh.StatusId " +
        "INNER JOIN CarrierShipMethods sm ON sm.ShipMethodId = oi.ShipMethodId " +
        "INNER JOIN Clients cli ON cli.ClientId = oh.ClientId", new { });

    public async Task<OrderHeaderMDL?> GetOrderDetails(int Id)
    {
        var results = await _db.LoadData<OrderHeaderMDL, dynamic>(
            "SELECT oh.HeaderId,ss.ItemStatus,oi.MustShipDate,oh.SentDate,oi.ShipmentRetryCount,oi.MustShipDate,oi.PONumber, " +
            "cr.Description,sm.ShipMethod,ci.FirstName,ci.LastName,ci.StreetAddress,ci.AddressBox,ci.City,ci.State,ci.ZipCode " +
            "FROM OrderHeader oh " +
            "INNER JOIN OrderHeaderInfo oi ON oi.OrderHeaderId = oh.HeaderId " +
            "INNER JOIN OrderShippingAddress ci ON ci.OrderHeaderId = oh.HeaderId " +
            "INNER JOIN ItemStatus ss ON ss.StatusId = oh.StatusId " +
            "INNER JOIN Carriers cr ON cr.CarrierId = oi.CarrierId " +
            "INNER JOIN CarrierShipMethods sm ON sm.ShipMethodId = oi.ShipMethodId " +
            "WHERE oi.OrderHeaderId = @Id", new { Id });

        return results.FirstOrDefault();
    }

    public async Task<IEnumerable<OrderHeaderMDL?>> GetOrderEvents(int Id)
    {
        var results = await _db.LoadData<OrderHeaderMDL, dynamic>(
            "SELECT oe.OrderEventId,oe.OrderHeaderId,stat.ItemStatus,oe.OrderInfo,oe.EventDate " +
            "FROM OrderEvent oe " +
            "INNER JOIN ItemStatus stat ON stat.StatusId = oe.OrderStatusId " +
            "WHERE oe.OrderHeaderId = @Id", new { Id });

        return results;
    }

    public async Task<OrderHeaderMDL?> GetOrderHeader(int Id)
    {
        var results = await _db.LoadData<OrderHeaderMDL, dynamic>("SELECT HeaderId FROM OrderHeader WHERE HeaderId = @Id", new { Id });

        return results.FirstOrDefault();
    }

    public Task<IEnumerable<OrderHeaderMDL>> GetResetOrders() => _db.LoadData<OrderHeaderMDL, dynamic>(
        "SELECT oh.HeaderId,ci.FirstName,ci.LastName,stat.ItemStatus,oe.OrderInfo " +
        "FROM OrderHeader oh " +
        "INNER JOIN OrderShippingAddress ci ON ci.OrderHeaderId = oh.HeaderId " +
        "INNER JOIN OrderEvent oe ON oe.OrderHeaderId = oh.HeaderId AND oe.OrderStatusId = 11 OR oe.OrderStatusId = 8 " +
        "INNER JOIN ItemStatus stat ON stat.StatusId = oh.StatusId", new { });

    public Task UpdateOrderHeaderReset(OrderHeaderMDL orderheader) =>
        _db.SaveData("UPDATE OrderHeader SET StatusID = 2 WHERE HeaderId =  @Id", new { orderheader.HeaderId });
}