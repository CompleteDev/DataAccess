using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DbAccess;
using DataAccess.Models.ASNHeader;
using DataAccess.Models.ASNTracking;

namespace DataAccess.Data.ASNHeader;
public class ASNHeaderData : IASNHeaderData
{
    private readonly ISqlDataAccess _db;

    public ASNHeaderData(ISqlDataAccess db)
    {
        _db = db;
    }

    public async Task<ASNHeadersMDL> GetASNHeader(int asnHeaderId)
    {
        var result = await _db.LoadData<ASNHeadersMDL, dynamic>(
            "SELECT ASNHeaderId, AccountNumber, VendorReference, SentDate, Cartons," +
                "Pallets, Cartons, StatusId FROM dbo.ASNHeader " +
            "WHERE ASNHeaderId = @asnHeaderId",
            new { asnHeaderId });
        return result.FirstOrDefault();
    }

    public async Task<ASNHeadersMDL> GetASNTrackingHeader(string TrackingNumber)
    {
        var result = await _db.LoadData<ASNHeadersMDL, dynamic>(
            "SELECT a.ASNHeaderId, a.AccountNumber, a.VendorReference, a.SentDate, a.Cartons," +
                "a.Pallets, a.Cartons, a.StatusId FROM dbo.ASNHeader a INNER JOIN dbo.ASNTracking at ON a.ASNHeaderId = at.ASNHeaderId" +
                " WHERE at.TrackingNumber = @TrackingNumber",
            new { TrackingNumber });
        return result.FirstOrDefault();
    }

    public Task<IEnumerable<ASNHeadersMDL>> GetASNHeaders() =>
        _db.LoadData<ASNHeadersMDL, dynamic>(
            "SELECT ASNHeaderId, AccountNumber, VendorReference, SentDate, Cartons," +
                "Pallets, Cartons, StatusId FROM dbo.ASNHeader ", new { });
}
