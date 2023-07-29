using Code.Bullet;
using UnityEngine;

namespace Code.Enemy
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
            
            // if (other.gameObject.layer == Layers.Enemy)
            // {
            //     gameObject.SetActive(false);
            //     OnEnemyCollision.Invoke(Damage, other);
            // }
            //
            // if (other.gameObject.layer == Layers.Hero)
            // {
            //     gameObject.SetActive(false);
            //     OnHeroCollision.Invoke(Damage);
            // }
            //
            // if(other.gameObject.layer == Layers.Wall)
            // {
            //     gameObject.SetActive(false);
            // }
        }
    }
}