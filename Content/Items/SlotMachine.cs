using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using CaseyAdventure.Content.Items.Ammo;

namespace CaseyAdventure.Content.Items
{
    public class SlotMachine : Terraria.ModLoader.ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Starter"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Slot machine, Shoots 1 to 20 projectiles using casino coins");
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 10;
            Item.autoReuse = true;
            Item.damage = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 0;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Yellow;
            Item.shootSpeed = 10f;
            Item.useAnimation = 20;
            Item.useTime = 20; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Guitar;
            Item.value = Item.buyPrice(gold: 1);
            Item.shoot = ModContent.ProjectileType<Projectiles.CustomCoins>();
            Item.useAmmo = ModContent.ItemType<CustomCoinAmmo>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IronBar, 6);
            recipe.AddIngredient(ItemID.GoldBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            Recipe recipeLead = CreateRecipe();
            recipeLead.AddIngredient(ItemID.LeadBar, 6);
            recipe.AddIngredient(ItemID.PlatinumBar, 10);
            recipeLead.AddTile(TileID.Anvils);
            recipeLead.Register();
        }
        public override bool? UseItem(Player player)
        {
            if (player.whoAmI != Main.LocalPlayer.whoAmI)
            {
                return true;
            }

            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int slotRollAmount = WeaponFunctions.SpinSlotMachine();
            for (int i = 0; i < slotRollAmount; i++)
            {
                Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(12));
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, Main.myPlayer);
            }
            return true;
        }
    }
}