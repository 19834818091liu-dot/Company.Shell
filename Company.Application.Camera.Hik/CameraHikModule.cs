using Company.Core.Config;
using Company.Core.Models;
using Company.Hardware.Camera.Base;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Camera.Hik
{
    [Module(ModuleName =Names.CameraModule,OnDemand = true)]
    public class CameraHikModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            if(SystemConfig.HasHardware)
            {
                containerRegistry.Register<ICamera, CameraHik_CS_050_60GM>(CameraType.left.ToString());
                containerRegistry.Register<ICamera, CameraHik_CS_050_60GM>(CameraType.right.ToString());
            }
        }
    }
}
