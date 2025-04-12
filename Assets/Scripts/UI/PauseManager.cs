using UnityEngine;

namespace UI
{
    public class PauseManager : MonoBehaviour
    {
        [SerializeField] private GameObject pausePanel;
        private bool _isPaused = false;

        private void Start()
        {
            // Make sure pause panel is hidden at start
            if (pausePanel != null)
                pausePanel.SetActive(false);
        }

        private void Update()
        {
            // Check for pause input (Escape key by default)
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }

        public void TogglePause()
        {
            _isPaused = !_isPaused;
        
            // Activate or deactivate the pause panel
            if (pausePanel != null)
                pausePanel.SetActive(_isPaused);
        
            // Set the time scale (0 = paused, 1 = normal speed)
            Time.timeScale = _isPaused ? 0f : 1f;
        
            // Optional: Enable/disable player input or other game systems
            // playerController.enabled = !isPaused;
        }

        // Call this from UI buttons in your pause panel
        public void ResumeGame()
        {
            if (_isPaused)
                TogglePause();
        }
    }
}