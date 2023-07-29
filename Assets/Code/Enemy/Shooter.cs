using Code.Bullet;
using Code.Pool;
using Code.Weapon;
using UnityEngine;

namespace Code.Enemy
{
    public class Shooter
    {
        private readonly WeaponSettings _weaponSettings;
        private readonly GameObjectPool<Transform> _bulletPool;
        
        public Shooter(WeaponSettings weaponSettings)
        {
            _weaponSettings = weaponSettings;
            _bulletPool = new GameObjectPool<Transform>(weaponSettings.Bullet.transform, 20);
        }

        public void Shoot(out float timeReload, out bool isReload)
        {
            var bullet = _bulletPool.GetObject();
            bullet.transform.position = _weaponSettings.transform.position;
            bullet.GetComponent<BulletSettings>().SetPool(_bulletPool);
            var bulletRigidBody = bullet.GetComponent<Rigidbody>();
            bulletRigidBody.AddForce(_weaponSettings.transform.forward * _weaponSettings.ShootForce, ForceMode.Impulse);
            timeReload = 0;
            isReload = false;
        }
    }
}