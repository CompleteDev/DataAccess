using DataAccess.Data.OrderFile;
using DataAccess.Data.Shared;
using DataAccess.Data.Shared.SystemErrorLog;
using DataAccess.DbAccess;
using DataAccess.Models.OrderLines;
using SharedClass;

namespace DataAccess.Data.Service.OrderLineProcessing;
public class ProcOrderLineData : IProcOrderLineData
{
    private readonly ISqlDataAccess _db;
    private readonly ISystemErrorLog _er;
    private readonly IOrderFileData _ofd;

    public ProcOrderLineData(ISqlDataAccess db, ISystemErrorLog er, IOrderFileData ofd)
    {
        _db = db;
        _er = er;
        _ofd = ofd;
    }

    //Check if product exists
    public async Task<int> DoesProductExist(OrderLinesMDL orderLinesMDL, long FileID)
    {
        try
        {
            var results = await _db.LoadData<int, dynamic>("SELECT COUNT(ProductId) FROM Products WHERE ClientProdNumber = @ClientProdNumber AND SKU = @SKU AND ProductCondition = @ProductCondition", new { orderLinesMDL.ClientProdNumber, orderLinesMDL.SKU, orderLinesMDL.ProductCondition });
            return results.FirstOrDefault();
        }
        catch (Exception e)
        {
            await _er.InsertSystemError((int)EnumCS.ProcessType.OrderHeader, orderLinesMDL.OrderHeaderId, "Created from ProcOrderLineData.cs -> InsertOrderHeader function - " + e.Message);
            await _ofd.UpdateOrderFileStatus(FileID, EnumCS.ItemStatus.SystemError);
            throw;
        }
    }

    //Get Prod ID
    public async Task<long> GetProductID(OrderLinesMDL orderLinesMDL, long FileID)
    {
        try
        {
            var results = await _db.LoadData<long, dynamic>("SELECT ProductId FROM Products WHERE ClientProdNumber = @ClientProdNumber AND SKU = @SKU AND ProductCondition = @ProductCondition", new { orderLinesMDL.ClientProdNumber, orderLinesMDL.SKU, orderLinesMDL.ProductCondition });
            return results.FirstOrDefault();
        }
        catch (Exception e)
        {
            await _er.InsertSystemError((int)EnumCS.ProcessType.OrderHeader, orderLinesMDL.OrderHeaderId, "Created from ProcOrderLineData.cs -> GetProductID function - " + e.Message);
            await _ofd.UpdateOrderFileStatus(FileID, EnumCS.ItemStatus.SystemError);
            throw;
        }
    }

    //Insert OrderLine Info
    public async Task<long> InsertOrderLine(OrderLinesMDL orderLinesMDL, long FileID)
    {
        try
        {
            var results = await _db.ExecuteScalar<long, dynamic>(
                "INSERT INTO OrderLine(StatusId,OrderHeaderId,ProductId,RequestedQuantity) VALUES(@ItemStatusId,@OrderHeaderId,@ProductId,@RequestedQuantity) SELECT SCOPE_IDENTITY()",
                new { orderLinesMDL.ItemStatusId, orderLinesMDL.OrderHeaderId, orderLinesMDL.ProductId, orderLinesMDL.RequestedQuantity });
            return results;
        }
        catch (Exception e)
        {
            await _er.InsertSystemError((int)EnumCS.ProcessType.OrderHeader, orderLinesMDL.OrderHeaderId, "Created from ProcOrderLineData.cs -> InsertOrderLine function - " + e.Message);
            await _ofd.UpdateOrderFileStatus(FileID, EnumCS.ItemStatus.SystemError);
            throw;
        }
    }
}