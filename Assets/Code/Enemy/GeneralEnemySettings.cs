using UnityEngine;

namespace Code.Enemy
{
    [DisallowMultipleComponent]
    public class GeneralEnemySettings : MonoBehaviour
    {
        [field: SerializeField] public int HP { get; private set; }
        [field: SerializeField] public int CollisionDamage { get; private set; }
        [field: SerializeField] public int Value { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public Transform HealthBarPosition { get; private set; }
    }
}       
