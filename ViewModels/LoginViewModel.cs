using IntelligentControl.Base;
using IntelligentControl.DataAccess;
using IntelligentControl.Models;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace IntelligentControl.ViewModels
{
    internal class LoginViewModel : NotifyBase
    {

        private MysqlAccess mysqlAccess;

        public UserModel UserModel { set; get; }

        public LoginViewModel()
        {
            this.UserModel = new UserModel("", "");
            this._closeCommand = new CommandBase(CloseLoginWindow);
            this._loginCommand = new CommandBase(LoginLogic);
            this.mysqlAccess = new MysqlAccess();
            this._errorMessge = string.Empty;
        }

        // 关闭命令
        private readonly CommandBase _closeCommand;

        public CommandBase CloseCommand
        {
            get { return _closeCommand; }
        }

        // 登录命令
        private readonly CommandBase _loginCommand;

        public CommandBase LoginCommand
        {
            get { return _loginCommand; }
        }

        private string _errorMessge;

        // 登录错误描述
        public string ErrorMessage
        {
            get { return _errorMessge; }
            set
            {
                _errorMessge = value;
                this.NotifyChanged();
            }
        }


        /// <summary>
        /// 关闭命令
        /// </summary>
        /// <param name="obj"></param>
        public void CloseLoginWindow(object? obj)
        {
            if (obj != null)
            {
                var win = (obj as Window);
                if (win == null)
                {
                    return;
                }
                win.DialogResult = false;
            }
        }

        /// <summary>
        /// 登录命令
        /// </summary>
        /// <param name="obj"></param>
        public void LoginLogic(object? obj)
        {
            this.ErrorMessage = "";
            if (obj != null)
            {
                var win = (obj as Window);
                if (win == null)
                {
                    return;
                }
                if (string.IsNullOrEmpty(this.UserModel.UserName) || string.IsNullOrEmpty(this.UserModel.Password))
                {
                    this.ErrorMessage = "用户名和密码不能为空";
                    return;
                }
                var mysqlAccess = new MysqlAccess();
                mysqlAccess.OpenConnect();
                // 填充参数
                string userSql = "select * from users where user_name=@userName and password=@Password and is_validation=1";
                var sqlParams = new Dictionary<string, object>
                {
                    { "@userName", this.UserModel.UserName },
                    { "@Password", this.UserModel.Password }
                };
                DataTable ds = mysqlAccess.ExecuteDataTable(userSql, sqlParams);
                if (ds == null)
                {
                    this.ErrorMessage = "未知错误";
                    return;
                }

                if (ds.Rows.Count <= 0)
                {
                    this.ErrorMessage = "用户名或者密码不正确";
                    return;
                }
                var row = ds.Rows[0];
                var is_can_login = row.Field<bool>("is_can_login");
                if (!is_can_login)
                {
                    this.ErrorMessage = "此用户无权限使用此平台";
                    return;
                }
                mysqlAccess.CloseConnect();
                win.DialogResult = true;
            }
        }

    }
}
