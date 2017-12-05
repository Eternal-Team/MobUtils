using MobUtils.TileEntities;
using Terraria.ModLoader;
using TheOneLibrary.Base.UI;
using TheOneLibrary.Utility;

namespace MobUtils.UI
{
	public class ObeliskUI : BaseUI, ITileEntityUI
	{
		public TEObelisk obelisk;

		public override void OnInitialize()
		{
			panelMain.Width.Precent = 0.2f;
			panelMain.Height.Pixels = 92;
			panelMain.Center();
			panelMain.SetPadding(0);
			panelMain.OnMouseDown += DragStart;
			panelMain.OnMouseUp += DragEnd;
			panelMain.BackgroundColor = panelColor;
			base.Append(panelMain);

			//rangeX = new UIOption("Range (X): ", 32, 0, TEObelisk.MaxRange);
			//rangeX.Width.Set(panelMain.Width.Pixels - 16f, 0f);
			//rangeX.Height.Set(20f, 0f);
			//rangeX.Left.Set(8f, 0f);
			//rangeX.Top.Set(8f, 0f);
			//rangeX.OnChange += () =>
			//{
			//	((TEObelisk)TileEntity.ByID[ID]).rangeX = rangeX.Value;
			//};
			//panelMain.Append(rangeX);

			//rangeY = new UIOption("Range (Y): ", 32, 0, TEObelisk.MaxRange);
			//rangeY.Width.Set(panelMain.Width.Pixels - 16f, 0f);
			//rangeY.Height.Set(20f, 0f);
			//rangeY.Left.Set(8f, 0f);
			//rangeY.Top.Set(36f, 0f);
			//rangeY.OnChange += () =>
			//{
			//	((TEObelisk)TileEntity.ByID[ID]).rangeY = rangeY.Value;
			//};
			//panelMain.Append(rangeY);

			//speed = new UIOption("Speed: ", 1, 5, 60);
			//speed.Width.Set(panelMain.Width.Pixels - 16f, 0f);
			//speed.Height.Set(20f, 0f);
			//speed.Left.Set(8f, 0f);
			//speed.Top.Set(64f, 0f);
			//speed.OnChange += () =>
			//{
			//	((TEObelisk)TileEntity.ByID[ID]).speed = speed.Value;
			//};
			//panelMain.Append(speed);
		}

		public override void Load()
		{
			//ID = passID;

			//TEObelisk obelisk = ((TEObelisk)TileEntity.ByID[ID]);
			//rangeX.Value = obelisk.rangeX;
			//rangeY.Value = obelisk.rangeY;
			//speed.Value = obelisk.speed;
		}

		public void SetTileEntity(ModTileEntity tileEntity) => obelisk = (TEObelisk)tileEntity;
	}
}