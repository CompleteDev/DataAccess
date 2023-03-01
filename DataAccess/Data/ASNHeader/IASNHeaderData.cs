using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models.ASNHeader;
using DataAccess.Models.ASNTracking;

namespace DataAccess.Data.ASNHeader;
public interface IASNHeaderData
{
    Task<ASNHeadersMDL> GetASNHeader(int asnHeaderId);
    Task<IEnumerable<ASNHeadersMDL>> GetASNHeaders();
    Task<ASNHeadersMDL> GetASNTrackingHeader(string TrackingNumber);
}
