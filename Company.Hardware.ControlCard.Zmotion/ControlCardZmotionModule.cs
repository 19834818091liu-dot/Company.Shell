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

namespace Company.Hardware.ControlCard.Zmotion
{
    [Module(ModuleName =Names.ControlCardZmotionModule,OnDemand =true)]
    public class ControlCardZmotionModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            if(SystemConfig.HasHardware)
            {
                containerRegistry.RegisterSingleton<IControlCard,ControlCardZmotion>();
            }
        }
    }
}
