using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[Serializable]
public class AnglePos
{
    public float angle;
}

public class ReadAngleDataBase : MonoBehaviour
{
    public AnglePos dtsPos;
    DatabaseReference dbRef;
    public float pos;

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
        LoadDataAngle();
    }

    public void LoadDataAngle()
    {
        StartCoroutine(LoadDataVal());
    }
    IEnumerator LoadDataVal()
    {
        var serverData = dbRef.Child("Exercise2").GetValueAsync();
        yield return new WaitUntil(predicate: () => serverData.IsCompleted);

        //print("process is complete");

        DataSnapshot snapshot = serverData.Result;
        string jsonData = snapshot.GetRawJsonValue();

        if (jsonData != null)
        {
            dtsPos = JsonUtility.FromJson<AnglePos>(jsonData);
            //Debug.Log("Angle: " + dtsPos.angle);
            VariableController.AnglePosition = dtsPos.angle;
            Debug.Log("Angle: " + VariableController.AnglePosition);
        }
        else
        {
            print("no data found");
        }
    }
}
