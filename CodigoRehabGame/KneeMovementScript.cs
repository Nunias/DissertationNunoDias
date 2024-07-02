using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KneeMovementScript : MonoBehaviour
{
    //public float pos = 0;
    private float valorMinimoEntrada = -5;
    private float valorMaximoEntrada = 70.33f;
    private float novoValorMinimo = -5.2299f;
    private float novoValorMaximo = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //VariableController.AnglePosition = pos.ToString();
        float valorMapeado = Mathf.Lerp(novoValorMinimo, novoValorMaximo, Mathf.InverseLerp(valorMinimoEntrada, valorMaximoEntrada, VariableController.AnglePosition));

        transform.position = new Vector3(transform.position.x, valorMapeado, 0);
    }
}
