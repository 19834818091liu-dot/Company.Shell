using Company.Application.Config.Services;
using Company.Application.Config.Views;
using Company.Application.Share.Config;
using Company.Core.Models;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Config
{
    [Module(ModuleName =Names.ConfigModule,OnDemand = true)]
    public class ConfigModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ISystemConfigManager, SystemConfigManager>();
            containerRegistry.RegisterSingleton<ICalibrationManager, CalibrationManager>();
            containerRegistry.RegisterForNavigation<ConfigView>();       //配置主界面
            containerRegistry.RegisterForNavigation<ConfigAxisView>();   //轴配置界面
            containerRegistry.RegisterForNavigation<ConfigIOView>();     //IO配置界面
            containerRegistry.RegisterForNavigation<ConfigUserView>();   //用户配置界面
            containerRegistry.RegisterForNavigation<ConfigSoftwareView>(); //软件配置界面
            containerRegistry.RegisterForNavigation<CameraCalibrationView>();    //相机标定界面
        }
    }
}
