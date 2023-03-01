using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data.OrderEvent;
public class OrderEventData : IOrderEventData
{
    private readonly ISqlDataAccess _db;

    public OrderEventData(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<long> CreateOrderEvent(OrderHeaderMDL orderheader)
    {
        var result = _db.ExecuteScalar<int, dynamic>(
            "INSERT INTO OrderEvent (OrderHeaderId, OrderStatusId, OrderInfo) " +
            "VALUES (@HeaderId, @ItemStatusId, @OrderInfo);" +
            "SELECT SCOPE_IDENTITY()", new { orderheader.HeaderId, orderheader.ItemStatusId, orderheader.OrderInfo });
        return result;
    }
}