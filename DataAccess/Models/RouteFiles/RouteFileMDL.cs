using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.RouteFiles;
public class RouteFileMDL
{
    public int customdata2 { get; set; }
    public int invoice_number { get; set; }
    public string item_no { get; set; }
    public int ROUTE_FILE_ID { get; set; }
    public string item_description { get; set; }
    public string batch_no { get; set; }
    public int batch_status { get; set; }
    public int SKU_STATE_ID { get; set; }
    public string state_descr { get; set; }
    public string SERIAL_ID { get; set; }
}