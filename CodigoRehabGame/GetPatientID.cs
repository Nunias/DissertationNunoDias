using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GetPatientID : MonoBehaviour
{

    public int ID;

    [Header("Valor obtido no input")]
    public string inputPatientID;
    public int inputParsedAsInt;

    public void GrabFromInputField (string input)
    {
        if (input == string.Empty)
        {
            return;
        }
        
        inputPatientID = input;
        inputParsedAsInt = int.Parse(input);
        ID = inputParsedAsInt;
        VariableController.PatientID = ID;

        //SceneManager.LoadSceneAsync("Main Menu");
    }
}
