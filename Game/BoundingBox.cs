using System;
using OpenTK;

namespace Cubecraft.Game
{
	public class BoundingBox
	{
		public Vector3d Location { get; set; }
		public Vector3 Size { get; set; }
		
		#region Location
		public double X {
			get { return Location.X; }
			set { Location = new Vector3d(value, Location.Y, Location.Z); }
		}
		public double Y {
			get { return Location.Y; }
			set { Location = new Vector3d(Location.X, value, Location.Z); }
		}
		public double Z {
			get { return Location.Z; }
			set { Location = new Vector3d(Location.X, Location.Y, value); }
		}
		#endregion
		#region Size
		public float Width {
			get { return Size.X; }
			set { Size = new Vector3(value, Size.Y, Size.Z); }
		}
		public float Depth {
			get { return Size.Y; }
			set { Size = new Vector3(Size.X, value, Size.Z); }
		}
		public float Height {
			get { return Size.Z; }
			set { Size = new Vector3(Size.X, Size.Y, value); }
		}
		#endregion
		#region Left, Top, Front, Right, Bottom, Back
		public double Left { get { return X; } }
		public double Top { get { return Y; } }
		public double Front { get { return Z; } }
		public double Right { get { return X + Width; } }
		public double Bottom { get { return Y + Depth; } }
		public double Back { get { return Z + Height; } }
		#endregion
		
		public BoundingBox(Vector3d location, Vector3 size)
		{
			Location = location;
			Size = size;
		}
		
		public bool Overlaps(BoundingBox region)
		{
			return (region.Right >= Left || region.Left < Right) &&
				(region.Bottom >= Top || region.Top < Bottom) &&
				(region.Back >= Front || region.Front < Back);
		}
		public bool Contains(BoundingBox region)
		{
			return (region.Right >= Left && region.Left < Right) &&
				(region.Bottom >= Top && region.Top < Bottom) &&
				(region.Back >= Front && region.Front < Back);
		}
	}
}
