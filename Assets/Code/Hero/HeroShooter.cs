using System;
using Code.Logger;
using Code.Weapon;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Hero
{
    public class HeroShooter : IDisposable
    {
        private readonly HeroSettings _heroSettings;
        private readonly HeroMove _heroMove;
        private readonly WeaponSettings _weaponSettings;

        private float currentReloadTime;
        public HeroShooter(HeroSettings heroSettings, HeroMove heroMove, WeaponSettings weaponSettings)
        {
            _heroSettings = heroSettings;
            _heroMove = heroMove;
            _weaponSettings = weaponSettings;
            currentReloadTime = _heroSettings.ShootingSpeed;
            Subscribe();
        }

        private void Subscribe()
        {
            _heroMove.HeroStopped += Reload;
        }

        private void UnSubscribe()
        {
            _heroMove.HeroStopped -= Reload;
        }

        private void Shoot()
        {
            var bullet = Object.Instantiate(_weaponSettings.Bullet, _weaponSettings.transform.position,
                Quaternion.identity);

            var bulletRigidBody = bullet.GetComponent<Rigidbody>();
            bulletRigidBody.AddForce(_weaponSettings.transform.forward * _weaponSettings.ShootForce, ForceMode.Impulse);
        }

        private void Reload()
        {
            currentReloadTime -= Time.deltaTime;
            if (currentReloadTime <= 0)
            {
                Shoot();
                currentReloadTime = _heroSettings.ShootingSpeed;
            }
        }

        public void Dispose()
        {
            UnSubscribe();
        }
    }
}