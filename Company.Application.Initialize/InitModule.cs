using Company.Application.Initialize.Services;
using Company.Application.Initialize.Views;
using Company.Application.Share.Init;
using Company.Application.Share.Login;
using Company.Core.Models;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Initialize
{
    [Module(ModuleName =Names.InitModule,OnDemand =true)]   //按需加载
    [ModuleDependency(nameof(Names.ConfigModule))]        //要在这里依赖系统配置模块，因为这里使用到了
    [ModuleDependency(nameof(Names.CameraModule))]   //依赖相机模块
    [ModuleDependency(nameof(Names.CameraEmulationModule))]  //依赖相机仿真模块
    [ModuleDependency(nameof(Names.ControlCardZmotionModule))]  //依赖正运动控制卡模块
    [ModuleDependency(nameof(Names.ControlCardEmulationModule))]  //依赖正运动控制卡仿真模块
    public class InitModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<InitView>();
            containerRegistry.RegisterSingleton<IHardwareManager, HardwareManager>();
        }
    }
}
