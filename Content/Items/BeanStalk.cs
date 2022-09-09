using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CaseyAdventure.Content.Items
{
    class BeanStalk : Terraria.ModLoader.ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bean Stalk");
            Tooltip.SetDefault("Grows a vine rope to the sky border, or to a sky island\nCurrently set at one for convenience.");
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 1;
            Item.useTurn = true;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI != Main.LocalPlayer.whoAmI)
            {
                return true;
            }

            Vector2 playerPosition = player.position;
            Vector2 mousePosition = Main.MouseWorld;
            int tileX = (int)(mousePosition.X / 16f);
            int tileY = (int)(mousePosition.Y / 16f);
            int playerX = (int)(playerPosition.X / 16f);
            int playerY = (int)(playerPosition.Y / 16f);
            int diffX = playerX - tileX;
            int diffY = playerY - tileY;
            if (diffX >= -5 && diffX <= 5 && diffY >= -5 && diffY <= 5)
            {
                if (ActionFunctions.BuildSkyRope(tileX, tileY))
                {
                    return true;
                }
            }

            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.VineRope, 100);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}