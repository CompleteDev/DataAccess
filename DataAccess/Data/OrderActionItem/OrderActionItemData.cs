using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DbAccess;
using DataAccess.Models.OrderActionItem;

namespace DataAccess.Data.OrderActionItem;
public class OrderActionItemData : IOrderActionItemData
{
    private readonly ISqlDataAccess _db;

    public OrderActionItemData(ISqlDataAccess db)
    {
        _db = db;
    }

    public async Task<long> Create(OrderActionItemMDL actionItem)
    {
        var result = await _db.ExecuteScalar<int, dynamic>(
            "INSERT INTO OrderActionItems (OrderHeaderId, ClientId, ActionMessage, Viewed, Resolved) " +
            "VALUES (@orderHeaderId, @clientId, @actionMessage, @viewed, @resolved); " +
            "SELECT SCOPE_IDENTITY()", new { actionItem.OrderHeaderId, actionItem.ClientId, actionItem.ActionMessage, actionItem.Viewed, actionItem.Resolved });
        return result;
    }
}
