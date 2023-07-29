using UnityEngine;

namespace Code.Enemy
{
    public class FlyingEnemySettings : MonoBehaviour
    {
        [field: SerializeField] public int MeleeDamage { get; private set; }
        [field: SerializeField] public float AttackDistance { get; private set; }
        [field: SerializeField] public float CooldownAttack { get; private set; }
    }
}