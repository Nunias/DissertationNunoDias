using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticsMenu : MonoBehaviour
{
    public void StatsMenuPainel()
    {
        SceneManager.LoadSceneAsync("Stats Patient Menu");
    }
    
}


