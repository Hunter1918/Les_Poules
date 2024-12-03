using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public string Game1SceneName;
    public string Game2SceneName;

    public void LoadGame1()
    {
        SceneManager.LoadScene(Game1SceneName);
    }

    public void LoadGame2()
    {
        SceneManager.LoadScene(Game2SceneName);
    }
}
