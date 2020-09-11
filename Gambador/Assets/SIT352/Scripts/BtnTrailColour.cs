using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnTrailColour : MonoBehaviour, IPointerClickHandler
{
    public GuiBtnFlexTrail mainScript;
    public Color thisColour;

    public void OnPointerClick(PointerEventData eventData)
    {
	Debug.Log("Pointer click");
	if(eventData.button == PointerEventData.InputButton.Left)
	{
	    // Change near colour
	    Debug.Log("Changing trail colour to: " + thisColour.r + ", " + thisColour.g + ", " + thisColour.b);
	    PlayerColourData.trailColourNear = thisColour;
	}
	if(eventData.button == PointerEventData.InputButton.Right)
	{
	    // Change far colour
	    Debug.Log("Changing trail colour to: " + thisColour.r + ", " + thisColour.g + ", " + thisColour.b);
	    PlayerColourData.trailColourFar = thisColour;
	}
    }
}
