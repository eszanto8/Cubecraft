using System;

namespace Cubecraft.Game
{
	public class World
	{
		public Block[,,] Blocks { get; private set; }
		
		public int Width { get; private set; }
		public int Depth { get; private set; }
		public int Height { get; private set; }
		
		public World(int width, int depth, int height)
		{
			Width = width;
			Depth = depth;
			Height = height;
			
			Blocks = new Block[width, depth, height];
		}
		
		public World() : this(48, 48, 48)
		{
//			PerlinNoise noise = new PerlinNoise(seed);
//			Block earth = new Block(Terrain.Earth);
//			for (int x = 0; x < Width; x++)
//				for (int y = 0; y < Depth; y++)
//					for (int z = 0; z < Height; z++) {
//				double value = noise.Compute(x, y, z);
//				if (value < foo) Blocks[x, y, z] = earth;
//			}
			
			Random random = new Random();
			Block earth = new Block(Terrain.Earth);
			Block stone = new Block(Terrain.Stone);
			Block white = new Block(Terrain.White);
			int rnd;
			for (int x = 0; x < Width; x++)
				for (int z = 0; z < Height; z++) {
				Blocks[x, 0, z] = stone;
				rnd = random.Next(0, 4);
				if (rnd == 1) Blocks[x, 1, z] = earth;
				else if (rnd == 2) Blocks[x, 1, z] = white;
				else Blocks[x, 1, z] = stone;
				rnd = random.Next(0, 7);
				if (rnd < 3) Blocks[x, 2, z] = earth;
				else if (rnd == 3) Blocks[x, 2, z] = stone;
				else if (rnd == 4) Blocks[x, 2, z] = white;
				rnd = random.Next(0, 7);
				if (rnd < 3) Blocks[x, 3, z] = earth;
			}
		}
		
		public Block GetBlock(int x, int y, int z)
		{
			if (x < 0 || y < 0 || z < 0 || x >= Width || y >= Depth || z >= Height)
				return new Block();
			return Blocks[x, y, z];
		}
	}
}
