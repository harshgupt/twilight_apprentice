using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadScript : MonoBehaviour {

    public GameObject skeleton;
    public SceneScript scenescript;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (scenescript.IsSkeletonHittable() && other.gameObject.tag == "pickable")
        {
            skeleton.GetComponent<Animator>().Play("Wow");
        }
    }
}
