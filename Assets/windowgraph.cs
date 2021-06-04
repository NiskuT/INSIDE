using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class windowgraph : MonoBehaviour
{

    private RectTransform graphContainer;
    [SerializeField] private Sprite circleSprite;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    private RectTransform dashTemplateX;
    private RectTransform dashTemplateY;
    private List<GameObject> gameObjectList;
    
    //cached values
    private List<int> valueList;
    private int maxVisibleValueAmount = -1;
    private int zoom = 1;
    private double deltaT = 0.2222222222222222;

    void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();
        dashTemplateX = graphContainer.Find("dashTemplateX").GetComponent<RectTransform>();
        dashTemplateY = graphContainer.Find("dashTemplateY").GetComponent<RectTransform>();

        gameObjectList = new List<GameObject>();
        
        List<int> valueList = new List<int>() {0, 5, 11, 15, 18, 19, 19, 17, 13, 8, 2, -3, -8, -13, -17, -19, -19, -18, -15, -11, -5, 0, 6, 11, 15, 18, 19, 19, 17, 13,0, 5, 11, 15, 18, 19, 19, 17, 13, 8, 2, -3, -8, -13, -17, -19, -19, -18, -15, -11, -5, 0, 6, 11, 15, 18, 19, 19, 17, 13 };


  
           
        ShowGraph(valueList,deltaT, maxVisibleValueAmount, zoom);
              
        transform.Find("leftButton").GetComponent<Button_UI>().ClickFunc = () =>
        {
            moveGraphLeft();
        };
        transform.Find("rightButton").GetComponent<Button_UI>().ClickFunc = () =>
        {
            moveGraphRight();
        };
        transform.Find("zoomInButton").GetComponent<Button_UI>().ClickFunc = () =>
        {
            IncreaseVisibleAmount();
        };
        transform.Find("zoomOutButton").GetComponent<Button_UI>().ClickFunc = () =>
        {
            DecreaseVisibleAmount();
        };
        transform.Find("quBit1").GetComponent<Button_UI>().ClickFunc = () =>
        {
            quBit1();
        };
        transform.Find("quBit2").GetComponent<Button_UI>().ClickFunc = () =>
        {
            quBit2();
        };
        transform.Find("quBit3").GetComponent<Button_UI>().ClickFunc = () =>
        {
            quBit3();
        };
        transform.Find("quBit4").GetComponent<Button_UI>().ClickFunc = () =>
        {
            quBit4();
        };

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

    private void IncreaseVisibleAmount()
    {
        ShowGraph(this.valueList,this.deltaT, this.maxVisibleValueAmount, this.zoom-5);
    }
    private void DecreaseVisibleAmount()
    {
        ShowGraph(this.valueList,this.deltaT, this.maxVisibleValueAmount, this.zoom+5);
    }
    private void moveGraphRight()
    {
        ShowGraph(this.valueList,this.deltaT, this.maxVisibleValueAmount-5, this.zoom);
    }
    private void moveGraphLeft()
    {
        ShowGraph(this.valueList,this.deltaT, this.maxVisibleValueAmount+5, this.zoom);
    }

    private void quBit1()
    {
        List<int> valueList = new List<int>() {0, 5, 11, 15, 18, 19};
        ShowGraph(valueList,this.deltaT, this.maxVisibleValueAmount, this.zoom);
    }
    private void quBit2()
    {
        List<int> valueList = new List<int>() {0, 5, 11, 15, 18, 19, 5, 11, 15, 18, 19};
        ShowGraph(valueList,this.deltaT, this.maxVisibleValueAmount, this.zoom);
    }
    private void quBit3()
    {
        List<int> valueList = new List<int>() {11, 15, 18, 19, 5, 11, 15, 18, 19};
        ShowGraph(valueList,this.deltaT, this.maxVisibleValueAmount, this.zoom);
    }
    private void quBit4()
    {
        List<int> valueList = new List<int>() {0, 5, 11, 15, 18, 19, 5, 11, 15,11, 15, 18, 19, 5, 11, 15, 18};
        ShowGraph(valueList,this.deltaT, this.maxVisibleValueAmount, this.zoom);
    }

    private void ShowGraph(List<int> valueList, double T, int maxVisibleValueAmount, int zoom)
    {
        int nbDeValSurGraphParDefaut = 10;
        this.valueList = valueList;
        this.deltaT = T;
        

        if (maxVisibleValueAmount <= 0)
        {
            maxVisibleValueAmount = valueList.Count;
        }

        if (maxVisibleValueAmount > valueList.Count-1)
        {
            maxVisibleValueAmount = valueList.Count-1;
        }

        if (  maxVisibleValueAmount <  this.zoom + nbDeValSurGraphParDefaut)
        {
            maxVisibleValueAmount = this.zoom + nbDeValSurGraphParDefaut;
        }

        this.maxVisibleValueAmount = maxVisibleValueAmount;
        
        foreach (GameObject gameObject in gameObjectList)
        {
            Destroy(gameObject);
        }

        gameObjectList.Clear();
        
        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = graphContainer.sizeDelta.x;
        float yMax = valueList[0];

        
        
        foreach (int value in valueList)
        {
            if (Mathf.Abs(value) > yMax)
            {
                yMax = Mathf.Abs(value);
            }
        }

        yMax = yMax * 1.2f;
        
        

        GameObject lastCircleGameObject = null;

        int min=Mathf.Max(valueList.Count - maxVisibleValueAmount,0);
        int max = valueList.Count - maxVisibleValueAmount + zoom + nbDeValSurGraphParDefaut;

        if (max > valueList.Count-1)
        {
            max = valueList.Count-1;
        }
        else if (max < 2)
        {
            max = 2;
        }
        else
        {
            this.zoom = zoom;
        }
        

        int xIndex = 0;
        for (int i = min; i <= max; i++)
        {
            float xPosition = (xIndex / (float)(max-min)) * graphWidth;
            float yPosition = (graphHeight/2f)+(valueList[i] / yMax) * (graphHeight/2f);
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            gameObjectList.Add(circleGameObject);
            if (lastCircleGameObject != null)
            {
                GameObject dotConnectionGameObject = CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition,
                    circleGameObject.GetComponent<RectTransform>().anchoredPosition);
                gameObjectList.Add(dotConnectionGameObject);
            }
            lastCircleGameObject = circleGameObject;
            xIndex++;

        }

        int nbbarverticals = 20;
        for (int i = 1; i <= nbbarverticals; i++)
        {
            float xPos = graphWidth * ((float)i / (float)nbbarverticals);
            RectTransform dashY = Instantiate(dashTemplateY); 
            dashY.SetParent(graphContainer, false); 
            dashY.gameObject.SetActive(true);
            dashY.anchoredPosition = new Vector2(xPos, 0);
            gameObjectList.Add(dashY.gameObject);
            
            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPos-6f, -7f);

            double nb = min + i * (double)(max + 1 - min) / nbbarverticals;   //i+(valueList.Count - maxVisibleValueAmount);
            nb = nb * T;
            nb = Mathf.Round((float)nb * 100) / 100;
            labelX.GetComponent<Text>().text = nb.ToString();
            gameObjectList.Add(labelX.gameObject);

        }
        
        int nbbarhoriz = 8;
        for (int i = 1; i <= nbbarhoriz; i++)
        {
            float yPos = graphHeight * ((float)i / (float)nbbarhoriz);
            RectTransform dashX = Instantiate(dashTemplateX);
            dashX.SetParent(graphContainer, false);
            dashX.gameObject.SetActive(true);
            dashX.anchoredPosition = new Vector2(0, yPos);
            gameObjectList.Add(dashX.gameObject);
            
            
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer);
            labelY.gameObject.SetActive(true);
            float normalizeValue = yMax*(i-(nbbarhoriz/2f))/(nbbarhoriz/2f);
            labelY.anchoredPosition = new Vector2(-20f, yPos);
            labelY.GetComponent<Text>().text = Mathf.RoundToInt(normalizeValue).ToString();
            gameObjectList.Add(labelY.gameObject);

        }
        

    }

    private GameObject CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);

        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        
        
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA+dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(dir));
        return gameObject;

    }
    
    private static float GetAngleFromVectorFloat(Vector3 dir) {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
    
    
}



