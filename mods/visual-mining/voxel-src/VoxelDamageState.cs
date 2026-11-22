using System;
using System.Collections.Generic;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace Kuboczoch.VisualMining;

internal sealed class VoxelDamageState
{
	private const int Size = 16;

	private readonly bool[] solid = new bool[4096];

	private readonly List<int[]> removedBatches = new List<int[]>();

	private int hits;

	public int BlockId
	{
		get;
	}

	public bool HasLocalSource
	{
		get;
		private set;
	}

	public VoxelDamageState(int blockId)
	{
		BlockId = blockId;
		Array.Fill(solid, true);
	}

	public bool ApplyHit(Vec3d hitPosition, BlockFacing face, EnumTool tool, float strength, bool localSource)
	{
		int hitCount = hits + 1;
		List<int> val = new List<int>();
		strength = Math.Clamp(strength, 0.45f, 1.75f);
		if (tool == EnumTool.Axe && face.Axis != EnumAxis.Y)
		{
			CarveAxe(hitPosition, face, hitCount, strength, val);
		}
		else if (tool == EnumTool.Shovel)
		{
			CarveShovel(hitPosition, face, hitCount, strength, val);
		}
		else
		{
			CarvePickaxe(hitPosition, face, hitCount, strength, val);
		}
		if (val.Count == 0)
		{
			return false;
		}
		hits = hitCount;
		removedBatches.Add(val.ToArray());
		if (localSource)
		{
			HasLocalSource = true;
		}
		return true;
	}

	public bool RestoreLastHit()
	{
		if (removedBatches.Count == 0)
		{
			return true;
		}
		int num = removedBatches.Count - 1;
		int[] array = removedBatches[num];
		foreach (int num2 in array)
		{
			solid[num2] = true;
		}
		removedBatches.RemoveAt(num);
		if (hits > 0)
		{
			hits--;
		}
		return removedBatches.Count == 0;
	}

	public bool HasOpenBoundary(int normalX, int normalY, int normalZ)
	{
		if (normalX > 0)
		{
			return HasRemovedVoxel(15, 16, 0, 16, 0, 16);
		}
		if (normalX < 0)
		{
			return HasRemovedVoxel(0, 1, 0, 16, 0, 16);
		}
		if (normalY > 0)
		{
			return HasRemovedVoxel(0, 16, 15, 16, 0, 16);
		}
		if (normalY < 0)
		{
			return HasRemovedVoxel(0, 16, 0, 1, 0, 16);
		}
		if (normalZ > 0)
		{
			return HasRemovedVoxel(0, 16, 0, 16, 15, 16);
		}
		if (normalZ < 0)
		{
			return HasRemovedVoxel(0, 16, 0, 16, 0, 1);
		}
		return false;
	}

	public List<uint> BuildCuboids()
	{
		bool[] array = new bool[solid.Length];
		List<uint> val = new List<uint>();
		for (int i = 0; i < 16; i++)
		{
			for (int j = 0; j < 16; j++)
			{
				for (int k = 0; k < 16; k++)
				{
					int num = Index(k, i, j);
					if (solid[num] && !array[num])
					{
						int l;
						for (l = k + 1; l < 16 && solid[Index(l, i, j)] && !array[Index(l, i, j)]; l++)
						{
						}
						int m;
						for (m = j + 1; m < 16 && IsSolidUnusedBox(k, l, i, i + 1, m, m + 1, array); m++)
						{
						}
						int n;
						for (n = i + 1; n < 16 && IsSolidUnusedBox(k, l, n, n + 1, j, m, array); n++)
						{
						}
						MarkUsed(k, l, i, n, j, m, array);
						val.Add(BlockEntityMicroBlock.ToUint(k, i, j, l, n, m, 0));
					}
				}
			}
		}
		return val;
	}

	private void CarvePickaxe(Vec3d hit, BlockFacing face, int hitCount, float strength, List<int> batch)
	{
		float num = SizeScale(strength);
		int depth = ScaleDepth(Math.Min(14, 1 + hitCount), num, 14);
		float radius = Math.Min(5.2f, (1.6f + (float)hitCount * 0.12f) * num);
		ForEachVoxel(delegate(int x, int y, int z)
		{
			int num2 = DepthFromFace(x, y, z, face);
			if (num2 < depth)
			{
				GetFaceDelta(hit, face, x, y, z, out var a, out var b);
				float num3 = 1f - (float)num2 / (float)depth;
				float num4 = radius * (0.75f + num3 * 0.25f) + Noise(x, y, z, hitCount) * 0.45f;
				if (a * a + b * b <= num4 * num4)
				{
					RemoveVoxel(x, y, z, batch);
				}
			}
		});
	}

	private void CarveAxe(Vec3d hit, BlockFacing face, int hitCount, float strength, List<int> batch)
	{
		float num = SizeScale(strength);
		int depth = ScaleDepth(Math.Min(5, 2 + hitCount / 4), num, 5);
		float width = Math.Min(7.4f, (4.2f + (float)hitCount * 0.12f) * num);
		float height = Math.Min(4f, (1.5f + (float)hitCount * 0.08f) * num);
		ForEachVoxel(delegate(int x, int y, int z)
		{
			int num2 = DepthFromFace(x, y, z, face);
			if (num2 < depth)
			{
				float num3 = (float)((face.Axis == EnumAxis.X) ? Math.Abs((double)((float)z + 0.5f) - hit.Z * 16.0) : Math.Abs((double)((float)x + 0.5f) - hit.X * 16.0));
				float num4 = (float)Math.Abs((double)((float)y + 0.5f) - hit.Y * 16.0);
				float num5 = (float)num2 / (float)depth;
				float num6 = 1f - num5 * 0.55f;
				float num7 = num3 / (width * num6);
				float num8 = num4 / (height * num6);
				float num9 = Noise(x, y, z, hitCount) * 0.18f;
				if (num7 * num7 + num8 * num8 <= 1f + num9)
				{
					RemoveVoxel(x, y, z, batch);
				}
			}
		});
	}

	private void CarveShovel(Vec3d hit, BlockFacing face, int hitCount, float strength, List<int> batch)
	{
		float num = SizeScale(strength);
		int depth = ScaleDepth(Math.Min(10, 3 + hitCount / 2), num, 10);
		float radius = Math.Min(7.8f, (3.8f + (float)hitCount * 0.2f) * num);
		ForEachVoxel(delegate(int x, int y, int z)
		{
			int num2 = DepthFromFace(x, y, z, face);
			if (num2 < depth)
			{
				GetFaceDelta(hit, face, x, y, z, out var a, out var b);
				float num3 = (float)num2 / (float)depth;
				float num4 = radius * (1f - num3 * 0.35f) + Noise(x, y, z, hitCount) * 0.65f;
				if (a * a + b * b <= num4 * num4)
				{
					RemoveVoxel(x, y, z, batch);
				}
			}
		});
	}

	private static float SizeScale(float strength)
	{
		return 0.5f + strength * 0.5f;
	}

	private static int ScaleDepth(int depth, float scale, int max)
	{
		return Math.Min(max, Math.Max(1, (int)Math.Ceiling((double)((float)depth * scale))));
	}

	private void RemoveVoxel(int x, int y, int z, List<int> batch)
	{
		int num = Index(x, y, z);
		if (solid[num])
		{
			solid[num] = false;
			batch.Add(num);
		}
	}

	private static void GetFaceDelta(Vec3d hit, BlockFacing face, int x, int y, int z, out float a, out float b)
	{
		float num = (float)x + 0.5f;
		float num2 = (float)y + 0.5f;
		float num3 = (float)z + 0.5f;
		if (face.Axis == EnumAxis.X)
		{
			a = num2 - (float)(hit.Y * 16.0);
			b = num3 - (float)(hit.Z * 16.0);
		}
		else if (face.Axis == EnumAxis.Y)
		{
			a = num - (float)(hit.X * 16.0);
			b = num3 - (float)(hit.Z * 16.0);
		}
		else
		{
			a = num - (float)(hit.X * 16.0);
			b = num2 - (float)(hit.Y * 16.0);
		}
	}

	private static int DepthFromFace(int x, int y, int z, BlockFacing face)
	{
		if (face.Normali.X > 0)
		{
			return 15 - x;
		}
		if (face.Normali.X < 0)
		{
			return x;
		}
		if (face.Normali.Y > 0)
		{
			return 15 - y;
		}
		if (face.Normali.Y < 0)
		{
			return y;
		}
		if (face.Normali.Z > 0)
		{
			return 15 - z;
		}
		return z;
	}

	private static float Noise(int x, int y, int z, int seed)
	{
		int num = (x * 73428767) ^ (y * 912931) ^ (z * 42349) ^ (seed * 19937);
		num = (num ^ (num >> 13)) * 1274126177;
		return (float)((num ^ (num >> 16)) & 0x3FF) / 1023f - 0.5f;
	}

	private static void ForEachVoxel(Action<int, int, int> action)
	{
		for (int i = 0; i < 16; i++)
		{
			for (int j = 0; j < 16; j++)
			{
				for (int k = 0; k < 16; k++)
				{
					action.Invoke(k, i, j);
				}
			}
		}
	}

	private bool IsSolidUnusedBox(int x1, int x2, int y1, int y2, int z1, int z2, bool[] used)
	{
		for (int i = y1; i < y2; i++)
		{
			for (int j = z1; j < z2; j++)
			{
				for (int k = x1; k < x2; k++)
				{
					int num = Index(k, i, j);
					if (!solid[num] || used[num])
					{
						return false;
					}
				}
			}
		}
		return true;
	}

	private bool HasRemovedVoxel(int x1, int x2, int y1, int y2, int z1, int z2)
	{
		for (int i = y1; i < y2; i++)
		{
			for (int j = z1; j < z2; j++)
			{
				for (int k = x1; k < x2; k++)
				{
					if (!solid[Index(k, i, j)])
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	private static void MarkUsed(int x1, int x2, int y1, int y2, int z1, int z2, bool[] used)
	{
		for (int i = y1; i < y2; i++)
		{
			for (int j = z1; j < z2; j++)
			{
				for (int k = x1; k < x2; k++)
				{
					used[Index(k, i, j)] = true;
				}
			}
		}
	}

	private static int Index(int x, int y, int z)
	{
		return x + y * 16 + z * 16 * 16;
	}
}
