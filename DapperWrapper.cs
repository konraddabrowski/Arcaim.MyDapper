using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Arcaim.MyDapper.Exceptions;
using Dapper;
using Microsoft.Extensions.Logging;

namespace Arcaim.MyDapper
{
    public class DapperWrapper : IDapperWrapper
    {
        private Func<IDbConnection> _dbConnectionFactory;
        private readonly ILogger<DapperWrapper> _logger;

        public DapperWrapper(IDbConnection dbConnection, ILogger<DapperWrapper> logger)
        {
            _dbConnectionFactory = () => dbConnection;
            _logger = logger;
        }

        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                using var sqlConnection = _dbConnectionFactory();
                
                return await sqlConnection.ExecuteAsync(sql, param, transaction, commandTimeout,commandType);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,
                    @"An exception occurred while calling ExecuteAsync method with parameters:
                      sql = {0}
                      param = {1}",
                    sql, param);
                throw QueryFailedException.Create(ex.Message);
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            try
            {
                using var sqlConnection = _dbConnectionFactory();

                return await sqlConnection.QueryAsync<T>(sql, param, transaction, commandTimeout,commandType);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,
                    @"An exception occurred while calling QueryAsync method with parameters:
                      sql = {0}
                      param = {1}",
                    sql, param);
                throw QueryFailedException.Create(ex.Message);
            }
        }
    }
}