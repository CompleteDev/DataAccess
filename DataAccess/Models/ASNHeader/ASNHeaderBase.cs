using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.ASNHeader;
public class ASNHeaderBase
{
    public int ASNHeaderId { get; set; }
    public string VendorAccount { get; set; }
    public string VendorReference { get; set; }
    public DateTime SentDate { get; set; }
    public DateTime EstArrivalDate { get; set; }
    public int NumberofCartons { get; set; }
    public int NumberofPallets { get; set; }
    public decimal TotalWeight { get; set; }
    public string ShipmentCarrier { get; set; }
    public string ShipmentMethod { get; set; }
    public string ShipmentType { get; set; }
    public int ASNStatus { get; set; }
    public string Notes { get; set; }
}
