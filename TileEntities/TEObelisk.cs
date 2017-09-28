using BaseLib;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using MobUtils.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader.IO;
using static BaseLib.Utility.Utility;

namespace MobUtils.TileEntities
{
	public class TEObelisk : BaseTE
	{
		public static int MaxRange = 640;

		public int rangeX = 160;
		public int rangeY = 160;
		public int speed = 20;

		public override bool ValidTile(Tile tile) => tile.active() && tile.type == mod.TileType<Obelisk>() && tile.TopLeft();

		public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
		{
			if (Main.netMode != NetmodeID.MultiplayerClient) return Place(i, j - 1);

			NetMessage.SendTileSquare(Main.myPlayer, i, j - 1, 2);
			NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i, j - 1, Type);
			return -1;
		}

		public override void Update()
		{
			for (int i = 0; i < Main.npc.Length; i++)
			{
				if (Main.npc[i].active && !Main.npc[i].friendly)
				{
					NPC npc = Main.npc[i];

					Rectangle npcHitbox = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
					Rectangle obeliskBox = new Rectangle(Position.X * 16 + 16 - rangeX, Position.Y * 16 + 16 - rangeY, rangeX * 2, rangeY * 2);

					if (npcHitbox.Intersects(obeliskBox))
					{
						Vector2 newMove = new Vector2(Position.X * 16 + 16, Position.Y * 16 + 16) - npc.position;

						AdjustMagnitude(ref newMove);
						npc.velocity = (npc.velocity + newMove) * new Vector2((float)speed / 60f);
						AdjustMagnitude(ref npc.velocity);
					}
				}
			}

			this.HandleUIFar();
		}

		public static void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > 6f) vector *= 6f / magnitude;
		}

		public override TagCompound Save() => new TagCompound
		{
			["RangeX"] = rangeX,
			["RangeY"] = rangeY,
			["Speed"] = speed
		};

		public override void Load(TagCompound tag)
		{
			rangeX = tag.GetInt("RangeX");
			rangeY = tag.GetInt("RangeY");
			speed = tag.GetInt("Speed");
		}

		public override void NetSend(BinaryWriter writer, bool lightSend) => TagIO.Write(Save(), writer);

		public override void NetReceive(BinaryReader reader, bool lightReceive) => Load(TagIO.Read(reader));
	}
}