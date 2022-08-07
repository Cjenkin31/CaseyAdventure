using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CaseyAdventure.Content.Projectiles;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
namespace CaseyAdventure.Content.Items
{
    public class BaseballBat : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Baseball Bat");
            Tooltip.SetDefault("Shoots a baseball that you can reflect to deal more damage.");
        }

        public override void SetDefaults()
        {
            Item.damage = 50;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 60;
            Item.useAnimation = 20;
            Item.useStyle = 1;
            Item.knockBack = 6;
            Item.value = 10000;
            Item.rare = 2;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Baseball>();
            Item.shootSpeed = 5;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bone, 20);
            recipe.AddIngredient(ItemID.SandBlock, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}