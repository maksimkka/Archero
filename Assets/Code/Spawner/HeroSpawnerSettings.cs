using UnityEngine;

namespace Code.Spawner
{
    [DisallowMultipleComponent]
    public class HeroSpawnerSettings : MonoBehaviour
    {
        [field: SerializeField] public GameObject HeroPrefab { get; private set; }
    }
}