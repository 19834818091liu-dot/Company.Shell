using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Hardware.ControlCard.Base
{
    /// <summary>
    /// 控制卡参数实体
    /// </summary>
    public class ControlCardParameter
    {
        /// <summary>
        /// 轴类型,1：表示普通脉冲轴(只发脉冲+方向).脉冲方向方式的步进或伺服,4：表示脉冲轴+编码器输入。步进+编码器
        /// </summary>
        public Dictionary<AxisType, int> Types { get; set; } = new Dictionary<AxisType, int>()
        {
            {AxisType.X轴,4 },
            {AxisType.Y轴,4 },
            {AxisType.Z轴,1 },
            {AxisType.左棘轮轴,1 },
            {AxisType.右棘轮轴,1 },
            {AxisType.剥刀轴,1 },
            {AxisType.主轴,1 },
            {AxisType.轨道轴,1 },
            {AxisType.探针轴,1 },
        };

        /// <summary>
        /// 轴当量(脉冲/毫米，走1mm需要发多少脉冲，控制卡把脉冲换算成毫米)
        /// </summary>
        public Dictionary<AxisType, float> Units { get; set; } = new Dictionary<AxisType, float>()
        {
            {AxisType.X轴,1000 },
            {AxisType.Y轴,1000 },
            {AxisType.Z轴,5000 },
            {AxisType.左棘轮轴,83.3333f },
            {AxisType.右棘轮轴,83.3333f },
            {AxisType.剥刀轴,5000 },
            {AxisType.主轴,5000 },
            {AxisType.轨道轴,5000 },
            {AxisType.探针轴,5000 },
        };

        /// <summary>
        /// 轴最小速度 
        /// </summary>
        public Dictionary<AxisType, float> MinSpeeds { get; set; } = new Dictionary<AxisType, float>()
        {
            {AxisType.X轴,10 },
            {AxisType.Y轴,10 },
            {AxisType.Z轴,2 },
            {AxisType.左棘轮轴,7 },
            {AxisType.右棘轮轴,7 },
            {AxisType.剥刀轴,22 },
            {AxisType.主轴,22 },
            {AxisType.轨道轴,22 },
            {AxisType.探针轴,22 },
        };

        /// <summary>
        /// 轴加速度 
        /// </summary>
        public Dictionary<AxisType, float> AccSpeeds { get; set; } = new Dictionary<AxisType, float>()
        {
            {AxisType.X轴,20 },
            {AxisType.Y轴,20 },
            {AxisType.Z轴,4 },
            {AxisType.左棘轮轴,14 },
            {AxisType.右棘轮轴,14 },
            {AxisType.剥刀轴,44 },
            {AxisType.主轴,44 },
            {AxisType.轨道轴,44 },
            {AxisType.探针轴,44 },
        };

        /// <summary>
        /// 轴减速度 
        /// </summary>
        public Dictionary<AxisType, float> DecSpeeds { get; set; } = new Dictionary<AxisType, float>()
        {
            {AxisType.X轴,20 },
            {AxisType.Y轴,20 },
            {AxisType.Z轴,4 },
            {AxisType.左棘轮轴,14 },
            {AxisType.右棘轮轴,14 },
            {AxisType.剥刀轴,44 },
            {AxisType.主轴,44 },
            {AxisType.轨道轴,44 },
            {AxisType.探针轴,44 },
        };

        /// <summary>
        /// 轴运行速度 
        /// </summary>
        public Dictionary<AxisType, float> RunSpeeds { get; set; } = new Dictionary<AxisType, float>()
        {
            {AxisType.X轴,60 },
            {AxisType.Y轴,60 },
            {AxisType.Z轴,6 },
            {AxisType.左棘轮轴,21 },
            {AxisType.右棘轮轴,21 },
            {AxisType.剥刀轴,66 },
            {AxisType.主轴,66 },
            {AxisType.轨道轴,66 },
            {AxisType.探针轴,66 },
        };

        /// <summary>
        /// 轴回零速度 ，回零时快速接近原点
        /// </summary>
        public Dictionary<AxisType, float> ToZeroSpeeds { get; set; } = new Dictionary<AxisType, float>()
        {
            {AxisType.X轴,10 },
            {AxisType.Y轴,10 },
            {AxisType.Z轴,2 },
            {AxisType.左棘轮轴,6 },
            {AxisType.右棘轮轴,6 },
            {AxisType.剥刀轴,6 },
            {AxisType.主轴,6 },
            {AxisType.轨道轴,6 },
            {AxisType.探针轴,6 },
        };

        /// <summary>
        /// 轴爬行速度，在做对位和校准的时候就使用爬行速度来做，接近原点后慢速爬行精确找零点
        /// </summary>
        public Dictionary<AxisType, float> CreepSpeeds { get; set; } = new Dictionary<AxisType, float>()
        {
            {AxisType.X轴,10 },
            {AxisType.Y轴,10 },
            {AxisType.Z轴,2 },      //z轴速度最低，别的轴可以快一点
            {AxisType.左棘轮轴,6 },
            {AxisType.右棘轮轴,6 },
            {AxisType.剥刀轴,6 },
            {AxisType.主轴,6 },
            {AxisType.轨道轴,6 },
            {AxisType.探针轴,6 },
        };

        /// <summary>
        /// 轴零点IO(输入点编号)
        /// </summary>
        public Dictionary<AxisType, int> ZeroIO { get; set; } = new Dictionary<AxisType, int>()
        {
            {AxisType.X轴,0 },     //IN0
            {AxisType.Y轴,1 },     //IN1
            {AxisType.Z轴,2 },     //IN2
            {AxisType.左棘轮轴,3 }, //IN3
            {AxisType.右棘轮轴,4 }, //IN4
            {AxisType.剥刀轴,5 },
            {AxisType.主轴,6 },
            {AxisType.轨道轴,7 },
            {AxisType.探针轴,8 },
        };

        /// <summary>
        /// 轴回零模式：3=正向回零，4=反向回零,就是轴先往负方向找零点
        /// </summary>
        public Dictionary<AxisType, float> ZeroMode { get; set; } = new Dictionary<AxisType, float>()
        {
            {AxisType.X轴,4 },
            {AxisType.Y轴,4 },
            {AxisType.Z轴,4 },
            {AxisType.左棘轮轴,4 },
            {AxisType.右棘轮轴,4 },
            {AxisType.剥刀轴,4 },
            {AxisType.主轴,4 },
            {AxisType.轨道轴,4 },
            {AxisType.探针轴,4 },
        };

        /// <summary>
        /// 轴软限位，单位是mm
        /// 超过 150mm (X)、80mm (Y)、56mm (Z) 就报超限停止，防止撞机；
        /// 0 = 不限位**(棘轮 / 剥刀 / 主轴 / 轨道是旋转或受限轴，不需要)。
        /// 软限位是 "保险", 硬限位靠硬件限位开关 (IN 点)。
        /// </summary>
        public Dictionary<AxisType, float> AxisSoftLimit { get; set; } = new Dictionary<AxisType, float>()
        {
            {AxisType.X轴,150 },
            {AxisType.Y轴,80 },
            {AxisType.Z轴,56 },
            {AxisType.左棘轮轴,0 },
            {AxisType.右棘轮轴,0 },
            {AxisType.剥刀轴,0 },
            {AxisType.主轴,0 },
            {AxisType.轨道轴,0 },
            {AxisType.探针轴,6 },
        };
    }
}
