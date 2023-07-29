using System;
using System.Collections.Generic;
using Code.Bullet;
using Code.Enemy;
using Code.Game.HealthBar;
using Code.HUD;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Hero
{
    public class HeroMove : IDisposable
    {
        public event Action HeroStopped;

        private readonly FloatingJoystick _joystick;
        private readonly HeroSettings _heroSettings;
        private readonly NavMeshAgent _agent;
        private readonly HealthBar _healthBar;
        private readonly DamageHandler _damageHandler;
        private GameObject _targetRotation;

        
        public HeroMove(FloatingJoystick joystick, HeroSettings heroSettings, HealthBar HealthBar, DamageHandler damageHandler)
        {
            _healthBar = HealthBar;
            _joystick = joystick;
            _heroSettings = heroSettings;
            _agent = heroSettings.GetComponent<NavMeshAgent>();
            _damageHandler = damageHandler;

            Subscribe();
        }

        public void Run(List<EnemySettings> enemies)
        {
            if(!_agent.gameObject.activeSelf) return;
            Move();
            Rotate();
            CheckDistance(enemies);
        }

        private void Subscribe()
        {
            HeroStopped += LookAtEnemy;
            _damageHandler.Dead += Dead;
            CollisionDetector.CollisionDetected += CollisionEnterHandler;
            CollisionDetector.TriggerDetected += TriggerEnterHandler;
            
        }

        private void Unsubscribe()
        {
            HeroStopped -= LookAtEnemy;
            _damageHandler.Dead -= Dead;
            CollisionDetector.CollisionDetected -= CollisionEnterHandler;
            CollisionDetector.TriggerDetected -= TriggerEnterHandler;
        }

        private void CollisionEnterHandler(Collider selfCollider, Collider otherCollider)
        {
            if (otherCollider.gameObject.layer == Layers.Hero && selfCollider.gameObject.layer == Layers.Enemy)
            {
                var enemySettings = selfCollider.gameObject.GetComponent<EnemySettings>();
                _damageHandler.TakeDamage(enemySettings.CollisionDamage);
                _healthBar.ChangeValue(enemySettings.CollisionDamage);
            }
        }

        private void TriggerEnterHandler(Collider selfCollider, Collider otherCollider)
        {
            if (otherCollider.gameObject.layer == Layers.Hero && selfCollider.gameObject.layer == Layers.EnemyBullet)
            {
                var enemySettings = selfCollider.gameObject.GetComponent<BulletSettings>();
                _damageHandler.TakeDamage(enemySettings.Damage);
                _healthBar.ChangeValue(enemySettings.Damage);
            }
        }

        private void Dead()
        {
            ScreenSwitcher.ShowScreen(ScreenType.Defeat);
            Time.timeScale = 0;
        }

        private void Move()
        {
            _agent.velocity = new Vector3(_joystick.Horizontal * _heroSettings.Speed,
                _agent.velocity.y, _joystick.Vertical * _heroSettings.Speed);

            if (_joystick.Horizontal == 0 && _joystick.Vertical == 0 && !Input.GetMouseButton(0) && _targetRotation)
            {
                HeroStopped?.Invoke();
            }
        }
        
        private void Rotate()
        {
            if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
            {
                _heroSettings.gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(
                    _agent.velocity.x,
                    0,
                    _agent.velocity.z));
            }
        }

        private void CheckDistance(List<EnemySettings> enemies)
        {
            _targetRotation = null;
            float closestDistance = Mathf.Infinity;

            foreach (var enemy in enemies)
            {
                if (!enemy.gameObject.activeSelf) continue;
                var distance = Vector3.Distance(_heroSettings.transform.position,
                    enemy.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    _targetRotation = enemy.gameObject;
                }
            }
        }
        private void LookAtEnemy()
        {
            _heroSettings.transform.LookAt(new Vector3(
                _targetRotation.transform.position.x,
                _heroSettings.transform.position.y,
                _targetRotation.transform.position.z));
        }

        public void Dispose()
        {
            Unsubscribe();
        }
    }
}