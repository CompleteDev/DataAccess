using DataAccess.Data.Shared.SystemErrorLog;
using DataAccess.DbAccess;
using DataAccess.Models.OrderFile;
using SharedClass;
using static SharedClass.EnumCS;

namespace DataAccess.Data.WMS;
public class WMSFileData : IWMSFileData
{
    private readonly ISqlDataAccess _db;
    private readonly ISystemErrorLog _er;

    public WMSFileData(ISqlDataAccess db, ISystemErrorLog er)
    {
        _db = db;
        _er = er;
    }

    public async Task<long> CreateWMSFile()
    {
        var result = await _db.ExecuteScalar<int, dynamic>(
            "INSERT INTO WMS_Files (FileStatus) " +
            "VALUES (0); " +
            "SELECT SCOPE_IDENTITY()", new { });
        return result;
    }

    public async Task<long> CheckFileStatus(int FileStatus)
    {
        try
        {
            var result = await _db.LoadData<long, dynamic>("SELECT COUNT(FileId) FROM WMS_Files WHERE FileStatus = @FileStatus ", new { FileStatus });

            return result.FirstOrDefault();
        }
        catch (Exception e)
        {
            await _er.InsertSystemError((int)EnumCS.ProcessType.System, -1, "Created from WMSFileData.cs -> CheckFileStatus function - " + e.Message);
            throw;
        }
    }

    public async Task<long> GetWMSFileID(int FileStatus)
    {
        try
        {
            var result = await _db.LoadData<long, dynamic>("SELECT TOP(1) FileId FROM WMS_Files WHERE FileStatus = @FileStatus ORDER BY WMS_Files ASC ", new { FileStatus });

            return result.FirstOrDefault();
        }
        catch (Exception e)
        {
            await _er.InsertSystemError((int)EnumCS.ProcessType.System, -1, "Created from WMSFileData.cs -> GetWMSFileID function - " + e.Message);
            throw;
        }
    }

    public Task UpdateWMSFileStatus(long wmsFileId, ItemStatus statusId) =>
        _db.SaveData("UPDATE WMS_Files SET FileStatus = @statusId WHERE FileId = @wmsFileId", new { wmsFileId, statusId });
}
