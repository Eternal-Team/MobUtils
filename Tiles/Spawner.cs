using Microsoft.Xna.Framework;
using MobUtils.TileEntities;
using MobUtils.UI;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;
using TheOneLibrary.Base;
using TheOneLibrary.Utility;

namespace MobUtils.Tiles
{
	public class Spawner : BaseTile
	{
		public override bool Autoload(ref string name, ref string texture)
		{
			texture = MobUtils.TileTexturePath + "Spawner";
			return base.Autoload(ref name, ref texture);
		}

		public override void SetDefaults()
		{
			Main.tileSolidTop[Type] = false;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = false;
			Main.tileLavaDeath[Type] = false;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16 };
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(mod.GetTileEntity<TESpawner>().Hook_AfterPlacement, -1, 0, false);
			TileObjectData.addTile(Type);
			disableSmartCursor = true;

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Spawner");
			AddMapEntry(Color.Black, name);
		}

		public override void RightClick(int i, int j)
		{
			int ID = mod.GetID<TESpawner>(i, j);
			if (ID == -1) return;

			MobUtils.Instance.HandleUI<SpawnerUI>(ID);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			int ID = mod.GetID<TESpawner>(i, j);

			MobUtils.Instance.HandleUI<SpawnerUI>(ID);

			Item.NewItem(i * 16, j * 16, 32, 32, mod.ItemType<Items.Spawner>());
			mod.GetTileEntity<TESpawner>().Kill(i, j);
		}
	}
}