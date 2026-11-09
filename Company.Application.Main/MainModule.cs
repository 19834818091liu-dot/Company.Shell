using Company.Application.Main.Views;
using Company.Core.Models;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Main
{
    [Module(ModuleName =Names.MainModule,OnDemand =true)]       //按需加载
    [ModuleDependency(Names.RunModule)]              //模块依赖
    [ModuleDependency(Names.ConfigModule)]
    public class MainModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainView>();
        }
    }
}
