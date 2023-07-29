using System;
using Code.Enemy;
using UnityEngine;

namespace Code.Bullet
{
    public class BulletCollisionHandler
    {
        public Action<int, Collider> EnemyCollision;
        public Action<int> HeroCollision;
        public BulletCollisionHandler()
        {
            CollisionDetector.TriggerDetected += TriggerEnterHandler;
        }
        private void TriggerEnterHandler(Collider selfCollider, Collider otherCollider)
        {
            if (otherCollider.gameObject.layer == Layers.Hero)
            {
                var damage = selfCollider.GetComponent<BulletSettings>().Damage;
                HeroCollision.Invoke(damage);
            }

            if (otherCollider.gameObject.layer == Layers.Enemy)
            {
                var damage = selfCollider.GetComponent<BulletSettings>().Damage;
                EnemyCollision.Invoke(damage, selfCollider);
            }

            var bullet = selfCollider.GetComponent<BulletSettings>();
            
            bullet.ReturnToPool();
        }
    }
}