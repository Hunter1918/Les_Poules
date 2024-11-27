using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Pause Menu Settings")]
    [SerializeField] private Button _resumeButton = null;
    [SerializeField] private Button _mainMenuButton = null;
    [SerializeField] private Button _restartButton = null;
    public string mainMenuSceneName;
    public string gameSceneName;

    private bool isPaused = false;
    private int originalSortingOrder;

    [Header("Time Settings")]
    public float FastTime = 5f;
    private bool isFastForward = false;
    public TMP_Text timerText;
    public GameController gameController;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeButtons();
        HideAllMenus(); 
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleFastForward();
        }
    }

    private void InitializeButtons()
    {
        if (_resumeButton != null)
            _resumeButton.onClick.AddListener(Resume);
        if (_mainMenuButton != null)
            _mainMenuButton.onClick.AddListener(LoadMainMenu);
        if (_restartButton != null)
            _restartButton.onClick.AddListener(RestartGame);
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        ShowPauseMenu();
        gameController?.OnPause();
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        HidePauseMenu();
        gameController?.OnResume();
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    private void ShowPauseMenu()
    {
        UIMenu.Instance.Show();
    }

    private void HidePauseMenu()
    {
        UIMenu.Instance.Hide();
    }

    private void HideAllMenus()
    {
        UIMenu.Instance.Hide();
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameSceneName);
    }

    private void ToggleFastForward()
    {
        if (isFastForward)
        {
            Time.timeScale = 1f;
            isFastForward = false;
        }
        else
        {
            Time.timeScale = FastTime;
            isFastForward = true;
        }
    }

    public void UpdateGameTime(float elapsedTime, int days, int hours, int minutes, float seconds)
    {
        if (timerText != null)
        {
            timerText.text = string.Format("Day: {0} Time: {1:00}:{2:00}:{3:00}", days, hours, minutes, (int)seconds);
        }
    }
}
