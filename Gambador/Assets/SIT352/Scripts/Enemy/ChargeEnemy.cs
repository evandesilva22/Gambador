using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemy : MonoBehaviour
{
    public enum EnemyState
    {
	IDLE,
	WALKING,
	CHARGING,
	DASHING
    };

    public bool debugMode;
    public EnemyState currentState;
    public float idleTime;
    public float walkSpeed;
    public float chargeTime;
    public float rotateSpeed;
    public float dashSpeed;
    public float dashDistance;
    public float detectionRange;
    public bool autoFindPlayerObj;
    public GameObject targetObject;
    public GameObject mesh;

    void Start()
    {
	if(autoFindPlayerObj) targetObject = GameObject.Find("Player");
        StartCoroutine("EnemyLoop");
    }

    IEnumerator EnemyLoop()
    {
	while(true)
	{
	    switch(currentState)
	    {
		case EnemyState.IDLE:
		    yield return StartCoroutine("EnemyIdle");
		    break;
		case EnemyState.WALKING:
		    yield return StartCoroutine("EnemyWalk");
		    break;
		case EnemyState.CHARGING:
		    yield return StartCoroutine("EnemyCharge");
		    break;
		case EnemyState.DASHING:
		    yield return StartCoroutine("EnemyDash");
		    break;
	    }
	    yield return null;
	}
    }

    IEnumerator EnemyIdle()
    {
	if(debugMode) ChangeColour(Color.blue);
	yield return new WaitForSeconds(idleTime);
	currentState = EnemyState.WALKING;
    }

    IEnumerator EnemyWalk()
    {
	if(debugMode) ChangeColour(Color.green);
	// Check if player is in range
	while(true)
	{
	    if(Vector3.Distance(targetObject.transform.position, transform.position) <= detectionRange)
	    {
		break;
	    }
	    yield return new WaitForSeconds(.1f);
	}
	
	currentState = EnemyState.CHARGING;
    }

    IEnumerator EnemyCharge()
    {
	if(debugMode) ChangeColour(Color.yellow);
	StartCoroutine("RotateToTarget");
	yield return new WaitForSeconds(chargeTime);
	StopCoroutine("RotateToTarget");
	currentState = EnemyState.DASHING;
    }

    IEnumerator RotateToTarget()
    {
	while(true)
	{
	    Quaternion oldRot = transform.rotation;
	    Vector3 targetPos = new Vector3(targetObject.transform.position.x, transform.position.y, targetObject.transform.position.z);
	    transform.LookAt(targetPos);
	    transform.rotation = Quaternion.RotateTowards(oldRot, transform.rotation, rotateSpeed*Time.deltaTime);
	    yield return null;
	}
    }

    IEnumerator EnemyDash()
    {
	if(debugMode) ChangeColour(Color.red);
	Vector3 startPos = transform.position;
	bool maxDistReached = false;
	while(true)
	{
	    transform.position += transform.forward * Time.deltaTime * dashSpeed;
	    if(Vector3.Distance(startPos, transform.position) >= dashDistance)
	    {
		maxDistReached = true;
	    	//transform.position = startPos + Vector3.ClampMagnitude(
		transform.position -= transform.forward * (Vector3.Distance(startPos, transform.position) - dashDistance);
	    }
	    if(maxDistReached) break;
	    yield return null;
	}
	
	currentState = EnemyState.IDLE;
    }

    void ChangeColour(Color newColour)
    {
	Renderer rend = mesh.GetComponent<Renderer>();
	for(int i = 0; i < rend.materials.Length; i++)
	{
	    rend.materials[i].color = newColour;
	    rend.materials[i].SetColor("_EmissionColor", newColour);
	    rend.materials[i].SetColor("_BaseColor", newColour);
	}
    }
}
