using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentControl.Base
{
    /// <summary>
    /// 通知属性
    /// </summary>
    internal class NotifyBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyChanged([CallerMemberName] string proName = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proName));
        }

    }
}
