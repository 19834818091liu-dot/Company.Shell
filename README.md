# WPF + OpenCV 工业视觉对位运动控制上位机

基于 .NET / WPF 的模块化工业上位机,实现多轴运动控制、双相机视觉对位、
IO 信号管理与参数配置。硬件层做了模拟实现,无真实硬件亦可完整运行。

## 技术栈

- .NET / WPF / Prism 模块化(21 个项目)/ ReactiveUI(Fody 属性通知)
- 正运动 Zmotion 控制卡(ZAux DLL P/Invoke),含模拟控制卡实现
- 海康工业相机 SDK(MV_CC,GigE),含模拟相机实现
- OpenCV 4.6:算法以 C++ 编写编译为原生 DLL,通过 P/Invoke 供 C# 调用
- SQLite / 串口(光源、桥接)

## 项目结构

| 层 | 说明 |
|---|---|
| Company.Hardware.* | 硬件抽象层 + 正运动 / 海康 / 模拟器三套实现 |
| Company.Algorithm.Wrapper | C++ OpenCV 算法封装(模板匹配、Mark 点定位、九点标定) |
| Company.Application.* | Prism 模块:初始化、运行、参数配置、登录、界面控件 |
| Company.Core | 公共模型、配置、日志 |

## 已实现功能

- 8 轴(+探针)绝对运动 / 回零 / JOG / 软限位,工位参数独立配置
- 双相机软触发采图,灰度图经 WriteableBitmap 实时显示
- 视觉对位:Mark 点定位、九点标定、单应性矩阵坐标换算
- IO 输入输出点位操作,模拟硬件下全流程可跑

> OpenCV 4.6 运行库需自行配置,不随仓库提供。

