using Code.Pool;
using UnityEngine;

namespace Code.Bullet
{
    [DisallowMultipleComponent]
    public class BulletSettings : MonoBehaviour
    {
        [field: SerializeField] public int Damage { get; private set; }

        private GameObjectPool<Transform> _gameObjectPool;

        public void SetPool(GameObjectPool<Transform> gameObjectPool)
        {
            _gameObjectPool = gameObjectPool;
        }

        public void ReturnToPool()
        {
            _gameObjectPool.ReturnObject(gameObject.transform);
        }
    }
}