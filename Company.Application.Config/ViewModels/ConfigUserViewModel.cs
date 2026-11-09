using Company.DataBase.Base.Models;
using Company.DataBase.Base.Repositories;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Company.Application.Config.ViewModels
{
    public class ConfigUserViewModel:ReactiveObject
    {
        [Reactive]
        public List<UserEntity> Users { get; set; }
        private IUserRepository UserRepository { get; set; }
        public ICommand UpdateUserCommand { get; set; }
        public ICommand LoadedCommand { get; set; }
        public ConfigUserViewModel(IUserRepository userRepository)
        {
            UserRepository = userRepository;
            UpdateUserCommand = ReactiveCommand.Create(OnUpdateUserCommand);
            LoadedCommand = ReactiveCommand.Create(OnLoadedCommand);
        }

        private void OnLoadedCommand()
        {
            Users =  UserRepository.GetAll();
        }

        /// <summary>
        /// 保存命令
        /// </summary>
        private void OnUpdateUserCommand()
        {
            UserRepository.SaveChanged();
        }
    }
}
