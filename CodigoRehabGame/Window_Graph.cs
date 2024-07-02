using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using System;

public class Window_Graph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    private RectTransform dashTemplateX;
    private RectTransform dashTemplateY;

    private void Awake()
    {
        graphContainer = transform.Find("GraphContainer").GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();
        dashTemplateX = graphContainer.Find("dashTemplateX").GetComponent<RectTransform>();
        dashTemplateY = graphContainer.Find("dashTemplateY").GetComponent<RectTransform>();
    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    private void ShowGraph(List<float> valueList)
    {
        foreach (Transform child in graphContainer)
        {
            if (child.name != "Background Graph" && child.name != "labelTemplateX" && child.name != "labelTemplateY" && child.name != "dashTemplateX" && child.name != "dashTemplateY")
            {
                Destroy(child.gameObject);
            }
        }

        float graphWidth = graphContainer.sizeDelta.x;
        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = 90f;
        float xSize = graphWidth/100;

        GameObject lastCircleGameObject = null;
        
        int separatorCount = 9;
        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = xSize/2+i * xSize;
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition,yPosition));
            if(lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;

            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer);
            labelX.gameObject.SetActive(true);
            if (i % 10 == 0) {
                labelX.anchoredPosition = new Vector2(xPosition, -7f);
                labelX.GetComponent<Text>().text = i.ToString();
            }
            

            RectTransform dashX = Instantiate(dashTemplateX);
            dashX.SetParent(graphContainer);
            dashX.gameObject.SetActive(true);
            dashX.anchoredPosition = new Vector2(xPosition, -7f);
        }

        for (int i = 0;i <= separatorCount; i++) {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f/separatorCount;
            labelY.anchoredPosition = new Vector2(-7f, normalizedValue*graphHeight);
            labelY.GetComponent<Text>().text = Mathf.RoundToInt(normalizedValue * yMaximum).ToString();

            RectTransform dashY = Instantiate(dashTemplateY);
            dashY.SetParent(graphContainer);
            dashY.gameObject.SetActive(true);
            dashY.anchoredPosition = new Vector2(-4f, normalizedValue * graphHeight);
        }
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1,1,1, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform> ();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0,UtilsClass.GetAngleFromVectorFloat(dir));
    }

    private void Start()
    {
        List<float> floatList = new List<float>();

        // Remover espaços adicionais e dividir a string por ';'
        string[] parts = VariableController.SessionLastPathBD.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string part in parts)
        {
            // Remover espaços em branco e converter para float
            string trimmedPart = part.Trim();
            if (float.TryParse(trimmedPart, out float floatValue))
            {
                floatList.Add(floatValue);
            }
            else
            {
                Debug.Log("Não foi possível converter '{trimmedPart}' para float.");
            }
        }

        /*
        // Obter os primeiros 15 valores
        List<float> first100 = GetFirstNValues(floatList, 100);
        

        //List<int> valueList = new List<int>() { 10, 20, 20, 10, 7, 10, 27, 50, 55, 20, 10, 20, 20, 10, 7, 10, 27 };
        ShowGraph(first100);*/

        StartCoroutine(ProcessListInChunks(floatList, 100, 5));
    }

    public static List<float> GetFirstNValues(List<float> list, int n)
    {
        // Garantir que não tentamos obter mais valores do que existem na lista
        int count = Math.Min(n, list.Count);
        return list.GetRange(0, count);
    }

    private List<float> GetChunk(List<float> list, int start, int chunkSize)
    {
        List<float> chunk = new List<float>();
        for (int i = start; i < start + chunkSize && i < list.Count; i++)
        {
            chunk.Add(list[i]);
        }
        return chunk;
    }

    private IEnumerator ProcessListInChunks(List<float> list, int chunkSize, float intervalSeconds)
    {
        for (int i = 0; i < list.Count; i += chunkSize)
        {
            List<float> chunk = GetChunk(list, i, chunkSize);

            // Processar o bloco de 100 valores
            ShowGraph(chunk);

            // Esperar por 5 segundos antes de processar o próximo bloco
            yield return new WaitForSeconds(intervalSeconds);
        }
    }
}
