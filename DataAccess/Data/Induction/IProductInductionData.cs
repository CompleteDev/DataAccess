using DataAccess.Models.Induction;

namespace DataAccess.Data.Induction;
public interface IProductInductionData
{
    Task InductProduct(InductProductMDL prodmdl);
}