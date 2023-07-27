using System;
using Code.Enemy;
using Code.Logger;
using UnityEngine;
using UnityEngine.AI;


namespace Code.Hero
{
    public class HeroMove
    {
        public event Action HeroStopped;
        
        private readonly FloatingJoystick _joystick;
        private readonly HeroSettings _heroSettings;
        //private readonly Rigidbody _heroRigidbody;
        private readonly NavMeshAgent _agent;
        private GameObject _targetRotation;
        
        public HeroMove(FloatingJoystick joystick, HeroSettings heroSettings)
        {
            _joystick = joystick;
            _heroSettings = heroSettings;
            _agent = heroSettings.GetComponent<NavMeshAgent>();
        }

        public void Run(EnemySettings[] enemies)
        {
            Move();
            Rotate();
            IsCheckDistance(enemies);
        }

        private void Move()
        {
            _agent.velocity = new Vector3(_joystick.Horizontal * _heroSettings.Speed,
                _agent.velocity.y, _joystick.Vertical * _heroSettings.Speed);
            // _heroRigidbody.velocity = new Vector3(_joystick.Horizontal * _heroSettings.Speed,
            //     _heroRigidbody.velocity.y, _joystick.Vertical * _heroSettings.Speed);

            if (_joystick.Horizontal == 0 && _joystick.Vertical == 0 && !Input.GetMouseButton(0) && _targetRotation)
            {
                HeroStopped?.Invoke();
            }
        }

        private void IsCheckDistance(EnemySettings[] enemies)
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

        private void Rotate()
        {
            if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
            {
                _heroSettings.gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(
                    _agent.velocity.x,
                    0,
                    _agent.velocity.z));

                //heroData.Animator.PlayAnimation(heroData.RunAnimationHash);
            }

            else if (_joystick.Horizontal == 0 && _joystick.Vertical == 0)
            {
                //heroData.Animator.PlayAnimation(heroData.IdleAnimationHash);
                RotateForEnemy();
            }
            
        }

        private void RotateForEnemy()
        {
            if (_targetRotation != null)
            {
                //var originalRotation = _heroSettings.transform.eulerAngles;
                _heroSettings.transform.LookAt(new Vector3(
                    _targetRotation.transform.position.x,
                    _heroSettings.transform.position.y,
                    _targetRotation.transform.position.z));
            
                // heroData.HeroGameObject.transform.eulerAngles = new Vector3(
                //     originalRotation.x,
                //     heroData.HeroGameObject.transform.eulerAngles.y,
                //     originalRotation.z);
            }
        }
    }
}