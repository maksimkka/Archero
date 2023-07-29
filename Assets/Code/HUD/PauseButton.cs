using UnityEngine;
using UnityEngine.UI;

namespace Code.HUD
{
    [DisallowMultipleComponent]
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] public Button OpenMenuButton;

        private bool _isPause;

        private void Awake()
        {
            OpenMenuButton.onClick.AddListener(() => Pause());
        }

        private void Pause()
        {
            _isPause = !_isPause;

            if (_isPause)
            {
                Time.timeScale = 0;
                ScreenSwitcher.ShowScreen(ScreenType.Pause);
            }

            else
            {
                Time.timeScale = 1;
                ScreenSwitcher.HideAllScreens();
            }
        }
    }
}