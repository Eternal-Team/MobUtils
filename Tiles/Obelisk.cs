using BaseLib.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MobUtils.TileEntities;
using MobUtils.UI;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace MobUtils.Tiles
{
	public class Obelisk : ModTile
	{
		public override bool Autoload(ref string name, ref string texture)
		{
			texture = MobUtils.TileTexturePath + "Obelisk";
			return base.Autoload(ref name, ref texture);
		}

		public override void SetDefaults()
		{
			Main.tileSolidTop[Type] = false;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = false;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16 };
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(mod.GetTileEntity<TEObelisk>().Hook_AfterPlacement, -1, 0, false);
			TileObjectData.addTile(Type);
			disableSmartCursor = true;

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Obelisk");
			AddMapEntry(Color.Pink, name);
		}

		public override void RightClick(int i, int j)
		{
			int ID = mod.GetID<TEObelisk>(i, j);
			if (ID == -1) return;

			MobUtils.Instance.HandleUI<ObeliskUI>(ID);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			int ID = mod.GetID<TEObelisk>(i, j);
			if (ID != -1) MobUtils.Instance.CloseUI(ID);

			Item.NewItem(i * 16, j * 16, 32, 32, mod.ItemType<Items.Obelisk>());
			mod.GetTileEntity<TEObelisk>().Kill(i, j);
		}

		public override void MouseOver(int i, int j) => mod.DrawInfo<TEObelisk>(i, j);

		public override void MouseOverFar(int i, int j) => mod.DrawInfo<TEObelisk>(i, j);

		public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
		{
			Main.specX[nextSpecialDrawIndex] = i;
			Main.specY[nextSpecialDrawIndex] = j;
			nextSpecialDrawIndex++;
		}

		public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
		{
			int ID = mod.GetID<TEObelisk>(i, j);
			if (ID == -1) return;

			Tile tile = Main.tile[i, j];
			if (tile.TopLeft())
			{
				TEObelisk obelisk = (TEObelisk)TileEntity.ByID[ID];

				if (obelisk.drawInfo) spriteBatch.DrawOutline(new Point16(i + 1 - obelisk.rangeX / 16, j + 1 - obelisk.rangeY / 16), new Point16(i + obelisk.rangeX / 16, j + obelisk.rangeY / 16), Color.Goldenrod, 2, true);
				obelisk.drawInfo = false;
			}
		}
	}
}