using UnityEngine;

namespace Code.Weapon
{
    public class WeaponSettings : MonoBehaviour
    {
        [field: SerializeField] public GameObject Bullet { get; private set; }
        [field: SerializeField] public float ShootForce { get; private set; }
    }
}