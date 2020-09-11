using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiBtnFlexKatana : MonoBehaviour
{
    public RectTransform viewportObj;
    public RectTransform contentObj;
    public GameObject buttonPrefab;
    public GameObject katanaObj;
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
	    newBtn.GetComponent<Button>().onClick.AddListener(delegate {BtnColourChange(btnImg.color);});
	}
    }

    void BtnColourChange(Color btnColour)
    {
	Debug.Log("Changing katana colour to: " + btnColour.r + ", " + btnColour.g + ", " + btnColour.b);
	Renderer charRenderer = katanaObj.GetComponent<Renderer>();
	charRenderer.materials[0].color = btnColour;
	charRenderer.materials[0].SetColor("_EmissionColor", btnColour);
	charRenderer.materials[0].SetColor("_BaseColor", btnColour);
	Debug.Log("Katana colour is: " + charRenderer.materials[0].color.ToString());
	PlayerColourData.katanaColour = btnColour;
    }
}
