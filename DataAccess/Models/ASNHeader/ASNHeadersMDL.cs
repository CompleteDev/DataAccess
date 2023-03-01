using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models.ASNTracking;

namespace DataAccess.Models.ASNHeader;
public class ASNHeadersMDL : IModel
{
    [DapperIgnore]
    public string TableName => "ASNHeader";
    [DapperKey]
    public int ASNHeaderId { get; set; }
    public string AccountNumber { get; set; }
    public string VendorReference { get; set; }
    public List<ASNTrackingMDL> TrackingNumber { get; set; }
    public int Cartons { get; set; }
    public int Pallets { get; set; }
    public DateTime SentDate { get; set; }
    public int StatusId { get; set; }
}
