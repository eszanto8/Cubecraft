using System;

namespace Cubecraft.Game
{
	public struct Block
	{
		public short Data { get; private set; }
		
		public Terrain Terrain {
			get { return (Terrain)Bits.Get(Data, 0, 4); }
			set { Data = (short)Bits.Set(Data, 0, 4, (int)value); }
		}
		public byte Amount {
			get { return (byte)Bits.Get(Data, 4, 3); }
			set { Data = (short)Bits.Set(Data, 4, 3, value); }
		}
		
		public TerrainInfo TerrainInfo {
			get { return TerrainInfo.Find(Terrain); }
		}
		
		public Block(Terrain terrain) : this()
		{
			Terrain = terrain;
		}
		
		public Block(short data) : this()
		{
			Data = data;
		}
		
		public override bool Equals(object obj)
		{
			if (!(obj is Block)) return false;
			return ((Block)obj).Data == Data;
		}
		public override int GetHashCode()
		{
			return Data;
		}
	}
}
