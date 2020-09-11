using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiBtnFlexTrail : MonoBehaviour
{
    public RectTransform viewportObj;
    public RectTransform contentObj;
    public GameObject buttonPrefab;
    public int btnSize = 100;
    public int btnMargin = 10;
    public Color[] colours;

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
	    btnImg.color = new Color(colours[i].r, colours[i].g, colours[i].b);
	    BtnTrailColour btnScript = newBtn.AddComponent<BtnTrailColour>();
	    btnScript.mainScript = this;
	    btnScript.thisColour = btnImg.color;
	    //newBtn.GetComponent<Button>().onClick.AddListener(delegate {BtnColourChangeNear(btnImg.color);});
	}
    }

    public void BtnColourChangeNear(Color btnColour)
    {
	Debug.Log("Changing trail colour to: " + btnColour.r + ", " + btnColour.g + ", " + btnColour.b);
	PlayerColourData.trailColourNear = btnColour;
    }

    public void BtnColourChangeFar(Color btnColour)
    {
	Debug.Log("Changing trail colour to: " + btnColour.r + ", " + btnColour.g + ", " + btnColour.b);
	PlayerColourData.trailColourFar = btnColour;
    }
}
