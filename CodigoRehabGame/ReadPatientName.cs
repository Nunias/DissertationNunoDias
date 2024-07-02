using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class NewPatient
{
    public string Name;
    public string SessionHighScore;
    public string SessionLastPath;
}

public class ReadPatientName : MonoBehaviour
{
    public Text PatientName;
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
        print(PatientIDS.GetType());
        DownloadPatientData();
    }

    // Update is called once per frame
    void Update()
    {
        DownloadPatientData();
    }

    public void DownloadPatientData()
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
            //print(NP.Name);
            PatientName.text = NP.Name;
        }
        else
        {
            print("no data found");
        }

    }
    
    public void UpdateName()
    {
        NP.Name = VariableController.NamePatient;
        string json = JsonUtility.ToJson(NP);
        dbRef.Child("Patient").Child(VariableController.PatientID.ToString()).SetRawJsonValueAsync(json);
        print("Envio completo");
    }
}
