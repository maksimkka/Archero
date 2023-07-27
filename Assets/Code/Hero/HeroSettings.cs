using UnityEngine;

namespace Code.Hero
{
    public class HeroSettings : MonoBehaviour
    {
        [field: SerializeField] public int HP { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float ShootingSpeed { get; private set; }
    }
}