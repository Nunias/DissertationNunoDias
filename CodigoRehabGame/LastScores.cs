using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class HighScoreSession
{
    public string SessionHighScore;
}

public class LastScores : MonoBehaviour
{
    public int NumbSessions;
    public string PatientIDS = VariableController.PatientID.ToString();
    public HighScoreSession dtsHS;
    DatabaseReference dbRef;

    public Text Score1;
    public Text Score2;
    public Text Score3;
    public Text Score4;
    public Text Score5;

    private void Awake()
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LoadNumSession();
    }

    public void LoadNumSession()
    {
        StartCoroutine(LoadHS());
    }
    IEnumerator LoadHS()
    {
        var serverData = dbRef.Child("Patient").Child(VariableController.PatientID.ToString()).GetValueAsync();
        yield return new WaitUntil(predicate: () => serverData.IsCompleted);

        //print("process is complete");

        DataSnapshot snapshot = serverData.Result;
        string jsonData = snapshot.GetRawJsonValue();

        if (jsonData != null)
        {
            //print("server data found");

            dtsHS = JsonUtility.FromJson<HighScoreSession>(jsonData);
            string[] valores = dtsHS.SessionHighScore.Split(';');
            NumbSessions = valores.Length;
            if (NumbSessions == 1)
            {
                string ultimoValor = valores[valores.Length - 1];
                ultimoValor = ultimoValor.Trim();
                //ebug.Log("Último valor: " + ultimoValor);
                Score1.text = ultimoValor;
                Score2.text = "----";
                Score3.text = "----";
                Score4.text = "----";
                Score5.text = "----";
            }
            else if (NumbSessions == 2)
            {
                string ultimoValor = valores[valores.Length - 1];
                ultimoValor = ultimoValor.Trim();
                //Debug.Log("Último valor: " + ultimoValor);
                Score1.text = ultimoValor;
                string ultimoValor2 = valores[valores.Length - 2];
                ultimoValor2 = ultimoValor2.Trim();
                //Debug.Log("Último valor: " + ultimoValor2);
                Score2.text = ultimoValor2;
                Score3.text = "----";
                Score4.text = "----";
                Score5.text = "----";
            }
            else if (NumbSessions == 3)
            {
                string ultimoValor = valores[valores.Length - 1];
                ultimoValor = ultimoValor.Trim();
                //Debug.Log("Último valor: " + ultimoValor);
                Score1.text = ultimoValor;
                string ultimoValor2 = valores[valores.Length - 2];
                ultimoValor2 = ultimoValor2.Trim();
                //Debug.Log("Último valor: " + ultimoValor2);
                Score2.text = ultimoValor2;
                string ultimoValor3 = valores[valores.Length - 3];
                ultimoValor3 = ultimoValor3.Trim();
                //Debug.Log("Último valor: " + ultimoValor3);
                Score3.text = ultimoValor3;
                Score4.text = "----";
                Score5.text = "----";
            }
            else if (NumbSessions == 4)
            {
                string ultimoValor = valores[valores.Length - 1];
                ultimoValor = ultimoValor.Trim();
                //Debug.Log("Último valor: " + ultimoValor);
                Score1.text = ultimoValor;
                string ultimoValor2 = valores[valores.Length - 2];
                ultimoValor2 = ultimoValor2.Trim();
                //Debug.Log("Último valor: " + ultimoValor2);
                Score2.text = ultimoValor2;
                string ultimoValor3 = valores[valores.Length - 3];
                ultimoValor3 = ultimoValor3.Trim();
                //Debug.Log("Último valor: " + ultimoValor3);
                Score3.text = ultimoValor3;
                string ultimoValor4 = valores[valores.Length - 4];
                ultimoValor4 = ultimoValor4.Trim();
                //Debug.Log("Último valor: " + ultimoValor4);
                Score4.text = ultimoValor4;
                Score5.text = "----";
            }
            else if (NumbSessions >= 5)
            {
                string ultimoValor = valores[valores.Length - 1];
                ultimoValor = ultimoValor.Trim();
                //Debug.Log("Último valor: " + ultimoValor);
                Score1.text = ultimoValor;
                string ultimoValor2 = valores[valores.Length - 2];
                ultimoValor2 = ultimoValor2.Trim();
                //Debug.Log("Último valor: " + ultimoValor2);
                Score2.text = ultimoValor2;
                string ultimoValor3 = valores[valores.Length - 3];
                ultimoValor3 = ultimoValor3.Trim();
                //Debug.Log("Último valor: " + ultimoValor3);
                Score3.text = ultimoValor3;
                string ultimoValor4 = valores[valores.Length - 4];
                ultimoValor4 = ultimoValor4.Trim();
                //Debug.Log("Último valor: " + ultimoValor4);
                Score4.text = ultimoValor4;
                string ultimoValor5 = valores[valores.Length - 5];
                ultimoValor5 = ultimoValor5.Trim();
                //Debug.Log("Último valor: " + ultimoValor5);
                Score5.text = ultimoValor5;
            }

        }
        else
        {
            print("no data found");
        }
    }
}
