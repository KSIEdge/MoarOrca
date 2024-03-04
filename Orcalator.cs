using MoarOrca.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace MoarOrca.Content.Items.Weapons
{
	public class Orcalator : ModItem
	{
		public override void SetDefaults() {
			Item.width = 58;
			Item.height = 28;
			Item.scale = 1f;
			Item.rare = ItemRarityID.Yellow;
			
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 43;
			Item.knockBack = 4f; 
			Item.noMelee = true;

			Item.useTime = 12;
			Item.useAnimation = 12;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.autoReuse = true;
			Item.UseSound = SoundID.Item21;

			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 6f;
			Item.useAmmo = AmmoID.Bullet;
		}
		
		public override bool CanConsumeAmmo(Item ammo, Player player) {
			return Main.rand.NextFloat() >= 0.33f;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			if (Main.rand.NextBool(6)) {
				Projectile.NewProjectile(source, position, velocity * 2f, ModContent.ProjectileType<OrcaEnragedProjectile>(), damage * 2, knockback * 2f, player.whoAmI); }
			return base.Shoot(player, source, position, velocity, type, damage, knockback);
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			type = ModContent.ProjectileType<OrcaProjectile>();		
			velocity = velocity.RotatedByRandom(MathHelper.ToRadians(10));
		}

		public override Vector2? HoldoutOffset() {
			return new Vector2(-6f, -2f);
		}
	}
}
