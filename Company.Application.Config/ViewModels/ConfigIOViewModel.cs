using Company.Hardware.ControlCard.Base;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Company.Application.Config.ViewModels
{
    public class ConfigIOViewModel
    {
        public IControlCard ControlCard { get; set; }

        public ICommand SetOutCommand { get; set; }
        public ConfigIOViewModel(IControlCard controlCard)
        {
            ControlCard = controlCard;
            SetOutCommand = ReactiveCommand.Create<IOType>(OnSetOutCommand);
        }
        /// <summary>
        /// 控制该输出点
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void OnSetOutCommand(IOType iOType)
        {
            ControlCard.SetSingleOutput(iOType,uint.MaxValue);
        }
    }
}
