namespace DataAccess.Data;

public interface IDataBaseTable<Model>
{
    Task Insert(Model model);
    Task Update(Model obj);
    Task<IEnumerable<Model>> Select(Model criteria);
    Task<IEnumerable<Model>> SelectByForeignKey(Model criteria, string keyName);
    Task<IEnumerable<Model>> SelectByPrimaryKey(Model criteria);
}