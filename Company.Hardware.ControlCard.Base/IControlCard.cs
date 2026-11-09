using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Company.Hardware.ControlCard.Base
{
    public interface IControlCard
    {
         bool Initialized { get; }
         bool Connected { get; }

        string Message { get; }
        IObservable<string> MessageObservable { get; }

        bool Init(ControlCardConfig controlCardConfig);
        void Close();

        IOEntity IOEntity { get; }

        float XAxisPosition { get; set; }
        float YAxisPosition { get; set; }
        float ZAxisPosition { get; set; }
        float LeftAxisPosition { get; set; }
        float RightAxisPosition { get; set; }
        float MainAxisPosition { get; set; }
        float KinfeAxisPosition { get; set; }
        float TrackAxisPosition { get; set; }
        float StyletAxisPosition { get; set; }

        int LeftAxisEnable {  get; set; }
        int RightAxisEnable { get; set; }

        void UpdateAxis(AxisType axis, float position, bool absolute = false);
        /// <summary>
        /// 回零点
        /// </summary>
        /// <param name="axisType"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        bool MoveToZero(AxisType axisType, CancellationToken? cancellationToken = null);

        Task<bool> MoveToZeroAsync(AxisType axisType, CancellationToken? cancellationToken = null);
        bool MoveAbsolute(AxisType axisType,float distance, CancellationToken ? cancellationToken = null);
        Task<bool> MoveAbsoluteAsync(AxisType axisType, float distance, CancellationToken? cancellationToken = null);

        bool MoveRelative(AxisType axisType, float distance, CancellationToken? cancellationToken = null);
        Task<bool> MoveRelativeAsync(AxisType axisType, float distance, CancellationToken? cancellationToken = null);
        /// <summary>
        /// 轴异步连续运动，需要传轴的运动方向
        /// </summary>
        /// <param name="axisType"></param>
        /// <param name=""></param>
        /// <returns></returns>
        Task<bool> MoveContinueAsync(AxisType axisType, MoveDirection direction);
        /// <summary>
        /// 轴停止
        /// </summary>
        /// <param name="axisType"></param>
        /// <param name="stopMode"></param>
        /// <returns></returns>
        bool StopAxis(AxisType axisType,StopMode stopMode = StopMode.取消当前运动);
        /// <summary>
        /// 轴停止(异步)
        /// </summary>
        /// <param name="axisType"></param>
        /// <param name="stopMode"></param>
        /// <returns></returns>
        Task<bool> StopAxisAsync(AxisType axisType, StopMode stopMode = StopMode.取消当前运动);
        /// <summary>
        /// 停止所有轴
        /// </summary>
        /// <param name="stopMode"></param>
        /// <returns></returns>
        bool StopAxis(StopMode stopMode = StopMode.立即中断脉冲发送);
        /// <summary>
        /// 轴使能
        /// </summary>
        /// <param name="axisType"></param>
        /// <returns></returns>
        bool AxisEnable(AxisType axisType);

        bool AxisEnable(AxisType axisType,bool isEnable);
        bool AxisResume(AxisType axisType);
        /// <summary>
        /// 轴暂停
        /// </summary>
        /// <param name="axisType"></param>
        /// <returns></returns>
        bool AxisPause(AxisType axisType,int mode);
        /// <summary>
        /// 获取轴位置
        /// </summary>
        /// <param name="axisType"></param>
        /// <param name="position"></param>
        /// <param name="runState"></param>
        /// <returns></returns>
        bool GetAxisPosition(AxisType axisType, ref float position, ref int runState);
        /// <summary>
        /// 获取多个输出点
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool GetMultiOutput(ushort start,ushort end,ref uint[] value);
        /// <summary>
        /// 获取多个输入点
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool GetMultiInput(int start, int end, ref int[] value);
        /// <summary>
        /// 获取单个输入点
        /// </summary>
        /// <param name="iOType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool GetSingleInput(IOType iOType, ref uint value);

        bool SetSingleOutput(IOType iOType, uint value);
        /// <summary>
        /// 设置轴速度
        /// </summary>
        /// <param name="axisType"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        bool SetSpeed(AxisType axisType, float speed);
        /// <summary>
        /// 设置轴当量
        /// </summary>
        /// <param name="axisType"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        bool SetUnit(AxisType axisType, float unit);

    }
}
