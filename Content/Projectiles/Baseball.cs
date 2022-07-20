using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using CaseyAdventure.Content.Dusts;
using Terraria.DataStructures;
using System;
using System.Linq;
using CaseyAdventure.Content.Items;
namespace CaseyAdventure.Content.Projectiles
{
    public class Baseball : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Baseball");
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = 1;
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 100;
            Projectile.timeLeft = 1000;
            Projectile.alpha = 255;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
            Projectile.scale = 1f;
            AIType = ProjectileID.Bullet;

        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            else
            {
                Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

                // If the projectile hits the left or right side of the tile, reverse the X velocity

                if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }

                // If the projectile hits the top or bottom side of the tile, reverse the Y velocity
                if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
            }

            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.velocity.X = -Projectile.velocity.X;
            Projectile.velocity.Y = -Projectile.velocity.Y;
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public override void AI()
        {
            int rdm = Main.rand.Next(1, 100);
            Projectile.rotation += Projectile.velocity.ToRotation() * rdm;

            Player player = Main.player.FirstOrDefault();
            int distanceFromPlayerX = (int)(Projectile.position.X - player.position.X);
            int distanceFromPlayerY = (int)(Projectile.position.Y - player.position.Y);
            Projectile.velocity.X -= 1 / Projectile.timeLeft;
            if (IsCloseToPlayer(player, distanceFromPlayerX, distanceFromPlayerY))
                {
                if (IsSwingingWithBat(player,Projectile))
                {
                    Projectile.velocity.X *= (float)-1.0001;
                    Projectile.velocity.Y *= (float)-1.0001;
                    Projectile.timeLeft += 10;
                    Projectile.damage += 5;
                }
            }
        }

        private bool IsSwingingWithBat(Player player, Projectile projectile)
        {
            if (player.ItemAnimationActive && projectile.timeLeft < 900 && player.HeldItem.type == ModContent.ItemType<BaseballBat>())
            {
                return true;
            }
            return false;
        }

        private bool IsCloseToPlayer(Player player, int distanceFromPlayerX, int distanceFromPlayerY)
        {
            if (distanceFromPlayerX < 20 && distanceFromPlayerX > -20 && distanceFromPlayerY < 20 && distanceFromPlayerY > -20)
            {
                return true;
            }
            return false;
        }
    }
}