using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePath2P : MonoBehaviour
{
    public LogicScript logic;
    public AudioScript sf;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        sf = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        logic.addScore(1);
        sf.GoodSound();
    }
}