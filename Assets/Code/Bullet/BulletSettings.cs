using System;
using Code.Enemy;
using UnityEngine;

namespace Code.Bullet
{
    public class BulletSettings : MonoBehaviour
    {
        [field: SerializeField] public int Damage { get; private set; }

        public static Action<int, Collider> OnEnemyCollision;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == Layers.Enemy)
            {
                gameObject.SetActive(false);

                OnEnemyCollision.Invoke(Damage, other);
            }

            else if(other.gameObject.layer == Layers.Wall)
            {
                gameObject.SetActive(false);
            }
        }
    }
}