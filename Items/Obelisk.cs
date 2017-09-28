using BaseLib.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MobUtils.Items
{
	public class Obelisk : BaseItem
	{
		public override string Texture => MobUtils.ItemTexturePath + "Obelisk";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Obelisk");
			Tooltip.SetDefault("Pulls/Pushes NPCs");
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
			item.createTile = mod.TileType<Tiles.Obelisk>();
			item.value = Item.sellPrice(0, 0, 75, 0);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.PlatinumBar, 20);
			recipe.AddIngredient(ItemID.Ruby, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}