using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheOneLibrary.Base.Items;

namespace MobUtils.Items
{
	public class Spawner : BaseItem
	{
		public override string Texture => MobUtils.ItemTexturePath + "Spawner";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spawner");
			Tooltip.SetDefault("Spawns NPCs");
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
			item.createTile = mod.TileType<Tiles.Spawner>();
			item.value = Item.sellPrice(0, 1, 0, 0);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Diamond);
			recipe.AddIngredient(ItemID.PlatinumBar, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}