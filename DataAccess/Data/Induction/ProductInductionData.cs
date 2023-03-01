using DataAccess.DbAccess;
using DataAccess.Models;
using DataAccess.Models.Induction;
using Microsoft.VisualBasic;

namespace DataAccess.Data.Induction;
public class ProductInductionData : IProductInductionData
{
    private readonly ISqlDataAccess _db;
    public ProductInductionData(ISqlDataAccess db)
    {
        _db = db;
    }

    public async Task InductProduct(InductProductMDL prodmdl)
    {
        try
        {
            if (await CheckProdId(prodmdl.SKU) != 0)
            {
                long prodID = await GetProdID(prodmdl.SKU);
                long matID = await AssignMaterialBit(prodmdl.BookID);
                DateTime scannedDate = Convert.ToDateTime(prodmdl.ScannedDate);
                int dateInt = Convert.ToInt32(scannedDate.ToString("yyyyMMdd"));
                int yearInt = Convert.ToInt32(scannedDate.ToString("yyyy"));
                int clientId = 1;
                int quantity = 1;
                if (matID != 0)
                {
                   long defID = await _db.ExecuteScalar<long, dynamic>(
                    "INSERT INTO Prod_Product_Definitions(ProdId,ClientId,DefinitionValue,IndcutionDate,DateInt,YearInt) " +
                    "VALUES(@ProdID,@ClientId,@MatID,@ScannedDate,@DateInt,@YearInt) " +
                    "SELECT SCOPE_IDENTITY()", new { prodID, clientId, matID, scannedDate, dateInt, yearInt });

                    await _db.SaveData(
                        "INSERT INTO Prod_Definition_Info(DefId,BookId,ShipmentNumber,ShipmentType,Quantity,Station,UserId,Destination) " +
                        "VALUES(@DefId,@BookId,@ShipmentNumber,@ShipType,@Quantity,@WorkStation,@EmpID,@Destination)",
                        new { defID, prodmdl.BookID, prodmdl.ShipmentNumber, prodmdl.ShipType, quantity, prodmdl.WorkStation, prodmdl.EmpID, prodmdl.Destination });
                }
                else
                {
                    //Error
                }
            }
            else
            {
                //Pull book
                //For now insert into Induction event table
                //Email proper parties
                //Log error
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task<int> CheckProdId(string SKU)
    {
        try
        {
            var prodID = await _db.LoadData<int, dynamic>("SELECT COUNT(ProductId) FROM Prod_Products WHERE SKU = @SKU", new { SKU });

            return prodID.FirstOrDefault();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task<long> GetProdID(string SKU)
    {
        try
        {
            var prodID = await _db.LoadData<long, dynamic>("SELECT ProductId FROM Prod_Products WHERE SKU = @SKU", new { SKU });

            return prodID.FirstOrDefault();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task<long> AssignMaterialBit(string MaterialID)
    {
        var getMatId = MaterialID.Substring(0, 3);
        var conId = MaterialID.Substring(3, 1);

        var matId = getMatId switch
        {
            "881" => 1,
            "880" => 2,
            "882" => 8,
            "883" => 4,
            "884" => 16
        };

        if (matId != 0)
        {
            if (Convert.ToInt32(conId) == 1)
            {
                matId = matId + 32;
            }
            else
            {
                matId = matId + 64;
            }
        }

        return matId;
    }
}