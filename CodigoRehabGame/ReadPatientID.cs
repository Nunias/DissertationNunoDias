using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ReadPatientID : MonoBehaviour
{
    public Text ID;

    // Start is called before the first frame update
    void Start()
    {
        ID.text = VariableController.PatientID.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
