using BaseLib.Items;
using Terraria;

namespace MobUtils.Items
{
	public class Wrench : BaseItem, IWrench
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wrench");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.value = Item.sellPrice(0, 0, 50, 0);
			item.rare = 6;
		}
	}
}