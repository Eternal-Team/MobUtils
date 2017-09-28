using BaseLib.UI;
using BaseLib.Utility;
using MobUtils.TileEntities;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;

namespace MobUtils.UI
{
	public class FanUI : BaseUI, TileEntityUI
	{
		public TEFan fan;

		//public UIOption rangeX;
		//public UIOption rangeY;
		//public UIOption speed;

		public override void OnInitialize()
		{
			panelMain = new UIPanel();
			panelMain.Width.Precent = 0.2f;
			panelMain.Height.Pixels = 92;
			panelMain.Center();
			panelMain.SetPadding(0);
			panelMain.OnMouseDown += DragStart;
			panelMain.OnMouseUp += DragEnd;
			panelMain.BackgroundColor = panelColor;
			base.Append(panelMain);

			//rangeX = new UIOption("Range (X): ", 16, 0, TEFan.MaxRange);
			//rangeX.Width.Set(mainPanel.Width.Pixels - 16f, 0f);
			//rangeX.Height.Set(20f, 0f);
			//rangeX.Left.Set(8f, 0f);
			//rangeX.Top.Set(8f, 0f);
			//rangeX.OnChange += () =>
			//{
			//	((TEFan)TileEntity.ByID[ID]).rangeX = rangeX.Value;
			//};
			//mainPanel.Append(rangeX);

			//rangeY = new UIOption("Range (Y): ", 16, 0, TEFan.MaxRange);
			//rangeY.Width.Set(mainPanel.Width.Pixels - 16f, 0f);
			//rangeY.Height.Set(20f, 0f);
			//rangeY.Left.Set(8f, 0f);
			//rangeY.Top.Set(36f, 0f);
			//rangeY.OnChange += () =>
			//{
			//	((TEFan)TileEntity.ByID[ID]).rangeY = rangeY.Value;
			//};
			//mainPanel.Append(rangeY);

			//speed = new UIOption("Speed: ", 5, 0, 60);
			//speed.Width.Set(mainPanel.Width.Pixels - 16f, 0f);
			//speed.Height.Set(20f, 0f);
			//speed.Left.Set(8f, 0f);
			//speed.Top.Set(64f, 0f);
			//speed.OnChange += () =>
			//{
			//	((TEFan)TileEntity.ByID[ID]).speed = speed.Value;
			//};
			//mainPanel.Append(speed);
		}

		public override void Load()
		{
			//ID = passID;

			//TEFan fan = (TEFan)TileEntity.ByID[ID];
			//rangeX.Value = fan.rangeX;
			//rangeY.Value = fan.rangeY;
			//speed.Value = fan.speed;
		}

		public void SetTileEntity(ModTileEntity tileEntity) => fan = (TEFan)tileEntity;
	}
}