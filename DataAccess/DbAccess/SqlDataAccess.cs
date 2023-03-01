using System.Data;
using System.Data.SqlClient;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace DataAccess.DbAccess;
public class SqlDataAccess : ISqlDataAccess
{
    private readonly string _default;
    private readonly string _mfConnx;
    private readonly string _velociti;

    public SqlDataAccess(IConfiguration Config)
    {
        IConfiguration config = Config;
        SecretClientOptions options = new SecretClientOptions()
        {
            Retry =
            {
                Delay = TimeSpan.FromSeconds(1),
                MaxDelay = TimeSpan.FromSeconds(5),
                MaxRetries = 5,
                Mode = RetryMode.Exponential,
            },
        };
        SecretClient secretClient = new SecretClient(new Uri(config.GetSection("KeyVaultURL").Value), new DefaultAzureCredential(), options);
        _default = secretClient.GetSecret(config.GetSection("Secrets:CThreePLDB").Value).Value.Value;
        _mfConnx = secretClient.GetSecret(config.GetSection("Secrets:MFConnxDB").Value).Value.Value;
        _velociti = secretClient.GetSecret(config.GetSection("Secrets:VelocitiDB").Value).Value.Value;
    }

    public async Task<IEnumerable<T>> LoadData<T, U>(string SQLStatment, U parameters, string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(GetConnectionString(connectionId));

        return await connection.QueryAsync<T>(SQLStatment, parameters, commandType: CommandType.Text);
    }

    public async Task<IEnumerable<T>> CallSP<T, U>(string SQLStatment, U parameters, string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(GetConnectionString(connectionId));

        return await connection.QueryAsync<T>(SQLStatment, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task SaveData<T>(string SQLStatment, T parameters, string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(GetConnectionString(connectionId));

        await connection.ExecuteAsync(SQLStatment, parameters, commandType: CommandType.Text);
    }

    public async Task<long> ExecuteScalar<T, U>(string SQLStatment, U parameters, string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(GetConnectionString(connectionId));

        return await connection.ExecuteScalarAsync<long>(SQLStatment, parameters, commandType: CommandType.Text);
    }

    private string GetConnectionString(string connectionId)
    {
        string connectionString = _default;
        switch (connectionId)
        {
            case "MFConnx":
                connectionString = _mfConnx;
                break;

            case "Velociti":
                connectionString = _velociti;
                break;
        }

        return connectionString;
    }
}