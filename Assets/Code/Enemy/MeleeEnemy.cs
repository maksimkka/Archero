using System;
using Code.Bullet;
using Code.Logger;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Enemy
{
    public class MeleeEnemy : IEnemy
    {
        private readonly EnemySettings _enemySettings;
        private readonly NavMeshAgent _agent;
        private readonly GameObject _targetMove;
        private readonly Collider _collider;
        
        private int _currentHp;

        public MeleeEnemy(EnemySettings enemySettings, GameObject targetMove)
        {
            _collider = enemySettings.GetComponent<Collider>();
            _enemySettings = enemySettings;
            _agent = enemySettings.GetComponent<NavMeshAgent>();
            _targetMove = targetMove;
            _currentHp = enemySettings.HP;

            BulletSettings.OnEnemyCollision += GiveDamage;
        }


        public void Move()
        {
            if(!_agent.gameObject.activeSelf) return;
            _agent.SetDestination(_targetMove.transform.position);
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }

        public void Reload()
        {
            throw new NotImplementedException();
        }
        private void GiveDamage(int damage, Collider collider)
        {
            if(_collider != collider) return;
            _currentHp -= damage;
            
            if (_currentHp <= 0)
            {
                _enemySettings.gameObject.SetActive(false);
                Debug.Log("YMER");
            }
        }

        public void Dispose()
        {
            BulletSettings.OnEnemyCollision -= GiveDamage;
        }
    }
}