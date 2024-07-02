using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpawner : MonoBehaviour
{
    public GameObject path;
    public float spawnRate = 17.73f;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawnPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate / VariableController.Velocity)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            spawnPath();
            timer = 0;
        }
        
    }

    void spawnPath()
    {
        Instantiate(path, transform.position, transform.rotation);
    }
}
