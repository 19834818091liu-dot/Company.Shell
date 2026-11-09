using Company.Application.Login.Services;
using Company.Application.Login.ViewModels;
using Company.Application.Login.Views;
using Company.Application.Share.Login;
using Company.Core.Models;
using Company.DataBase.Base.Repositories;
using Company.DataBase.Sqlite;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Login
{
    [Module(ModuleName =Names.LoginModule,OnDemand =false)]   //初始化时就加载loginModule
    public class LoginModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LoginView>();
            containerRegistry.RegisterSingleton<ISession, Session>();
        }
    }
}
