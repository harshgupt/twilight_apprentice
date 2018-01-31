using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
{
    bool isWalking = false;
    bool hasStartedWalking = false;
    Animator anim;
    int waypoint = 0;
    float timer = 0.0f;
    Transform defaultTransform;
    public GameObject vicinityTrigger;

    public GameObject waypointStart;
    public GameObject waypointFire;
    public GameObject waypointAnvil;
    public GameObject waypointCauldron;

    public GameObject player;
    public GameObject fireplace;
    public GameObject anvil;
    public GameObject cauldron;

    public SceneScript scenescript;

    public void StartWalking()
    {
        hasStartedWalking = true;
        isWalking = true;
        anim.Play("Walk");
        anim.speed = 0.8f;
        timer = 0.0f;
    }

    public void ContinueWalking()
    {
        if (hasStartedWalking && !isWalking)
        {
            isWalking = true;
            anim.Play("Walk");
        }
    }

    public bool GetHasStartedWalking()
    {
        return hasStartedWalking;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!scenescript.HasGameEnded() && other.gameObject == vicinityTrigger.gameObject)
        {
            ContinueWalking();
        }
    }
    
	void Start ()
    {
        anim = GetComponent<Animator>();
        defaultTransform = transform;
    }
	
	void FixedUpdate()
    {
		if (isWalking)
        {
            if (waypoint < 1)
            {
                if (timer < 3.0f)
                {
                    timer += Time.deltaTime;
                    transform.position = Vector3.Lerp(waypointStart.transform.position, waypointFire.transform.position, timer / 3.0f);
                    transform.LookAt(waypointFire.transform);
                }
                else
                {
                    transform.LookAt(new Vector3(fireplace.transform.position.x, transform.position.y, fireplace.transform.position.z));
                    waypoint = 1;
                    timer = 0.0f;
                    anim.Play("idle1");
                    isWalking = false;
                }
            }
            else if (waypoint == 1)
            {
                if (timer < 2.0f)
                {
                    timer += Time.deltaTime;
                    transform.position = Vector3.Lerp(waypointFire.transform.position, waypointAnvil.transform.position, timer / 2.0f);
                    transform.LookAt(waypointAnvil.transform);
                }
                else
                {
                    transform.LookAt(new Vector3(anvil.transform.position.x, transform.position.y, anvil.transform.position.z));
                    waypoint = 2;
                    timer = 0.0f;
                    anim.Play("idle1");
                    isWalking = false;
                }
            }
            else if (waypoint == 2)
            {
                if (timer < 2.0f)
                {
                    timer += Time.deltaTime;
                    transform.position = Vector3.Lerp(waypointAnvil.transform.position, waypointCauldron.transform.position, timer / 2.0f);
                    transform.LookAt(waypointCauldron.transform);
                }
                else
                {
                    transform.LookAt(new Vector3(cauldron.transform.position.x, transform.position.y, cauldron.transform.position.z));
                    waypoint = 3;
                    timer = 0.0f;
                    anim.Play("idle1");
                    isWalking = false;
                }
            }
            else if (waypoint > 2)
            {
                if (timer < 3.0f)
                {
                    timer += Time.deltaTime;
                    transform.position = Vector3.Lerp(waypointCauldron.transform.position, waypointStart.transform.position, timer / 3.0f);
                    transform.LookAt(waypointStart.transform);
                }
                else
                {
                    transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
                    waypoint = 0;
                    timer = 0.0f;
                    anim.Play("idle1");
                    isWalking = false;
                }
            }

        }
	}
}
