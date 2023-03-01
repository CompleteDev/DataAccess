using DataAccess.Models.OrderFile;
using SharedClass;

namespace DataAccess.Data.OrderFile;

public interface IOrderFileData
{
    Task<long> CheckFileStatus(int StatusID);
    Task<long> CreateOrderFile();
    Task<OrderFileMDL> GetOrderFileID(int StatusID);
    Task UpdateOrderFileWithHeaderID(long FileID, long HeaderID);
    Task UpdateOrderFileStatus(long orderFileId, EnumCS.ItemStatus statusId);
}