using Company.Application.Share.Login;
using Company.DataBase.Base.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Company.Application.Login.Services
{
    class Session : ReactiveObject, ISession
    {
        [Reactive]
        public UserEntity CurrentUser { get ; set; }
        //这里不能[reactive]，因为=>只有getter属性，没有setter属性。Fody 的 `[Reactive]` 要求属性**必须有可写 setter** 才能生成 "值变化时发通知" 的代码。
        public Window MainWindow  => System.Windows.Application.Current.MainWindow;

    }
}
