using DataAccess.DbAccess;
using DataAccess.Models;
using DataAccess.Models.OrderLines;

namespace DataAccess.Data.OrderLines;
public class OrderLinesData : IOrderLinesData
{
    private readonly ISqlDataAccess _db;
    public OrderLinesData(ISqlDataAccess db)
    {
        _db = db;
    }

    //public Task<IEnumerable<OrderHeaderMDL>> GetOrderHeaders() => _db.LoadData<OrderHeaderMDL, dynamic>("SELECT Id FROM OrderHeader WHERE LOWER(OrderStatus) = 'processing'", new { });

    public async Task<IEnumerable<OrderLinesMDL?>> GetOrderLinesFromHeaderID(int Id)
    {
        var results = await _db.LoadData<OrderLinesMDL, dynamic>(
            "SELECT ol.OrderLineId,ol.OrderHeaderId,ol.ReceivedDate,stat.ItemStatus,prod.ProductTitle,prod.SKU,ol.RequestedQuantity,tn.TrackingNumber,tn.TrackingDate,COALESCE(CONVERT(VARCHAR, olh.HistoryDate,101),'Not Shipped') AS ShippedDate " +
            "FROM OrderLine ol " +
            "INNER JOIN ItemStatus stat ON stat.StatusId = ol.StatusId " +
            "INNER JOIN Products prod ON prod.ProductId = ol.ProductId " +
            "LEFT JOIN OrderLineTrackingNumber tn ON tn.OrderLineID = ol.OrderLineId " +
            "LEFT JOIN OrderLineHistory olh ON olh.OrderLineId = ol.OrderLineId AND olh.OrderStatusId = 6 " +
            "WHERE ol.OrderHeaderId = @Id", new { Id });
        return results;
    }

    public async Task<IEnumerable<OrderLinesMDL?>> GetOrderLineHistory(int orderlineId)
    {
        var results = await _db.LoadData<OrderLinesMDL, dynamic>(
            "SELECT oh.HistoryId,oh.OrderLineId,oh.OrderHeaderID,oh.Info,stat.ItemStatus,oh.HistoryDate " +
            "FROM OrderLineHistory oh " +
            "INNER JOIN ItemStatus stat ON stat.StatusId = oh.OrderStatusId " +
            "WHERE oh.OrderLineId = @orderlineId", new { orderlineId });

        return results;
    }

    public async Task<IEnumerable<OrderLinesMDL?>> GetOrderLinesHistory(int orderHeaderId)
    {
        var results = await _db.LoadData<OrderLinesMDL, dynamic>(
            "SELECT oh.OrderLineId,oh.OrderHeaderId,oh.Info,stat.ItemStatus,oh.HistoryDate " +
            "FROM OrderLineHistory oh " +
            "INNER JOIN ItemStatus stat ON stat.StatusId = oh.OrderStatusId " +
            "WHERE oh.OrderHeaderId = @orderHeaderId", new { orderHeaderId });

        return results;
    }

    public async Task<OrderLinesMDL?> GetOrderLine(int Id)
    {
        var results = await _db.LoadData<OrderLinesMDL, dynamic>(
            "SELECT ol.OrderLineId,ol.OrderHeaderId,ol.ReceivedDate,stat.ItemStatus,prod.ProductTitle,prod.SKU,ol.RequestedQuantity,tn.TrackingNumber,tn.TrackingDate,COALESCE(CONVERT(VARCHAR, olh.HistoryDate,101),'Not Shipped') AS ShippedDate " +
            "FROM OrderLine ol " +
            "INNER JOIN ItemStatus stat ON stat.StatusId = ol.StatusId " +
            "INNER JOIN Products prod ON prod.ProductId = ol.ProductId " +
            "LEFT JOIN OrderLineTrackingNumber tn ON tn.OrderLineID = ol.OrderLineId " +
            "LEFT JOIN OrderLineHistory olh ON olh.OrderLineId = ol.OrderLineId AND olh.OrderStatusId = 6 " +
            "WHERE ol.OrderLineId = @Id", new { Id });

        return results.FirstOrDefault();
    }
}
