using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DbAccess;
using DataAccess.Models.OrderLines;

namespace DataAccess.Data.OrderLineHistory;
public class OrderLineHistoryData : IOrderLineHistoryData
{
    private readonly ISqlDataAccess _db;

    public OrderLineHistoryData(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task CreateOrderLineHistory(OrderLinesMDL orderline) =>

        _db.SaveData("INSERT INTO OrderLineHistory(OrderHeaderId,OrderLineId,OrderStatusId,Info) VALUES(@headerID,@lineID,@StatID,@Info)", new { orderline.OrderHeaderId, orderline.OrderLineId, orderline.ItemStatusId, orderline.EventText });
}
