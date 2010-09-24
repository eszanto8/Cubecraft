using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using Cubecraft.Game;

namespace Cubecraft.Graphics
{
	public class BlockRenderer
	{
		public virtual void Render(World world, int x, int y, int z)
		{
			Block block = world.Blocks[x, y, z];
			if (block.Terrain == Terrain.Air) return;
			
			Block right  = world.GetBlock(x + 1, y, z);
			Block left   = world.GetBlock(x - 1, y, z);
			Block top    = world.GetBlock(x, y + 1, z);
			Block bottom = world.GetBlock(x, y - 1, z);
			Block front  = world.GetBlock(x, y, z + 1);
			Block back   = world.GetBlock(x, y, z - 1);
			
			RenderSolid(block, x, y, z, right, left, top, bottom, front, back);
		}
		
		void RenderSolid(Block block, int x, int y, int z, Block right, Block left,
		                 Block top, Block bottom, Block front, Block back)
		{
			RectangleD rect = block.TerrainInfo.Rectangle;
			if (SideVisible(block, top)) {
				GL.Normal3(0.0, 1.0, 0.0);
				GL.TexCoord2(rect.Left,  rect.Top);    GL.Vertex3(x, y+1.0, z);
				GL.TexCoord2(rect.Right, rect.Top);    GL.Vertex3(x, y+1.0, z+1.0);
				GL.TexCoord2(rect.Right, rect.Bottom); GL.Vertex3(x+1.0, y+1.0, z+1.0);
				GL.TexCoord2(rect.Left,  rect.Bottom); GL.Vertex3(x+1.0, y+1.0, z);
			}
			if (SideVisible(block, bottom)) {
				GL.Normal3(0.0, 1.0, 0.0);
				GL.TexCoord2(rect.Left,  rect.Top);    GL.Vertex3(x, y, z);
				GL.TexCoord2(rect.Right, rect.Top);    GL.Vertex3(x+1.0, y, z);
				GL.TexCoord2(rect.Right, rect.Bottom); GL.Vertex3(x+1.0, y, z+1.0);
				GL.TexCoord2(rect.Left,  rect.Bottom); GL.Vertex3(x, y, z+1.0);
			}
			if (SideVisible(block, right)) {
				GL.Normal3(1.0, 0.0, 0.0);
				GL.TexCoord2(rect.Left,  rect.Top);    GL.Vertex3(x+1.0, y+1.0, z);
				GL.TexCoord2(rect.Right, rect.Top);    GL.Vertex3(x+1.0, y+1.0, z+1.0);
				GL.TexCoord2(rect.Right, rect.Bottom); GL.Vertex3(x+1.0, y, z+1.0);
				GL.TexCoord2(rect.Left,  rect.Bottom); GL.Vertex3(x+1.0, y, z);
			}
			if (SideVisible(block, left)) {
				GL.Normal3(1.0, 0.0, 0.0);
				GL.TexCoord2(rect.Left,  rect.Top);    GL.Vertex3(x, y+1.0, z+1.0);
				GL.TexCoord2(rect.Right, rect.Top);    GL.Vertex3(x, y+1.0, z);
				GL.TexCoord2(rect.Right, rect.Bottom); GL.Vertex3(x, y, z);
				GL.TexCoord2(rect.Left,  rect.Bottom); GL.Vertex3(x, y, z+1.0);
			}
			if (SideVisible(block, front)) {
				GL.Normal3(0.0, 0.0, 1.0);
				GL.TexCoord2(rect.Left,  rect.Top);    GL.Vertex3(x+1.0, y+1.0, z+1.0);
				GL.TexCoord2(rect.Right, rect.Top);    GL.Vertex3(x, y+1.0, z+1.0);
				GL.TexCoord2(rect.Right, rect.Bottom); GL.Vertex3(x, y, z+1.0);
				GL.TexCoord2(rect.Left,  rect.Bottom); GL.Vertex3(x+1.0, y, z+1.0);
			}
			if (SideVisible(block, back)) {
				GL.Normal3(0.0, 0.0, 1.0);
				GL.TexCoord2(rect.Left,  rect.Top);    GL.Vertex3(x, y+1.0, z);
				GL.TexCoord2(rect.Right, rect.Top);    GL.Vertex3(x+1.0, y+1.0, z);
				GL.TexCoord2(rect.Right, rect.Bottom); GL.Vertex3(x+1.0, y, z);
				GL.TexCoord2(rect.Left,  rect.Bottom); GL.Vertex3(x, y, z);
			}
		}
		
		bool SideVisible(Block block, Block side)
		{
			return (!side.TerrainInfo.Opaque);
		}
	}
}
