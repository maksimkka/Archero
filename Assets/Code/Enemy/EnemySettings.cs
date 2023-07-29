using System;
using UnityEngine;

namespace Code.Enemy
{
    public class EnemySettings : MonoBehaviour
    {
        [field: SerializeField] public EnemyType Type { get; private set; }
        [field: SerializeField] public LayerMask IgnoreLayerMask { get; private set; }
        [field: SerializeField] public int HP { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public int CollisionDamage { get; private set; }
        [field: SerializeField] public int Value { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float ShootingSpeed { get; private set; }
        [field: SerializeField] public float AttackDistance { get; private set; }
        [field: SerializeField] public float TimeStillnessAndMovement { get; private set; }
        [field: SerializeField] public Transform RayOrigin { get; private set; }
        [field: SerializeField] public Transform HealthBarPosition { get; private set; }
    }
}