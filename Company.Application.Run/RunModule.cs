using Company.Application.Run.Services;
using Company.Application.Run.Views;
using Company.Application.Share.Run;
using Company.Core.Models;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Run
{
    [Module(ModuleName = Names.RunModule, OnDemand = true)]       //按需加载
    public class RunModule:IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<RunView>();
            containerRegistry.RegisterSingleton<IPhotoManager, PhotoManager>();
        }
    }
}
