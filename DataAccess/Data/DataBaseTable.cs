using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Dapper;
using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data.ShipmentHeader;

public partial class DataBaseTable<Model> : IDataBaseTable<Model>
    where Model : IModel
{
    private readonly ISqlDataAccess _db;
    public DataBaseTable(ISqlDataAccess db)
    {
        _db = db;
    }

    public async Task Insert(Model model)
    {
        var propertyContainer = ParseProperties(model);
        //var potentialsql = $"INSERT INTO [{model.TableName}] ({string.Join(", ", propertyContainer.ValueNames)}) VALUES(@{string.Join(", @", propertyContainer.ValueNames)})";
        var sql = string.Format(
            @"INSERT INTO [{0}] ({1}) VALUES(@{2})",
                model.TableName,
                string.Join(", ", propertyContainer.AllNames),
                string.Join(", @", propertyContainer.AllNames));
        var parameters = new DynamicParameters(propertyContainer.AllPairs);
        await _db.SaveData(sql, parameters);
    }

    public async Task<IEnumerable<Model>> Select(Model criteria)
    {
        var properties = ParseProperties(criteria);
        var sqlPairs = GetSqlPairs(properties.AllNames, " AND ");
        var valuesToGet = string.Join(", ", properties.AllNames);
        var sql = string.Format("SELECT {0} FROM [{1}] WHERE {2}", valuesToGet, criteria.TableName, sqlPairs);
        var parameters = new DynamicParameters(properties.AllPairs);
        return await _db.LoadData<Model, dynamic>(sql, parameters);
    }

    public async Task<IEnumerable<Model>> SelectByPrimaryKey(Model criteria)
    {
        var properties = ParseProperties(criteria);
        var valuesToGet = string.Join(", ", properties.AllNames);
        var sqlIdPairs = GetSqlPairs(properties.IdNames);

        var parameters = new DynamicParameters(properties.IdPairs);
        var sql = string.Format("SELECT {0} FROM [{1}] WHERE {2}", valuesToGet, criteria.TableName, sqlIdPairs);
        return await _db.LoadData<Model, dynamic>(sql, parameters);
    }

    public async Task<IEnumerable<Model>> SelectByForeignKey(Model criteria, string keyName)
    {
        var properties = ParseProperties(criteria);
        var valuesToGet = string.Join(", ", properties.AllNames);
        var foreignKeyComparison = $"{keyName} = @{keyName}";
        var parameters = new DynamicParameters(properties.AllPairs);

        var sql = string.Format("SELECT {0} FROM [{1}] WHERE {2}", valuesToGet, criteria.TableName, foreignKeyComparison);
        return await _db.LoadData<Model, dynamic>(sql, parameters);
    }

    public async Task Update(Model obj)
    {
        var propertyContainer = ParseProperties(obj);
        var sqlIdPairs = GetSqlPairs(propertyContainer.IdNames);
        var sqlValuePairs = GetSqlPairs(propertyContainer.ValueNames);
        var sql = string.Format(
            @"UPDATE [{0}] SET {1} WHERE {2}",
            obj.TableName,
            sqlValuePairs,
            sqlIdPairs);
        var parameters = new DynamicParameters(propertyContainer.AllPairs);
        await _db.ExecuteScalar<Model, dynamic>(sql, parameters);
    }

    /// <summary>
    /// Create a commaseparated list of value pairs on
    /// the form: "key1=@value1, key2=@value2, ..."
    /// </summary
    private static string GetSqlPairs(IEnumerable<string> keys, string separator = ", ")
    {
        var pairs = keys.Select(key => string.Format("{0}=@{0}", key)).ToList();
        return string.Join(separator, pairs);
    }

    /// <summary>
    /// Retrieves a Dictionary with name and value
    /// for all object properties matching the given criteria.
    /// </summary>
    private PropertyContainer ParseProperties(Model obj)
    {
        var propertyContainer = new PropertyContainer();

        var typeName = typeof(Model).Name;
        var validKeyNames = new[]
        {
            "Id", string.Format("{0}Id", typeName), string.Format("{0}_Id", typeName)
        };

        var properties = typeof(Model).GetProperties();
        foreach (var property in properties)
        {
            // Skip reference types (but still include string!)
            if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                continue;

            // Skip methods without a public setter
            if (property.GetSetMethod() == null)
                continue;

            // Skip methods specifically ignored
            if (property.IsDefined(typeof(DapperIgnore), false))
                continue;

            var name = property.Name;
            var value = typeof(Model).GetProperty(property.Name).GetValue(obj, null);

            if (property.IsDefined(typeof(DapperKey), false) || validKeyNames.Contains(name))
            {
                propertyContainer.AddId(name, value);
            }
            else
            {
                propertyContainer.AddValue(name, value);
            }
        }

        return propertyContainer;
    }
}
