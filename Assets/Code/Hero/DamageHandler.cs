using System;

namespace Code.Hero
{
    public class DamageHandler
    {
        private int _currentHP;
        public event Action Dead;

        public DamageHandler(int HP)
        {
            _currentHP = HP;
        }
        
        public void TakeDamage(int damage)
        {
            _currentHP -= damage;
            if (_currentHP <= 0)
            {
                _currentHP = 0;
                Dead?.Invoke();
            }
        }
    }
}