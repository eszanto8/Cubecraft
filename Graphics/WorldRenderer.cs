using System;
using OpenTK.Graphics.OpenGL;
using Cubecraft.Game;

namespace Cubecraft.Graphics
{
	public class WorldRenderer
	{
		Texture _terrain = new Texture("../../terrain.png");
		
		public BlockRenderer BlockRenderer { get; set; }
		
		public WorldRenderer()
		{
			BlockRenderer = new BlockRenderer();
		}
		
		public virtual void Render(World world)
		{
			Display.Texture = _terrain;
			GL.Begin(BeginMode.Quads);
			for (int x = 0; x < world.Width; x++)
				for (int y = 0; y < world.Depth; y++)
					for (int z = 0; z < world.Height; z++)
						BlockRenderer.Render(world, x, y, z);
			GL.End();
			Display.Texture = null;
		}
	}
}
