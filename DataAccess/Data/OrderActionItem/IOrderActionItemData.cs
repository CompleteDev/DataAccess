using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models.OrderActionItem;

namespace DataAccess.Data.OrderActionItem;
public interface IOrderActionItemData
{
    Task<long> Create(OrderActionItemMDL actionItem);
}
