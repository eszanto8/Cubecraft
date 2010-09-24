using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Cubecraft
{
	public class Camera
	{
		public Vector3d Location { get; set; }
		public Vector3 Rotation { get; set; }
		
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
		#region Rotation
		public float Yaw {
			get { return Rotation.X; }
			set { Rotation = new Vector3(value, Rotation.Y, Rotation.Z); }
		}
		public float Pitch {
			get { return Rotation.Y; }
			set { Rotation = new Vector3(Rotation.X, value, Rotation.Z); }
		}
		public float Roll {
			get { return Rotation.Z; }
			set { Rotation = new Vector3(Rotation.X, Rotation.Y, value); }
		}
		#endregion
		
		public Camera() {  }
		
		public virtual void Update(double time) {  }
		
		public void Render()
		{
			GL.Rotate(Pitch, 1.0, 0.0, 0.0);
			GL.Rotate(Yaw, 0.0, 1.0, 0.0);
			GL.Rotate(Roll, 0.0, 0.0, 1.0);
			GL.Translate(Location);
		}
	}
}
