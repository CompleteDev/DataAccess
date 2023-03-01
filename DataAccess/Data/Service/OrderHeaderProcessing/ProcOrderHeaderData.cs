using DataAccess.Data.OrderFile;
using DataAccess.Data.Shared;
using DataAccess.Data.Shared.SystemErrorLog;
using DataAccess.DbAccess;
using DataAccess.Models;
using DataAccess.Models.OrderFile;
using SharedClass;

namespace DataAccess.Data.Service.OrderHeaderProcessing;
public class ProcOrderHeaderData : IProcOrderHeaderData
{
    private readonly ISqlDataAccess _db;
    private readonly ISystemErrorLog _er;
    private readonly IOrderFileData _ofd;
    private readonly OrderHeaderMDL _ohm;

    public ProcOrderHeaderData(ISqlDataAccess db, ISystemErrorLog er, IOrderFileData ofd, OrderHeaderMDL ohm)
    {
        _db = db;
        _er = er;
        _ofd = ofd;
        _ohm = ohm;
    }

    public async Task<long> InsertOrderHeader(OrderHeaderMDL orderHeaderMDL)
    {
        try
        {
            var result = await _db.ExecuteScalar<long, dynamic>(
                "INSERT INTO OrderHeader(StatusId,ClientId,OrderTypeId,OrderNumber,DateInt) VALUES(@ItemStatusId,@ClientId,@OrderTypeId,@OrderNumber,@SentDateInt) " +
                "SELECT SCOPE_IDENTITY()", new { orderHeaderMDL.ItemStatusId, orderHeaderMDL.Client.ClientId, orderHeaderMDL.OrderTypeId, orderHeaderMDL.OrderNumber, orderHeaderMDL.SentDateInt });

            return result;
        }
        catch (Exception ex)
        {
            await _er.InsertSystemError((int)EnumCS.ProcessType.FileRead, orderHeaderMDL.FileID, "Created from ProcOrderHeader.cs -> InsertOrderHeader function - " + ex.Message);
            await _ofd.UpdateOrderFileStatus(orderHeaderMDL.FileID, EnumCS.ItemStatus.FailedToInsert);
            throw;
        }
    }

    public async Task<int> DoesOrderExist(OrderHeaderMDL orderHeaderMDL)
    {
        try
        {
            var results = await _db.LoadData<int, dynamic>(
                "SELECT COUNT(HeaderId) FROM OrderHeader " +
                "WHERE OrderNumber = @OrderNumber AND ClientId = @ClientId", new { orderHeaderMDL.OrderNumber, orderHeaderMDL.Client.ClientId });
            return results.FirstOrDefault();
        }
        catch (Exception ex)
         {
            await _er.InsertSystemError((int)EnumCS.ProcessType.FileRead, orderHeaderMDL.FileID, "Created from ProcOrderHeader.cs -> DoesOrderExist function - " + ex.Message);
            orderHeaderMDL.ItemStatusId = EnumCS.ItemStatus.SystemError;
            await UpdateOrderHeaderStatus(orderHeaderMDL);
            await _ofd.UpdateOrderFileStatus(orderHeaderMDL.FileID, EnumCS.ItemStatus.SystemError);
            throw;
        }
    }

    public async Task<int> GetClientID(string ClientName, long FileID)
    {
        try
        {
            var result = await _db.LoadData<int, dynamic>("SELECT ClientId FROM Clients WHERE Name = @ClientName", new { ClientName });

            return result.FirstOrDefault();
        }
        catch (Exception ex)
        {
            await _er.InsertSystemError((int)EnumCS.ProcessType.FileRead, FileID, "Created from ProcOrderHeader.cs -> GetClientID function - " + ex.Message);
            _ohm.ItemStatusId = EnumCS.ItemStatus.SystemError;
            await UpdateOrderHeaderStatus(_ohm);
            await _ofd.UpdateOrderFileStatus(FileID, EnumCS.ItemStatus.SystemError);
            throw;
        }
    }

    public async Task<string> GetClientContactInfo(int clientId, long fileID)
    {
        try
        {
            var result = await _db.LoadData<string, dynamic>(
                "SELECT cci.ContactInfo " +
                "FROM ClientContactinfo cci " +
                "INNER JOIN ClientContacts cc ON cc.ContactId = cci.ClientContactId " +
                "WHERE cc.ClientId = @clientId AND cci.ContactTypeID = 1 AND cc.IsPrimary = 1", new { clientId });

            return result.FirstOrDefault();
        }
        catch (Exception ex)
        {
            await _er.InsertSystemError((int)EnumCS.ProcessType.FileRead, fileID, "Created from ProcOrderHeader.cs -> GetClientContactInfo function - " + ex.Message);
            _ohm.ItemStatusId = EnumCS.ItemStatus.SystemError;
            await UpdateOrderHeaderStatus(_ohm);
            await _ofd.UpdateOrderFileStatus(fileID, EnumCS.ItemStatus.SystemError);
            throw;
        }
    }

    public async Task<int> GetCarrierID(long FileID, string Carrier)
    {
        try
        {
            var result = await _db.LoadData<int, dynamic>("SELECT CarrierId FROM Carriers WHERE Description = @Carrier", new { Carrier });

            return result.FirstOrDefault();
        }
        catch (Exception ex)
        {
            await _er.InsertSystemError((int)EnumCS.ProcessType.FileRead, FileID, "Created from ProcOrderHeader.cs -> GetCarrierID function - " + ex.Message);
            _ohm.ItemStatusId = EnumCS.ItemStatus.SystemError;
            await UpdateOrderHeaderStatus(_ohm);
            await _ofd.UpdateOrderFileStatus(FileID, EnumCS.ItemStatus.SystemError);
            throw;
        }
    }

    public async Task<int> GetShippingMethodID(long FileID, string ShipMethod)
    {
        try
        {
            var result = await _db.LoadData<int, dynamic>("SELECT ShipMethodId FROM CarrierShipMethods WHERE ShipMethod = @ShipMethod", new { ShipMethod });

            return result.FirstOrDefault();
        }
        catch (Exception ex)
        {
            await _er.InsertSystemError((int)EnumCS.ProcessType.FileRead, FileID, "Created from ProcOrderHeader.cs -> GetShippingMethodID function - " + ex.Message);
            _ohm.ItemStatusId = EnumCS.ItemStatus.SystemError;
            await UpdateOrderHeaderStatus(_ohm);
            await _ofd.UpdateOrderFileStatus(FileID, EnumCS.ItemStatus.SystemError);
            throw;
        }
    }

    public async Task<int> GetOrderTypeID(long FileID, string OrderType)
    {
        try
        {
            var result = await _db.LoadData<int, dynamic>("SELECT OrderTypeId FROM OrderType WHERE OrderType = @OrderType", new { OrderType });

            return result.FirstOrDefault();
        }
        catch (Exception ex)
        {
            await _er.InsertSystemError((int)EnumCS.ProcessType.FileRead, FileID, "Created from ProcOrderHeader.cs -> GetOrderType function - " + ex.Message);
            _ohm.ItemStatusId = EnumCS.ItemStatus.SystemError;
            await UpdateOrderHeaderStatus(_ohm);
            await _ofd.UpdateOrderFileStatus(FileID, EnumCS.ItemStatus.SystemError);
            throw;
        }
    }

    public async Task InsertHeaderInfo(OrderHeaderMDL orderHeaderMDL)
    {
        try
        {
            await _db.SaveData(
                "INSERT INTO OrderHeaderInfo(OrderHeaderId,CarrierId,ShipMethodId,MustShipDate,PONumber,ShipmentRetryCount) VALUES(@OrderHeaderId,@CarrierId,@ShipMethodId,@MustShipDate,@PONumber,@ShipmentRetryCount)",
                new { orderHeaderMDL.OrderHeaderId, orderHeaderMDL.CarrierId, orderHeaderMDL.ShipMethodId, orderHeaderMDL.MustShipDate, orderHeaderMDL.PONumber, orderHeaderMDL.ShipmentRetryCount });
        }
        catch (Exception ex)
        {
            await _er.InsertSystemError((int)EnumCS.ProcessType.OrderHeader, orderHeaderMDL.HeaderId, "Created from ProcOrderHeader.cs -> InsertHeaderInfo function - " + ex.Message);
            _ohm.ItemStatusId = EnumCS.ItemStatus.SystemError;
            await UpdateOrderHeaderStatus(_ohm);
            await _ofd.UpdateOrderFileStatus(orderHeaderMDL.FileID, EnumCS.ItemStatus.FailedToInsert);
            throw;
        }
    }

    public async Task<long> InsertShippingInfo(OrderHeaderMDL orderHeader)
    {
        try
        {
            var result = await _db.ExecuteScalar<long, dynamic>(
                "INSERT INTO OrderShippingAddress (OrderHeaderId,FirstName,LastName,StreetAddress,AddressBox,City,State,ZipCode,Country) " +
                               "VALUES (@OrderHeaderId,@FirstName,@LastName,@StreetAddress,@AddressBox,@City,@State,@ZipCode,@Country);" +
                               "SELECT SCOPE_IDENTITY()",
                               new
                               {
                                   orderHeader.HeaderId,
                                   orderHeader.ShippingAddress.FirstName,
                                   orderHeader.ShippingAddress.LastName,
                                   orderHeader.ShippingAddress.StreetAddress,
                                   orderHeader.ShippingAddress.AddressBox,
                                   orderHeader.ShippingAddress.City,
                                   orderHeader.ShippingAddress.State,
                                   orderHeader.ShippingAddress.ZipCode,
                                   orderHeader.ShippingAddress.Country
                               });
            return result;
        }
        catch (Exception ex)
        {
            await _er.InsertSystemError((int)EnumCS.ProcessType.OrderHeader, orderHeader.HeaderId, $"Created from ProcOrderHeader.cs -> InsertShippingInfo method - {ex.Message}");
            _ohm.ItemStatusId = EnumCS.ItemStatus.SystemError;
            await UpdateOrderHeaderStatus(_ohm);
            await _ofd.UpdateOrderFileStatus(orderHeader.FileID, EnumCS.ItemStatus.FailedToInsert);
            throw;
        }
    }

    public async Task UpdateOrderHeaderStatus(OrderHeaderMDL orderHeaderMDL)
    {
        try
        {
            await _db.SaveData("UPDATE OrderHeader SET StatusId = @StatusId WHERE HeaderId = @HeaderId", new { orderHeaderMDL.ItemStatusId, orderHeaderMDL.HeaderId });
        }
        catch (Exception ex)
        {
            await _er.InsertSystemError((int)EnumCS.ProcessType.OrderHeader, orderHeaderMDL.HeaderId, "Created from ProcOrderHeader.cs -> UpdateOrderHeaderStatus function - " + ex.Message);
            await _ofd.UpdateOrderFileStatus(orderHeaderMDL.FileID, EnumCS.ItemStatus.FailedToInsert);
            throw;
        }
    }
}
