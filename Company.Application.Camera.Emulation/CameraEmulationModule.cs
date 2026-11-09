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

namespace Company.Application.Camera.Emulation
{
    [Module(ModuleName =Names.CameraEmulationModule,OnDemand =true)]
    public class CameraEmulationModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            if(!SystemConfig.HasHardware)
            {
                containerRegistry.Register<ICamera, CameraEmulation>(CameraType.left.ToString());
                containerRegistry.Register<ICamera, CameraEmulation>(CameraType.right.ToString());
            }
        }
    }
}
