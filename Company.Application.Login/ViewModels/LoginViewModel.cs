using Company.Application.Share.Login;
using Company.Application.Share.Models;
using Company.Core.Enums;
using Company.DataBase.Base.Models;
using Company.DataBase.Base.Repositories;
using Prism.Events;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Company.Application.Login.ViewModels
{
    public class LoginViewModel:ReactiveObject
    {
        [Reactive]
        public UserEntity User { get; set; } = new UserEntity();
        public ICommand LoginCommand { get; set; }

        private IUserRepository UserRepository { get; set; }
        private IEventAggregator EventAggregator { get; set; }
        private ISession Session { get; set; }
        public LoginViewModel(IUserRepository userRepository,IEventAggregator eventAggregator,ISession session)
        {
            LoginCommand = ReactiveCommand.Create(OnLoginCommand);
            UserRepository = userRepository;
            EventAggregator = eventAggregator;
            Session  = session;
#if DEBUG
            User.UserName = "admin";
            User.Password = "123456";
#endif
        }

        private Task OnLoginCommand()
        {
            return Task.Run(() =>
            {
                UserEntity user = UserRepository.Select(User.UserName);     //获取用户名为User.UserName的User信息
               //判断user实体是否为null
                if (user != null)
                {
                    //比较密码
                    if (user.Password == User.Password)
                    {
                        //登录成功，跳转页面
                        EventAggregator.GetEvent<LoginEvent>().Publish(user);
                        Session.CurrentUser = user;
                    }
                    else
                    {
                        MessageBox.Show("用户名或密码错误");
                    }
                }
                else
                {
                    user = new UserEntity();
                    user.UserName =User.UserName;
                    user.Password = User.Password;
                    user.InsertDate = DateTime.Now;
                    user.Role = (int)RoleType.操作员;
                    if(user.UserName == "admin")
                    {
                        user.Role = (int)RoleType.管理员;
                    }
                    var count = UserRepository.Insert(user);
                    if(count > 0)
                    {
                        MessageBox.Show($"注册新用户成功\r\n用户名:{user.UserName}\r\n密码:{user.Password}");
                        Session.CurrentUser = user;
                        //todo 跳转页面
                        EventAggregator.GetEvent<LoginEvent>().Publish(user);

                    }
                }
            });
        }
    }
}
