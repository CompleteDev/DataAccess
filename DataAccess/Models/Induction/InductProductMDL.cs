using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Induction;
public class InductProductMDL
{
    public string DivID { get; set; }
    public string MessageID { get; set; }
    public string MessageDate { get; set; }
    public string MessageTime { get; set; }
    public string BookID { get; set; }
    public string SKU { get; set; }
    public string BookType { get; set; }
    public string ShipmentNumber { get; set; }
    public string PartNumber { get; set; }
    public string ShipType { get; set; }
    public string WorkStation { get; set; }
    public string Destination { get; set; }
    public string ScannedDate { get; set; }
    public string ScannedTime { get; set; }
    public string EmpID { get; set; }
}
