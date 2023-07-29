using System;
using Code.DifferentMechanics;
using Code.Weapon;

namespace Code.Hero
{
    public class HeroWeapon : IDisposable
    {
        private readonly HeroMove _heroMove;
        private readonly WeaponReloader _weaponReloader;
        
        public HeroWeapon(HeroMove heroMove, WeaponSettings weaponSettings)
        {
            _heroMove = heroMove;
            var shooter = new Shooter(weaponSettings);
            _weaponReloader = new WeaponReloader(shooter, weaponSettings.ShootReload);
            Subscribe();
        }

        private void Subscribe()
        {
            _heroMove.HeroStopped += _weaponReloader.Reload;
        }

        private void UnSubscribe()
        {
            _heroMove.HeroStopped -= _weaponReloader.Reload;
        }

        public void Dispose()
        {
            UnSubscribe();
        }
    }
}