using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PatientAreaMenu : MonoBehaviour
{
    public void PatientMenuPainel()
    {
        SceneManager.LoadSceneAsync("Patient Area");
    }
}
