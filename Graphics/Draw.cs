using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace Cubecraft.Graphics
{
	public static class Draw
	{
		public static void Rectangle(Rectangle rect)
		{
			Rectangle(rect, RectangleD.Unit);
		}
		public static void Rectangle(Rectangle rect, RectangleD texCoords)
		{
			Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
		}
		public static void Rectangle(Rectangle rect, Texture tex)
		{
			Rectangle(rect, tex, RectangleD.Unit);
		}
		public static void Rectangle(Rectangle rect, Texture tex, RectangleD texCoords)
		{
			Rectangle(rect.X, rect.Y, rect.Width, rect.Height, tex, texCoords);
		}
		
		public static void Rectangle(int x, int y, int width, int height)
		{
			Rectangle(x, y, width, height, RectangleD.Unit);
		}
		public static void Rectangle(int x, int y, int width, int height, RectangleD texCoords)
		{
			GL.Begin(BeginMode.Quads);
			GL.TexCoord2(texCoords.Left,  texCoords.Top);    GL.Vertex2(x, y);
			GL.TexCoord2(texCoords.Right, texCoords.Top);    GL.Vertex2(x + width, y);
			GL.TexCoord2(texCoords.Right, texCoords.Bottom); GL.Vertex2(x + width, y + height);
			GL.TexCoord2(texCoords.Left,  texCoords.Bottom); GL.Vertex2(x, y + height);
			GL.End();
		}
		public static void Rectangle(int x, int y, int width, int height, Texture tex)
		{
			Rectangle(x, y, width, height, tex, RectangleD.Unit);
		}
		public static void Rectangle(int x, int y, int width, int height, Texture tex, RectangleD texCoords)
		{
			Display.Texture = tex;
			Rectangle(x, y, width, height, texCoords);
		}
	}
}
