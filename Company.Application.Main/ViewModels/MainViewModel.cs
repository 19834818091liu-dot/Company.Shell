using Company.Application.Share.Login;
using Company.Application.Share.Models;
using Company.Core.Models;
using Prism.Events;
using Prism.Regions;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Company.Application.Main.ViewModels
{
    public class MainViewModel:ReactiveObject

    {
        public ICommand LogoutCommand { get; set; }
        public ICommand LoadedCommand   { get; set; }
        public ICommand NavigationgToViewCommand { get; set; }
        private IEventAggregator EventAggregator { get; set; }
        private IRegionManager RegionManager { get; set; }
        private ISession Session { get; set; }
        public MainViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, ISession session)
        {
            EventAggregator = eventAggregator;
            RegionManager = regionManager;
            Session = session;
            LogoutCommand = ReactiveCommand.Create(OnLogoutCommand);
            LoadedCommand = ReactiveCommand.Create<ContentControl>(OnLoadedCommand);
            NavigationgToViewCommand = ReactiveCommand.Create<string>(OnNavigationgToViewCommand);
       }

        private void OnNavigationgToViewCommand(string view)
        {
            RegionManager.RequestNavigate(Names.MainRegion, view);
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void OnLoadedCommand(ContentControl contentControl)
        {
            if(contentControl.Content == null)
            {
                RegionManager.RequestNavigate(Names.MainRegion, Names.RunView);
            }
            Window mainWindow = Session.MainWindow;
            mainWindow.Width = 1800;
            mainWindow.Height = 1000;
            Rect workArea = SystemParameters.WorkArea;
            mainWindow.Left = (workArea.Width - mainWindow.Width) / 2 + workArea.Left;
            mainWindow.Top = (workArea.Height - mainWindow.Height) / 2 + workArea.Top;
            mainWindow.WindowState = WindowState.Maximized;
        }


        /// <summary>
        /// 登出方法
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void OnLogoutCommand()
        {
            EventAggregator.GetEvent<LogoutEvent>().Publish();   
        }
    }
}
