using Code.Bullet;
using UnityEngine;

namespace Code.Collision
{
    [DisallowMultipleComponent]
    public class TriggerEnterDetector : MonoBehaviour
    {
        private Collider selfCollider;

        private void Awake()
        {
            selfCollider = gameObject.GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider otherCollider)
        {
            CollisionDetector.HandleTriggerEnter(selfCollider, otherCollider);

            var bullet = selfCollider.GetComponent<BulletSettings>();
            selfCollider.GetComponent<Rigidbody>().velocity = Vector3.zero;
            bullet.ReturnToPool();
        }
    }
}