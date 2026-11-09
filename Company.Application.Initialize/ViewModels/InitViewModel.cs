using Company.Application.Initialize.Services;
using Company.Application.Share.Init;
using Company.Application.Share.Login;
using Company.Core.Models;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Company.Application.Initialize.ViewModels
{
    public class InitViewModel:ReactiveObject
    {
        [Reactive]
        public string Message { get; set; }
        public ICommand LoadedCommand { get; set; }
        public ICommand InitializeCommand   { get; set; }
        public ICommand OpenMainViewCommand { get; set; }
        private IHardwareManager HardwareManager { get; set; }

        public ISession Session     { get; set; }
        private IModuleManager ModuleManager { get; set; }
        private IRegionManager RegionManager { get; set; }

        public InitViewModel(IHardwareManager  hardwareManager ,ISession session,IModuleManager moduleManager,IRegionManager regionManager)
        {
            HardwareManager = hardwareManager;
            Session = session;
            ModuleManager = moduleManager;
            RegionManager = regionManager;
            LoadedCommand = ReactiveCommand.Create(OnLoadedCommand);
            InitializeCommand = ReactiveCommand.Create(OnLoadedCommand);     //初始化命令就相当于是加载命令
            OpenMainViewCommand = ReactiveCommand.Create(OnOpenMainViewCommand);

        }
        /// <summary>
        /// 跳过加载硬件模块，直接进入主界面
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void OnOpenMainViewCommand()
        {
            ModuleManager.LoadModule(Names.MainModule);
            RegionManager.RequestNavigate(Names.ShellRegion, Names.MainView);
        }

        /// <summary>
        /// 加载初始化界面命令
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private async Task OnLoadedCommand()
        {
            Rect workArea = SystemParameters.WorkArea;            // 电脑屏幕可用的公共区域，不包含状态栏
            Session.MainWindow.Left = (workArea.Width - Session.MainWindow.Width) / 2 + workArea.Left;
            Session.MainWindow.Top = (workArea.Height - Session.MainWindow.Height) / 2 + workArea.Top;
            Session.MainWindow.WindowState = WindowState.Maximized; 
            //如果硬件没有加载
            if(!HardwareManager.Initialized)
            {
                Message = "正在初始化所有硬件";
                var result =  await HardwareManager.InitAsync();
                if(result.Success)
                {
                    ModuleManager.LoadModule(Names.MainModule);
                    RegionManager.RequestNavigate(Names.ShellRegion, Names.MainView);
                }
                else
                {
                    Message = result.Message;
                }
            }
        }
    }
}
