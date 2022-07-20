using CaseyAdventure.Content.Dusts;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CaseyAdventure.Content.Items
{
    class CanOfAir : Terraria.ModLoader.ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Air Can");
            Tooltip.SetDefault("shoots air at your enemies.");
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<AirDust>());
        }
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 5;

            Item.autoReuse = true;
            Item.damage = 2;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 4f;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Yellow;
            Item.shootSpeed = 2f;
            Item.useAnimation = 5;
            Item.useTime = 5; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.RaiseLamp;
            Item.value = Item.buyPrice(gold: 1);
            Item.shoot = ModContent.ProjectileType<Projectiles.AirDustProjectile>();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int rdm = Main.rand.Next(1, 10);
            for (int i = 0; i < rdm; i++)
            {
                // Credit to https://forums.terraria.org/index.php?threads/tutorial-tmodloader-projectile-help.68337/
                // User: AwesomePerson159 (great name btw)
                // I would be so lost without this
                Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(12)); //12 is the spread in degrees, although like with Set Spread it's technically a 24 degree spread due to the fact that it's randomly between 12 degrees above and 12 degrees below your cursor.

                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback);
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IronBar, 8);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}