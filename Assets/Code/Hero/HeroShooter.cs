using System;
using Code.Enemy;
using Code.Pool;
using Code.Weapon;

namespace Code.Hero
{
    public class HeroShooter : IDisposable
    {
        private readonly HeroMove _heroMove;
        private readonly WeaponReloader _weaponReloader;
        

        public HeroShooter(HeroMove heroMove, WeaponSettings weaponSettings)
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