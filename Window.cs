using System;
using System.Drawing;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Cubecraft.Graphics;
using Cubecraft.Game;

namespace Cubecraft
{
	public class Window : GameWindow
	{
		public static Window Instance { get; private set; }
		
		public Framebuffer Framebuffer { get; private set; }
		public bool Running { get; private set; }
		
		public Camera Camera { get; set; }
		public World World { get; set; }
		public List<Entity> Entities { get; private set; }
		
		public WorldRenderer WorldRenderer { get; set; }
		
		public Window() : base(1152, 864, new GraphicsMode(32, 24, 0, 0), "Cubecraft")
		{
			Instance = this;
			WindowBorder = WindowBorder.Fixed;
			
			Camera = new FreeCamera();
			World = new World();
			Entities = new List<Entity>();
			
			WorldRenderer = new WorldRenderer();
		}
		
		protected override void OnLoad(EventArgs e)
		{
			GL.ClearColor(new Color4(0.0f, 0.0f, 0.0f, 1.0f));
			GL.Enable(EnableCap.CullFace);
			GL.Enable(EnableCap.Blend);
			GL.Enable(EnableCap.Texture2D);
			GL.Enable(EnableCap.Light0);
			//GL.Enable(EnableCap.AlphaTest);
			
			GL.Light(LightName.Light0, LightParameter.Ambient,
			         new Color4(0.6f, 0.6f, 0.6f, 1.0f));
			GL.Light(LightName.Light0, LightParameter.Diffuse,
			         new Color4(0.6f, 0.6f, 0.6f, 1.0f));
			GL.ColorMaterial(MaterialFace.Front,
			                 ColorMaterialParameter.AmbientAndDiffuse);
			//GL.AlphaFunc(AlphaFunction.Greater, 0.5f);
			
			GL.Fog(FogParameter.FogMode, (int)FogMode.Linear);
			GL.Fog(FogParameter.FogColor, new float[]{ 0.0f, 0.0f, 0.0f, 1.0f });
			GL.Fog(FogParameter.FogStart, 96.0f);
			GL.Fog(FogParameter.FogEnd, 128.0f);
		}
		
		protected override void OnResize(EventArgs e)
		{
			GL.Viewport(ClientSize);
			if (Framebuffer != null) Framebuffer.Dispose();
			Framebuffer = new Framebuffer(Width / 2, Height / 3);
		}
		
		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			if (Keyboard[Key.Escape]) Exit();
			Camera.Update(e.Time);
		}
		
		protected override void OnRenderFrame(FrameEventArgs e)
		{
			GL.Clear(ClearBufferMask.DepthBufferBit);
			
			Framebuffer.Begin();
			
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(
				(float)Math.PI/4, (float)Width/Height, 0.2f, 128.0f);
			GL.LoadMatrix(ref projection);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadIdentity();
			
			GL.Enable(EnableCap.DepthTest);
			GL.Enable(EnableCap.Lighting);
			GL.Enable(EnableCap.ColorMaterial);
			GL.Enable(EnableCap.Fog);
			
			RenderWorld();
			
			Framebuffer.End();
			
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(0, Width, 0, Height, -1, 1);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadIdentity();
			
			GL.Disable(EnableCap.DepthTest);
			GL.Disable(EnableCap.Lighting);
			GL.Disable(EnableCap.ColorMaterial);
			GL.Disable(EnableCap.Fog);
			
			Display.BlendMode = BlendMode.Blend;
			
			GL.Color4(new Color4(0.0f, 0.0f, 0.0f, 0.5f));
			GL.Begin(BeginMode.Lines);
			for (int i = 0; i < Height; i += 3) {
				GL.Vertex2(0, i);
				GL.Vertex2(Width, i);
			}
			GL.End();
			
			GL.Color4(new Color4(1.0f, 1.0f, 1.0f, 0.6f));
			Draw.Rectangle(ClientRectangle, Framebuffer.ColorTexture);
			Display.Texture = null;
			
			Display.BlendMode = BlendMode.None;
			
			SwapBuffers();
		}
		
		void RenderWorld()
		{
			Camera.Render();
			GL.Light(LightName.Light0, LightParameter.Position,
			         new Vector4(0.7f, 1.0f, 0.4f, 0.0f));
			
			GL.Translate(-World.Width / 2, -World.Depth / 2, -World.Height / 2);
			GL.Color4(Color4.White);
			WorldRenderer.Render(World);
			
			foreach (Entity entity in Entities) {
				GL.PushMatrix();
				entity.Render();
				GL.PopMatrix();
			}
		}
	}
}
