using System;
using Code.Bullet;
using Code.Collision;
using Code.DifferentMechanics;
using Code.Game.HealthBar;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Enemy
{
    public class FlyingEnemy : IEnemy
    {
        private readonly GeneralEnemySettings _generalEnemySettings;
        private readonly NavMeshAgent _agent;
        private readonly HeroSettings _targetForMovementPlayer;
        private readonly Collider _collider;
        private readonly HealthBar _healthBar;
        private readonly CounterEnemies _counterEnemies;
        private readonly MeleeAttack _meleeAttack;

        private int _currentHp;

        public event Action<int> HitClose;
        public event Action<int> Died;

        public FlyingEnemy(GeneralEnemySettings generalEnemySettings, MeleeAttack meleeAttack,
            HeroSettings targetForMovementPlayer, HealthBar healthBar, CounterEnemies counterEnemies)
        {
            _counterEnemies = counterEnemies;
            _healthBar = healthBar;
            _collider = generalEnemySettings.GetComponent<Collider>();
            _generalEnemySettings = generalEnemySettings;
            _meleeAttack = meleeAttack;
            _agent = generalEnemySettings.GetComponent<NavMeshAgent>();
            _targetForMovementPlayer = targetForMovementPlayer;
            _agent.speed = generalEnemySettings.Speed;
            _currentHp = generalEnemySettings.HP;
            CollisionDetector.TriggerDetected += CollisionEnterHandler;
        }

        public void Run()
        {
            if (!_agent.gameObject.activeSelf) return;
            Move();
            _meleeAttack.Run(HitClose);
        }

        private void CollisionEnterHandler(Collider selfCollider, Collider otherCollider)
        {
            if (otherCollider.gameObject.layer == Layers.Enemy && selfCollider.gameObject.layer == Layers.Bullet)
            {
                var bullet = selfCollider.GetComponent<BulletSettings>();
                TakeDamage(bullet.Damage, otherCollider);
            }
        }

        private void Move()
        {
            if (!_targetForMovementPlayer) return;

            _agent.SetDestination(_targetForMovementPlayer.transform.position);
        }
        
        private void TakeDamage(int damage, Collider collider)
        {
            if (_collider != collider) return;
            _currentHp -= damage;
            _healthBar.ChangeValue(damage);

            if (_currentHp <= 0)
            {
                _generalEnemySettings.gameObject.SetActive(false);
                _counterEnemies.DecreaseEnemies();
                Died?.Invoke(_generalEnemySettings.Value);
            }
        }

        public void Dispose()
        {
            CollisionDetector.TriggerDetected -= CollisionEnterHandler;
        }
    }
}