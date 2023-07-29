using UnityEngine;

namespace Code.DifferentMechanics
{
    [DisallowMultipleComponent]
    public class HeroSettings : MonoBehaviour
    {
        [field: SerializeField] public int HP { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public Transform HealthBarPosition { get; private set; }
    }
}