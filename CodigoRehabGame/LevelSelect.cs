using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void Level1()
    {
        SceneManager.LoadSceneAsync("Level 1");
    }

    public void Level2()
    {
        SceneManager.LoadSceneAsync("Level 2");
    }
    
    public void Level3()
    {
        SceneManager.LoadSceneAsync("Level 3");
    }
}
