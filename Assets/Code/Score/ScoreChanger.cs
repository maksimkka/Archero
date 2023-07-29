using System;
using System.Collections.Generic;
using Code.Enemy;

namespace Code.Score
{
    public class ScoreChanger : IDisposable
    {
        private readonly ScoreView _scoreView;
        private int _currentScore;
        private readonly List<IEnemy> _enemies;
        public ScoreChanger(ScoreView scoreView, List<IEnemy> enemies)
        {
            _scoreView = scoreView;
            _enemies = enemies;
            Subscribe();
        }

        private void Subscribe()
        {
            foreach (var enemy in _enemies)
            {
                enemy.Died += ChangeScore;
            }
        }

        private void Unsubscribe()
        {
            foreach (var enemy in _enemies)
            {
                enemy.Died -= ChangeScore;
            }
        }

        private void ChangeScore(int value)
        {
            _currentScore += value;
            _scoreView.ScoreText.text = _currentScore.ToString();
        }

        public void Dispose()
        {
            Unsubscribe();
        }
    }
}