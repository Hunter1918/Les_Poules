using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject _FreeCamera;
    public GameObject pauseMenuUI;
    public string mainMenuSceneName;
    public string gameSceneName;

    private bool isPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                OpenPauseMenu();
        }
    }

    public void OpenPauseMenu()
    {
        _FreeCamera.SetActive (false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; 
        isPaused = true;
        Cursor.visible = true;
    }

    public void ClosePauseMenu()
    {
        _FreeCamera.SetActive(true);
        pauseMenuUI.SetActive(false); 
        Time.timeScale = 1f; 
        isPaused = false;
        Cursor.visible = false;
    }

    public void Resume()
    {
        ClosePauseMenu();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName); 
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(gameSceneName); 
        Time.timeScale = 1f;
    }
}