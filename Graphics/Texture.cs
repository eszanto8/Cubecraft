using System;
using System.Drawing;
using Imaging = System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;

namespace Cubecraft.Graphics
{
	public class Texture : IDisposable
	{
		public int ID { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		
		public Texture()
		{
			ID = GL.GenTexture();
			Display.Texture = this;
			GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
			GL.TexParameter(TextureTarget.Texture2D,
			                TextureParameterName.TextureMinFilter,
			                (int)TextureMinFilter.Nearest);
			GL.TexParameter(TextureTarget.Texture2D,
			                TextureParameterName.TextureMagFilter,
			                (int)TextureMagFilter.Nearest);
		}
		
		public Texture(int width, int height) : this()
		{
			Width = width;
			Height = height;
			GL.TexImage2D(TextureTarget.Texture2D, 0,
			              PixelInternalFormat.Rgba8,
			              Width, Height, 0, PixelFormat.Bgra,
			              PixelType.UnsignedByte, IntPtr.Zero);
		}
		
		public Texture(Bitmap bitmap)
			: this(bitmap, false) {  }
		public Texture(Bitmap bitmap, bool dispose) : this()
		{
			Width = bitmap.Width;
			Height = bitmap.Height;
			Imaging.BitmapData data = bitmap.LockBits(
				new Rectangle(0, 0, Width, Height),
				Imaging.ImageLockMode.ReadOnly,
				Imaging.PixelFormat.Format32bppArgb);
			GL.TexImage2D(TextureTarget.Texture2D, 0,
			              PixelInternalFormat.Rgba8,
			              Width, Height, 0, PixelFormat.Bgra,
			              PixelType.UnsignedByte, data.Scan0);
			bitmap.UnlockBits(data);
			if (dispose) bitmap.Dispose();
		}
		public Texture(string filename)
			: this(new Bitmap(filename), true) {  }
		
		public void Dispose()
		{
			if (ID == 0) return;
			GL.DeleteTexture(ID);
			ID = 0;
			Width = 0;
			Height = 0;
		}
	}
}
