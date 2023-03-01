using DataAccess.Data.Shared.SystemErrorLog;
using DataAccess.DbAccess;
using DataAccess.Models.OrderFile;
using SharedClass;
using static SharedClass.EnumCS;

namespace DataAccess.Data.OrderFile;
public class OrderFileData : IOrderFileData
{
    private readonly ISqlDataAccess _db;
    private readonly ISystemErrorLog _er;

    public OrderFileData(ISqlDataAccess db, ISystemErrorLog er)
    {
        _db = db;
        _er = er;
    }

    public async Task<long> CreateOrderFile()
    {
        var result = await _db.ExecuteScalar<int, dynamic>(
            "INSERT INTO OrderFile (StatusId) " +
            "VALUES (1); " +
            "SELECT SCOPE_IDENTITY()", new { });
        return result;
    }

    public async Task<long> CheckFileStatus(int StatusID)
    {
        try
        {
            var result = await _db.LoadData<long, dynamic>("SELECT COUNT(Id) FROM OrderFile WHERE StatusId = @StatusId ", new { StatusID });

            return result.FirstOrDefault();
        }
        catch (Exception e)
        {
            await _er.InsertSystemError((int)EnumCS.ProcessType.System, -1, "Created from OrderFileData.cs -> CheckFileStatus function - " + e.Message);
            throw;
        }
    }

    public async Task<OrderFileMDL> GetOrderFileID(int StatusID)
    {
        try
        {
            var result = await _db.LoadData<OrderFileMDL, dynamic>("SELECT TOP(1) Id FROM OrderFile WHERE StatusId = 2 ORDER BY Id ASC ", new { });

            return result.FirstOrDefault();
        }
        catch (Exception e)
        {
            await _er.InsertSystemError((int)EnumCS.ProcessType.System, -1, "Created from OrderFileData.cs -> GetOrderFileID function - " + e.Message);
            throw;
        }
    }

    public async Task UpdateOrderFileWithHeaderID(long FileID, long HeaderID)
    {
        try
        {
            await _db.SaveData("UPDATE OrderFile SET OrderHeaderId = @HeaderID WHERE Id = @Id)", new { HeaderID, FileID });
        }
        catch (Exception e)
        {
            await _er.InsertSystemError((int)EnumCS.ProcessType.FileRead, FileID, "Created from OrderFileData.cs -> UpdateOrderFileWithHeaderID function - " + e.Message);
            await UpdateOrderFileStatus((long)FileID, EnumCS.ItemStatus.FailedToInsert);
        }
    }

    public Task UpdateOrderFileStatus(long orderFileId, ItemStatus statusId) =>
        _db.SaveData("UPDATE OrderFile SET StatusId = @statusId WHERE Id = @orderFileId", new { orderFileId, statusId });
}