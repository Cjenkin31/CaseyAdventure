using CaseyAdventure.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;

namespace CaseyAdventure.Content.Items
{
    public class MonkieVision : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Monkey Vision");
            Tooltip.SetDefault("Pew pew pew");
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 5;

            Item.autoReuse = true;
            Item.damage = 17;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 4f;
            Item.mana = 4;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Yellow;
            Item.shootSpeed = 10f;
            Item.useAnimation = 5;
            Item.useTime = 5; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.buyPrice(gold: 1);
            Item.shoot = ModContent.ProjectileType<Projectiles.MonkeyLaser>();
            Item.holdStyle = ItemHoldStyleID.HoldGolfClub;
        }
        public override void HoldStyle(Player player, Rectangle heldItemFrame)
        {

            player.itemLocation.X = player.Center.X - 25;
            player.itemLocation.Y = player.position.Y - 13 + 21f - 3f * player.gravDir + player.mount.PlayerOffsetHitbox;
            player.itemRotation = 0f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-9f, -10f);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Sunglasses, 1);
            recipe.AddIngredient(ItemID.MeteoriteBar, 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }

}