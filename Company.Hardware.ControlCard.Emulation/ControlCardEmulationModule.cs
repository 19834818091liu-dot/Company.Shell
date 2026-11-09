using Company.Core.Config;
using Company.Core.Models;
using Company.Hardware.ControlCard.Base;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Hardware.ControlCard.Emulation
{
    [Module(ModuleName =Names.ControlCardEmulationModule,OnDemand =true)]
    public class ControlCardEmulationModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            if (!SystemConfig.HasHardware)
            {
                containerRegistry.RegisterSingleton<IControlCard, ControlCardEmulation>();
            }
        }
    }
}
