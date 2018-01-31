using UnityEngine;
using System.Collections;

public class FluidParticle : MonoBehaviour {

    float durationTimer = 0.0f;
    public bool isEnabled = false;
    public float duration = 2.0f;
    public float scale = 0.2f;
    public float despawnDistance = 50.0f;
    public string parentCollisionName = "";

    // Use this for initialization
    void Start () {
        transform.localScale = new Vector3(scale, scale, scale);
    }
	
	// Update is called once per frame
	void Update () {
        if (isEnabled)
        {
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                Destroy(gameObject);
                return;
            }
            if ((transform.parent.position - transform.position).sqrMagnitude > despawnDistance * despawnDistance)
            {
                Destroy(gameObject);
                return;
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Sphere")
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.9f, 0.3f, 0.1f, 1.0f));
        }

        if (collision.transform.parent != null && collision.transform.parent.name == parentCollisionName)
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.1f, 0.9f, 0.2f, 1.0f));
            GameObject p = Instantiate<GameObject>(GameObject.Find("BigSparks"));
            p.transform.position = transform.position;
        }
    }
}
