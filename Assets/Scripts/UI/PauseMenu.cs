using System;
using UnityEngine;
using UnityEngine.InputSystem;
using YakisobaGang.Script.Player;

namespace YakisobaGang.Player
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] public GameObject pauseMenuPanel;
        private bool paused;
        private PlayerInputActions input;

        private void Awake()
        {
            input = new PlayerInputActions();
        }

        private void OnEnable()
        {
            input.Enable();
            input.Gameplay.Pause.performed += TogglePauseMenu;
        }

        private void OnDisable()
        {
            input.Gameplay.Pause.performed -= TogglePauseMenu;
            input.Disable();
        }

        private void TogglePauseMenu(InputAction.CallbackContext _)
        {
            if (!paused)
            {
                paused = true;
                pauseMenuPanel.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                paused = false;
                pauseMenuPanel.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
}
