using UnityEngine;
using UnityEngine.UI;

namespace Code.Game.HealthBar
{
    [DisallowMultipleComponent]
    public class HealthBarSettings : MonoBehaviour
    {
        [field: SerializeField] public Slider Slider { get; private set; }
        [field: SerializeField] public bool IsSelected { get; private set; }

        private void OnEnable()
        {
            IsSelected = true;
        }

        private void OnDisable()
        {
            IsSelected = false;
        }
    }
}