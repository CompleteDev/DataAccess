using DataAccess.Models.Client;
using DataAccess.Models.OrderShippingAddress;
using static SharedClass.EnumCS;

namespace DataAccess.Models;
public class OrderHeaderMDL
{
    public long HeaderId { get; set; }
    public long OrderHeaderId
    {
         get { return HeaderId; }
         set { value = HeaderId; }
    }

    public string PONumber { get; set; }
    public string OrderType { get; set; }
    public string ItemStatus { get; set; }
    public ShippingAddressMDL ShippingAddress { get; set; }
    public string Carrier { get; set; }
    public string ShipMethod { get; set; }
    public DateTime MustShipDate { get; set; }
    public DateTime SentDate { get; set; }
    public int ShipmentRetryCount { get; set; }
    public string EventText { get; set; }
    public string ClientName { get; set; }
    public long OrderEventId { get; set; }
    public string OrderInfo { get; set; }
    public DateTime EventDate { get; set; }
    public ItemStatus ItemStatusId { get; set; }
    public int ShipMethodId { get; set; }
    public string OrderNumber { get; set; }
    public int OrderTypeId { get; set; }
    public int CarrierId { get; set; }
    public long FileID { get; set; }
    public int SentDateInt { get; set; }
    public ClientMDL Client { get; set; }
}
