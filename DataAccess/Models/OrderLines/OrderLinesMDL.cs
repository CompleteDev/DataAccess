using static SharedClass.EnumCS;

namespace DataAccess.Models.OrderLines;
public class OrderLinesMDL
{
    public long OrderHeaderId { get; set; }
    public long OrderLineId { get; set; }
    public int HistoryId { get; set; }
    //public string orderline { get; set; }
    public string SKU { get; set; }
    public string TrackingNumber { get; set; }
    public string ItemStatus { get; set; }
    public int RequestedQuantity { get; set; }
    public DateTime ReceivedDate { get; set; }
    public string PONumber { get; set; }
    public DateTime MustShipDate { get; set; }
    public string ProductTitle { get; set; }
    public string ShippedDate { get; set; }
    public string EventText { get; set; }
    public string Info { get; set; }
    public DateTime HistoryDate { get; set; }
    public string ClientProdNumber { get; set; }
    public int ProductCondition { get; set; }
    public long ProductId { get; set; }

    public ItemStatus ItemStatusId { get; set; }
}
