using UnityEngine;
using UnityEngine.AI;

namespace Code.Spawner
{
    public class EnemySpawner
    {
        private readonly Vector2 _minSpawnPos;
        private readonly Vector2 _maxSpawnPos;

        public EnemySpawner(Vector2 minSpawnPos, Vector2 maxSpawnPos)
        {
            _minSpawnPos = minSpawnPos;
            _maxSpawnPos = maxSpawnPos;
        }

        public GameObject SpawnEnemy(GameObject enemyPrefab)
        {
            var spawnPosition = GetRandomNavMeshPosition();
            return Object.Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }

        private Vector3 GetRandomNavMeshPosition()
        {
            Vector3 randomPosition;
            int attempts = 0;
            int maxAttempts = 30;

            do
            {
                randomPosition = new Vector3(Random.Range(_minSpawnPos.x, _maxSpawnPos.x),
                    1f, Random.Range(_minSpawnPos.y, _maxSpawnPos.y));

                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPosition, out hit, 1.0f, NavMesh.GetAreaFromName("Walkable")))
                {
                    randomPosition = hit.position;
                    break;
                }

                attempts++;
            } while (attempts < maxAttempts);

            return randomPosition;
        }
    }
}