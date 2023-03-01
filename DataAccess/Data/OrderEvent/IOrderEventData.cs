using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.Data.OrderEvent;
public interface IOrderEventData
{
    Task<long> CreateOrderEvent(OrderHeaderMDL orderheader);
}
