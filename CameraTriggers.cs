using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggers : MonoBehaviour
{
    public SceneScript sceneScript;
    float timer = 0.0f;
    bool scrollRead = false;
    public GameObject opening1;
    
    bool textTimerStart = false;
    float textChangeTime = 0.0f;

    void Start()
    {
        sceneScript = (SceneScript)GameObject.Find("SceneScript").GetComponent(typeof(SceneScript));
    }

    void OnTriggerEnter(Collider collider)
    {   
        if (collider.gameObject.name == "Skeleton")
        {
            sceneScript.TriggerSkeletonStart();
            

        }
        else if (collider.gameObject.name == "PaperCollider")
        {
            sceneScript.TriggerScrollPickup();
            scrollRead = true;
            
        }
    }

    void FixedUpdate()
    {
        if (scrollRead)
        {
            if (timer > 3.0f)
            {
                sceneScript.TriggerScrollVanish();
            }
            timer += Time.deltaTime;
        }
        if (textTimerStart == true)
        {
            textChangeTime += 1.0f * Time.deltaTime;
        }
        if (textChangeTime > 3.0f)
        {
            textTimerStart = false;
            textChangeTime = 0.0f;
        }
    }
}
