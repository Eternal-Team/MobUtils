using Microsoft.Xna.Framework;
using MobUtils.Tiles;
using System.IO;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader.IO;
using TheOneLibrary.Base;
using TheOneLibrary.Utility;

namespace MobUtils.TileEntities
{
	public class TEFan : BaseTE
	{
		public static int MaxRange = 320;

		public int rangeX = 80;
		public int rangeY = 80;
		public int speed = 20;

		public override bool ValidTile(Tile tile) => tile.active() && tile.type == mod.TileType<Fan>() && tile.TopLeft();

		public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
		{
			if (Main.netMode != NetmodeID.MultiplayerClient) return Place(i, j - 1);

			NetMessage.SendTileSquare(Main.myPlayer, i, j - 1, 2);
			NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i, j - 1, Type);
			return -1;
		}

		public override void Update()
		{
			TileObjectDirection dir = TheOneLibrary.Utility.Utility.GetDirection(Position.X, Position.Y, mod.TileType<Fan>());
			Rectangle fanBox = new Rectangle(Position.X * 16 + (dir == TileObjectDirection.PlaceRight ? 32 : -rangeX), Position.Y * 16 + 32 - rangeY, rangeX, rangeY);

			for (int i = 0; i < Main.npc.Length; i++)
			{
				if (Main.npc[i].active)
				{
					NPC npc = Main.npc[i];

					Rectangle npcHitbox = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
					if (npcHitbox.Intersects(fanBox)) npc.velocity.X = (float)(dir == TileObjectDirection.PlaceRight ? speed : -speed) / 60f;
				}
			}

			for (int i = 0; i < Main.item.Length; i++)
			{
				if (Main.item[i].active)
				{
					Item item = Main.item[i];

					Rectangle itemHitbox = new Rectangle((int)item.position.X, (int)item.position.Y, item.width, item.height);
					if (itemHitbox.Intersects(fanBox)) item.velocity.X = (float)(dir == TileObjectDirection.PlaceRight ? speed : -speed) / 60f;
				}
			}

			this.HandleUIFar();
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