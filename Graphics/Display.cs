using System;
using OpenTK.Graphics.OpenGL;
using Cubecraft.Graphics;

namespace Cubecraft.Graphics
{
	public static class Display
	{
		static Texture _texture = null;
		public static Texture Texture {
			get { return _texture; }
			set {
				_texture = value;
				if (_texture == null) GL.BindTexture(TextureTarget.Texture2D, 0);
				else GL.BindTexture(TextureTarget.Texture2D, value.ID);
			}
		}
		
		static BlendMode _blendMode = BlendMode.None;
		public static BlendMode BlendMode {
			get { return _blendMode; }
			set {
				_blendMode = value;
				GL.BlendFunc(_blendMode.Source, _blendMode.Destination);
			}
		}
	}
}
