using BaseLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MobUtils.TileEntities;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static BaseLib.Utility.Utility;

namespace MobUtils.Tiles
{
	public class Grinder : ModTile
	{
		public override bool Autoload(ref string name, ref string texture)
		{
			texture = MobUtils.TileTexturePath + "Grinder";
			return base.Autoload(ref name, ref texture);
		}

		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = false;
			Main.tileLavaDeath[Type] = false;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
			TileObjectData.newTile.StyleWrapLimit = 2;
			TileObjectData.newTile.StyleMultiplier = 2;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16 };
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(mod.GetTileEntity<TEGrinder>().Hook_AfterPlacement, -1, 0, false);
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
			TileObjectData.addAlternate(1);
			TileObjectData.addTile(Type);
			disableSmartCursor = true;

			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Grinder");
			AddMapEntry(Color.Red, name);
		}

		public override void RightClick(int i, int j)
		{
			int ID = mod.GetID<TEGrinder>(i, j);
			if (ID == -1) return;

			TEGrinder grinder = (TEGrinder)TileEntity.ByID[ID];

			if (HasWrench)
			{
				grinder.active = !grinder.active;
				Main.NewText($"Grinder has been {(grinder.active ? "activated" : "deactivated")}.");
			}
			else
			{
				Main.NewText(grinder.GetEnergyStored() + "/" + grinder.GetMaxEnergyStored());
			}

			grinder.SendUpdate();
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 32, mod.ItemType<Items.Grinder>());
			mod.GetTileEntity<TEGrinder>().Kill(i, j);
		}

		public override void MouseOver(int i, int j) => mod.DrawInfo<TEGrinder>(i, j);

		public override void MouseOverFar(int i, int j) => mod.DrawInfo<TEGrinder>(i, j);

		public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
		{
			Main.specX[nextSpecialDrawIndex] = i;
			Main.specY[nextSpecialDrawIndex] = j;
			nextSpecialDrawIndex++;
		}

		public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
		{
			int ID = mod.GetID<TEGrinder>(i, j);
			if (ID == -1) return;

			Tile tile = Main.tile[i, j];
			if (tile.TopLeft())
			{
				TEGrinder grinder = (TEGrinder)TileEntity.ByID[ID];

				TileObjectDirection dir = GetDirection(i, j, mod.TileType<Grinder>());
				int startX = dir == TileObjectDirection.PlaceRight ? i + 2 : i - grinder.range / 16 - 1;
				int endX = dir == TileObjectDirection.PlaceRight ? i + 2 + grinder.range / 16 : i - 1;

				if (grinder.drawInfo) spriteBatch.DrawOutline(new Point16(startX, j + 1 - grinder.range / 16), new Point16(endX, j + 1), Color.Goldenrod, 2, true);
				grinder.drawInfo = false;
			}
		}
	}
}