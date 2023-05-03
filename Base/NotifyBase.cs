using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IntelligentControl.Base
{
    /// <summary>
    /// 通知属性
    /// </summary>
    internal class NotifyBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyChanged([CallerMemberName] string proName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proName));
        }

    }
}
