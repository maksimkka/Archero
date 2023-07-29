using System;
using Code.Bullet;
using Code.Game.HealthBar;
using Code.Hero;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Enemy
{
    public class FlyingEnemy : IEnemy
    {
        private readonly EnemySettings _enemySettings;
        private readonly NavMeshAgent _agent;
        private readonly HeroSettings _targetForMovementPlayer;
        private readonly Collider _collider;
        private readonly HealthBar _healthBar;
        private readonly CounterEnemies _counterEnemies;
        private bool _isReadyAttack;
        private float _distance;
        private float _currentReloadTime;
        private int _currentHp;

        public event Action<int> HitClose;
        public event Action<int> Died;

        public FlyingEnemy(EnemySettings enemySettings, HeroSettings targetForMovementPlayer,
            HealthBar healthBar, CounterEnemies counterEnemies)
        {
            _counterEnemies = counterEnemies;
            _healthBar = healthBar;
            _collider = enemySettings.GetComponent<Collider>();
            _enemySettings = enemySettings;
            _agent = enemySettings.GetComponent<NavMeshAgent>();
            _targetForMovementPlayer = targetForMovementPlayer;
            _agent.speed = enemySettings.Speed;
            _currentHp = enemySettings.HP;
            _currentReloadTime = enemySettings.ShootingSpeed;
            CollisionDetector.TriggerDetected += CollisionEnterHandler;
        }
        
        public void Run()
        {
            if (!_agent.gameObject.activeSelf) return;
            Move();
            MeleeAttack();
            Reload();
            CheckDistance();
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

        private void MeleeAttack()
        {
            if (_distance <= _enemySettings.AttackDistance && _isReadyAttack)
            {
                _isReadyAttack = false;
                _currentReloadTime = 0;
                HitClose?.Invoke(_enemySettings.Damage);
            }
        }

        private void Reload()
        {
            if (_isReadyAttack) return;
            _currentReloadTime += Time.deltaTime;
            
            if (_currentReloadTime >= _enemySettings.ShootingSpeed)
            {
                _isReadyAttack = true;
            }
        }

        private void CheckDistance()
        {
            _distance = Vector3.Distance(_enemySettings.gameObject.transform.position,
                _targetForMovementPlayer.transform.position);
        }

        private void TakeDamage(int damage, Collider collider)
        {
            if (_collider != collider) return;
            _currentHp -= damage;
            _healthBar.ChangeValue(damage);
        
            if (_currentHp <= 0)
            {
                _enemySettings.gameObject.SetActive(false);
                _counterEnemies.DecreaseEnemies();
                Died?.Invoke(_enemySettings.Value);
            }
        }

        public void Dispose()
        {
            CollisionDetector.TriggerDetected -= CollisionEnterHandler;
        }
    }
}