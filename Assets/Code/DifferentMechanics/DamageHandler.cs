using System;
using Code.Game.HealthBar;

namespace Code.DifferentMechanics
{
    public class DamageHandler
    {
        private readonly HealthBar _healthBar;
        private int _currentHP;
        public event Action Dead;

        public DamageHandler(int HP, HealthBar healthBar)
        {
            _currentHP = HP;
            _healthBar = healthBar;
        }
        
        public void TakeDamage(int damage)
        {
            _currentHP -= damage;
            _healthBar.ChangeValue(damage);
            if (_currentHP <= 0)
            {
                _currentHP = 0;
                Dead?.Invoke();
            }
        }
    }
}