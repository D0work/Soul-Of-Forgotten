using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Pause Menu")]
    [Tooltip("Le panel à activer/désactiver avec la touche Échap")]
    [SerializeField] private GameObject pausePage;

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        if (pausePage != null)
            pausePage.SetActive(true);
        isPaused = true;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        if (pausePage != null)
            pausePage.SetActive(false);
        isPaused = false;
    }

    /// <param name="fromPage">Le panel à masquer</param>
    /// <param name="toPage">Le panel à afficher</param>
    public void SwitchPage(GameObject fromPage, GameObject toPage)
    {
        if (fromPage != null)
            fromPage.SetActive(false);

        if (toPage != null)
            toPage.SetActive(true);
    }

    public void Deactivate(GameObject panel)
    {
        if (panel != null)
            panel.SetActive(false);
    }

    public void Activate(GameObject panel)
    {
        if (panel != null)
            panel.SetActive(true);
    }

}

