using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Cubecraft.Graphics;

namespace Cubecraft.Game
{
	public abstract class Entity
	{
		public BoundingBox Box { get; set; }
		public Vector3 Speed { get; set; }
		public float Gravity { get; set; }
		
		#region Box
		public Vector3d Location {
			get { return Box.Location; }
			set { Box.Location = value; }
		}
		public Vector3 Size {
			get { return Box.Size; }
			set { Box.Size = value; }
		}
		#endregion
		#region Speed
		public float SpeedX {
			get { return Speed.X; }
			set { Speed = new Vector3(value, Speed.Y, Speed.Z); }
		}
		public float SpeedY {
			get { return Speed.Y; }
			set { Speed = new Vector3(Speed.X, value, Speed.Z); }
		}
		public float SpeedZ {
			get { return Speed.Z; }
			set { Speed = new Vector3(Speed.X, Speed.Y, value); }
		}
		#endregion
		
		public Entity() {  }
		
		public virtual void Step()
		{
			SpeedY += Gravity;
			Location += (Vector3d)Speed;
		}
		
		public virtual void Render()
		{
			GL.Translate(Location);
		}
	}
}
