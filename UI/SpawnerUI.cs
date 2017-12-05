using MobUtils.TileEntities;
using Terraria.ModLoader;
using TheOneLibrary.Base.UI;
using TheOneLibrary.UI.Elements;
using TheOneLibrary.Utility;

namespace MobUtils.UI
{
	public class SpawnerUI : BaseUI, ITileEntityUI
	{
		public TESpawner spawner;

		public UIContainerSlot slot;

		public override void OnInitialize()
		{
			panelMain.Width.Pixels = 84;
			panelMain.Height.Pixels = 64;
			panelMain.Center();
			panelMain.BackgroundColor = panelColor;
			panelMain.SetPadding(0);
			panelMain.OnMouseDown += DragStart;
			panelMain.OnMouseUp += DragEnd;
			base.Append(panelMain);

			slot = new UIContainerSlot(spawner);
			slot.Left.Pixels = 8;
			slot.Top.Pixels = 8;
			panelMain.Append(slot);
		}

		public override void Load()
		{

		}

		public void SetTileEntity(ModTileEntity tileEntity) => spawner = (TESpawner)tileEntity;
	}
}