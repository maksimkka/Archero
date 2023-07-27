using TMPro;
using UnityEngine;

namespace Code.Score
{
    public class ScoreView : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI ScoreText { get; private set; }
    }
}