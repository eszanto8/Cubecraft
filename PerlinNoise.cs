using System;

namespace Cubecraft
{
	public class PerlinNoise
	{
		int[] permutation;
		int[] p;
		
		public float Frequency { get; set; }
		public float Amplitude { get; set; }
		public float Persistence { get; set; }
		public int Octaves { get; set; }
		
		public PerlinNoise()
			: this((int)DateTime.Now.Ticks) {  }
		public PerlinNoise(int seed)
		{
			permutation = new int[256];
			p = new int[permutation.Length * 2];
			
			Random rnd = new Random(seed);
			for (int i = 0; i < permutation.Length; i++)
				permutation[i] = -1;
			
			for (int i = 0; i < permutation.Length; i++) while (true) {
				int iP = rnd.Next() % permutation.Length;
				if (permutation[iP] == -1) {
					permutation[iP] = i;
					break;
				}
			}
			for (int i = 0; i < permutation.Length; i++)
				p[permutation.Length + i] = p[i] = permutation[i];

			Frequency = 0.023f;
			Amplitude = 2.2f;
			Persistence = 0.9f;
			Octaves = 2;
		}
		
		public float Compute(float x, float y, float z)
		{
			float noise = 0;
			float amp = Amplitude;
			float freq = Frequency;
			for (int i = 0; i < Octaves; i++) {
				noise += Noise(x * freq, y * freq, z * freq) * amp;
				freq *= 2;
				amp *= Persistence;
			}
			if (noise < 0) return 0;
			else if (noise > 1) return 1;
			return noise;
		}
		
		float Noise(float x, float y, float z)
		{
			// Find unit cube that contains point
			int iX = (int)Math.Floor(x) & 255;
			int iY = (int)Math.Floor(y) & 255;
			int iZ = (int)Math.Floor(z) & 255;
			
			// Find relative x, y, z of the point in the cube.
			x -= (float)Math.Floor(x);
			y -= (float)Math.Floor(y);
			z -= (float)Math.Floor(z);
			
			// Compute fade curves for each of x, y, z
			float u = Fade(x);
			float v = Fade(y);
			float w = Fade(z);
			
			// Hash coordinates of the 8 cube corners
			int A = p[iX] + iY;
			int AA = p[A] + iZ;
			int AB = p[A + 1] + iZ;
			int B = p[iX + 1] + iY;
			int BA = p[B] + iZ;
			int BB = p[B + 1] + iZ;
			
			// And add blended results from 8 corners of cube.
			return Lerp(w, Lerp(v, Lerp(u, Grad(p[AA], x, y, z),
			                            Grad(p[BA], x - 1, y, z)),
			                    Lerp(u, Grad(p[AB], x, y - 1, z),
			                         Grad(p[BB], x - 1, y - 1, z))),
			            Lerp(v, Lerp(u, Grad(p[AA + 1], x, y, z - 1),
			                         Grad(p[BA + 1], x - 1, y, z - 1)),
			                 Lerp(u, Grad(p[AB + 1], x, y - 1, z - 1),
			                      Grad(p[BB + 1], x - 1, y - 1, z - 1))));
		}
		static float Fade(float t)
		{
			// Smooth interpolation parameter
			return (t * t * t * (t * (t * 6 - 15) + 10));
		}
		static float Lerp(float alpha, float a, float b)
		{
			// Linear interpolation
			return (a + alpha * (b - a));
		}
		static float Grad(int hashCode, float x, float y, float z)
		{
			// Convert lower 4 bits of hash code into 12 gradient directions
			int h = hashCode & 15;
			float u = h < 8 ? x : y;
			float v = h < 4 ? y : h == 12 || h == 14 ? x : z;
			return (((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v));
		}
	}
}
