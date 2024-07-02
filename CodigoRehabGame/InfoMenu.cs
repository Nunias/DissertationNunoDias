using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoMenu : MonoBehaviour
{
    public void LoadMenuPainel()
    {
        SceneManager.LoadSceneAsync("Load Menu");
    }
}
