using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TabControl : MonoBehaviour
{
    [Serializable]
    public struct ButtonData
    {
	public string label;
	public GameObject tabContent;
    }

    //public RectTransform tabBarObj;
    public GameObject buttonPrefab;
    public int barHeight = 50;
    public int btnWidth = 160;
    public int btnMargin = 0;
    [SerializeField]
    public ButtonData[] buttons;
    private GameObject currentTabOpen;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < buttons.Length; i++)
	{
	    GameObject newBtn = Instantiate(buttonPrefab, this.gameObject.transform);
	    RectTransform btnRect = newBtn.GetComponent<RectTransform>();
	    int btnLeft = i*(btnWidth + btnMargin);
	    btnRect.localPosition = new Vector3(btnLeft, -barHeight, 0);
	    //Graphic btnImg = newBtn.GetComponent<Graphic>();
	    newBtn.GetComponentInChildren<Text>().text = buttons[i].label;
	    GameObject thisTabObject = buttons[i].tabContent;
	    newBtn.GetComponent<Button>().onClick.AddListener(delegate {ChangeTabContent(thisTabObject);});
	    Debug.Log("Y = " + btnRect.localPosition.y);
	}
	
	ChangeTabContent(buttons[0].tabContent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeTabContent(GameObject newTabContent)
    {
	if(currentTabOpen) currentTabOpen.SetActive(false);
	newTabContent.SetActive(true);
	currentTabOpen = newTabContent;
    }
}
