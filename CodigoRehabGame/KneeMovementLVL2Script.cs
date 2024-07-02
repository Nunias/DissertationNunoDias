using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KneeMovementLVL2Script : MonoBehaviour
{
    public float pos = 0;
    private float valorMinimoEntrada = -1.8056f;
    private float valorMaximoEntrada = 46.8056f;
    private float novoValorMinimo = -5;
    private float novoValorMaximo = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //VariableController.AnglePosition = pos.ToString();
        float valorMapeado = Mathf.Lerp(novoValorMinimo, novoValorMaximo, Mathf.InverseLerp(valorMinimoEntrada, valorMaximoEntrada, VariableController.AnglePosition));// pos));

        transform.position = new Vector3(transform.position.x, valorMapeado, 0);
    }
}
