using MoarOrca.Content.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace MoarOrca.Content.Projectiles
{
	public class OrcaEnragedProjectile : ModProjectile
	{
		public override void SetStaticDefaults() {
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
			Main.projFrames[Projectile.type] = 2;
		}

		public override void SetDefaults() {
			Projectile.width = 50; 
			Projectile.height = 28;
			Projectile.aiStyle = 0;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.ignoreWater = true;
			Projectile.light = 1f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 600;
			Projectile.alpha = 255;
			Projectile.penetrate = 2;
		}

		public override void AI() {
			float maxDetectRadius = 500f;
			float projSpeed = 12f;
			Projectile.ai[0] += 1f;
			
			Projectile.velocity *= 0.98f;

			NPC closestNPC = FindClosestNPC(maxDetectRadius);
			if (closestNPC == null)
				return;
			
			Projectile.direction = Projectile.spriteDirection = (Projectile.velocity.X > 0f) ? 1 : -1;
			Projectile.rotation = Projectile.velocity.ToRotation();
			Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
			if (Projectile.spriteDirection == -1) {
				Projectile.rotation += MathHelper.Pi;
			}
			if (Projectile.timeLeft <= 300) {
				Projectile.tileCollide = true;
			}
			if (++Projectile.frameCounter >= 5) {
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= Main.projFrames[Projectile.type])
					Projectile.frame = 0;
			}
			if (Projectile.alpha >= 105) {
				Projectile.alpha -= 15;
			}
		}

		public NPC FindClosestNPC(float maxDetectDistance) {
			NPC closestNPC = null;
			float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;
			for (int k = 0; k < Main.maxNPCs; k++) {
				NPC target = Main.npc[k];
				if (target.CanBeChasedBy()) {
					float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);
					if (sqrDistanceToTarget < sqrMaxDetectDistance) {
						sqrMaxDetectDistance = sqrDistanceToTarget;
						closestNPC = target;
					}
					if (sqrDistanceToTarget < 300f) {
						if (Main.rand.NextBool(6)) {
							for (int i = 0; i < 80; i++) {
								Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 3f);
								dust.noGravity = true;
								dust.velocity *= 5f;
								dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 2f);
								dust.velocity *= 3f;
							}
						}
					}
				}
			}
			
			return closestNPC;
		}

		public override void OnKill(int timeLeft) {
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
			for (int i = 0; i < 50; i++) {
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 2f);
				dust.velocity *= 1.4f;
			}
			for (int i = 0; i < 80; i++) {
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 3f);
				dust.noGravity = true;
				dust.velocity *= 5f;
				dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 2f);
				dust.velocity *= 3f;
			}
			for (int g = 0; g < 2; g++) {
				var goreSpawnPosition = new Vector2(Projectile.position.X + Projectile.width / 2 - 24f, Projectile.position.Y + Projectile.height / 2 - 24f);
				Gore gore = Gore.NewGoreDirect(Projectile.GetSource_FromThis(), goreSpawnPosition, default, Main.rand.Next(61, 64), 1f);
				gore.scale = 1.5f;
				gore.velocity.X += 1.5f;
				gore.velocity.Y += 1.5f;
				gore = Gore.NewGoreDirect(Projectile.GetSource_FromThis(), goreSpawnPosition, default, Main.rand.Next(61, 64), 1f);
				gore.scale = 1.5f;
				gore.velocity.X -= 1.5f;
				gore.velocity.Y += 1.5f;
				gore = Gore.NewGoreDirect(Projectile.GetSource_FromThis(), goreSpawnPosition, default, Main.rand.Next(61, 64), 1f);
				gore.scale = 1.5f;
				gore.velocity.X += 1.5f;
				gore.velocity.Y -= 1.5f;
				gore = Gore.NewGoreDirect(Projectile.GetSource_FromThis(), goreSpawnPosition, default, Main.rand.Next(61, 64), 1f);
				gore.scale = 1.5f;
				gore.velocity.X -= 1.5f;
				gore.velocity.Y -= 1.5f;
			}
		}
	}
}
