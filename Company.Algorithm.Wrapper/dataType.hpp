#pragma once
#include <opencv2/opencv.hpp>

namespace Company
{
	struct ImageU8C1
	{
		uchar* IntPtr;
		int Width;
		int Height;
	};
	struct ImageU8C3
	{
		uchar* IntPtr;
		int Width;
		int Height;
	};
	struct CppRectangle
	{
		double X;
		double Y;
		double Width;
		double Height;
	};
	struct CppSetting
	{
		/// <summary>
		/// 图片TOP向下偏移ROI的像素值
		/// </summary>
		int OffSetup;
		/// <summary>
		/// 图片BOTTOM向上偏移ROI的像素值
		/// </summary>
		int OffSetdown;
		/// <summary>
		/// 是否保存中间结果，0表示不保存，1表示保存
		/// </summary>
		int Output; 
		/// <summary>
		/// 元件的二值化阈值
		/// </summary>
		int threshold_element;
		/// <summary>
		/// 齿轮孔的二值化阈值
		/// </summary>
		int threshold_circle;
	};
}