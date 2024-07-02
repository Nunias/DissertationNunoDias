using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputHandler : MonoBehaviour
{
    public TMP_InputField inputField;  // Reference to the TMP_InputField
    public float sp;

    private void Start()
    {

    }

    public void GetInputValue()
    {
        string inputValue = inputField.text;
        Debug.Log("Input Value: " + inputValue);

        if (inputValue == "")
        {
            VariableController.Velocity = 1;
        }
        else
        {
            sp = float.Parse(inputValue);
        }

        if (sp <= 0.5f && inputValue != "")
        {
            VariableController.Velocity = 0.5f;
        }
        else if (sp > 0.5f && inputValue != "")
        {
            VariableController.Velocity = 18/sp;
        }
        
        Debug.Log("Input Value Parse: " + VariableController.Velocity);

    }
}
