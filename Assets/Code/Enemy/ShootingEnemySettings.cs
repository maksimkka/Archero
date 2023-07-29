using UnityEngine;

namespace Code.Enemy
{
    public class ShootingEnemySettings : MonoBehaviour
    {
        [field: SerializeField] public LayerMask IgnoreLayerMask { get; private set; }
        [field: SerializeField] public float TimeStillness { get; private set; }
        [field: SerializeField] public Transform RayOrigin { get; private set; }
    }
}