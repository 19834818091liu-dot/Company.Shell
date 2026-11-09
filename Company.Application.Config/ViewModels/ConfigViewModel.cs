using Company.Application.Share.Config;
using Company.Application.Share.Login;
using Company.Core.Models;
using Prism.Regions;
using ReactiveUI;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Company.Application.Config.ViewModels
{
    public class ConfigViewModel
    {
        public ICommand LoadedCommand { get; set; }
        public ICommand NavigatonToViewCommand { get; set; }
        public ICommand SaveConfigCommand { get; set; }
      
        public ISession Session { get; set; }
        private ISystemConfigManager SystemConfigManager { get; set; }
        private IRegionManager RegionManager { get; set; }

        public ConfigViewModel(ISession session,ISystemConfigManager systemConfigManager,IRegionManager regionManager)
        {
            Session = session;
            SystemConfigManager = systemConfigManager;
            RegionManager = regionManager;
            LoadedCommand = ReactiveCommand.Create(OnLoadedCommand);
            NavigatonToViewCommand = ReactiveCommand.Create<string>(OnNavigatonToViewCommand);
            SaveConfigCommand = ReactiveCommand.Create(OnSaveConfigCommand);
        }
        /// <summary>
        /// 保存配置
        /// </summary>
        private void OnSaveConfigCommand()
        {
            SystemConfigManager.Save();
            MessageBox.Show("保存成功");
        }

        private void OnNavigatonToViewCommand(string view)
        {
            RegionManager.RequestNavigate(Names.ConfigRegion, view);
        }
        private bool Loaded = false;
        private void OnLoadedCommand()
        {
            if(Loaded) return;
            RegionManager.RequestNavigate(Names.ConfigRegion, Names.ConfigAxisView);    //加载时默认导航到运动轴设置页面
            Loaded = true;
        }
    }
}
