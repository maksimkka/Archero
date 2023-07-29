using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.HUD
{
    public class ScreenService : MonoBehaviour
    {
        [SerializeField] private List<ScreenView> screens;

        private void Awake()
        {
            ScreenSwitcher.Initialize(screens);
        }
    }
}