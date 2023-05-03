using IntelligentControl.config;
using log4net;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace IntelligentControl.DataAccess
{
    internal class MysqlAccess
    {

        private static readonly ILog logger = LogManager.GetLogger(typeof(MysqlAccess));

        /// <summary>
        /// mysql数据库连接信息
        /// </summary>
        private MySqlConnection mySqlConnection;

        /// <summary>
        /// mysql数据库事务信息
        ///     可以为null, 因为并不一定存在事务
        /// </summary>
        private MySqlTransaction? mySqlTransaction = null;


        public MysqlAccess()
        {
            var connectionStr = MysqlConnectionConfig.GetMysqlConfigInfoStr();
            if (string.IsNullOrEmpty(connectionStr))
            {
                throw new Exception("数据库配置读取出错");
            }
            mySqlConnection = new MySqlConnection(connectionStr);
        }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public void OpenConnect()
        {
            try
            {
                if (mySqlConnection.State == ConnectionState.Closed)
                {
                    mySqlConnection.Open();
                }
                else if (mySqlConnection.State == ConnectionState.Broken)
                {
                    mySqlConnection.Close();
                    mySqlConnection.Open();
                }
            }
            catch (Exception ex)
            {
                logger.Info($"打开数据库连接时发生异常, errorMsg: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 关闭连接
        ///     如果存在事务还没有提交，就会进行一次事务提交
        /// </summary>
        public void CloseConnect()
        {
            try
            {
                if (mySqlTransaction != null)
                {
                    mySqlTransaction.Commit();
                    mySqlTransaction.Dispose();
                    mySqlTransaction = null;
                }
                if (mySqlConnection.State != ConnectionState.Closed)
                {
                    mySqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Info($"关闭数据库连接时发生异常, errorMsg: {ex.Message}");
                throw;
            }
        }

        public void BeginTransaction()
        {
            try
            {
                mySqlTransaction = mySqlConnection.BeginTransaction();
            }
            catch (Exception ex)
            {
                logger.Info($"开启事务处理时发生异常, errorMsg: {ex.Message}");
                throw;
            }
        }

        public void CommitTransaction()
        {
            try
            {
                if (mySqlTransaction != null)
                {
                    mySqlTransaction.Commit();
                    mySqlTransaction.Dispose();
                    mySqlTransaction = null;
                }
            }
            catch (Exception e)
            {
                logger.Info($"提交事务发生异常, errorMsg: {e.Message}");
                throw;
            }
        }


        public void RollbackTransaction()
        {
            try
            {
                if (mySqlTransaction != null)
                {
                    mySqlTransaction.Rollback();
                    mySqlTransaction.Dispose();
                    mySqlTransaction = null;
                }
            }
            catch (Exception e)
            {
                logger.Info($"回滚事务发生异常, errorMsg: {e.Message}");
                throw;
            }
        }

        /// <summary>
        /// 获取sql语句影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, Dictionary<string, object> sqlParams)
        {
            int row = -1;
            try
            {
                using var command = mySqlConnection.CreateCommand();
                command.CommandText = sql;
                createMysqlCommand(command, sqlParams);
                row = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                logger.Info($"执行sql语句时发生异常, errorMsg: {e.Message}");
                throw;
            }
            return row;
        }

        /// <summary>
        /// 执行sql查询，并且返回第一行的第一列的值
        /// </summary>
        /// <returns></returns>
        public object? ExecuteScalar(string sql, Dictionary<string, object> sqlParams)
        {
            object? result = null;
            try
            {
                using var command = mySqlConnection.CreateCommand();
                command.CommandText = sql;
                createMysqlCommand(command, sqlParams);
                result = command.ExecuteScalar();
            }
            catch (Exception e)
            {
                logger.Info($"执行sql语句时发生异常, errorMsg: {e.Message}");
                throw;
            }
            return result;
        }


        /// <summary>
        /// 返回MysqlReader数据流对象，只有在使用reader的时候才会去访问数据库
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public MySqlDataReader? ExecuteReader(string sql, Dictionary<string, object> sqlParams)
        {
            MySqlDataReader? result = null;
            try
            {
                using var command = mySqlConnection.CreateCommand();
                command.CommandText = sql;
                createMysqlCommand(command, sqlParams);
                result = command.ExecuteReader();
            }
            catch (Exception e)
            {
                logger.Info($"执行sql语句时发生异常, errorMsg: {e.Message}");
                throw;
            }
            return result;
        }

        public DataTable ExecuteDataTable(string sql, Dictionary<string, object> sqlParams)
        {
            DataTable? result = null;
            try
            {
                result = new DataTable();
                using var mysqlDataApdater = new MySqlDataAdapter();
                using var command = mySqlConnection.CreateCommand();
                command.CommandText = sql;
                createMysqlCommand(command, sqlParams);
                mysqlDataApdater.SelectCommand = command;
                mysqlDataApdater.Fill(result);
            }
            catch (Exception e)
            {
                logger.Info($"执行sql语句时发生异常, errorMsg: {e.Message}");
                throw;
            }
            return result;
        }

        /// <summary>
        /// 填充DataSet数据类
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlParams"></param>
        /// <param name="srcTable"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string sql, Dictionary<string, object> sqlParams, string srcTable)
        {
            DataSet? result = null;
            try
            {
                result = new DataSet();
                using var mysqlDataApdater = new MySqlDataAdapter();
                using var command = mySqlConnection.CreateCommand();
                command.CommandText = sql;
                createMysqlCommand(command, sqlParams);
                mysqlDataApdater.SelectCommand = command;
                if (string.IsNullOrWhiteSpace(srcTable))
                {
                    mysqlDataApdater.Fill(result);
                }
                else
                {
                    mysqlDataApdater.Fill(result, srcTable);
                }
            }
            catch (Exception e)
            {
                logger.Info($"执行sql语句时发生异常, errorMsg: {e.Message}");
                throw;
            }
            return result;
        }

        /// <summary>
        /// 为sqlCommand执行类填充参数
        /// </summary>
        /// <param name="command"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        private void createMysqlCommand(MySqlCommand command, Dictionary<string, object> sqlParams)
        {
            if (sqlParams != null && sqlParams.Count > 0)
            {
                foreach (KeyValuePair<string, object> keyValuePair in sqlParams)
                {

                    command.Parameters.AddWithValue(keyValuePair.Key, keyValuePair.Value);
                }
            }
        }

    }
}
