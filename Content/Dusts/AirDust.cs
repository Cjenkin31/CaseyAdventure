using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CaseyAdventure.Content.Dusts
{
	public class AirDust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;
			dust.frame = new Rectangle(0, 0, 30, 30);
		}

		public override bool Update(Dust dust)
		{
			dust.scale -= 0.05f;
			if (dust.customData != null && dust.customData is Player player)
			{
				dust.position = player.Center * dust.scale * 5;
			}
			if (dust.scale < 0.25f)
				dust.active = false;

			return false;
		}
	}
}
