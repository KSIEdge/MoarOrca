using MoarOrca.Content.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MoarOrca.Content.Projectiles
{
	public class OrcaProjectile : ModProjectile
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
			float projSpeed = 6f;
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
				}
			}

			return closestNPC;
		}

	}
}
