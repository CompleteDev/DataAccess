using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.DateInt;
public class DateInt : IDateInt
{
    public int GetDateTimeInt()
    {
        var timeUtc = DateTime.UtcNow.ToString("yyyyMMdd");

        return Convert.ToInt32(timeUtc);
    }
}
