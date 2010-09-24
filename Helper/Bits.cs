using System;
using System.Text;

namespace Cubecraft
{
	public static class Bits
	{
		public static bool Get(int value, int index)
		{
			return (value >> index & 1) == 1;
		}
		public static int Set(int value, int index, bool bit)
		{
			if (bit) return value | (1 << index);
			else return value & ~(1 << index);
		}
		
		public static int Get(int value, int index, int length)
		{
			return (value >> index) & ~(~0 << length);
		}
		public static int Set(int value, params bool[] bits)
		{
			return Set(value, 0, bits);
		}
		public static int Set(int value, int index, params bool[] bits)
		{
			return Set(value, index, bits.Length, ToInt32(bits));
		}
		public static int Set(int value, string bits)
		{
			return Set(value, 0, bits);
		}
		public static int Set(int value, int index, string bits)
		{
			return Set(value, index, bits.Length, ToInt32(bits));
		}
		public static int Set(int value, int index, int length, int bits)
		{
			int range = ~(~0 << length);
			bits = (bits & range) << index;
			return value & ~(range << index) | bits;
		}
		
		public static int ToInt32(params bool[] bools)
		{
			int result = 0;
			for (int i = 0; i < bools.Length; i++)
				if (bools[i]) result |= 1 << i;
			return result;
		}
		public static int ToInt32(string value)
		{
			int result = 0;
			for (int i = 0; i < value.Length; i++)
				if (value[i] != '0') result |= 1 << i;
			return result;
		}
		
		public static string ToString(int value)
		{
			return ToString(value, sizeof(int)*8);
		}
		public static string ToString(short value)
		{
			return ToString(value, sizeof(short)*8);
		}
		public static string ToString(byte value)
		{
			return ToString(value, sizeof(byte)*8);
		}
		public static string ToString(int value, int length)
		{
			StringBuilder builder = new StringBuilder(length);
			for (int i = 0; i < length; i++)
				builder.Append(Get(value, i) ? '1' : '0');
			return builder.ToString();
		}
	}
}
