using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ReadEndScore : MonoBehaviour
{
    public Text score;
    public float HS;
    public static string Path = "; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2 ; 2.2 ; 4 ; 5 ; 1.2 ; 3.2";


    // Start is called before the first frame update
    void Start()
    {
        score.text = VariableController.ScoreFinalValue;
        // Remove leading and trailing semicolons, then split the string
        string[] pathElements = VariableController.Path.Trim(';').Split(';');
        float PathLength = pathElements.Length;
        float HighScore = PathLength * 0.4f;
        Debug.Log("Number of elements in the array: " + PathLength);
        Debug.Log("HighScore: " + PathLength * 0.4f);
        //HS = VariableController.Path * 0.4;

        if (int.Parse(VariableController.ScoreFinalValue) > HighScore * 0.75f)
        {
            score.color = Color.green;
        }
        else if (int.Parse(VariableController.ScoreFinalValue) > HighScore * 0.50f)
        {
            score.color = Color.yellow;
        }
        else{
            score.color = Color.red;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
