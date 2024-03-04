using MoarOrca.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace MoarOrca.Content.Items.Weapons
{
	public class EOMG : ModItem
	{
		public override void SetDefaults() {
			Item.width = 58;
			Item.height = 28;
			Item.scale = 1f;
			Item.rare = ItemRarityID.Red;
			
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 82;
			Item.knockBack = 5f; 
			Item.noMelee = true;

			Item.useTime = 8;
			Item.useAnimation = 8;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.autoReuse = true;
			Item.UseSound = SoundID.Item21;

			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 6f;
			Item.useAmmo = AmmoID.Bullet;
		}
		
		public override bool CanConsumeAmmo(Item ammo, Player player) {
			return Main.rand.NextFloat() >= 0.50f;
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			type = ModContent.ProjectileType<EOMGProjectile>();		
			velocity = velocity.RotatedByRandom(MathHelper.ToRadians(10));
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-6f, -2f);
		}
	}
}
