using System;

namespace Code.Score
{
    public class ScoreChanger : IDisposable
    {
        private readonly ScoreView _scoreView;
        private int _currentScore;
        public ScoreChanger(ScoreView scoreView)
        {
            _scoreView = scoreView;
        }

        private void ChangeScore(int value)
        {
            _currentScore += value;
            _scoreView.ScoreText.text = _currentScore.ToString();
        }

        public void Dispose()
        {
            
        }
    }
}