using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPatientID : MonoBehaviour
{
    public int totalPatient;
    public NewPatient NP;
    public string PatientIDS = VariableController.PatientID.ToString();
    DatabaseReference dbRef;
    private void Awake()
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SearchPatient()
    {
        StartCoroutine(SearchPatientEm());
    }

    IEnumerator SearchPatientEm()
    {
        var serverData = dbRef.Child("Patient").GetValueAsync();
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Patient");
        reference.GetValueAsync().ContinueWith(task =>
        {
            totalPatient = (int)task.Result.ChildrenCount; //mandar este valor para fora, assim se quiserem introduzir um novo paciente sabem qual o numero seguinte (se possivel meter dentro do input)
        });

        yield return new WaitUntil(predicate: () => serverData.IsCompleted);
        Debug.Log("Patient: " + totalPatient + " - PatientID: " + VariableController.PatientID.ToString());
        if (totalPatient >= int.Parse(VariableController.PatientID.ToString()))
        {
            Debug.Log("Paciente a Atualizar");
            SceneManager.LoadSceneAsync("Main Menu");
        }
        else
        {
            Debug.Log("Criar novo paciente");
            NewPatient();
        }

    }

    public void NewPatient()
    {
        VariableController.NameBD = NP.Name = "Patient";
        VariableController.SessionHighScoreBD = NP.SessionHighScore = "";
        VariableController.SessionLastPathBD = NP.SessionLastPath = "";

        string json = JsonUtility.ToJson(NP);
        dbRef.Child("Patient").Child(VariableController.PatientID.ToString()).SetRawJsonValueAsync(json);
        print("Envio completo");
        SceneManager.LoadSceneAsync("Main Menu");
    }
}
