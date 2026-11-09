using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Company.Hardware.ControlCard.Base
{
    public abstract class ControlCardbase :ReactiveObject, IControlCard
    {
        protected ControlCardbase()
        {
            MessageObservable = this.WhenAnyValue(p => p.Message);
        }
        [Reactive]
        public bool Initialized { get; set; }
        [Reactive]
        public bool Connected { get; set; }
        [Reactive]
        public string Message { get; set; }
        [Reactive]
        public IObservable<string> MessageObservable { get; set; }
        [Reactive]
        public IOEntity IOEntity { get; set; } = new IOEntity();
        [Reactive]
        public float XAxisPosition { get; set; }
        [Reactive]
        public float YAxisPosition { get; set; }
        [Reactive]
        public float ZAxisPosition { get; set; }
        [Reactive]
        public float LeftAxisPosition { get; set; }
        [Reactive]
        public float RightAxisPosition { get; set; }
        [Reactive]
        public float MainAxisPosition { get; set; }
        [Reactive]
        public float KinfeAxisPosition { get; set; }
        [Reactive]
        public float TrackAxisPosition { get; set; }
        [Reactive]
        public float StyletAxisPosition { get; set; }
        [Reactive]
        public int LeftAxisEnable { get; set; }
        [Reactive]
        public int RightAxisEnable { get; set; }

        /// <summary>
        /// 更新轴位置,传了一个是否为绝对运动的参数，默认为相对运动
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="position"></param>
        public void UpdateAxis(AxisType axis, float position, bool absolute = false)
        {
            switch (axis)
            {
                case AxisType.X轴:
                    if (absolute)
                        XAxisPosition = position;
                    else
                        XAxisPosition += position;
                    break;
                case AxisType.Y轴:
                    if (absolute)
                        YAxisPosition = position;
                    else
                        YAxisPosition += position;
                    break;
                case AxisType.Z轴:
                    if (absolute)
                        ZAxisPosition = position;
                    else
                        ZAxisPosition += position;
                    break;
                case AxisType.左棘轮轴:
                    if (absolute)
                        LeftAxisPosition = position;
                    else
                        LeftAxisPosition += position;
                    break;
                case AxisType.右棘轮轴:
                    if (absolute)
                        RightAxisPosition = position;
                    else
                        RightAxisPosition += position;
                    break;
                case AxisType.剥刀轴:
                    if (absolute)
                        KinfeAxisPosition = position;
                    else
                        KinfeAxisPosition += position;
                    break;
                case AxisType.主轴:
                    if (absolute)
                        MainAxisPosition = position;
                    else
                        MainAxisPosition += position;
                    break;
                case AxisType.轨道轴:
                    if (absolute)
                        TrackAxisPosition = position;
                    else
                        TrackAxisPosition += position;
                    break;
                case AxisType.探针轴:
                    if (absolute)
                        StyletAxisPosition = position;
                    else
                        StyletAxisPosition += position;
                    break;
                default:
                    break;
            }
        }


        public abstract bool AxisEnable(AxisType axisType);

        public abstract bool AxisEnable(AxisType axisType, bool isEnable);
       
        public abstract bool AxisPause(AxisType axisType, int mode);
        
        public abstract bool AxisResume(AxisType axisType);
        
        public abstract void Close();
        

        public abstract bool GetAxisPosition(AxisType axisType, ref float position, ref int runState);
        

        public abstract bool GetMultiInput(int start, int end, ref int[] value);
       

        public abstract bool GetMultiOutput(ushort start, ushort end, ref uint[] value);
       
        public abstract bool GetSingleInput(IOType iOType, ref uint value);
       

        public abstract bool Init(ControlCardConfig controlCardConfig);
       
        public abstract bool MoveAbsolute(AxisType axisType, float distance, CancellationToken? cancellationToken = null);
       

        public abstract Task<bool> MoveAbsoluteAsync(AxisType axisType, float distance, CancellationToken? cancellationToken = null);
        
        public abstract Task<bool> MoveContinueAsync(AxisType axisType, MoveDirection direction);
       
        public abstract bool MoveRelative(AxisType axisType, float distance, CancellationToken? cancellationToken = null);
        

        public abstract Task<bool> MoveRelativeAsync(AxisType axisType, float distance, CancellationToken? cancellationToken = null);
      
        public abstract bool MoveToZero(AxisType axisType, CancellationToken? cancellationToken = null);
       

        public abstract Task<bool> MoveToZeroAsync(AxisType axisType, CancellationToken? cancellationToken = null);
        
        public abstract bool SetSingleOutput(IOType iOType, uint value);
       
        public abstract bool SetSpeed(AxisType axisType, float speed);
        
        public abstract bool SetUnit(AxisType axisType, float unit);
       
        public abstract bool StopAxis(AxisType axisType, StopMode stopMode = StopMode.取消当前运动);


        public abstract bool StopAxis(StopMode stopMode = StopMode.立即中断脉冲发送);


        public abstract Task<bool> StopAxisAsync(AxisType axisType, StopMode stopMode = StopMode.取消当前运动);
      
    }
}
