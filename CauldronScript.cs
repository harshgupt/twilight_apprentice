using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronScript : MonoBehaviour
{
    public GameObject fxHaze;
    public GameObject fxCook;
    public GameObject fxWhirl;
    public GameObject DragonGlass;
    public GameObject fxMixBlast;


    enum PotState { Start, Deathcap, Nightshade, Rock, Transmute, Explode, End };
    PotState potState = PotState.Start;

    float timer = 0.0f;
    GameObject currObj;
    Vector3 currScale;

    Transform defaultTransform;

    public GameObject tutorialCauldron2;
    public GameObject tutorialCauldron3;
    public GameObject tutorialCauldron4;
    public GameObject tutorialCauldronFinished;

    public void ResetState()
    {
        timer = 0;
        potState = PotState.Start;
        transform.position = defaultTransform.position;
        transform.rotation = defaultTransform.rotation;
        transform.localScale = defaultTransform.localScale;
        fxMixBlast.SetActive(false);
        fxHaze.SetActive(false);
        fxCook.SetActive(false);
        fxWhirl.SetActive(false);
    }

    void Start()
    {
        defaultTransform = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (potState == PotState.Start && (other.name == "Deathcap1" || other.name == "Deathcap2" || other.name == "Deathcap3" || other.name == "Deathcap4"))
        {
            fxMixBlast.SetActive(true);
            fxHaze.SetActive(true);
            potState = PotState.Deathcap;
            currObj = other.gameObject;
            currScale = currObj.transform.localScale;
            tutorialCauldron2.gameObject.SetActive(false);
            tutorialCauldron3.gameObject.SetActive(true);
        }
        else if (potState == PotState.Deathcap && other.name == "Nightshade")
        {
            fxCook.SetActive(true);
            potState = PotState.Nightshade;
            currObj = other.gameObject;
            currScale = currObj.transform.localScale;
            tutorialCauldron3.gameObject.SetActive(false);
            tutorialCauldron4.gameObject.SetActive(true);
        }
        else if (potState == PotState.Nightshade && other.name == "Rock")
        {
            RockScript rockscript = (RockScript)other.GetComponent(typeof(RockScript));
            if (rockscript.checkIsMithril())
            {
                fxWhirl.SetActive(true);
                potState = PotState.Rock;
                currObj = other.gameObject;
                currScale = currObj.transform.localScale;
                tutorialCauldron4.gameObject.SetActive(false);
                
            }
        }
    }

    private void FixedUpdate()
    {
        if (potState == PotState.Deathcap || potState == PotState.Nightshade || potState == PotState.Rock)
        {
            if (currObj == null) return;

            if (timer < 1.0f)
            {
                currObj.transform.localScale = Vector3.Lerp(currScale, new Vector3(0, 0, 0), timer);
                timer += Time.deltaTime;
            }
            else
            {
                fxMixBlast.SetActive(false);
                currObj.SetActive(false);
                currObj = null;
                timer = 0.0f;

                if (potState == PotState.Rock)
                {
                    potState = PotState.Transmute;
                }
            }
        }
        else if (potState == PotState.Transmute)
        {
            if (timer < 7.0f)
            {
                transform.localScale = Vector3.Lerp(new Vector3(0.1604012f, 0.1604012f, 0.1604012f), new Vector3(0, 0, 0), timer / 7.0f);
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - timer * 3, transform.rotation.eulerAngles.z);
                timer += Time.deltaTime;
            }
            else
            {
                gameObject.SetActive(false);
                DragonGlass.SetActive(true);
                DragonGlass.transform.position = transform.position;
                tutorialCauldronFinished.gameObject.SetActive(true);

                potState = PotState.Explode;
            }
        }
        else if (potState == PotState.Explode)
        {
            /*Vector3 exPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(exPos, 50);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.AddExplosionForce(100000.0f, exPos, 50);
                }

            }*/
            potState = PotState.End;


        }
    }
}
