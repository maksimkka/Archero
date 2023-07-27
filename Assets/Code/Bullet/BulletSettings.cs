using System;
using Code.Enemy;
using UnityEngine;

namespace Code.Bullet
{
    public class BulletSettings : MonoBehaviour
    {
        //[field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }

        public static Action<int, Collider> OnEnemyCollision;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == Layers.Enemy)
            {
                gameObject.SetActive(false);

                OnEnemyCollision.Invoke(Damage, other);
                //var enemy = other.gameObject.GetComponent<IEnemy>();
                //enemy.GiveDamage(Damage);
                // var enemy = other.gameObject.GetComponent<EnemySettings>();
                // enemy.GiveDamage(Damage);
            }

            else if(other.gameObject.layer == Layers.Wall)
            {
                gameObject.SetActive(false);
            }
        }
    }
}