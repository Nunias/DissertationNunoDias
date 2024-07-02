using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastPathMenu : MonoBehaviour
{
    public void Path()
    {
        SceneManager.LoadSceneAsync("ShowLastPath");
    }
}
