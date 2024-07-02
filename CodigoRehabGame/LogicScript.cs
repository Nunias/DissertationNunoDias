using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;

    [ContextMenu("IncScore")]
    public void addScore(int ScoretoAdd)
    {
        playerScore = playerScore + ScoretoAdd;
        scoreText.text = playerScore.ToString();
        VariableController.ScoreFinalValue = playerScore.ToString();
    }

}
