using BaseLib;
using ContainerLib2;
using ContainerLib2.Container;
using EnergyLib.Energy;
using Microsoft.Xna.Framework;
using MobUtils.Items;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static BaseLib.Utility.Utility;
using Spawner = MobUtils.Tiles.Spawner;

namespace MobUtils.TileEntities
{
	public class TESpawner : BaseTE, IContainerTile, IEnergyReceiver
	{
		public bool active = true;

		public IList<Item> Items = new List<Item>();
		public EnergyStorage energy = new EnergyStorage(100000);

		public override bool ValidTile(Tile tile) => tile.type == mod.TileType<Spawner>() && tile.TopLeft();

		public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
		{
			if (Main.netMode != NetmodeID.MultiplayerClient) Place(i, j - 1);

			NetMessage.SendTileSquare(Main.myPlayer, i, j - 1, 2);
			NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i, j - 1, Type);
			return -1;
		}

		public override void OnPlace()
		{
			Items.Add(new Item());
		}

		public override void OnNetPlace() => OnPlace();

		public override void OnKill()
		{
			this.DropItems(new Rectangle(Position.X * 16, Position.Y * 16, 32, 32));
		}

		private int timer;
		public override void Update()
		{
			if (active && Items[0].type == mod.ItemType<EssenceVial>())
			{
				if (++timer >= 300)
				{
					EssenceVial vial = (EssenceVial)Items[0].modItem;

					for (int i = 0; i < Main.rand.Next(1, 5); i++)
					{
						NPC npc = vial.npc;

						Rectangle spawnBox = new Rectangle(Position.X * 16 - 128, Position.Y * 16 + 128, 320, 160);
						Vector2 position = spawnBox.TopLeft() + new Vector2(Main.rand.Next(spawnBox.Width) - npc.width / 2f, Main.rand.Next(spawnBox.Height) - npc.height / 2f);

						Utils.PoofOfSmoke(position);
						NPC.NewNPC((int)position.X, (int)position.Y, npc.type);
					}

					timer = 0;
				}
			}

			this.HandleUIFar();
		}

		public override TagCompound Save() => new TagCompound
		{
			["Items"] = Items.Save(),
			["Active"] = active
		};

		public override void Load(TagCompound tag)
		{
			Items = ContainerLib2.Utility.Load(tag);
			active = tag.GetBool("Active");
		}

		public override void NetSend(BinaryWriter writer, bool lightSend)
		{
			TagIO.Write(Save(), writer);
			writer.Write(timer);
		}

		public override void NetReceive(BinaryReader reader, bool lightReceive)
		{
			Load(TagIO.Read(reader));
			timer = reader.ReadInt32();
		}

		public IList<Item> GetItems() => Items;

		public Item GetItem(int slot) => Items[slot];

		public void SetItem(Item item, int slot) => Items[slot] = item;

		public ModTileEntity GetTileEntity() => this;

		public long GetEnergyStored() => energy.GetEnergyStored();

		public long GetMaxEnergyStored() => energy.GetCapacity();

		public EnergyStorage GetEnergyStorage() => energy;

		public long ReceiveEnergy(long maxReceive) => energy.ReceiveEnergy(maxReceive);
	}
}