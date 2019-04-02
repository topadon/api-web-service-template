using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DAL
{
    public class Helper : BaseConnection
    {
        public List<T> ExecuteQuery<T>(string sql, object paramlist, CommandType commandType) where T : new()
        {
            List<T> result = new List<T>();
            using (IDbConnection conn = GetOpenConnection())
            {
                try
                {
                    result = conn.Query<T>(sql, paramlist, null, false, null, commandType).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
            return result;
        }
        public string ExecuteQuery(string sql, object paramlist, CommandType commandType)
        {
            string result = string.Empty;
            using (IDbConnection conn = GetOpenConnection())
            {
                try
                {
                    result = conn.Query<string>(sql, paramlist, null, false, null, commandType).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
            return result;
        }

        public string ExecuteListQuery(string sql, object paramlist, CommandType commandType)
        {
            string result = string.Empty;
            using (IDbConnection conn = GetOpenConnection())
            {
                try
                {
                    result = string.Join(",", conn.Query<DataTable>(sql, paramlist, null, false, null, commandType).ToList());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
            return result;
        }

        public int ExecuteScalar(string sql, object paramlist)
        {
            int result = 0;
            using (IDbConnection conn = GetOpenConnection())
            {
                try
                {
                    result = conn.ExecuteScalar<int>(sql, paramlist);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }
            return result;
        }
        public bool ExecuteNonQuery(string sql, object paramlist, CommandType commandType)
        {
            bool result = false;
            using (IDbConnection conn = GetOpenConnection())
            {
                IDbTransaction transaction = null;
                try
                {
                    transaction = conn.BeginTransaction();
                    if (conn.Execute(sql, paramlist, transaction, null, commandType) > 0) result = true;
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                        transaction.Dispose();
                    }
                }
            }
            return result;
        }
    }
}