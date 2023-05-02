using log4net;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentControl.config
{
    /// <summary>
    /// 数据库连接配置类
    /// </summary>
    internal class MysqlConnectionConfig
    {

        private static readonly ILog logger = LogManager.GetLogger(typeof(MysqlConnectionConfig));

        private static readonly object INIT_LOCK = new object();

        private static MysqlConfigInfo? _configInfo = null;

        private static MysqlConfigInfo? ConfigInfo {
            get {
                try {
                    if (_configInfo == null) {
                        lock (INIT_LOCK) {
                            if (_configInfo == null) {
                                _configInfo = new MysqlConfigInfo();
                                Type type = _configInfo.GetType();
                                var properties = type.GetProperties();
                                foreach (var property in properties)
                                {
                                    var fieldName = property.Name;
                                    object? fieldValue = ConfigurationManager.ConnectionStrings[fieldName]?.ConnectionString;
                                    if (fieldValue == null) {
                                        continue;
                                    }
                                    fieldValue = Convert.ChangeType(fieldValue, property.PropertyType);
                                    property.SetValue(_configInfo, fieldValue);
                                }
                            }
                        }
                    }
                } catch (Exception e) {
                    logger.Info($"数据库连接配置读取出现异常, errorMsg: {e.Message}");
                    _configInfo = null;
                }
                return _configInfo;
            }
            set { _configInfo = value; }
        }

        /// <summary>
        /// 返回连接池配置信息
        /// </summary>
        /// <returns></returns>
        public static string? GetMysqlConfigInfoStr() {
            return ConfigInfo?.convertToConnectionString();
        }

        /// <summary>
        /// mysql数据库配置信息
        /// </summary>
        private class MysqlConfigInfo {

            /// <summary>
            /// 连接地址
            /// </summary>
            public string? Server { set; get; }

            /// <summary>
            /// 数据库名称
            /// </summary>
            public string? Database { set; get; }

            /// <summary>
            /// 用户名
            /// </summary>
            public string? Username { set; get; }

            /// <summary>
            /// 密码
            /// </summary>
            public string? Password { set; get; }

            /// <summary>
            /// 端口号
            /// </summary>
            public uint Port { set; get; }

            /// <summary> 
            /// 设置字符编码，例："utf8"
            /// </summary>
            public string CharacterSet { get; set; } = "utf8";

            /// <summary> 
            /// 连接超时时间，单位秒
            /// </summary>
            public uint ConnectionTimeout { get; set; } = 100;

            /// <summary> 
            /// 数据库执行超时时间，单位秒
            /// </summary>
            public uint DefaultCommandTimeout { get; set; } = 1000;

            /// <summary> 
            /// 是否开启连接池，true
            /// </summary>
            public bool Pooling { get; set; } = true;

            /// <summary> 
            /// 连接池中最小连接数
            /// </summary>
            public uint MinimumPoolSize { get; set; } = 4;

            /// <summary> 
            /// 连接池中最大连接数
            /// </summary>
            public uint MaximumPoolSize { get; set; } = 8;

            /// <summary> 
            /// 连接池中连接对象存活时间，单位秒
            /// </summary>
            public uint ConnectionLifeTime { get; set; } = 100;

            /// <summary> 
            /// 连接是否使用压缩，true
            /// </summary>
            public bool UseCompression { get; set; } = true;

            /// <summary> 
            /// 表示连接池程序是否会自动登记创建线程的当前事务语境中的连接，ture
            /// </summary>
            public bool AutoEnlist { get; set; } = true;

            public string convertToConnectionString()
            {
                MySqlConnectionStringBuilder mysqlConne = new MySqlConnectionStringBuilder();
                mysqlConne.UserID = Username;
                mysqlConne.Password = Password;
                mysqlConne.Port = Port;
                mysqlConne.Server = Server;
                mysqlConne.Database = Database;
                mysqlConne.CharacterSet = CharacterSet;
                mysqlConne.ConnectionTimeout = ConnectionTimeout;
                mysqlConne.DefaultCommandTimeout = DefaultCommandTimeout;
                mysqlConne.Pooling = Pooling;
                mysqlConne.MinimumPoolSize = MinimumPoolSize;
                mysqlConne.MaximumPoolSize = MaximumPoolSize;
                mysqlConne.ConnectionLifeTime = ConnectionLifeTime;
                mysqlConne.UseCompression = UseCompression;
                mysqlConne.AutoEnlist = AutoEnlist;
                return mysqlConne.ConnectionString;
            }
        }

    }
}
