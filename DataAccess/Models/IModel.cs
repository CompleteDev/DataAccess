namespace DataAccess.Models;

public interface IModel
{
    [DapperIgnore]
    string TableName { get; }
}

[AttributeUsage(AttributeTargets.Property)]
public class DapperKey : Attribute
{
}

[AttributeUsage(AttributeTargets.Property)]
public class DapperIgnore : Attribute
{
}