using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexiónAmigo.Model.Config;
using Microsoft.Extensions.Options;
using Dapper;

namespace ConexiónAmigo.Data
{
    public abstract class BaseRepository
    {
        public readonly IOptions<DataConfig> _connectionString;

        public BaseRepository(IOptions<DataConfig> connectionString)
        {
            _connectionString = connectionString;
        }

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        {
            using (var con = new SqlConnection(_connectionString.Value.DataConnection))
            {
                await con.OpenAsync();
                return await con.QueryFirstOrDefaultAsync<T>(sql, param, commandType: CommandType.StoredProcedure);
            }
        }

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        {
            using (SqlConnection con = new SqlConnection(_connectionString.Value.DataConnection))
            {
                await con.OpenAsync();
                return await con.QueryAsync<T>(sql, param, commandType: CommandType.StoredProcedure);
            }
        }

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        public async Task<object> ExecuteScalarAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        {
            using (SqlConnection con = new SqlConnection(_connectionString.Value.DataConnection))
            {
                await con.OpenAsync();
                return await con.ExecuteScalarAsync<object>(sql, param, commandType: CommandType.StoredProcedure);
            }
        }

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        public async Task<int> ExecuteAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        {
            using (SqlConnection con = new SqlConnection(_connectionString.Value.DataConnection))
            {
                await con.OpenAsync();
                return await con.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure);
            }
        }

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        public async Task<SqlMapper.GridReader> QueryMultipleAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        {
            using (SqlConnection con = new SqlConnection(_connectionString.Value.DataConnection))
            {
                await con.OpenAsync();
                return await con.QueryMultipleAsync(sql, param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
