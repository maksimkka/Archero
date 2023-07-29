using UnityEngine;

namespace Code.Spawner
{
    [DisallowMultipleComponent]
    public class EnemySpawnerSettings : MonoBehaviour
    {
        [field: SerializeField] public GameObject FlyingEnemyPrefab { get; private set; }
        [field: SerializeField] public GameObject ShooterEnemyPrefab { get; private set; }
        [field: SerializeField] public int CountFlyingEnemySpawn { get; private set; }
        [field: SerializeField] public int CountShooterEnemySpawn { get; private set; }
        [field: SerializeField] public Vector2 MinSpawnPos { get; private set; }
        [field: SerializeField] public Vector2 MaxSpawnPos { get; private set; }
    }
}