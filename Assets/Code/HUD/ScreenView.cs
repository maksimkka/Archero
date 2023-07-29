using UnityEngine;

namespace Code.HUD
{
    public class ScreenView : MonoBehaviour
    {
        [field: SerializeField] public ScreenType type { get; private set; }
    }
}