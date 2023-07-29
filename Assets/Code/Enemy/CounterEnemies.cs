using System;
using Code.Finish;

namespace Code.Enemy
{
    public class CounterEnemies
    {
        private readonly FinishGate _finishGate;
        private int _currentCountEnemies;
        public CounterEnemies(FinishGate finishGate)
        {
            _finishGate = finishGate;
        }

        public void IncreaseCountEnemies()
        {
            _currentCountEnemies++;
        }

        public void DecreaseEnemies()
        {
            _currentCountEnemies--;

            if (_currentCountEnemies <= 0)
            {
                _finishGate.gameObject.SetActive(true);
            }
        }
    }
}