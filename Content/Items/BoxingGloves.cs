using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CaseyAdventure.Content.Dusts;
using Terraria.Audio;

namespace CaseyAdventure.Content.Items
{
    public class BoxingGloves : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boxing Gloves");
            Tooltip.SetDefault("Pawnch");
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<AirDust>());
        }
        public override void SetDefaults()
        {

            Item.damage = 5;
            Item.DamageType = DamageClass.Melee;
            Item.width = 10;
            Item.useStyle = ItemUseStyleID.MowTheLawn;
            // Item.useStyle = ItemUseStyleID.DrinkLong; Uppercuts
            Item.height = 10;
            Item.useTime = 4;
            Item.useTurn = true;
            Item.useAnimation = 4;
            Item.holdStyle = ItemHoldStyleID.HoldGuitar;
            Item.knockBack = 1;
            Item.value = 10000;
            Item.rare = 2;
            Item.UseSound = new SoundStyle($"{nameof(CaseyAdventure)}/Content/Sounds/Frankie_Punch")
            {
                Volume = 0.5f,
                MaxInstances = 5,
            };
            Item.autoReuse = true;

        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Silk, 2);
            recipe.AddIngredient(ItemID.SandBlock, 20);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}