using System.Collections.Generic;
using System.Linq;
using Code.Enemy;
using Code.Finish;
using Code.Game.HealthBar;
using Code.Hero;
using Code.Score;
using Code.Spawner;
using Code.Weapon;
using UnityEngine;

namespace Code.Main
{
    public class LevelEntryPoint : MonoBehaviour
    {
        [SerializeField] private float DelayBeforeStartGame;
        [SerializeField] private FloatingJoystick Joystick;
        [SerializeField] private FinishGate _finishGate;
        private ScoreView _scoreView;
        private HeroSettings _heroSettings;
        private HealthBarSettings[] _healthBarsSettings;
        private EnemySpawnerSettings _enemySpawnerSettings;
        private CounterEnemies _counterEnemies;
        private EnemySpawner _spawner;
        private HeroSpawnerSettings _heroSpawnerSettings;
        private HeroSpawner _heroSpawner;
        private StartGameDelay _startGameDelay;
        private DamageHandler _heroDamageHandler;
        private HeroShooter _heroShooter;
        private ScoreChanger _scoreChanger;
        private HeroMove _heroMove;
        
        private List<EnemySettings> _enemySettings;
        private List<IEnemy> _enemies;
        private List<IHealthBar> _healthBars;

        private void Awake()
        {
            HealthBarsInit();
            SpawnerInit();
            HeroInit();
            EnemiesInit();
            ScoreInit();
            Subscribe();
            _startGameDelay = new StartGameDelay(DelayBeforeStartGame);
        }

        private void Update()
        {
            if(!_startGameDelay.IsStartGame) return;
            _heroMove.Run(_enemySettings);

            foreach (var enemy in _enemies)
            {
                enemy.Run();
            }
        }

        private void LateUpdate()
        {
            foreach (var healthBar in _healthBars)
            {
                healthBar.Follow();
            }
        }

        private void HealthBarsInit()
        {
            _healthBars = new List<IHealthBar>();
            _healthBarsSettings = FindObjectsOfType<HealthBarSettings>(true);
        }

        private void ScoreInit()
        {
            _scoreView = FindObjectOfType<ScoreView>();
            _scoreChanger = new ScoreChanger(_scoreView, _enemies);
        }

        private void SpawnerInit()
        {
            _enemySpawnerSettings = FindObjectOfType<EnemySpawnerSettings>();
            _spawner = new EnemySpawner(_enemySpawnerSettings.MinSpawnPos, _enemySpawnerSettings.MaxSpawnPos);
        }

        private void HeroInit()
        {
            _heroSpawnerSettings = FindObjectOfType<HeroSpawnerSettings>();
            _heroSpawner = new HeroSpawner();
            var hero = _heroSpawner.Spawn(_heroSpawnerSettings.HeroPrefab, _heroSpawnerSettings.transform.position);
            _heroSettings = hero.GetComponent<HeroSettings>();
            _heroDamageHandler = new DamageHandler(_heroSettings.HP);
            var weaponSettings = _heroSettings.GetComponentInChildren<WeaponSettings>();
            _heroMove = new HeroMove(Joystick, _heroSettings,
                InitHeathBars(_heroSettings.HealthBarPosition, _heroSettings.HP), _heroDamageHandler);
            _heroShooter = new HeroShooter(_heroMove, weaponSettings);
        }

        private void EnemiesInit()
        {
            _enemies = new List<IEnemy>();
            _enemySettings = new List<EnemySettings>();
            _counterEnemies = new CounterEnemies(_finishGate);
            for (int i = 0; i < _enemySpawnerSettings.CountFlyingEnemySpawn; i++)
            {
                var enemy = _spawner.SpawnEnemy(_enemySpawnerSettings.FlyingEnemyPrefab);
                var enemySettings = enemy.GetComponent<EnemySettings>();
                _counterEnemies.IncreaseCountEnemies();
                _enemySettings.Add(enemySettings);
                _enemies.Add(new FlyingEnemy(enemySettings, _heroSettings,
                        InitHeathBars(enemySettings.HealthBarPosition, enemySettings.HP), _counterEnemies));
            }

            for (int i = 0; i < _enemySpawnerSettings.CountShooterEnemySpawn; i++)
            {
                var enemy = _spawner.SpawnEnemy(_enemySpawnerSettings.ShooterEnemyPrefab);
                var enemySettings = enemy.GetComponent<EnemySettings>();
                _enemySettings.Add(enemySettings);
                var weaponSettings = enemySettings.GetComponentInChildren<WeaponSettings>();
                _enemies.Add(new ShooterEnemy(enemySettings, _heroSettings,weaponSettings,
                    InitHeathBars(enemySettings.HealthBarPosition, enemySettings.HP), _counterEnemies));
            }

            
        }

        private void Subscribe()
        {
            foreach (var enemy in _enemies)
            {
                enemy.HitClose += _heroDamageHandler.TakeDamage;
            }
        }
        
        private void Unsubscribe()
        {
            foreach (var enemy in _enemies)
            {
                enemy.HitClose -= _heroDamageHandler.TakeDamage;
            }
        }

        private HealthBar InitHeathBars(Transform healthBarPosition, int HP)
        {
            var healthBarSettings = _healthBarsSettings.FirstOrDefault(num => !num.IsSelected);
            healthBarSettings.gameObject.SetActive(true);
            var healthBar = new HealthBar(healthBarPosition, healthBarSettings, HP);
            _healthBars.Add(healthBar);

            return healthBar;
        }

        private void OnDestroy()
        {
            _heroShooter.Dispose();
            _heroMove.Dispose();
            _scoreChanger.Dispose();
            Unsubscribe();
            _startGameDelay.Dispose();

            foreach (var enemy in _enemies)
            {
                enemy.Dispose();
            }
        }
    }
}