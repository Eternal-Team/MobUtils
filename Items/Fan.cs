using BaseLib.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MobUtils.Items
{
	public class Fan : BaseItem
	{
		public override string Texture => MobUtils.ItemTexturePath + "Fan";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fan");
			Tooltip.SetDefault("Pushes NPCs and Items around");
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
			item.createTile = mod.TileType<Tiles.Fan>();
			item.value = Item.sellPrice(0, 0, 50, 0);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IronBar, 4);
			recipe.AddIngredient(ItemID.Wood, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.anyIronBar = true;
			recipe.anyWood = true;
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}