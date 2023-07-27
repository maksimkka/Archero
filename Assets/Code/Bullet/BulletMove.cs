using Code.Weapon;
using UnityEngine;

namespace Code.Bullet
{
    public class BulletMove
    {
        private float _speed;
        private bool _isShoot;
        private readonly BulletSettings _bulletSettings;
        private readonly WeaponSettings _weaponSettings;

        public BulletMove(BulletSettings bulletSettings, WeaponSettings weaponSettings)
        {
            _bulletSettings = bulletSettings;
            _weaponSettings = weaponSettings;
        }

        private void Move()
        {
            if (_bulletSettings.gameObject.activeSelf && !_isShoot)
            {
                var bulletRigidbody = _bulletSettings.GetComponent<Rigidbody>();
                bulletRigidbody.AddForce(_weaponSettings.gameObject.transform.forward * _speed, ForceMode.Impulse);

            }
        }
    }
}