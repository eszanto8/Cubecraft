using System;
using System.Drawing;
using Cubecraft.Graphics;

namespace Cubecraft.Game
{
	public enum Terrain : byte
	{
		Air = 0,
		Earth = 1,
		Stone = 2,
		White = 3
	}
	
	public class TerrainInfo
	{
		static TerrainInfo[] _infos { get; set; }
		
		static TerrainInfo()
		{
			_infos = new TerrainInfo[]{
				new TerrainInfo(Terrain.Air)  { Solid = false, Opaque = false },
				new TerrainInfo(Terrain.Earth){ Solid = true,  Opaque = true },
				new TerrainInfo(Terrain.Stone){ Solid = true,  Opaque = true },
				new TerrainInfo(Terrain.White){ Solid = true,  Opaque = true }
			};
		}
		
		public static TerrainInfo Find(Terrain terrain)
		{
			return _infos[(byte)terrain];
		}
		public static TerrainInfo Find(string name)
		{
			foreach (TerrainInfo info in _infos)
				if (info.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
					return info;
			return null;
		}
		
		
		public Terrain Terrain { get; private set; }
		public bool Solid { get; private set; }
		public bool Opaque { get; private set; }
		
		public string Name {
			get { return Terrain.ToString(); }
		}
		public RectangleD Rectangle { get; private set; }
		
		TerrainInfo(Terrain terrain)
		{
			Terrain = terrain;
			Rectangle = new RectangleD((int)Terrain % 4 / 4.0 + 0.0002,
			                           (int)Terrain / 4 / 4.0 + 0.0002,
			                           1 / 4.0 - 0.0004, 1 / 4.0 - 0.0004);
		}
	}
}
