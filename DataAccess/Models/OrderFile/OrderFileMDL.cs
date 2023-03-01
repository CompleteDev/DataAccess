using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.OrderFile;
public class OrderFileMDL
{
    public long Id { get; set; }
    public DateTime InsertedDate { get; set; }
    public int StatusId { get; set; }
    public long OrderHeaderId { get; set; }
}
