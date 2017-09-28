using BaseLib.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MobUtils.Items
{
	public class Grinder : BaseItem
	{
		public override string Texture => MobUtils.ItemTexturePath + "Grinder";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Grinder");
			Tooltip.SetDefault("Kills NPCs");
		}

		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 16;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.createTile = mod.TileType<Tiles.Grinder>();
			item.value = Item.sellPrice(0, 0, 50, 0);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Diamond, 4);
			recipe.AddIngredient(ItemID.PlatinumBar, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}