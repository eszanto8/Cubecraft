using System;
using OpenTK.Graphics.OpenGL;
using Cubecraft.Graphics;

namespace Cubecraft.Graphics
{
	public struct BlendMode
	{
		public static BlendMode None = new BlendMode(BlendingFactorSrc.One, BlendingFactorDest.Zero);
		public static BlendMode Blend = new BlendMode(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
		public static BlendMode Add = new BlendMode(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.One);
		
		public BlendingFactorSrc Source { get; private set; }
		public BlendingFactorDest Destination { get; private set; }
		
		public BlendMode(BlendingFactorSrc source, BlendingFactorDest destination) : this()
		{
			Source = source;
			Destination = destination;
		}
		
		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			if (!(obj is BlendMode)) return false;
			BlendMode bm = (BlendMode)obj;
			return (Source == bm.Source && Destination == bm.Destination);
		}
		public override int GetHashCode()
		{
			return ((int)Source ^ (int)Destination);
		}
	}
}
