using System;
using Code.Enemy;
using Code.Hero;
using Code.Weapon;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Main
{
    public class LevelEntryPoint : MonoBehaviour
    {
        [SerializeField] private HeroSettings HeroSettings;
        [SerializeField] private FloatingJoystick Joystick;
        [SerializeField] private WeaponSettings WeaponSettings;
        
        private EnemySettings[] _enemySettings;

        private HeroShooter _heroShooter;
        private HeroMove _heroMove;
        private IEnemy[] _enemy;

        private void Awake()
        {
            
            _heroMove = new HeroMove(Joystick, HeroSettings);
            _heroShooter = new HeroShooter(HeroSettings, _heroMove, WeaponSettings);
            
            CreateEnemies();
        }

        private void Update()
        {
            _heroMove.Run(_enemySettings);

            foreach (var enemy in _enemy)
            {
                enemy.Move();
            }
        }

        private void CreateEnemies()
        {
            _enemySettings = FindObjectsOfType<EnemySettings>();
            _enemy = new IEnemy[_enemySettings.Length];
            for (int i = 0; i < _enemySettings.Length; i++)
            {
                if (_enemySettings[i].Type == EnemyType.Warrior)
                {
                    _enemy[i] = new MeleeEnemy(_enemySettings[i], HeroSettings.gameObject);
                }
            }
        }

        private void OnDestroy()
        {
            _heroShooter.Dispose();

            foreach (var enemy in _enemy)
            {
                enemy.Dispose();
            }
        }
    }
}