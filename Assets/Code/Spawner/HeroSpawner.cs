using UnityEngine;

namespace Code.Spawner
{
    public class HeroSpawner
    {
        public GameObject Spawn(GameObject heroPrefab, Vector3 spawnPosition)
        {
            return Object.Instantiate(heroPrefab, spawnPosition, Quaternion.identity);
        }
    }
}