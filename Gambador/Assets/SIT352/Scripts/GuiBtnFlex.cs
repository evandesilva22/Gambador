using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiBtnFlex : MonoBehaviour
{
    public RectTransform viewportObj;
    public RectTransform contentObj;
    public GameObject buttonPrefab;
    public GameObject characterObj;
    public int btnSize = 100;
    public int btnMargin = 10;
    public Vector3[] colours;

    // Start is called before the first frame update
    void Start()
    {
	Vector3[] corners = new Vector3[4];
	viewportObj.GetLocalCorners(corners);
	float viewportWidth = corners[2].x - corners[0].x;
	Debug.Log("Width: " + viewportWidth);
	int btnRow = 0;
	int btnRight = 0;
	bool newRow = false;

        for(int i = 0; i < colours.Length; i++)
	{
	    // create a new button
	    btnRight += (btnSize + (btnMargin*2));
	    newRow = false;
	    if((int)(btnRight / viewportWidth) >= 1)
	    {
		btnRow += (int)(btnRight / viewportWidth);
		newRow = true;
	    }
	    GameObject newBtn = Instantiate(buttonPrefab, this.gameObject.transform);
	    RectTransform btnRect = newBtn.GetComponent<RectTransform>();
	    if(newRow) btnRight = (btnSize + (btnMargin*2));
	    int btnLeft = btnRight - (btnSize + btnMargin);
	    //btnLeft -= (int)viewportWidth*btnRow;
	    btnRect.transform.localPosition = new Vector3(btnLeft, -1 * ((btnRow * (btnSize + btnMargin*2)) + btnMargin), 0);
	    Graphic btnImg = newBtn.GetComponent<Graphic>();
	    btnImg.color = new Color(colours[i].x, colours[i].y, colours[i].z);
	    newBtn.GetComponent<Button>().onClick.AddListener(delegate {BtnColourChange(btnImg.color);});
	}
	
	RectTransform contentRect = contentObj.GetComponent<RectTransform>();
	//contentRect.rect.height = (btnRow+1)*(btnSize+(btnMargin*2));
	contentRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (btnRow+1)*(btnSize+(btnMargin*2)));
	//contentRect.rect = new Rect(0,0,0,0);
	//Debug.Log(
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BtnColourChange(Color btnColour)
    {
	Debug.Log("Changing player colour to: " + btnColour.r + ", " + btnColour.g + ", " + btnColour.b);
	Renderer charRenderer = characterObj.GetComponent<Renderer>();
	charRenderer.materials[0].color = btnColour;
	charRenderer.materials[1].color = btnColour;
	charRenderer.materials[2].color = btnColour;
	charRenderer.materials[0].SetColor("_EmissionColor", btnColour);
	charRenderer.materials[1].SetColor("_EmissionColor", btnColour);
	charRenderer.materials[2].SetColor("_EmissionColor", btnColour);
	charRenderer.materials[0].SetColor("_BaseColor", btnColour);
	charRenderer.materials[1].SetColor("_BaseColor", btnColour);
	charRenderer.materials[2].SetColor("_BaseColor", btnColour);
	Debug.Log("Player colour is: " + charRenderer.materials[0].color.ToString());
	PlayerColourData.playerColour = btnColour;
    }
}
