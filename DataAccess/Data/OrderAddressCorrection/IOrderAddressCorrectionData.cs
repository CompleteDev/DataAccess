using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models.OrderAddressCorrection;

namespace DataAccess.Data.OrderAddressCorrection;
public interface IOrderAddressCorrectionData
{
    Task CreateAddressCorrection(AddressCorrectionMDL addressCorrection);
}
