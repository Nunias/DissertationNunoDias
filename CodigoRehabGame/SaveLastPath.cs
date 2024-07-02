using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLastPath : MonoBehaviour
{
    DatabaseReference dbRef;
    public NewPatient NP;

    private void Awake()
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateLastPath();
    }
    public void UpdateLastPath()
    {
        //print(VariableController.Path);
        NP.Name = VariableController.NameBD;
        NP.SessionHighScore = VariableController.SessionHighScoreBD;
        NP.SessionLastPath = VariableController.Path;
        VariableController.SessionLastPathBD = VariableController.Path;

        string json = JsonUtility.ToJson(NP);
        dbRef.Child("Patient").Child(VariableController.PatientID.ToString()).SetRawJsonValueAsync(json);
        print("Envio completo"); 
        
    }
}
