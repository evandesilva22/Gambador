using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColourData : MonoBehaviour
{
    static public Color playerColour = new Color(1,1,1);
    static public Color katanaColour = new Color(1,1,1);
    static public Color trailColourNear = new Color(1,1,1,0);
    static public Color trailColourFar = new Color(1,1,1,0);

    void Start()
    {
	Renderer charRenderer = GameObject.Find("Plusio").GetComponent<Renderer>();
	for(int i = 0; i < charRenderer.materials.Length; i++)
	{
	    charRenderer.materials[i].color = playerColour;
	    charRenderer.materials[i].SetColor("_EmissionColor", playerColour);
	    charRenderer.materials[i].SetColor("_BaseColor", playerColour);
	}
	Renderer katanaRenderer = GameObject.Find("Katana").GetComponent<Renderer>();
	for(int i = 0; i < katanaRenderer.materials.Length; i++)
	{
	    katanaRenderer.materials[i].color = katanaColour;
	    katanaRenderer.materials[i].SetColor("_EmissionColor", katanaColour);
	    katanaRenderer.materials[i].SetColor("_BaseColor", katanaColour);
	}
	
	if(trailColourNear.a != 0)
	{
	    Debug.Log("Updating trail colour");
	    TrailRenderer trailRenderer = GameObject.Find("Trail").GetComponent<TrailRenderer>();
	    Gradient newGradient = new Gradient();
	    newGradient.SetKeys(
		new GradientColorKey[] { new GradientColorKey(trailColourNear, trailRenderer.colorGradient.colorKeys[0].time), new GradientColorKey(trailColourFar, trailRenderer.colorGradient.colorKeys[1].time) },
		new GradientAlphaKey[] { new GradientAlphaKey(trailRenderer.colorGradient.alphaKeys[0].alpha, trailRenderer.colorGradient.alphaKeys[0].time), new GradientAlphaKey(trailRenderer.colorGradient.alphaKeys[1].alpha, trailRenderer.colorGradient.alphaKeys[1].time) }
	    );
	    //trailRenderer.colorGradient.colorKeys[0] = new GradientColorKey(trailColourNear, trailRenderer.colorGradient.colorKeys[0].time);
	    //trailRenderer.colorGradient.colorKeys[1] = new GradientColorKey(trailColourFar, trailRenderer.colorGradient.colorKeys[1].time);
	    trailRenderer.colorGradient = newGradient;
	    Debug.Log("Trail: Near: " + trailRenderer.colorGradient.colorKeys[0].color.ToString() + ", Far: " + trailRenderer.colorGradient.colorKeys[1].color.ToString());
	}
    }
}
