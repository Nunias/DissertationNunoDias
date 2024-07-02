using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetPatientName : MonoBehaviour
{
    public string Name;

    public void GrabFromInputField(string input)
    {
        if (input == string.Empty)
        {
            return;
        }

        Name = input;
        VariableController.NamePatient = Name;

    }
}