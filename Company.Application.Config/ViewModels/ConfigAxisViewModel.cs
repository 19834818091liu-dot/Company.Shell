using Company.Hardware.ControlCard.Base;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using Company.Application.Share.Config;

namespace Company.Application.Config.ViewModels
{
    public class ConfigAxisViewModel
    {
        public ICommand MoveAbsoluteCommand { get; set; }
        public ICommand AxisJogPlusCommand { get; set; }
        public ICommand AxisJogMinusCommand { get; set; }
        public ICommand AxisRecoverCommand { get; set; }
        public ICommand AxisStopCommand { get; set; }
        public ICommand AxisResetCommand { get; set; }
        public ICommand AxisEnableCommand { get; set; }
        public IControlCard ControlCard { get; set; }

        public ISystemConfigManager SystemConfigManager { get; set; }
        public ConfigAxisViewModel(IControlCard controlCard,ISystemConfigManager systemConfigManager)
        {
            ControlCard = controlCard;
            SystemConfigManager = systemConfigManager;
            MoveAbsoluteCommand = ReactiveCommand.CreateFromTask<TextBox>(OnMoveAbsoluteCommand);
            AxisJogPlusCommand = ReactiveCommand.CreateFromTask<AxisType>(OnAxisJogPlusCommand);
            AxisJogMinusCommand = ReactiveCommand.CreateFromTask<AxisType>(OnAxisJogMinusCommand);
            AxisRecoverCommand = ReactiveCommand.CreateFromTask<AxisType>(OnAxisRecoverCommand);
            AxisStopCommand = ReactiveCommand.CreateFromTask<AxisType>(OnAxisStopCommand);
            AxisResetCommand = ReactiveCommand.CreateFromTask<AxisType>(OnAxisResetCommand);
            AxisEnableCommand = ReactiveCommand.Create<AxisType>(OnAxisEnableCommand);
        }
        /// <summary>
        /// 轴重置
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task OnAxisResetCommand(AxisType axisType)
        {
            //todo
            await Task.Delay(500);
        }

        /// <summary>
        /// 轴使能
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private void OnAxisEnableCommand(AxisType axisType)
        {
            ControlCard.AxisEnable(axisType);
        }

        /// <summary>
        /// 轴停止命令
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task OnAxisStopCommand(AxisType axisType)
        {
            await ControlCard.StopAxisAsync(axisType);
        }

        /// <summary>
        /// 复位回到零点
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task OnAxisRecoverCommand(AxisType axisType)
        {
            await ControlCard.MoveToZeroAsync(axisType);
        }

        /// <summary>
        /// Jog-命令
        /// </summary>
        /// <param name="axisType"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task OnAxisJogMinusCommand(AxisType axisType)
        {
            await ControlCard.MoveRelativeAsync(axisType, -SystemConfigManager.SoftwareConfig.AxisMoveStepMM);
        }

        /// <summary>
        /// Jog+命令
        /// </summary>
        /// <param name="axisType"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task OnAxisJogPlusCommand(AxisType axisType)
        {
            await ControlCard.MoveRelativeAsync(axisType,SystemConfigManager.SoftwareConfig.AxisMoveStepMM);
        }
        /// <summary>
        /// 绝对移动
        /// </summary>
        /// <param name="textBox"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task OnMoveAbsoluteCommand(System.Windows.Controls.TextBox textBox)
        {
            if (textBox != null && !string.IsNullOrEmpty(textBox.Text))
            {
                var axisType = (AxisType)textBox.Tag;
                var result = float.TryParse(textBox.Text,out float instance);
                if(result)
                {
                    await ControlCard.MoveAbsoluteAsync(axisType,instance);
                }
            }
        }
    }
}
