using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    GameObject goldBook1;
    GameObject goldBook2;

    public GameObject skeleton;
   
    void OnTriggerEnter(Collider collider)
    {
        ((SceneScript)GameObject.Find("SceneScript").GetComponent(typeof(SceneScript))).TriggerSkeletonStartTutorial(collider.gameObject);
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
