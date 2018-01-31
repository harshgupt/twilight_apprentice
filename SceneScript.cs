using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SceneScript : MonoBehaviour
{
    GameObject[] pickables;
    public GameObject skeleton;
    Animator skeletonAnim;
    float timerStandUpState = 0.0f;

    enum SkeletonState { Initial, StandUp, AskBook, Dead };
    SkeletonState skeletonState = SkeletonState.Initial;
    bool isInTutorial = false;

    enum GameState { Start, ScrollPickup, ScrollVanish, AskTutorial, PlayingGame, EndGame, GameOver, SmeltRock, SmiteRock, CookRock };
    GameState gameState = GameState.Start;

    GameObject paper;
    GameObject fxPaper;
    public GameObject fxSparkDead;

    RockScript rockScript;
    GameObject rock;
    Transform rockDefaultTransform;
    public GameObject panel;
    public GameObject opening1;
    bool textTimerStart = false;
    float textChangeTime = 0.0f;
    public GameObject opening2;
    public GameObject afterScrollDestroy;
    public GameObject afterScrollDestroy2;
    public GameObject tutorialYes;
    public GameObject tutorialNo;
    public GameObject tutorialFirepit;
    public GameObject ending1;
    public GameObject ending2;
    public GameObject gameOver;
    public GameObject afterstand2;
    public GameObject afterScroll1;
    public GameObject tutorialCauldron2;
    public GameObject tutorialCauldronFinished;

    // Use this for initialization
    void Start()
    {
        pickables = GameObject.FindGameObjectsWithTag("pickable");

        skeleton = GameObject.Find("Skeleton");
        skeleton.transform.position = new Vector3(8.062893f, 1.728f, 2.752899f);
        skeletonAnim = skeleton.GetComponent<Animator>();
        skeletonAnim.speed = 0.0f;

        paper = GameObject.Find("Paper");
        fxPaper = paper.transform.GetChild(0).gameObject;

        rock = GameObject.Find("Rock");
        rockScript = ((RockScript)rock.GetComponent(typeof(RockScript)));
        rockDefaultTransform = rock.transform;
        panel.gameObject.SetActive(true);
        opening1.gameObject.SetActive(true);
        textTimerStart = true;

    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameState == GameState.Start && skeletonState == SkeletonState.StandUp)
        {
            if (timerStandUpState < 1.0f)
            {
                skeleton.transform.position = Vector3.Lerp(new Vector3(8.062893f, 1.728f, 2.752899f), new Vector3(8.034f, 1.926f, 2.617f), timerStandUpState / 1.0f);
                timerStandUpState += 1.5f * Time.deltaTime;
            }
        }
        else if (skeletonState == SkeletonState.Dead)
        {
            if (timerStandUpState < 2.0f)
            {
                skeleton.transform.position = Vector3.Lerp(new Vector3(skeleton.transform.position.x, 1.728f, skeleton.transform.position.z), 
                    new Vector3(skeleton.transform.position.x, 1.51f, skeleton.transform.position.z), timerStandUpState / 2.0f);
                timerStandUpState += 1.0f * Time.deltaTime;
            }
            else
            {
                //tutorialCauldronFinished.gameObject.SetActive(false);
                ending2.gameObject.SetActive(false);
                gameOver.gameObject.SetActive(true);
                gameState = GameState.GameOver;
            }
        }
        /*if (textTimerStart == true)
        {
            textChangeTime += 1.0f * Time.deltaTime;
        }
        if(textChangeTime > 3.0f && gameState == GameState.Start)
        {
            textTimerStart = false;
            opening1.gameObject.SetActive(false);
            opening2.gameObject.SetActive(true);
            textChangeTime = 0.0f;
        }
        if(textChangeTime > 3.0f && gameState == GameState.ScrollVanish)
        {
            afterScrollDestroy.gameObject.SetActive(true);
        }
        if(textChangeTime > 6.0f && gameState == GameState.Start)
        {
            textTimerStart = false;
            afterScrollDestroy.gameObject.SetActive(false);
            afterScrollDestroy2.gameObject.SetActive(true);
            textChangeTime = 0.0f;
        }
        if (textChangeTime > 3.0f && gameState == GameState.AskTutorial)
        {
            textTimerStart = false;
            tutorialYes.gameObject.SetActive(false);
            tutorialFirepit.gameObject.SetActive(true);
            textChangeTime = 0.0f;
        }
        if (textChangeTime > 3.0f && skeletonState == SkeletonState.Dead)
        {
            textTimerStart = false;
            ending2.gameObject.SetActive(false);
            gameOver.gameObject.SetActive(true);
            textChangeTime = 0.0f;
        }*/

        if (gameState == GameState.ScrollPickup)
        {
            paper.transform.rotation = Quaternion.Euler(paper.transform.rotation.eulerAngles.x, paper.transform.rotation.eulerAngles.y, 50.0f + Mathf.Sin(Time.time * 3) * 7.0f);
            timerStandUpState = 0.0f;
        }

        if (gameState == GameState.ScrollVanish)
        {
            if (timerStandUpState < 3.0f)
            {
                float alpha = Mathf.Lerp(1.0f, 0.0f, timerStandUpState / 3.0f);
                Renderer rend = paper.GetComponent<Renderer>();
                rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, alpha);
                timerStandUpState += 1.0f * Time.deltaTime;
            }
            else
            {
                TriggerSkeletonAskTutorial();
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerSkeletonStart();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TriggerScrollPickup();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TriggerScrollVanish();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TriggerSkeletonAskTutorial();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TriggerRockCooking();
        }

       /* if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            TriggerRockToLava();
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            TriggerRockToMithril();
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            TriggerRockToAdamantium();
        }*/

        if (Input.GetKeyDown(KeyCode.T))
        {
            TriggerSkeletonStartTutorial(GameObject.Find("Book1b"));
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            GameObject obj = GameObject.Find("Deathcap1");
            obj.transform.position = GameObject.Find("Cauldron").transform.position + new Vector3(0, 1, 0);
            obj.GetComponent<Rigidbody>().isKinematic = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            GameObject obj = GameObject.Find("Nightshade");
            obj.transform.position = GameObject.Find("Cauldron").transform.position + new Vector3(0, 1, 0);
            obj.GetComponent<Rigidbody>().isKinematic = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            GameObject obj = GameObject.Find("Rock");
            obj.transform.position = GameObject.Find("Cauldron").transform.position + new Vector3(0, 1, 0);
        }

        if (Input.GetKeyDown(KeyCode.Minus))
        {
            TriggerSkeletonDead();
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            LoadStateSmelting();
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            LoadStateSmithing();
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            LoadStateCauldron();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            SkeletonStartWalking();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SkeletonContinueWalking();
        }
    }

    public void TriggerSkeletonStart()
    {
        if (gameState == GameState.Start && skeletonState == SkeletonState.Initial)
        {
            skeletonState = SkeletonState.StandUp;
            skeletonAnim.speed = 1.0f;

            opening1.gameObject.SetActive(false);
            afterstand2.gameObject.SetActive(true);
        }
        
    }

    public void TriggerScrollPickup()
    {
        if (gameState == GameState.Start && skeletonState == SkeletonState.StandUp)
        {
            gameState = GameState.ScrollPickup;

            //GameObject cam = GameObject.Find("TestCamera");
            GameObject cam = GameObject.Find("Camera (eye)");
            paper.transform.parent = cam.transform;
            paper.transform.position = cam.transform.position;

            paper.transform.position += cam.transform.forward * 0.3f;
            paper.transform.LookAt(cam.transform);
            paper.transform.rotation = Quaternion.Euler(paper.transform.rotation.eulerAngles.x, paper.transform.rotation.eulerAngles.y, 50.0f);
            afterstand2.gameObject.SetActive(false);
        }
        
    }

    public void TriggerScrollVanish()
    {
        if (gameState == GameState.ScrollPickup)
        {
            gameState = GameState.ScrollVanish;
            fxPaper.transform.position = paper.transform.position;
            fxPaper.SetActive(true);
            textChangeTime = 0.0f;
            textTimerStart = true;
        }
    }

    public void TriggerSkeletonAskTutorial()
    {
        if (gameState == GameState.ScrollVanish && skeletonState == SkeletonState.StandUp)
        {
            gameState = GameState.AskTutorial;
            skeletonState = SkeletonState.AskBook;
            skeletonAnim.Play("idle book");
            fxPaper.SetActive(false);
            afterScrollDestroy2.gameObject.SetActive(true);
        }
    }

    public void TriggerSkeletonStartTutorial(GameObject book)
    {
        if (gameState == GameState.AskTutorial)
        {
            if (book.name.Substring(0, 4) == "Book")
            {
                book.SetActive(false);
                fxPaper.transform.parent = null;
                fxPaper.transform.position = book.transform.position;
                fxPaper.SetActive(false);
                fxPaper.SetActive(true);
                isInTutorial = true;
                afterstand2.gameObject.SetActive(false);
                afterScrollDestroy2.gameObject.SetActive(false);
                //tutorialYes.gameObject.SetActive(true);
                tutorialFirepit.gameObject.SetActive(true);
                SkeletonStartWalking();
            }
            else if (book.tag == "pickable")
            {
                isInTutorial = false;
                panel.gameObject.SetActive(false);
                SkeletonStartWalking();
                //skeleton.GetComponent<Animator>().Play("Wow");
            }

            gameState = GameState.PlayingGame;
        }
    }

    public void TriggerRockCooking()
    {
        if (gameState == GameState.AskTutorial)
        {
            rockScript.SmeltingToLava();
            gameState = GameState.SmeltRock;
        }
    }

    public void TriggerRockToLava()
    {
        if (gameState == GameState.SmeltRock)
        {
            rockScript.ChangeToLava();
        }
    }

    public void TriggerRockToMithril()
    {
        if (gameState == GameState.SmeltRock)
        {
            rockScript.ChangeToMithril();
        }
    }

    public void TriggerRockToAdamantium()
    {
        if (gameState == GameState.SmeltRock)
        {
            rockScript.ChangeToAdamantium();
        }
    }

    public void TriggerSkeletonDead()
    {
        gameState = GameState.EndGame;
        skeletonState = SkeletonState.Dead;
        timerStandUpState = 0.0f;
        skeletonAnim.Play("Dead");
        //GameObject.Find("Stool2").GetComponent<Rigidbody>().velocity = new Vector3(-4, 4, 5);
        //GameObject.Find("Stool2").GetComponent<Rigidbody>().angularVelocity = new Vector3(-4, 4, 5);
        fxSparkDead.SetActive(true);
        tutorialCauldronFinished.gameObject.SetActive(false);
        ending2.gameObject.SetActive(true);
    }

    public void LoadStateSmelting()
    {
        gameState = GameState.AskTutorial;
        skeletonState = SkeletonState.AskBook;
        skeletonAnim.Play("idle book");
        fxPaper.SetActive(false);
        rockScript.ResetToSmelting();
        rock.transform.position = rockDefaultTransform.position;
        rock.transform.rotation = rockDefaultTransform.rotation;
        rock.transform.localScale = rockDefaultTransform.localScale;
    }

    public void LoadStateSmithing()
    {
        gameState = GameState.SmeltRock;
        skeletonState = SkeletonState.AskBook;
        skeletonAnim.Play("idle book");
        fxPaper.SetActive(false);
        rockScript.ResetToSmithing();
        rock.transform.position = rockDefaultTransform.position;
        rock.transform.rotation = rockDefaultTransform.rotation;
        rock.transform.localScale = rockDefaultTransform.localScale;
    }

    public void LoadStateCauldron()
    {
        gameState = GameState.CookRock;
        skeletonState = SkeletonState.AskBook;
        skeletonAnim.Play("idle book");
        fxPaper.SetActive(false);
        rockScript.ResetToCauldron();
        rock.transform.position = rockDefaultTransform.position;
        rock.transform.rotation = rockDefaultTransform.rotation;
        rock.transform.localScale = rockDefaultTransform.localScale;
    }

    public void SkeletonStartWalking()
    {
        ((Walking)skeleton.GetComponent(typeof(Walking))).StartWalking();
    }

    public void SkeletonContinueWalking()
    {
        ((Walking)skeleton.GetComponent(typeof(Walking))).ContinueWalking();
    }

    public bool HasGameEnded()
    {
        return gameState == GameState.EndGame;
    }

    public bool IsSkeletonHittable()
    {
        if (skeletonState == SkeletonState.StandUp || skeletonState == SkeletonState.AskBook)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
