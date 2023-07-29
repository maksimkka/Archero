using System;
using System.Collections.Generic;
using Code.DifferentMechanics;
using Code.Enemy;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Hero
{
    public class HeroMove : IDisposable
    {
        public event Action HeroStopped;
        private readonly FloatingJoystick _joystick;
        private readonly NavMeshAgent _agent;
        private readonly HeroSettings _heroSettings;
        private GameObject _targetRotation;
        
        public HeroMove(FloatingJoystick joystick, HeroSettings heroSettings)
        {
            _joystick = joystick;
            _heroSettings = heroSettings;
            _agent = heroSettings.GetComponent<NavMeshAgent>();
            HeroStopped += LookAtEnemy;
        }

        public void Run(List<GeneralEnemySettings> enemies)
        {
            if (!_agent.gameObject.activeSelf) return;
            Move();
            Rotate();
            CheckDistance(enemies);
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
                var velocity = _agent.velocity;
                _heroSettings.gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(
                    velocity.x,
                    0,
                    velocity.z));
            }
        }
        
        private void CheckDistance(List<GeneralEnemySettings> enemies)
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
            var position = _targetRotation.transform.position;
            _heroSettings.transform.LookAt(new Vector3(
                position.x,
                _heroSettings.transform.position.y,
                position.z));
        }

        public void Dispose()
        {
            HeroStopped -= LookAtEnemy;
        }
    }
}