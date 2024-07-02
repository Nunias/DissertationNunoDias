using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownloadPatientData : MonoBehaviour
{
    public NewPatient NP;
    public string PatientIDS = VariableController.PatientID.ToString();
    DatabaseReference dbRef;
    private void Awake()
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    void Start()
    {
        VariableController.Path = "";
        DownloadPatientDataFN();
    }

    public void DownloadPatientDataFN()
    {
        StartCoroutine(DownloadPatientDataSC());
    }

    IEnumerator DownloadPatientDataSC()
    {
        var serverData = dbRef.Child("Patient").Child(VariableController.PatientID.ToString()).GetValueAsync();
        yield return new WaitUntil(predicate: () => serverData.IsCompleted);

        DataSnapshot snapshot = serverData.Result;
        string jsonData = snapshot.GetRawJsonValue();

        if (jsonData != null)
        {
            NP = JsonUtility.FromJson<NewPatient>(jsonData);
            VariableController.NameBD = NP.Name;
            VariableController.SessionHighScoreBD = NP.SessionHighScore;
            VariableController.SessionLastPathBD = NP.SessionLastPath;
}
        else
        {
            print("no data found");
        }

    }
}
