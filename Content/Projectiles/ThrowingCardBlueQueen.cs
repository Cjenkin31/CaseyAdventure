using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using CaseyAdventure.Content.Dusts;
using Terraria.DataStructures;

namespace CaseyAdventure.Content.Projectiles
{
    public class ThrowingCardBlueQueen : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Blue Queen does the most damage to her enemies");
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = 1;
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 1000;
            Projectile.alpha = 255;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
            Projectile.scale = 2f;
            Projectile.damage = 10;
            AIType = ProjectileID.Bullet;

        }
        public override void AI()
        {
            int rdm = Main.rand.Next(1, 100);
            Projectile.rotation += Projectile.velocity.ToRotation() * rdm;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            int debuff = BuffID.Frostburn;
            if (debuff > 0)
            {
                target.AddBuff(debuff, 200);
            }
        }
    }
}