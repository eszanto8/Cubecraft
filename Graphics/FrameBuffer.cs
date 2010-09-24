using System;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Cubecraft.Graphics
{
	public class Framebuffer : IDisposable
	{
		public int ID { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		
		public Texture ColorTexture { get; set; }
		public Texture DepthTexture { get; set; }
		
		public Framebuffer()
		{
			int id;
			GL.GenFramebuffers(1, out id);
			ID = id;
		}
		public Framebuffer(int width, int height) : this()
		{
			Width = width;
			Height = height;
			
			ColorTexture = new Texture(width, height);
			DepthTexture = new Texture(){ Width = width, Height = height };
			GL.TexImage2D(TextureTarget.Texture2D, 0,
			              PixelInternalFormat.DepthComponent32,
			              width, height, 0,
			              PixelFormat.DepthComponent,
			              PixelType.UnsignedInt, IntPtr.Zero);
			GL.BindTexture(TextureTarget.Texture2D, 0);
			
			GL.BindFramebuffer(FramebufferTarget.Framebuffer, ID);
			GL.FramebufferTexture2D(FramebufferTarget.Framebuffer,
			                        FramebufferAttachment.ColorAttachment0,
			                        TextureTarget.Texture2D, ColorTexture.ID, 0);
			GL.FramebufferTexture2D(FramebufferTarget.Framebuffer,
			                        FramebufferAttachment.DepthAttachment,
			                        TextureTarget.Texture2D, DepthTexture.ID, 0);
			GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
		}
		
		public void Dispose()
		{
			if (ID == 0) return;
			int id = ID;
			GL.DeleteFramebuffers(1, ref id);
			ColorTexture.Dispose();
			DepthTexture.Dispose();
			ID = 0;
			Width = 0;
			Height = 0;
			ColorTexture = null;
			DepthTexture = null;
		}
		
		public void Begin()
		{
			GL.BindFramebuffer(FramebufferTarget.Framebuffer, ID);
			GL.PushAttrib(AttribMask.ViewportBit);
			GL.Viewport(0, 0, Width, Height);
		}
		public void End()
		{
			GL.PopAttrib();
			GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
		}
	}
}
