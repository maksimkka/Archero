using Code.DifferentMechanics;
using UnityEngine;

namespace Code.Weapon
{
    public class WeaponReloader
    {
        private readonly Shooter _shooter;
        private readonly float _timeReload;
        private bool _isReload;
        private float _currentReloadTime;

        public WeaponReloader(Shooter shooter, float timeReload)
        {
            _shooter = shooter;
            _timeReload = timeReload;
        }

        public void Reload()
        {
            if (!_isReload)
            {
                _currentReloadTime += Time.deltaTime;
            }

            if (_currentReloadTime >= _timeReload)
            {
                _isReload = true;
                _shooter.Shoot(out _currentReloadTime, out _isReload);
            }
        }
    }
}