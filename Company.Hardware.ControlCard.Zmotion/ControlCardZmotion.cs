using Company.Hardware.ControlCard.Base;
using Company.Logger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Company.Hardware.ControlCard.Zmotion
{
    public class ControlCardZmotion : ControlCardbase
    {
        private ControlCardConfig config;
        private IntPtr phandle = IntPtr.Zero;
        private CancellationTokenSource cts = new CancellationTokenSource();
        /// <summary>
        /// 设置轴使能状态,设置的是单个输出点
        /// 不同的轴使用的轴使能状态不同
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public override bool AxisEnable(AxisType axisType)
        {
            IsInitialized();
            bool result = false;
            switch (axisType)
            {
                case AxisType.X轴:
                    result &= SetSingleOutput(IOType.IO0, uint.MaxValue);
                    break;
                case AxisType.Y轴:
                    result &= SetSingleOutput(IOType.IO1, uint.MaxValue);
                    break;
                case AxisType.Z轴:
                    result &= SetSingleOutput(IOType.IO2, uint.MaxValue);
                    break;
                case AxisType.左棘轮轴:
                    result &= SetSingleOutput(IOType.IO3, uint.MaxValue);
                    break;
                case AxisType.右棘轮轴:
                    result &= SetSingleOutput(IOType.IO4, uint.MaxValue);
                    break;
                case AxisType.剥刀轴:
                    result &= SetSingleOutput(IOType.IO5, uint.MaxValue);
                    break;
                case AxisType.主轴:
                    result &= SetSingleOutput(IOType.IO6, uint.MaxValue);
                    break;
                case AxisType.轨道轴:
                    result &= SetSingleOutput(IOType.IO7, uint.MaxValue);
                    break;
                case AxisType.探针轴:
                    result &= SetSingleOutput(IOType.IO8, uint.MaxValue);
                    break;
                default:
                    break;
            }
            return result;
        }
        /// <summary>
        /// 设置轴使能状态
        /// </summary>
        /// <param name="axisType"></param>
        /// <param name="isEnable"></param>
        /// <returns></returns>
        public override bool AxisEnable(AxisType axisType, bool isEnable)
        {
            IsInitialized();
            var status = isEnable ? 1 : 0;          //true则为1，false则为0
            int result = zmcaux.ZAux_Direct_SetAxisEnable(phandle, (int)axisType, status);     //设置总线轴使能
            if (result != 0)
            {
                logs.LogError($"{this.GetType().FullName}设置{axisType.ToString()}失败，错误码：{result}");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 单轴暂停
        /// </summary>
        /// <param name="axisType"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public override bool AxisPause(AxisType axisType, int mode)
        {
            IsInitialized();

            var result = zmcaux.ZAux_Direct_MovePause(phandle, (int)axisType, mode);
            if (result != 0)
            {
                logs.LogError($"{this.GetType().FullName}{axisType.ToString()}暂停失败，错误码：{result}");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 单轴恢复
        /// </summary>
        /// <param name="axisType"></param>
        /// <returns></returns>
        public override bool AxisResume(AxisType axisType)
        {
            IsInitialized();
            var result = zmcaux.ZAux_Direct_MoveResume(phandle, (int)axisType);
            if (result != 0)
            {
                logs.LogError($"{this.GetType().FullName}{axisType.ToString()}恢复失败，错误码：{result}");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 关闭控制卡
        /// </summary>
        public override void Close()
        {
            IsInitialized();
            cts.Cancel();
            StopAxis();                 //急停
            SetSingleOutput(IOType.IO3,1);   // //单独将左棘轮轴和右棘轮轴使能状态设置为1，也就是说硬件接法规定了1为去使能
            SetSingleOutput(IOType.IO4, 1);
            Task.Delay(100).ContinueWith(t =>
            {
                var result = zmcaux.ZAux_Close(phandle);
                if(result != 0)
                {
                    logs.LogError($"{this.GetType().FullName}关闭失败，错误码为{result}");
                }
            });
        }
        /// <summary>
        /// 获取轴位置
        /// </summary>
        /// <param name="axisType"></param>
        /// <param name="position"></param>
        /// <param name="runState">0表示运行中，-1表示停止</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool GetAxisPosition(AxisType axisType, ref float position, ref int runState)
        {
            var result = zmcaux.ZAux_Direct_GetDpos(phandle, (int)axisType, ref position);
            if (result != 0)
            {
                logs.LogError($"{this.GetType().FullName}获取{axisType}位置失败，错误码为{result}");
                return false;
            }
            result = zmcaux.ZAux_Direct_GetIfIdle(phandle, (int)axisType, ref runState);
            if (result != 0)
            {

                logs.LogError($"{this.GetType().FullName}获取{axisType}运行状态失败，错误码为{result}");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 获取控制卡IN点参数
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool GetMultiInput(int start, int end, ref int[] value)
        {
            IsInitialized();
            var result = zmcaux.ZAux_Direct_GetInMulti(phandle,start,end,value);
            if(result != 0)
            {
                logs.LogError($"{this.GetType().FullName}获取IN点失败,错误码为{result}");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 获取控制卡IO点参数
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool GetMultiOutput(ushort start, ushort end, ref uint[] value)
        {
            IsInitialized();
            var result = zmcaux.ZAux_Direct_GetOutMulti(phandle, start, end, value);
            if (result != 0)
            {
                logs.LogError($"{this.GetType().FullName}获取IO点失败,错误码为{result}");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 获取单个输入点
        /// </summary>
        /// <param name="iOType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool GetSingleInput(IOType iOType, ref uint value)
        {
            IsInitialized();
            var result = zmcaux.ZAux_Direct_GetIn(phandle, (int)iOType, ref value);
            if (result != 0)
            {
                logs.LogError($"{this.GetType().FullName}获取IN点失败，错误码：{result}");
                return false;
            }
            return true;

        }
        /// <summary>
        /// 初始化控制卡
        /// </summary>
        /// <param name="controlCardConfig"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool Init(ControlCardConfig controlCardConfig)
        {
            config = controlCardConfig;
            if(config == null)
            {
                return  false;
            }
            if(Initialized)
            {
                return true;
            }
            int error = zmcaux.ZAux_OpenEth(config.IpAddress,out phandle);
            if(error == 0 && phandle != IntPtr.Zero)
            {
                logs.LogInfo($"初始化{this.GetType().FullName},ip={config.IpAddress}成功");
                Initialized = true;
                Connected = true;
            }
            else
            {
                logs.LogError($"初始化{this.GetType().FullName},ip={config.IpAddress}失败,错误码为{error}");
                return false;
            }
            if(Connected)
            {
                bool result =  Setting();
                if(result)
                {
                    Read();       //读取控制卡参数
                }
                return false;
            }
            return true;
        }
        /// <summary>
        /// 设置控制卡参数
        /// </summary>
        /// <returns></returns>
        private bool Setting()
        { 
            int result = 0;
            var axises = Enum.GetValues(typeof(AxisType)).Cast<AxisType>();       //枚举所有轴类型
            //设置轴类型
            foreach(AxisType axisType in axises)
            {
                if(config.ControlCardParameter.Types.TryGetValue(axisType,out int axistype))
                {
                    result = zmcaux.ZAux_Direct_SetAtype(phandle, (int)axisType,axistype);
                    if(result != 0)
                    {
                        logs.LogError($"{this.GetType().FullName}{axisType}设置轴类型失败,错误码为{result}");
                        return false ;
                    }
                }
            }
           
            //设置轴脉冲当量
            foreach (AxisType axisType in axises)
            {
                if (config.ControlCardParameter.Units.TryGetValue(axisType, out float pulse))
                {
                    result = zmcaux.ZAux_Direct_SetUnits(phandle, (int)axisType, pulse);
                    if (result != 0)
                    {
                        logs.LogError($"{this.GetType().FullName}{axisType}设置轴脉冲当量失败,错误码为{result}");
                        return false;
                    }
                }
            }
            //设置轴最小速度
            foreach (AxisType axisType in axises)
            {
                if (config.ControlCardParameter.MinSpeeds.TryGetValue(axisType, out float speed))
                {
                    result = zmcaux.ZAux_Direct_SetLspeed(phandle, (int)axisType, speed);
                    if (result != 0)
                    {
                        logs.LogError($"{this.GetType().FullName}{axisType}设置轴最小速度失败,错误码为{result}");
                        return false;
                    }
                }
            }
            //设置轴加速度
            foreach (AxisType axisType in axises)
            {
                if (config.ControlCardParameter.AccSpeeds.TryGetValue(axisType, out float speed))
                {
                    result = zmcaux.ZAux_Direct_SetAccel(phandle, (int)axisType, speed);
                    if (result != 0)
                    {
                        logs.LogError($"{this.GetType().FullName}{axisType}设置轴加速度失败,错误码为{result}");
                        return false;
                    }
                }
            }
            //设置轴减速度
            foreach (AxisType axisType in axises)
            {
                if (config.ControlCardParameter.DecSpeeds.TryGetValue(axisType, out float speed))
                {
                    result = zmcaux.ZAux_Direct_SetDecel(phandle, (int)axisType, speed);
                    if (result != 0)
                    {
                        logs.LogError($"{this.GetType().FullName}{axisType}设置轴减速度失败,错误码为{result}");
                        return false;
                    }
                }
            }
            //设置轴运行速度
            foreach (AxisType axisType in axises)
            {
                if (config.ControlCardParameter.RunSpeeds.TryGetValue(axisType, out float speed))
                {
                    result = zmcaux.ZAux_Direct_SetSpeed(phandle, (int)axisType, speed);
                    if (result != 0)
                    {
                        logs.LogError($"{this.GetType().FullName}{axisType}设置轴运行速度失败,错误码为{result}");
                        return false;
                    }
                }
            }
            //设置轴回零爬行速度
            foreach (AxisType axisType in axises)
            {
                if (config.ControlCardParameter.CreepSpeeds.TryGetValue(axisType, out float speed))
                {
                    result = zmcaux.ZAux_Direct_SetCreep(phandle, (int)axisType, speed);
                    if (result != 0)
                    {
                        logs.LogError($"{this.GetType().FullName}{axisType}设置轴回零爬行速度失败,错误码为{result}");
                        return false;
                    }
                }
            }
            //设置轴零点IO
            foreach (AxisType axisType in axises)
            {
                if (config.ControlCardParameter.ZeroIO.TryGetValue(axisType, out int  value))
                {
                    result = zmcaux.ZAux_Direct_SetDatumIn(phandle, (int)axisType, value);
                    if (result != 0)
                    {
                        logs.LogError($"{this.GetType().FullName}{axisType}设置轴零点IO失败,错误码为{result}");
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 读取控制卡参数
        /// </summary>
        private void Read()
        {
            int status = 0;
            var result = zmcaux.ZAux_Direct_GetAxisEnable(phandle, (int)AxisType.左棘轮轴, ref status);
            if(result != 0)
            {

                logs.LogError($"{this.GetType().FullName}{AxisType.左棘轮轴}读使能状态失败,错误码为{result}");
            }
            else
            {
                LeftAxisEnable = status;
            }
            result = zmcaux.ZAux_Direct_GetAxisEnable(phandle, (int)AxisType.右棘轮轴, ref status);
            if (result != 0)
            {

                logs.LogError($"{this.GetType().FullName}{AxisType.右棘轮轴}读使能状态失败,错误码为{result}");
            }
            else
            {
                RightAxisEnable = status;
            }
            //创建一个长时间运行任务
            Task.Factory.StartNew(() =>
            {
                var inputs = new int[1];
                var outputs = new uint[1];
                //循环1ms去轮询查找状态
                while(!cts.Token.IsCancellationRequested)
                {
                    Thread.Sleep(1);
                    GetMultiInput(0, 31, ref inputs);
                    GetMultiOutput(0, 11, ref outputs);

                    //更新IN点状态
                    IOEntity.IN0 = (inputs.FirstOrDefault() & (uint)IOType.IO0) == (uint)IOType.IO0;          //这里的意思就是获取到的第一个输入点值和IOType中IO0做与运算。位图 & 掩码 == 掩码就是 "测试这一位是否为 1"。
                    IOEntity.IN1 = (inputs.FirstOrDefault() & (uint)IOType.IO1) == (uint)IOType.IO1;
                    IOEntity.IN2 = (inputs.FirstOrDefault() & (uint)IOType.IO2) == (uint)IOType.IO2;
                    IOEntity.IN3 = (inputs.FirstOrDefault() & (uint)IOType.IO3) == (uint)IOType.IO3;
                    IOEntity.IN4 = (inputs.FirstOrDefault() & (uint)IOType.IO4) == (uint)IOType.IO4;
                    IOEntity.IN5 = (inputs.FirstOrDefault() & (uint)IOType.IO5) == (uint)IOType.IO5;
                    IOEntity.IN6 = (inputs.FirstOrDefault() & (uint)IOType.IO6) == (uint)IOType.IO6;
                    IOEntity.IN7 = (inputs.FirstOrDefault() & (uint)IOType.IO7) == (uint)IOType.IO7;
                    IOEntity.IN8 = (inputs.FirstOrDefault() & (uint)IOType.IO8) == (uint)IOType.IO8;
                    IOEntity.IN9 = (inputs.FirstOrDefault() & (uint)IOType.IO9) == (uint)IOType.IO9;
                    IOEntity.IN10 = (inputs.FirstOrDefault() & (uint)IOType.IO10) == (uint)IOType.IO10;
                    IOEntity.IN11 = (inputs.FirstOrDefault() & (uint)IOType.IO11) == (uint)IOType.IO11;
                    IOEntity.IN12 = (inputs.FirstOrDefault() & (uint)IOType.IO12) == (uint)IOType.IO12;
                    IOEntity.IN13 = (inputs.FirstOrDefault() & (uint)IOType.IO13) == (uint)IOType.IO13;
                    IOEntity.IN14 = (inputs.FirstOrDefault() & (uint)IOType.IO14) == (uint)IOType.IO14;
                    IOEntity.IN15 = (inputs.FirstOrDefault() & (uint)IOType.IO15) == (uint)IOType.IO15;
                    IOEntity.IN16 = (inputs.FirstOrDefault() & (uint)IOType.IO16) == (uint)IOType.IO16;
                    IOEntity.IN17 = (inputs.FirstOrDefault() & (uint)IOType.IO17) == (uint)IOType.IO17;
                    IOEntity.IN18 = (inputs.FirstOrDefault() & (uint)IOType.IO18) == (uint)IOType.IO18;
                    IOEntity.IN19 = (inputs.FirstOrDefault() & (uint)IOType.IO19) == (uint)IOType.IO19;
                    IOEntity.IN20 = (inputs.FirstOrDefault() & (uint)IOType.IO20) == (uint)IOType.IO20;
                    IOEntity.IN21 = (inputs.FirstOrDefault() & (uint)IOType.IO21) == (uint)IOType.IO21;
                    IOEntity.IN22 = (inputs.FirstOrDefault() & (uint)IOType.IO22) == (uint)IOType.IO22;
                    IOEntity.IN23 = (inputs.FirstOrDefault() & (uint)IOType.IO23) == (uint)IOType.IO23;

                    //更新OUT点状态
                    IOEntity.OUT0 = (outputs.FirstOrDefault() & (uint)IOType.IO0) == (uint)IOType.IO0;
                    IOEntity.OUT1 = (outputs.FirstOrDefault() & (uint)IOType.IO1) == (uint)IOType.IO1;
                    IOEntity.OUT2 = (outputs.FirstOrDefault() & (uint)IOType.IO2) == (uint)IOType.IO2;
                    IOEntity.OUT3 = (outputs.FirstOrDefault() & (uint)IOType.IO3) == (uint)IOType.IO3;
                    IOEntity.OUT4 = (outputs.FirstOrDefault() & (uint)IOType.IO4) == (uint)IOType.IO4;
                    IOEntity.OUT5 = (outputs.FirstOrDefault() & (uint)IOType.IO5) == (uint)IOType.IO5;
                    IOEntity.OUT6 = (outputs.FirstOrDefault() & (uint)IOType.IO6) == (uint)IOType.IO6;
                    IOEntity.OUT7 = (outputs.FirstOrDefault() & (uint)IOType.IO7) == (uint)IOType.IO7;
                    IOEntity.OUT8 = (outputs.FirstOrDefault() & (uint)IOType.IO8) == (uint)IOType.IO8;
                    IOEntity.OUT9 = (outputs.FirstOrDefault() & (uint)IOType.IO9) == (uint)IOType.IO9;
                    IOEntity.OUT10 = (outputs.FirstOrDefault() & (uint)IOType.IO10) == (uint)IOType.IO10;
                    IOEntity.OUT11 = (outputs.FirstOrDefault() & (uint)IOType.IO11) == (uint)IOType.IO11;
                    IOEntity.OUT12 = (outputs.FirstOrDefault() & (uint)IOType.IO12) == (uint)IOType.IO12;
                    IOEntity.OUT13 = (outputs.FirstOrDefault() & (uint)IOType.IO13) == (uint)IOType.IO13;
                    IOEntity.OUT14 = (outputs.FirstOrDefault() & (uint)IOType.IO14) == (uint)IOType.IO14;
                    IOEntity.OUT15 = (outputs.FirstOrDefault() & (uint)IOType.IO15) == (uint)IOType.IO15;
                    IOEntity.OUT16 = (outputs.FirstOrDefault() & (uint)IOType.IO16) == (uint)IOType.IO16;
                    IOEntity.OUT17 = (outputs.FirstOrDefault() & (uint)IOType.IO17) == (uint)IOType.IO17;
                    IOEntity.OUT18 = (outputs.FirstOrDefault() & (uint)IOType.IO18) == (uint)IOType.IO18;
                    IOEntity.OUT19 = (outputs.FirstOrDefault() & (uint)IOType.IO19) == (uint)IOType.IO19;
                    IOEntity.OUT20 = (outputs.FirstOrDefault() & (uint)IOType.IO20) == (uint)IOType.IO20;
                    IOEntity.OUT21 = (outputs.FirstOrDefault() & (uint)IOType.IO21) == (uint)IOType.IO21;
                    IOEntity.OUT22 = (outputs.FirstOrDefault() & (uint)IOType.IO22) == (uint)IOType.IO22;
                    IOEntity.OUT23 = (outputs.FirstOrDefault() & (uint)IOType.IO23) == (uint)IOType.IO23;
                    //如果IN2为真，那就触发急停，停止所有轴
                    if (IOEntity.IN2)
                    {
                        StopAxis();
                    }
                }

            },cts.Token, TaskCreationOptions.LongRunning,TaskScheduler.Default);

        }
        #region 轴运动
        /// <summary>
        /// 单轴绝对运动
        /// </summary>
        /// <param name="axisType"></param>
        /// <param name="distance"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override bool MoveAbsolute(AxisType axisType, float distance, CancellationToken? cancellationToken = null)
        {
            IsInitialized();
            var success = config.ControlCardParameter.AxisSoftLimit.TryGetValue(axisType, out var limit);
           if (success)
            {
                //如果运动距离大于软限位 并且软限位不为0(不为0说明不是从回零位置开始的)
                if (distance >= limit && limit != 0)
                {
                    logs.LogError($"{this.GetType().FullName}{axisType}移动距离{distance}大于{limit}");
                    return false;
                }
                else
                {

                    var result = zmcaux.ZAux_Direct_Single_MoveAbs(phandle, (int)axisType, distance);
                    if (result == 0)
                    {
                        float position = 0;
                        int runState = 0;
                        //如果不为null，则说明支持取消命令
                        if (cancellationToken.HasValue)
                        {
                            //当正在运行中并且没有收到取消命令
                            while (runState == 0 && !cancellationToken.Value.IsCancellationRequested)
                            {
                                Thread.Sleep(1);
                                success = GetAxisPosition(axisType, ref position, ref runState);
                                if (success)
                                {
                                    UpDateAxisPosition(axisType, position);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            //如果收到取消命令
                            if (cancellationToken.Value.IsCancellationRequested)
                            {
                                StopAxis(axisType, StopMode.立即中断脉冲发送);    //这是单轴停止运动
                            }
                        }
                        //如果为null，则说明不支持取消命令
                        else
                        {
                            //当正在运行中
                            while (runState == 0)
                            {
                                Thread.Sleep(1);
                                success = GetAxisPosition(axisType, ref position, ref runState);
                                if (success)
                                {
                                    UpDateAxisPosition(axisType, position);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        return true;
                    }
                    else
                    {
                        logs.LogError($"{this.GetType().FullName}{axisType}绝对移动失败，错误码：{result}");
                        return false;
                    }
                }
            }
           return false;
        }

        public override Task<bool> MoveAbsoluteAsync(AxisType axisType, float distance, CancellationToken? cancellationToken = null)
        {
            var t = Task.Run(() =>
            {
                return MoveAbsolute(axisType, distance, cancellationToken);
            });
            return t;
        }
        /// <summary>
        /// 单轴连续运动
        /// </summary>
        /// <param name="axisType"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public override  Task<bool> MoveContinueAsync(AxisType axisType, MoveDirection direction)
        {
            Task<bool> t = Task.Factory.StartNew(function: () =>
            {
                IsInitialized();
                var result = zmcaux.ZAux_Direct_Single_Vmove(phandle, (int)axisType, (int)direction);
                if(result != 0)
                {
                    logs.LogError($"{this.GetType().FullName}{axisType}连续移动失败，错误码：{result}");
                    return false;
                }
                float position = 0;
                int runState = 0;
                while (runState == 0)
                {
                    Thread.Sleep(1);
                    var success = GetAxisPosition(axisType, ref position, ref runState);
                    if (success)
                    {
                        UpDateAxisPosition(axisType, position);
                    }
                    else
                    {
                        break;
                    }
                }
                return true;
            });
            return t;
        }
        /// <summary>
        /// 轴相对运动
        /// </summary>
        /// <param name="axisType"></param>
        /// <param name="distance"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool MoveRelative(AxisType axisType, float distance, CancellationToken? cancellationToken = null)
        {
            IsInitialized();
            var success = config.ControlCardParameter.AxisSoftLimit.TryGetValue(axisType, out var limit);
            if (success)
            {
                //如果运动距离大于软限位 并且软限位不为0(不为0说明不是从回零位置开始的)
                if (distance >= limit && limit != 0)
                {
                    logs.LogError($"{this.GetType().FullName}{axisType}移动距离{distance}大于{limit}");
                    return false;
                }
                else
                {

                    var result = zmcaux.ZAux_Direct_Single_Move(phandle, (int)axisType, distance);
                    if (result == 0)
                    {
                        float position = 0;
                        int runState = 0;
                        //如果不为null，则说明支持取消命令
                        if (cancellationToken.HasValue)
                        {
                            //当正在运行中并且没有收到取消命令
                            while (runState == 0 && !cancellationToken.Value.IsCancellationRequested)
                            {
                                Thread.Sleep(1);
                                success = GetAxisPosition(axisType, ref position, ref runState);
                                if (success)
                                {
                                    UpDateAxisPosition(axisType, position);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            //如果收到取消命令
                            if (cancellationToken.Value.IsCancellationRequested)
                            {
                                StopAxis(axisType, StopMode.立即中断脉冲发送);    //这是单轴停止运动
                            }
                        }
                        //如果为null，则说明不支持取消命令
                        else
                        {
                            //当正在运行中
                            while (runState == 0)
                            {
                                Thread.Sleep(1);
                                success = GetAxisPosition(axisType, ref position, ref runState);
                                if (success)
                                {
                                    UpDateAxisPosition(axisType, position);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        return true;
                    }
                    else
                    {
                        logs.LogError($"{this.GetType().FullName}{axisType}相对移动失败，错误码：{result}");
                        return false;
                    }
                }
            }
            return false;
        }

        public override Task<bool> MoveRelativeAsync(AxisType axisType, float distance, CancellationToken? cancellationToken = null)
        {
            var t = Task.Run(() =>
            {
                return MoveRelative(axisType, distance, cancellationToken);
            }); return t;
        }
        /// <summary>
        /// 回零运动
        /// </summary>
        /// <param name="axisType"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool MoveToZero(AxisType axisType, CancellationToken? cancellationToken = null)
        {
            IsInitialized();
            var success = config.ControlCardParameter.ToZeroSpeeds.TryGetValue(axisType, out float speed);   //查询回零速度
            success &= config.ControlCardParameter.ZeroMode.TryGetValue(axisType, out float mode);               //查询回零模式
            if(success)
            {
                SetSpeed(axisType,speed);               //设置查询到的回零速度为该轴回零速度
                var result = zmcaux.ZAux_Direct_Single_Datum(phandle,(int)axisType,(int)mode);    //控制器方式回零
                if(result != 0)
                {
                    logs.LogError($"{this.GetType().FullName}{axisType}回零失败，错误码为{result}");
                    return false;
                }
                float position = 0;
                int runState = 0;
                //如果不为null，则说明支持取消命令
                if (cancellationToken.HasValue)
                {
                    //当正在运行中并且没有收到取消命令
                    while (runState == 0 && !cancellationToken.Value.IsCancellationRequested)
                    {
                        Thread.Sleep(1);
                        success = GetAxisPosition(axisType, ref position, ref runState);
                        if (success)
                        {
                            UpDateAxisPosition(axisType, position);
                        }
                        else
                        {
                            break;
                        }
                    }
                    //如果收到取消命令
                    if (cancellationToken.Value.IsCancellationRequested)
                    {
                        StopAxis(axisType, StopMode.立即中断脉冲发送);    //这是单轴停止运动
                    }
                }
                //如果为null，则说明不支持取消命令
                else
                {
                    //当正在运行中
                    while (runState == 0)
                    {
                        Thread.Sleep(1);
                        success = GetAxisPosition(axisType, ref position, ref runState);
                        if (success)
                        {
                            UpDateAxisPosition(axisType, position);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public override Task<bool> MoveToZeroAsync(AxisType axisType, CancellationToken? cancellationToken = null)
        {
            var t = Task.Run(() =>
            {
                return MoveToZero(axisType, cancellationToken);
            });
            return t;
        }
        /// <summary>
        /// 更新所有轴的位置
        /// </summary>
        /// <param name="axisType"></param>
        /// <param name="position"></param>
        private void UpDateAxisPosition(AxisType axisType,float position)
        {
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
                    StyletAxisPosition = position;
                    break;
                default:
                    break;
            }
        }
#endregion
        /// <summary>
        /// 设置单个输出点
        /// </summary>
        /// <param name="iOType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool SetSingleOutput(IOType iOType,  uint value)
        {
            IsInitialized();
            //如果输入的值为最大值
            if (value == uint.MaxValue)
            {
                var source = uint.MaxValue;
                var result = zmcaux.ZAux_Direct_GetOp(phandle, (int)iOType, ref value);
                if (result != 0)
                {
                    logs.LogError($"读取{this.GetType().FullName}{iOType.ToString()}失败，错误码：{result}");
                    return false;
                }
                source = (uint)(source == 0 ? 1 : 0);     //为0就变为1
                result = zmcaux.ZAux_Direct_SetOp(phandle, (int)iOType, value);
                if (result != 0)
                {
                    logs.LogError($"设置{this.GetType().FullName}{iOType.ToString()}失败，错误码：{result}");
                    return false;
                }
            }
            //否则输入的值为0/1
            else
            {
                var result = zmcaux.ZAux_Direct_SetOp(phandle, iOType.GetIndex(), value);     //设置输出点
                if (result != 0)
                {
                    logs.LogError($"{this.GetType().FullName}设置{iOType.ToString()}失败，错误码：{result}");
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 设置单轴运行速度
        /// </summary>
        /// <param name="axisType"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public override bool SetSpeed(AxisType axisType, float speed)
        {
            IsInitialized();
            var result = zmcaux.ZAux_Direct_SetSpeed(phandle, (int)axisType, speed);
            if(result != 0)
            {
                logs.LogError($"{this.GetType().FullName}设置{axisType}运行速度失败,错误码为{result}");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 设置单轴脉冲当量
        /// </summary>
        /// <param name="axisType"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public override bool SetUnit(AxisType axisType, float unit)
        {
            IsInitialized();
            var result = zmcaux.ZAux_Direct_SetUnits(phandle, (int)axisType, unit);
            if (result != 0)
            {
                logs.LogError($"{this.GetType().FullName}设置{axisType}脉冲当量失败,错误码为{result}");
                return false;
            }
            return true;
        }
        #region 停轴
        /// <summary>
        /// 停止单轴
        /// </summary>
        /// <param name="axisType"></param>
        /// <param name="stopMode"></param>
        /// <returns></returns>
        public override bool StopAxis(AxisType axisType, StopMode stopMode = StopMode.取消当前运动)
        {
            var result = zmcaux.ZAux_Direct_Single_Cancel(phandle, (int)axisType, (int)stopMode);
            if (result != 0)
            {
                logs.LogError($"{this.GetType().FullName}{axisType}停止失败,错误码为{result}");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 急停，停止所有轴
        /// </summary>
        /// <param name="stopMode"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool StopAxis(StopMode stopMode = StopMode.立即中断脉冲发送)
        {
            if(Initialized)
            {
                var result = zmcaux.ZAux_Direct_Rapidstop(phandle,(int)stopMode);
                if(result != 0)
                {
                    logs.LogError($"{this.GetType().FullName}急停失败,错误码为{result}");
                    return false;
                }  
            }
            return true;
        }
        /// <summary>
        /// 停止单轴(异步)
        /// </summary>
        /// <param name="axisType"></param>
        /// <param name="stopMode"></param>
        /// <returns></returns>
        public override Task<bool> StopAxisAsync(AxisType axisType, StopMode stopMode = StopMode.取消当前运动)
        {
            Task<bool> t = Task.Run(() =>
            {
                return StopAxis(axisType, stopMode);
            });
            return t;
        }
        #endregion
        private bool IsInitialized()
        {
            if(!Initialized)
            {
                logs.LogError($"{this.GetType().FullName}初始化失败");
                return false;
            }
            return true;
        }
    }
}
