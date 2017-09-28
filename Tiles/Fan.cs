using BaseLib.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MobUtils.TileEntities;
using MobUtils.UI;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static BaseLib.Utility.Utility;

namespace MobUtils.Tiles
{
	public class Fan : ModTile
	{
		public override bool Autoload(ref string name, ref string texture)
		{
			texture = MobUtils.TileTexturePath + "Fan";
			return base.Autoload(ref name, ref texture);
		}

		public override void SetDefaults()
		{
			Main.tileSolidTop[Type] = false;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = false;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.StyleMultiplier = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(mod.GetTileEntity<TEFan>().Hook_AfterPlacement, -1, 0, false);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(1);
			TileObjectData.addTile(Type);
			disableSmartCursor = true;

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Fan");
			AddMapEntry(Color.Yellow, name);
		}

		public override void RightClick(int i, int j)
		{
			int ID = mod.GetID<TEFan>(i, j);
			if (ID == -1) return;

			MobUtils.Instance.HandleUI<FanUI>(ID);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			int ID = mod.GetID<TEFan>(i, j);
			if (ID != -1) MobUtils.Instance.CloseUI(ID);

			Item.NewItem(i * 16, j * 16, 32, 32, mod.ItemType<Items.Fan>());
			mod.GetTileEntity<TEFan>().Kill(i, j);
		}

		public override void MouseOver(int i, int j) => mod.DrawInfo<TEFan>(i, j);

		public override void MouseOverFar(int i, int j) => mod.DrawInfo<TEFan>(i, j);

		public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
		{
			Main.specX[nextSpecialDrawIndex] = i;
			Main.specY[nextSpecialDrawIndex] = j;
			nextSpecialDrawIndex++;
		}

		public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
		{
			int ID = mod.GetID<TEFan>(i, j);
			if (ID == -1) return;

			Tile tile = Main.tile[i, j];
			if (tile.TopLeft())
			{
				TEFan fan = (TEFan)TileEntity.ByID[ID];

				TileObjectDirection dir = GetDirection(i, j, mod.TileType<Fan>());
				int startX = dir == TileObjectDirection.PlaceRight ? i + 2 : i - fan.rangeX / 16;
				int endX = dir == TileObjectDirection.PlaceRight ? i + 1 + fan.rangeX / 16 : i - 1;

				if (fan.drawInfo) spriteBatch.DrawOutline(new Point16(startX, j + 2 - fan.rangeY / 16), new Point16(endX, j + 2), Color.Goldenrod, 2, true);
				fan.drawInfo = false;
			}
		}
	}
}