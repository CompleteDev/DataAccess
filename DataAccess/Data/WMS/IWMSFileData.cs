using SharedClass;

namespace DataAccess.Data.WMS;
public interface IWMSFileData
{
    Task<long> CheckFileStatus(int FileStatus);
    Task<long> CreateWMSFile();
    Task<long> GetWMSFileID(int FileStatus);
    Task UpdateWMSFileStatus(long wmsFileId, EnumCS.ItemStatus statusId);
}