using BaseLib;
using EnergyLib.Energy;
using Microsoft.Xna.Framework;
using System.IO;
using MobUtils.Tiles;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader.IO;
using static BaseLib.Utility.Utility;

namespace MobUtils.TileEntities
{
	public class TEGrinder : BaseTE, IEnergyReceiver
	{
		public bool active = true;
		public int range = 144;

		public EnergyStorage energyStorage = new EnergyStorage(100000);

		public override bool ValidTile(Tile tile) => tile.active() && tile.type == mod.TileType<Grinder>() && tile.TopLeft();

		public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
		{
			if (Main.netMode != NetmodeID.MultiplayerClient) return Place(i, j - 1);

			NetMessage.SendTileSquare(Main.myPlayer, i, j - 1, 2);
			NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i, j - 1, Type);
			return -1;
		}

		private int timer;
		public override void Update()
		{
			TileObjectDirection dir = GetDirection(Position.X, Position.Y, mod.TileType<Grinder>());

			if (active && ++timer >= 120)
			{
				for (int i = 0; i < Main.npc.Length; i++)
				{
					if (Main.npc[i].active)
					{
						NPC npc = Main.npc[i];

						Rectangle npcHitbox = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
						Rectangle grinderBox = new Rectangle(Position.X * 16 + (dir == TileObjectDirection.PlaceRight ? 32 : -range), Position.Y * 16 + 32 - range, range, range);

						if (npcHitbox.Intersects(grinderBox))
						{
							npc.StrikeNPC(npc.life, 0f, 0, true, true);
							Utils.PoofOfSmoke(npc.Center);
						}
					}
				}
				timer = 0;
			}
		}

		public override TagCompound Save() => new TagCompound
		{
			["Active"] = active,
			["Range"] = range
		};

		public override void Load(TagCompound tag)
		{
			active = tag.GetBool("Active");
			range = tag.GetInt("Range");
		}

		public override void NetSend(BinaryWriter writer, bool lightSend) => TagIO.Write(Save(), writer);

		public override void NetReceive(BinaryReader reader, bool lightReceive) => Load(TagIO.Read(reader));

		public EnergyStorage GetEnergyStorage() => energyStorage;

		public long ReceiveEnergy(long maxReceive) => energyStorage.ReceiveEnergy(maxReceive);

		public long GetEnergyStored() => energyStorage.GetEnergyStored();

		public long GetMaxEnergyStored() => energyStorage.GetCapacity();
	}
}