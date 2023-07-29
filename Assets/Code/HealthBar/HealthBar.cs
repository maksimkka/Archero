using UnityEngine;
using UnityEngine.UI;

namespace Code.Game.HealthBar
{
    public class HealthBar : IHealthBar
    {
        private readonly Transform _characterTransform;
        private readonly RectTransform hpBarRectTransform;
        private readonly HealthBarSettings _barSettings;
        private readonly Slider _slider;
        private readonly Camera _camera = Camera.main;

        public HealthBar(Transform characterTransform, HealthBarSettings barSettings, int maxHP)
        {
            _characterTransform = characterTransform;
            _barSettings = barSettings;
            hpBarRectTransform = barSettings.GetComponent<RectTransform>();
            _slider = barSettings.Slider;
            _slider.maxValue = maxHP;
            _slider.value = maxHP;
        }

        public void Follow()
        {
            if (_characterTransform != null)
            {
                var targetPosition = _characterTransform.position;
                Vector2 screenPoint = _camera.WorldToScreenPoint(targetPosition);
                hpBarRectTransform.position = screenPoint;
            }
        }

        public void ChangeValue(int damage)
        {
            _slider.value -= damage;
            if (_slider.value <= 0)
            {
                _barSettings.gameObject.SetActive(false);
                _slider.value = 0;
            }
        }
    }
}