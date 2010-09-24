using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Input;

namespace Cubecraft
{
	public class FreeCamera : Camera
	{
		bool _lastMouseEnabled = false;
		
		public float YawSpeed { get; set; }
		public float PitchSpeed { get; set; }
		
		public float MoveSpeed { get; set; }
		public float MouseSpeed { get; set; }
		public bool MoveEnabled { get; set; }
		public bool MouseEnabled { get; set; }
		public Point MousePosition { get; set; }
		
		public FreeCamera() : base()
		{
			MoveSpeed = 15;
			MouseSpeed = 5;
			
			Window window = Window.Instance;
			MousePosition = new Point(window.Width/2, window.Height/2);
		}
		
		public override void Update(double time)
		{
			Window window = Window.Instance;
			
			KeyboardDevice keyboard = window.Keyboard;
			MouseDevice mouse = window.Mouse;
			Point center = new Point(window.Width / 2, window.Height / 2);
			
			bool camEnabled = mouse[MouseButton.Right];
			if (camEnabled && !MoveEnabled)
				MousePosition = new Point(mouse.X, mouse.Y);
			MoveEnabled = camEnabled;
			MouseEnabled = camEnabled;
			
			if (MouseEnabled && _lastMouseEnabled && window.Focused) {
				YawSpeed += (mouse.X - center.X) * MouseSpeed * (float)time;
				PitchSpeed += (mouse.Y - center.Y) * MouseSpeed * (float)time;
				Cursor.Position = window.PointToScreen(center);
			} else if (MouseEnabled && !_lastMouseEnabled && window.Focused) {
				Cursor.Position = window.PointToScreen(center);
				Cursor.Hide();
				_lastMouseEnabled = true;
			} else if (_lastMouseEnabled) {
				Cursor.Position = window.PointToScreen(MousePosition);
				Cursor.Show();
				_lastMouseEnabled = false;
			}
			YawSpeed *= 0.6f;
			PitchSpeed *= 0.6f;
			Yaw = (((Yaw + YawSpeed) % 360) + 360) % 360;
			Pitch = Math.Max(-90, Math.Min(90, Pitch + PitchSpeed));
			
			if (MoveEnabled && window.Focused) {
				float yaw = (float)Math.PI * Yaw / 180;
				float pitch = (float)Math.PI * Pitch / 180;
				double speed = MoveSpeed * time;
				if ((keyboard[Key.A] ^ keyboard[Key.D]) &&
				    (keyboard[Key.W] ^ keyboard[Key.S]))
					speed /= Math.Sqrt(2);
				if (keyboard[Key.ShiftLeft] && !keyboard[Key.ControlLeft]) speed *= 5;
				else if (keyboard[Key.ControlLeft] && !keyboard[Key.ShiftLeft]) speed /= 3;
				if (keyboard[Key.W] && !keyboard[Key.S]) {
					X -= Math.Sin(yaw) * Math.Cos(pitch) * speed;
					Y += Math.Sin(pitch) * speed;
					Z += Math.Cos(yaw) * Math.Cos(pitch) * speed;
				} else if (keyboard[Key.S] && !keyboard[Key.W]) {
					X += Math.Sin(yaw) * Math.Cos(pitch) * speed;
					Y -= Math.Sin(pitch) * speed;
					Z -= Math.Cos(yaw) * Math.Cos(pitch) * speed;
				}
				if (keyboard[Key.A] && !keyboard[Key.D]) {
					X += Math.Cos(yaw) * speed;
					Z += Math.Sin(yaw) * speed;
				} else if (keyboard[Key.D] && !keyboard[Key.A]) {
					X -= Math.Cos(yaw) * speed;
					Z -= Math.Sin(yaw) * speed;
				}
			}
		}
	}
}
