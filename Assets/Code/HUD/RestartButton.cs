using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.HUD
{
    public class RestartButton : MonoBehaviour
    {
        private Button restartButton;

        private void Awake()
        {
            restartButton = gameObject.GetComponent<Button>();
            restartButton.onClick.AddListener(Restart);
        }

        private void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}