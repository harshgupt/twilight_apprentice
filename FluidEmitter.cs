using UnityEngine;
using System.Collections.Generic;

public class FluidEmitter : MonoBehaviour {

	float spawnTimer = 0.0f;
    public int spawnPerInterval = 3;
	public float spawnInterval = 1.0f;
    public float spawnSpread = 1.0f;
	GameObject particle;

	// Use this for initialization
	void Start()
    {
        particle = transform.GetChild(0).gameObject;
	}

	// Update is called once per frame
	void Update()
    {
		spawnTimer += Time.deltaTime;

		if (spawnTimer > spawnInterval)
        {
            for (int i = 0; i < spawnPerInterval; i++)
            {
                GameObject p = Instantiate<GameObject>(particle);
                p.transform.position = transform.position + new Vector3(spawnSpread * (Random.value - 0.5f), spawnSpread * (Random.value - 0.5f), spawnSpread * (Random.value - 0.5f));
                p.transform.SetParent(transform);
                p.GetComponent<Renderer>().enabled = true;
                p.GetComponent<Collider>().enabled = true;
                p.GetComponent<FluidParticle>().isEnabled = true;
            }

            spawnTimer = 0.0f;
		}
	}
}
