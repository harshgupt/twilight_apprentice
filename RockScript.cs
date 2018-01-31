using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    public Material Lava;
    public GameObject LavaLight;
    public GameObject fxParticle;
    public Material Mithril;
    public Material Adamantium;
    public GameObject fxHammerSparks;
    public GameObject controller1;
    public GameObject controller2;

    public enum RockState { Start, Smelting, Lava, Smithing, Mithril, Adamantium };
    public RockState rockState = RockState.Start;

    float cooktime = 0.0f;
    int hammerhits = 0;

    bool textTimerStart = false;
    float textChangeTime = 0.0f;
    public GameObject tutorialFirepit;
    public GameObject tutorialFirepitFinished;
    public GameObject tutorialAnvil;
    public GameObject tutorialAnvilFinished;
    public GameObject tutorialCauldron1;
    public GameObject tutorialCauldron2;


    void OnTriggerStay(Collider other)
    {
        if (rockState == RockState.Smelting && other.name == "SmeltTrigger")
        {
            SmeltingToLava();
            if (cooktime >= 5.0f)
            {
                ChangeToLava();
            }
            cooktime += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (rockState == RockState.Start && other.name == "SmeltTrigger")
        {
            SmeltingToLava();
        }
        else if (rockState == RockState.Lava && other.name == "AnvilTop")
        {
            GetComponent<Rigidbody>().isKinematic = true;
            rockState = RockState.Smithing;
        }
        else if (rockState == RockState.Smithing && other.name == "HammerTrigger")
        {
            float minVelocity = 0.0002f;
            /*if (controller1.GetComponent<Rigidbody>().velocity.sqrMagnitude > Mathf.Pow(minVelocity, 2) || 
                controller2.GetComponent<Rigidbody>().velocity.sqrMagnitude > Mathf.Pow(minVelocity, 2))*/
            {
                fxHammerSparks.SetActive(true);
                hammerhits += 1;
                if (hammerhits >= 4)
                {
                    ChangeToMithril();
                    fxHammerSparks.SetActive(false);
                    GetComponent<Rigidbody>().isKinematic = false;
                }
            }
        }
    }

    public bool checkIsMithril()
    {
        return rockState == RockState.Mithril;
    }
    public void SmeltingToLava()
    {
        LavaLight.SetActive(true);
        fxParticle.SetActive(true);
        rockState = RockState.Smelting;
    }

    public void ChangeToLava()
    {
        rockState = RockState.Lava;
        Renderer rend = GetComponent<Renderer>();
        rend.material = Lava;
        LavaLight.SetActive(true);
        fxParticle.SetActive(true);
        Light light = LavaLight.GetComponent<Light>();
        light.color = new Color(0.99f, 0.45f, 0.25f, 1);
        ((modulateLighting)GetComponentInChildren<modulateLighting>()).baseIntensity = 4;
        tutorialFirepit.gameObject.SetActive(false);
        tutorialAnvil.gameObject.SetActive(true);
        //tutorialFirepitFinished.gameObject.SetActive(true);
        textTimerStart = true;

    }

    public void ChangeToMithril()
    {
        rockState = RockState.Mithril;
        LavaLight.SetActive(true);
        fxParticle.SetActive(false);
        Light light = LavaLight.GetComponent<Light>();
        light.color = new Color(0.2f, 0.3f, 0.7f, 1);
        Renderer rend = GetComponent<Renderer>();
        rend.material = Mithril;
        ((modulateLighting)GetComponentInChildren<modulateLighting>()).baseIntensity = 2;
        tutorialAnvil.gameObject.SetActive(false);
        //tutorialAnvilFinished.gameObject.SetActive(true);
        tutorialCauldron2.gameObject.SetActive(true);
        textTimerStart = true;

    }

    public void ChangeToAdamantium()
    {
        rockState = RockState.Adamantium;
        LavaLight.SetActive(true);
        fxParticle.SetActive(false);
        Light light = LavaLight.GetComponent<Light>();
        light.color = new Color(0.6f, 0.9f, 0.4f, 1);
        Renderer rend = GetComponent<Renderer>();
        rend.material = Adamantium;
        ((modulateLighting)GetComponentInChildren<modulateLighting>()).baseIntensity = 2;
    }

    public void ResetToSmelting()
    {
        rockState = RockState.Start;
        LavaLight.SetActive(false);
        fxParticle.SetActive(false);
        cooktime = 0.0f;
        hammerhits = 0;
        fxHammerSparks.SetActive(false);
    }

    public void ResetToSmithing()
    {
        ChangeToLava();
        cooktime = 0.0f;
        hammerhits = 0;
        fxHammerSparks.SetActive(false);
    }

    public void ResetToCauldron()
    {
        ChangeToMithril();
        fxHammerSparks.SetActive(false);
    }

    public void FixedUpdate()
    {
        if (rockState == RockState.Lava)
        {
            Renderer rend = GetComponent<Renderer>();
            rend.material.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 0, 0, 1), Mathf.Cos(Time.time * 2) / 2 + 0.5f);
        }
        else if (rockState == RockState.Mithril)
        {
            Renderer rend = GetComponent<Renderer>();
            rend.material.SetColor("_EmissionColor", Color.Lerp(new Color(0.2348075f, 0.4955486f, 0.5426471f, 1), new Color(0.2497837f, 0.7618034f, 0.9705881f, 1), Mathf.Sin(Time.time * 3) / 2 + 0.5f));
        }
        else if (rockState == RockState.Adamantium)
        {
            Renderer rend = GetComponent<Renderer>();
            rend.material.SetColor("_EmissionColor", Color.Lerp(new Color(0.5651175f, 0.6617647f, 0.4865916f, 1), new Color(0.1477897f, 0.1985294f, 0.1065636f, 1), Mathf.Sin(Time.time * 2) / 2 + 0.5f));
        }
        if (textTimerStart == true)
        {
            textChangeTime += 1.0f * Time.deltaTime;
        }
        /*if (textChangeTime > 3.0f && rockState == RockState.Lava)
        {
            textTimerStart = false;
            tutorialFirepitFinished.gameObject.SetActive(false);
            tutorialAnvil.gameObject.SetActive(true);
            textChangeTime = 0.0f;
        }*/
        /*if (textChangeTime > 3.0f && rockState == RockState.Mithril)
        {
            textTimerStart = false;
            tutorialAnvilFinished.gameObject.SetActive(false);
            tutorialCauldron2.gameObject.SetActive(true);
        }*/
        /*if (textChangeTime > 6.0f && rockState == RockState.Lava)
        {
            textTimerStart = false;
            tutorialCauldron1.gameObject.SetActive(false);
            tutorialCauldron2.gameObject.SetActive(true);
            textChangeTime = 0.0f;
        }*/
    }
}
