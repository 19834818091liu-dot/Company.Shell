using Company.Hardware.ControlCard.Base;
using Company.Logger;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Company.Hardware.ControlCard.Emulation
{
    public class ControlCardEmulation : ControlCardbase
    {
        public override bool AxisEnable(AxisType axisType)
        {
            return true;
        }

        public override bool AxisEnable(AxisType axisType, bool isEnable)
        {
            return true;
        }

        public override bool AxisPause(AxisType axisType, int mode)
        {
            return true;
        }

        public override bool AxisResume(AxisType axisType)
        {
            return true;
        }

        public override void Close()
        {
            logs.LogInfo("关闭仿真控制卡");
        }

        public override bool GetAxisPosition(AxisType axisType, ref float position, ref int runState)
        {
            return true;
        }

        public override bool GetMultiInput(int start, int end, ref int[] value)
        {
            return true;
        }

        public override bool GetMultiOutput(ushort start, ushort end, ref uint[] value)
        {
                return true ;
        }

        public override bool GetSingleInput(IOType iOType, ref uint value)
        {
            return true;
        }

        public override bool Init(ControlCardConfig controlCardConfig)
        {
            return true;
        }

        public override bool MoveAbsolute(AxisType axisType, float distance, CancellationToken? cancellationToken = null)
        {
            int timeout = Math.Abs((int)(distance*1000));
            Task.Delay(timeout).Wait();
            UpdateAxis(axisType,distance,true);
            return true;
        }

        public async override Task<bool> MoveAbsoluteAsync(AxisType axisType, float distance, CancellationToken? cancellationToken = null)
        {
            int timeout = Math.Abs((int)(distance * 100));
            Stopwatch stopwatch = Stopwatch.StartNew();
            //没有超时并且没有触发取消，就一直等待，手动点击按钮就相当于触发了取消
            while (stopwatch.ElapsedMilliseconds < timeout && !(cancellationToken?.IsCancellationRequested ?? false))
            {
                await Task.Delay(1);
            }
            UpdateAxis(axisType, distance, true);
            return true;
        }

        public override async Task<bool> MoveContinueAsync(AxisType axisType, MoveDirection direction)
        {
            await Task.Delay(5000);
            return true;
        }

        public override bool MoveRelative(AxisType axisType, float distance, CancellationToken? cancellationToken = null)
        {
            int timeout = Math.Abs((int)(distance * 1000));
            Task.Delay(timeout).Wait();
            UpdateAxis(axisType, distance);
            return true;
        }

        public override async Task<bool> MoveRelativeAsync(AxisType axisType, float distance, CancellationToken? cancellationToken = null)
        {
            int timeout = Math.Abs((int)(distance * 1000));
            await Task.Delay(timeout);
            UpdateAxis(axisType, distance);
            return true;
        }

        public override bool MoveToZero(AxisType axisType, CancellationToken? cancellationToken = null)
        {
            ToZeroPosition(axisType);
            return true;
        }

        public override async Task<bool> MoveToZeroAsync(AxisType axisType, CancellationToken? cancellationToken = null)
        {
            int timeout = 500;
            Task.Delay(timeout).Wait();
            ToZeroPosition(axisType);
            return true;
        }

        public override bool SetSingleOutput(IOType iOType, uint value)
        {
            return true;
        }

        public override bool SetSpeed(AxisType axisType, float speed)
        {
            return true;
        }

        public override bool SetUnit(AxisType axisType, float unit)
        {
            return true;
        }

        public override bool StopAxis(AxisType axisType, StopMode stopMode = StopMode.取消当前运动)
        {
            return true;
        }

        public override bool StopAxis(StopMode stopMode = StopMode.立即中断脉冲发送)
        {
            return true;
        }

        public override async Task<bool> StopAxisAsync(AxisType axisType, StopMode stopMode = StopMode.取消当前运动)
        {
            int tiemout = 500;
            await Task.Delay(tiemout);
            return true;
        }
        private void ToZeroPosition(AxisType axisType)
        {
            var position = 0;
            switch (axisType)
            {
                case AxisType.X轴:
                    XAxisPosition = position;
                    break;
                case AxisType.Y轴:
                    YAxisPosition = position;
                    break;
                case AxisType.Z轴:
                    ZAxisPosition = position;
                    break;
                case AxisType.左棘轮轴:
                    LeftAxisPosition = position;
                    break;
                case AxisType.右棘轮轴:
                    RightAxisPosition = position;
                    break;
                case AxisType.主轴:
                    MainAxisPosition = position;
                    break;
                case AxisType.剥刀轴:
                    KinfeAxisPosition = position;
                    break;
                case AxisType.轨道轴:
                    TrackAxisPosition = position;
                    break;
                case AxisType.探针轴:
                    StyletAxisPosition= position;
                    break;
                default:
                    break;
            }
        }
    }
}
