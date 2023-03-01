using DataAccess.Data.Shared.SystemErrorLog;
using DataAccess.DbAccess;
using DataAccess.Models.OrderAddressCorrection;
using SharedClass;

namespace DataAccess.Data.OrderAddressCorrection;
public class OrderAddressCorrectionData : IOrderAddressCorrectionData
{
    private readonly ISqlDataAccess _db;
    private readonly ISystemErrorLog _er;

    public OrderAddressCorrectionData(ISqlDataAccess db, ISystemErrorLog er)
    {
        _db = db;
        _er = er;
    }

    public async Task CreateAddressCorrection(AddressCorrectionMDL addressCorrection)
    {
       try
       {
            await _db.SaveData(
                "INSERT INTO OrderAddressCorrection (shippingId, StreetAddress, BoxAddress, City, State, ZipCode) " +
                "VALUES (@ShippingId, @StreetAddress, @BoxAddress, @City, @State, @ZipCode)",
                new
                {
                    addressCorrection.ShippingId,
                    addressCorrection.StreetAddress,
                    addressCorrection.BoxAddress,
                    addressCorrection.City,
                    addressCorrection.State,
                    addressCorrection.ZipCode,
                });
        }
        catch (Exception ex)
        {
            await _er.InsertSystemError((int)EnumCS.ProcessType.System, -1, $"Created from OrderAddressCorrectionData.cs -> CreateAddressCorrection method - {ex.Message}");
            throw;
        }
    }
}