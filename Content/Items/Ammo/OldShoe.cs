using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace CaseyAdventure.Content.Items.Ammo
{
	public class OldShoe : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Spinning Shoe");
		}

		public override void SetDefaults()
		{
			Item.width = 14;
			Item.height = 14;

			Item.damage = 8;
			Item.DamageType = DamageClass.Ranged;

			Item.maxStack = 999;
			Item.consumable = true;
			Item.knockBack = 2f;
			Item.value = Item.sellPrice(0, 0, 1, 0);
			Item.rare = ItemRarityID.Yellow;
			Item.shoot = ModContent.ProjectileType<Projectiles.ShoeProjectile>();

			Item.ammo = Item.type; 
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.OldShoe, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
