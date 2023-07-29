using System;
using Code.Bullet;
using Code.Collision;
using Code.DifferentMechanics;
using Code.Game.HealthBar;
using Code.Weapon;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Enemy
{
    public class ShooterEnemy : IEnemy
    {
        public event Action<int> HitClose;
        public event Action<int> Died;

        private readonly GeneralEnemySettings _generalEnemySettings;
        private readonly NavMeshAgent _agent;
        private readonly HeroSettings _targetForMovementPlayer;
        private readonly Collider _collider;
        private readonly LayerMask _ignoreLayer;
        private readonly WeaponReloader _weaponReloader;
        private readonly HealthBar _healthBar;
        private readonly CounterEnemies _counterEnemies;
        private readonly ShootingEnemySettings _shootingEnemySettings;

        private readonly float _timeStillnessAndMovement;
        private float _currentCountdownTimer;
        private bool _isGoing;
        private int _currentHp;

        public ShooterEnemy(GeneralEnemySettings generalEnemySettings, HeroSettings targetForMovementPlayer,
            WeaponSettings weaponSettings, HealthBar healthBar, CounterEnemies counterEnemies,
            ShootingEnemySettings shootingEnemySettings)
        {
            _shootingEnemySettings = shootingEnemySettings;
            _counterEnemies = counterEnemies;
            _healthBar = healthBar;
            _ignoreLayer = _shootingEnemySettings.IgnoreLayerMask;
            _collider = generalEnemySettings.GetComponent<Collider>();
            var shooter = new Shooter(weaponSettings);
            _weaponReloader = new WeaponReloader(shooter, weaponSettings.ShootReload);
            _generalEnemySettings = generalEnemySettings;
            _agent = generalEnemySettings.GetComponent<NavMeshAgent>();
            _targetForMovementPlayer = targetForMovementPlayer;
            _agent.speed = generalEnemySettings.Speed;
            _currentHp = generalEnemySettings.HP;
            _timeStillnessAndMovement = _shootingEnemySettings.TimeStillness;
            CollisionDetector.TriggerDetected += CollisionEnterHandler;
        }

        public void Run()
        {
            if (!_agent.gameObject.activeSelf) return;
            Move();
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

            var direction = _targetForMovementPlayer.transform.position - _shootingEnemySettings.RayOrigin.position;
            float distanceToTarget = direction.magnitude;
            direction.Normalize();

            if (Physics.Raycast(_shootingEnemySettings.RayOrigin.position, direction, out var hit, distanceToTarget,
                    ~_ignoreLayer))
            {
                if (hit.transform.gameObject.layer == Layers.Hero)
                {
                    CountdownToContinueWalking();
                }

                else
                {
                    _agent.isStopped = false;
                    _agent.SetDestination(_targetForMovementPlayer.transform.position);
                }
            }
        }

        private void CountdownToContinueWalking()
        {
            if (!_isGoing)
            {
                _currentCountdownTimer += Time.deltaTime;
                _agent.isStopped = true;
                LookAtHero();
                _weaponReloader.Reload();
                DiscardTimer();
            }

            else
            {
                _currentCountdownTimer += Time.deltaTime;
                _agent.isStopped = false;
                _agent.SetDestination(_targetForMovementPlayer.transform.position);
                DiscardTimer();
            }
        }

        private void DiscardTimer()
        {
            if (_currentCountdownTimer >= _timeStillnessAndMovement)
            {
                _isGoing = !_isGoing;
                _currentCountdownTimer = 0;
            }
        }

        private void LookAtHero()
        {
            _generalEnemySettings.transform.LookAt(new Vector3(
                _targetForMovementPlayer.transform.position.x,
                _generalEnemySettings.transform.position.y,
                _targetForMovementPlayer.transform.position.z));
        }

        private void TakeDamage(int damage, Collider collider)
        {
            if (_collider != collider) return;
            _healthBar.ChangeValue(damage);
            _currentHp -= damage;

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