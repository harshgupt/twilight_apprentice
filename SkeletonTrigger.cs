using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonTrigger : MonoBehaviour {

    SceneScript sceneScript;
    GameObject sceneObj;

    void Start()
    {
        sceneObj = GameObject.Find("SceneScript");
        sceneScript = ((SceneScript)sceneObj.GetComponent(typeof(SceneScript)));
    }

    void FixedUpdate()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "DragonGlass")
        {
            sceneScript.TriggerSkeletonDead();
        }
    }
}
