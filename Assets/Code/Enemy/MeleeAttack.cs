using System;
using Code.DifferentMechanics;
using UnityEngine;

namespace Code.Enemy
{
    public class MeleeAttack
    {

        private readonly FlyingEnemySettings _flyingEnemySettings;
        private readonly HeroSettings _targetForMovementPlayer;

        private float _distance;
        private bool _isReadyAttack;
        private float _currentReloadTime;

        public MeleeAttack(FlyingEnemySettings flyingEnemySettings, HeroSettings targetForMovementPlayer)
        {
            _flyingEnemySettings = flyingEnemySettings;
            _targetForMovementPlayer = targetForMovementPlayer;
        }

        public void Run(Action<int> HitClose)
        {
            CheckDistance();
            Reload();
            Attack(HitClose);
        }
        private void Attack(Action<int> HitClose)
        {
            if (_distance <= _flyingEnemySettings.AttackDistance && _isReadyAttack)
            {
                _isReadyAttack = false;
                _currentReloadTime = 0;
                HitClose?.Invoke(_flyingEnemySettings.MeleeDamage);
            }
        }

        private void Reload()
        {
            if (_isReadyAttack) return;
            _currentReloadTime += Time.deltaTime;

            if (_currentReloadTime >= _flyingEnemySettings.CooldownAttack)
            {
                _isReadyAttack = true;
            }
        }

        private void CheckDistance()
        {
            _distance = Vector3.Distance(_flyingEnemySettings.gameObject.transform.position,
                _targetForMovementPlayer.transform.position);
        }
    }
}