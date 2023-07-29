using Code.HUD;
using UnityEngine;

namespace Code.Finish
{
    [DisallowMultipleComponent]
    public class FinishGate : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == Layers.Hero)
            {
                ScreenSwitcher.ShowScreen(ScreenType.Victory);
            }
        }
    }
}