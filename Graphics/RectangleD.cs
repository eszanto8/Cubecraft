using System;
using System.Drawing;

namespace Cubecraft.Graphics
{
	public struct RectangleD
	{
		public static RectangleD Empty = new RectangleD(0.0, 0.0, 0.0, 0.0);
		public static RectangleD Unit = new RectangleD(0.0, 0.0, 1.0, 1.0);
		
		public double X { get; private set; }
		public double Y { get; private set; }
		public double Width { get; private set; }
		public double Height { get; private set; }
		
		public double Left { get { return X; } }
		public double Top { get { return Y; } }
		public double Right { get { return X + Width; } }
		public double Bottom { get { return Y + Height; } }
		
		public RectangleD(double x, double y, double width, double height) : this()
		{
			X = x; Y = y;
			Width = width;
			Height = height;
		}
		
		public static implicit operator RectangleD(RectangleF rect)
		{
			return new RectangleD(rect.X, rect.Y, rect.Width, rect.Height);
		}
		public static explicit operator RectangleF(RectangleD rect)
		{
			return new RectangleF((float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
		}
	}
}
