using IntelligentControl.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentControl.Models
{

    /// <summary>
    /// 存储用户信息
    /// </summary>

    internal class UserModel : NotifyBase
    {
        private string? _userName;

        private string? _password;
        
        public string? UserName { 
            get { return _userName; }
            set {
                _userName = value;
                this.NotifyChanged();
            }
        }

        public string? Password { 
            get { return _password; }
            set {
                _password = value;
                this.NotifyChanged();
            } 
        }

        public UserModel(string userName, string password) { 
            this.UserName = userName;
            this.Password = password;
        }

        

    }
}
