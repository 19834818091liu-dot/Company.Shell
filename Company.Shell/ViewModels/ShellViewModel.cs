using Company.Application.Share.Login;
using Company.Application.Share.Models;
using Company.Core.Models;
using Company.DataBase.Base.Models;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Company.Shell.ViewModels
{
    public class ShellViewModel:ReactiveObject
    {
        public ICommand LoadedCommand { get; set; }

        private IRegionManager RegionManager { get; set; }
        private IEventAggregator EventAggregator { get; set; }
        private IModuleManager ModuleManager { get; set; }
        
        public ShellViewModel(IRegionManager regionManager,IEventAggregator eventAggregator,IModuleManager moduleManager)
        {
             RegionManager = regionManager;
             EventAggregator = eventAggregator;
             ModuleManager = moduleManager;
           
             LoadedCommand = ReactiveCommand.Create(OnLoadedCommand);
        }

        private void OnLoadedCommand()
        {
            RegionManager.RequestNavigate(Names.ShellRegion, Names.LoginView);
            EventAggregator.GetEvent<LoginEvent>().Subscribe(LoginSuccess,ThreadOption.UIThread);
            EventAggregator.GetEvent<InitSuccessEvent>().Subscribe(InitSuccess, ThreadOption.UIThread);
            EventAggregator.GetEvent<LogoutEvent>().Subscribe(LogoutSuccess, ThreadOption.UIThread);
        }
        /// <summary>
        /// 登出成功事件
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void LogoutSuccess()
        {
            RegionManager.RequestNavigate(Names.ShellRegion, Names.LoginView);
            Window mainWindow = System.Windows.Application.Current.MainWindow;

            Rect workArea = SystemParameters.WorkArea;
            mainWindow.Width = 1500;
            mainWindow.Height = 850;
            mainWindow.Left = (workArea.Width - mainWindow.Width) / 2 + workArea.Left;
            mainWindow.Top = (workArea.Height - mainWindow.Height) / 2 + workArea.Top;
            mainWindow.WindowState = WindowState.Normal;
        }

        private bool IsHardwareInitialized  = false;
        /// <summary>
        /// 初始化硬件成功事件
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void InitSuccess()
        {
            IsHardwareInitialized = true;        //初始化硬件成功就赋值为true
        }

        /// <summary>
        /// 登录成功事件
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void LoginSuccess(UserEntity entity)
        {
           if(!IsHardwareInitialized)
            {
                ModuleManager.LoadModule(Names.InitModule);   //先加载初始化硬件模块
                RegionManager.RequestNavigate(Names.ShellRegion, Names.InitView);   //再导航到初始化硬件界面
            }
           else
            {
                ModuleManager.LoadModule(Names.MainModule);
                RegionManager.RequestNavigate(Names.ShellRegion, Names.MainView);
            }
        }
    }
}
