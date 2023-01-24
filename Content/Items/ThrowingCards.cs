using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using CaseyAdventure.Content.Items.Ammo;
using System;

namespace CaseyAdventure.Content.Items
{
    public class ThrowingCards : Terraria.ModLoader.ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Throwing Cards"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Throws a red, blue, and yellow card. \nRed leaves a OnFire! effect\nBlue leaves a FrostBurn effect\nYellow leaves a Confused effect\nSmall chance to throw all 3");
        }
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 10;
            Item.autoReuse = true;
            Item.damage = 19;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 5;
            Item.knockBack = 2;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Yellow;
            Item.shootSpeed = 5f;
            Item.useAnimation = 20;
            Item.useTime = 19; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.value = Item.buyPrice(gold: 1);
            Item.shoot = ModContent.ProjectileType<Projectiles.ThrowingCardBlueQueen>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.YellowDye, 1);
            recipe.AddIngredient(ItemID.BlueDye, 1);
            recipe.AddIngredient(ItemID.RedDye, 1);
            recipe.AddIngredient(ItemID.Silk, 15);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Random rnd = new Random();
            int randomNum = rnd.Next(101);
            int timesThrown = 0;

            if (randomNum <= 33)
            {
                Item.shoot = ModContent.ProjectileType<Projectiles.ThrowingCardBlueQueen>();
            }
            else if (randomNum > 33 && randomNum < 66)
            {
                Item.shoot = ModContent.ProjectileType<Projectiles.ThrowingCardRedAce>();
            }
            else if (randomNum >= 66 && randomNum < 100)
            {
                Item.shoot = ModContent.ProjectileType<Projectiles.ThrowingCardYellowKing>();
            }
            else if (randomNum >= 100)
            {
                for (int i = 0; i < 3; i++)
                {
                    switch (i)
                    {
                        case 0:
                            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Projectiles.ThrowingCardYellowKing>(), damage, knockback, Main.myPlayer);
                            break;
                        case 1:
                            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Projectiles.ThrowingCardBlueQueen>(), damage, knockback, Main.myPlayer);
                            break;
                        case 2:
                            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Projectiles.ThrowingCardRedAce>(), damage, knockback, Main.myPlayer);
                            break;
                        default:
                            break;
                    }
                }
                
            }
            
            
            return true;
        }
    }
}