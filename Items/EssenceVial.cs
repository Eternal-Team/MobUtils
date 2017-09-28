using BaseLib.Items;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace MobUtils.Items
{
	public class EssenceVial : BaseItem
	{
		public override string Texture => MobUtils.ItemTexturePath + "EssenceVial";

		public override bool CloneNewInstances => false;

		public override ModItem Clone()
		{
			EssenceVial clone = (EssenceVial)base.Clone();
			clone.npc = npc;
			return base.Clone();
		}

		public NPC npc = new NPC();

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Essence Vial");
		}

		public override void SetDefaults()
		{
			item.width = 40;
			item.height = 40;
			item.useTime = 20;
			item.useAnimation = 20;
			item.maxStack = 1;
			item.useStyle = 1;
			item.UseSound = SoundID.Item1;
			item.value = Item.sellPrice(0, 0, 50, 0);
		}

		public override bool UseItem(Player player)
		{
			// capture a npc
			//if (soulData.type == 0)
			//{
			//	foreach (NPC npc in Main.npc)
			//	{
			//		Rectangle npcRect = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);

			//		if (npcRect.Contains((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y))
			//		{
			//			soulData.type = npc.type;
			//			item.SetNameOverride($"Mob Soul ({Lang.GetNPCName(npc.netID)})");
			//			Main.NewTextMultiline($"This Mob Soul has been tagged to {Lang.GetNPCName(npc.netID)}\nKill NPCs to increase the tier", c: Color.Goldenrod);
			//		}
			//	}
			//	return true;
			//}
			//return false;
			return false;
		}

		public override TagCompound Save()
		{
			return new TagCompound
			{
				["Type"] = npc.type
			};
		}

		public override void Load(TagCompound tag)
		{
			npc = new NPC();
			npc.SetDefaults(tag.GetInt("Type"));
			item.SetNameOverride($"Essence Vial {npc.GivenName}");
		}

		public override void NetSend(BinaryWriter writer) => TagIO.Write(Save(), writer);

		public override void NetRecieve(BinaryReader reader) => Load(TagIO.Read(reader));

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Diamond);
			recipe.AddIngredient(ItemID.PlatinumBar);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}