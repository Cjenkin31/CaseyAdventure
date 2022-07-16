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
			// If our texture had 3 different dust on top of each other (a 30x90 pixel image), we might do this:
			// dust.frame = new Rectangle(0, Main.rand.Next(3) * 30, 30, 30);
		}

		public override bool Update(Dust dust)
		{
			// Here we rotate and scale down the dust. The dustIndex % 2 == 0 part lets half the dust rotate clockwise and the other half counter clockwise
			dust.scale -= 0.05f;

			// Here we use the customData field. If customData is the type we expect, Player, we do some special movement.
			if (dust.customData != null && dust.customData is Player player)
			{
				// Here we assign position to some offset from the player that was assigned. This offset scales with dust.scale. The scale and rotation cause the spiral movement we desired.
				dust.position = player.Center * dust.scale * 5;
			}

			// Here we make sure to kill any dust that get really small.
			if (dust.scale < 0.25f)
				dust.active = false;

			return false;
		}
	}
}
