using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace CaseyAdventure.Content.Items.Ammo
{
	public class CustomCoinAmmo : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Slot Machine Coin");
			Tooltip.SetDefault("Slot Machine Coins");
		}

		public override void SetDefaults()
		{
			Item.width = 14;
			Item.height = 14;

			Item.damage = 8;
			Item.DamageType = DamageClass.Ranged;

			Item.maxStack = 9999;
			Item.consumable = true;
			Item.knockBack = 2f;
			Item.value = Item.sellPrice(0, 0, 0, 50);
			Item.rare = ItemRarityID.Yellow;

			Item.ammo = Item.type;
		}
		public override void AddRecipes()
		{
			Recipe copperCoinRecipe = CreateRecipe(1);
			copperCoinRecipe.AddIngredient(ItemID.CopperCoin, 20);
			copperCoinRecipe.AddTile(TileID.WorkBenches);
			copperCoinRecipe.Register();

			Recipe silverCoinRecipe = CreateRecipe(5);
			silverCoinRecipe.AddIngredient(ItemID.SilverCoin, 1);
			silverCoinRecipe.AddTile(TileID.WorkBenches);
			silverCoinRecipe.Register();

			Recipe goldCoinRecipe = CreateRecipe(500);
			goldCoinRecipe.AddIngredient(ItemID.GoldCoin, 1);
			goldCoinRecipe.AddTile(TileID.WorkBenches);
			goldCoinRecipe.Register();
		}

	}
}
