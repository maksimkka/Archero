using System;
using Code.Bullet;
using Code.Collision;
using Code.DifferentMechanics;
using Code.Enemy;
using Code.HUD;
using UnityEngine;

namespace Code.Hero
{
    public class HeroCollisionHandler : IDisposable
    {
        private readonly DamageHandler _damageHandler;

        public HeroCollisionHandler(DamageHandler damageHandler)
        { 
            _damageHandler = damageHandler;

            Subscribe();
        }

        private void Subscribe()
        {
            _damageHandler.Dead += Dead;
            CollisionDetector.CollisionDetected += CollisionEnterHandler;
            CollisionDetector.TriggerDetected += TriggerEnterHandler;
        }

        private void Unsubscribe()
        {
            _damageHandler.Dead -= Dead;
            CollisionDetector.CollisionDetected -= CollisionEnterHandler;
            CollisionDetector.TriggerDetected -= TriggerEnterHandler;
        }

        private void CollisionEnterHandler(Collider selfCollider, Collider otherCollider)
        {
            var damage = selfCollider.gameObject.GetComponent<GeneralEnemySettings>().CollisionDamage;
            HandleCollision(selfCollider, otherCollider, Layers.Enemy, damage);
        }

        private void TriggerEnterHandler(Collider selfCollider, Collider otherCollider)
        {
            var damage = selfCollider.gameObject.GetComponent<BulletSettings>().Damage;
            HandleCollision(selfCollider, otherCollider, Layers.EnemyBullet, damage);
        }

        private void HandleCollision(Collider selfCollider, Collider otherCollider, int layer, int damage)
        {
            if (otherCollider.gameObject.layer == Layers.Hero && selfCollider.gameObject.layer == layer)
            {
                _damageHandler.TakeDamage(damage);
            }
        }

        private void Dead()
        {
            ScreenSwitcher.ShowScreen(ScreenType.Defeat);
            Time.timeScale = 0;
        }
        
        public void Dispose()
        {
            Unsubscribe();
        }
    }
}