using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveHighScore : MonoBehaviour
{
    DatabaseReference dbRef;
    public string PatientIDS = VariableController.PatientID.ToString();
    public NewPatient NP;

    private void Awake()
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Start is called before the first frame update
    void Start()
    {
        AddHighScore();
    }

    public void AddHighScore()
    {
        VariableController.SessionHighScoreBD = VariableController.SessionHighScoreBD + " ; " + VariableController.ScoreFinalValue;
        NP.Name= VariableController.NameBD;
        NP.SessionHighScore = VariableController.SessionHighScoreBD;
        NP.SessionLastPath = VariableController.SessionLastPathBD;

        string json = JsonUtility.ToJson(NP);
        dbRef.Child("Patient").Child(VariableController.PatientID.ToString()).SetRawJsonValueAsync(json);
        print("Envio completo");
    }
}
