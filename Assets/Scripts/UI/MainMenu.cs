using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        public void LoadGame()
        {
            // Load the game scene (assuming it's the first scene in the build settings)
            SceneManager.LoadScene(1);
        }
    }
}
