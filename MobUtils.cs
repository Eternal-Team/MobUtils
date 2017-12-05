using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.UI;
using TheOneLibrary.Base;
using TheOneLibrary.Base.UI;
using TheOneLibrary.Utility;

namespace MobUtils
{
	public class MobUtils : Mod
	{
		public static MobUtils Instance;

		public const string ItemTexturePath = "MobUtils/Textures/Items/";
		public const string ProjTexturePath = "MobUtils/Textures/Projectiles/";
		public const string TileTexturePath = "MobUtils/Textures/Tiles/";
		public const string BuffTexturePath = "MobUtils/Textures/Buffs/";
		public const string UITexturePath = "MobUtils/Textures/UI/";
		public const string Placeholder = "MobUtils/Textures/Placeholder";

		[UI("TileEntity")]
		public static IDictionary<ModTileEntity, GUI> TEUI = new Dictionary<ModTileEntity, GUI>();

		public MobUtils()
		{
			Properties = new ModProperties
			{
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true
			};
		}

		public override void PreSaveAndQuit()
		{
			TEUI.Clear();
		}

		public override void Load()
		{
			Instance = this;
		}

		public override void Unload()
		{
			Instance = null;
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int MouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));

			if (MouseTextIndex != -1)
			{
				layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
				"MobUtils: UI",
				delegate
				{
					TEUI.Values.Draw();

					return true;
				},
				InterfaceScaleType.UI)
			);
			}
		}
	}
}