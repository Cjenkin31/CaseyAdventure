using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
namespace CaseyAdventure.Content.Projectiles
{
	public class AirDustProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("DUST AIR?");
		}

		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.aiStyle = 1;
			Projectile.friendly = true; // Can the projectile deal damage to enemies?
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 5;
			Projectile.timeLeft = 200;
			Projectile.alpha = 255;
			Projectile.light = 0.5f;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			Projectile.extraUpdates = 1;

			AIType = ProjectileID.Bullet;
			
		}
	}
}