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
using CaseyAdventure.Content.Items.Ammo;
namespace CaseyAdventure.Content.Items
{
    class ShoeGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shoe Gun");
            Tooltip.SetDefault("shoots your ditry shoes at your enemies.");
        }
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 5;

            Item.autoReuse = true;
            Item.damage = 15;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 4f;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Yellow;
            Item.shootSpeed = 10f;
            Item.useAnimation = 20;
            Item.useTime = 20; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.buyPrice(gold: 1);
            Item.shoot = ModContent.ProjectileType<Projectiles.ShoeProjectile>();
            Item.useAmmo = ModContent.ItemType<OldShoe>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IronBar, 6);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
