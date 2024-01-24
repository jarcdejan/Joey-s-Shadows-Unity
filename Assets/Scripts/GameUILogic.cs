using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUILogic : MonoBehaviour
{

    public GameObject crosshair;
    public GameObject crosshairAlt1;
    public GameObject crosshairAlt2;

    public GameObject batteryEmpty;
    public GameObject batteryFull;

    public Sprite battery1;
    public Sprite battery2;
    public Sprite battery3;

    public GameObject sanityEmpty;
    public GameObject sanityFull;

    public Sprite sanity1;
    public Sprite sanity2;
    public Sprite sanity3;

    public GameObject buttonQ;

    public Sprite buttonQ1;
    public Sprite buttonQ2;
    public Sprite buttonQ3;

    public GameObject buttonE;

    public Sprite buttonE1;
    public Sprite buttonE2;
    public Sprite buttonE3;

    public GameObject buttonQPressed;

    public Sprite buttonQPressed1;
    public Sprite buttonQPressed2;
    public Sprite buttonQPressed3;

    public GameObject buttonEPressed;

    public Sprite buttonEPressed1;
    public Sprite buttonEPressed2;
    public Sprite buttonEPressed3;

    public GameObject progressBarEmpty;
    public GameObject progressBarFull;

    public Sprite progressBar1;
    public Sprite progressBar2;
    public Sprite progressBar3;

    public GameObject sanityBorder;

    public Sprite sanityBorder1;
    public Sprite sanityBorder2;
    public Sprite sanityBorder3;

    public GameObject MonsterEventUI;

    public GameObject tabButton;

    public Sprite tabButton1;
    public Sprite tabButton2;
    public Sprite tabButton3;

    public GameObject playerObj;
    public GameObject flashLight;

    public float animateFrameDuration = 0.1f;
    private float frameTime = 0f;

    private int currentFrame = 0;
    private int frameCount = 3;
    
    private float sanityChangeRate = 3f;
    private float batteryChangeRate = 3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {      
        //set the crosshair based on player logic
        bool playerLookingAtObj = playerObj.GetComponent<PlayerLogic>().lookingAtIntractableObj;
        crosshair.SetActive(!playerLookingAtObj);
        crosshairAlt1.SetActive(playerLookingAtObj);

        //set alt crosshair 2 if the player is looking at the monster
        bool playerLookingAtMonster = playerObj.GetComponent<MonsterEvent>().lookingAtMonster;
        crosshairAlt2.SetActive(playerLookingAtMonster);

        //set battery percentage
        float targetBatPercent = flashLight.GetComponent<LightLogic>().battery / flashLight.GetComponent<LightLogic>().maxBattery;
        float currBatPercent = batteryFull.GetComponent<Image>().fillAmount;
        batteryFull.GetComponent<Image>().fillAmount = currBatPercent + (targetBatPercent - currBatPercent) * batteryChangeRate * Time.deltaTime;


        //set sanity percentage
        float targetSanityPercent = playerObj.GetComponent<PlayerLogic>().sanity / playerObj.GetComponent<PlayerLogic>().maxSanity;
        float currSanityPercent = sanityFull.GetComponent<Image>().fillAmount;
        sanityFull.GetComponent<Image>().fillAmount = currSanityPercent + (targetSanityPercent - currSanityPercent) * sanityChangeRate * Time.deltaTime;

        //set alpha of sanity border
        float targetAlpha = Mathf.Pow((1 - targetSanityPercent), 3) * 0.5f;
        float currAlpha = sanityBorder.GetComponent<Image>().color.a;
        sanityBorder.GetComponent<Image>().color = new Color(1, 1, 1, currAlpha + (targetAlpha - currAlpha) * sanityChangeRate * Time.deltaTime);

        //set progress bar
        float progressPercent = playerObj.GetComponent<MonsterEvent>().progress / playerObj.GetComponent<MonsterEvent>().maxProgress;
        progressBarFull.GetComponent<Image>().fillAmount = progressPercent;

        //set next key
        if(playerObj.GetComponent<MonsterEvent>().nextKey.Equals("q")) {
            buttonQPressed.SetActive(true);
            buttonEPressed.SetActive(false);
            buttonQ.SetActive(false);
            buttonE.SetActive(true);
        }
        else if(playerObj.GetComponent<MonsterEvent>().nextKey.Equals("e")) {
            buttonQPressed.SetActive(false);
            buttonEPressed.SetActive(true);
            buttonQ.SetActive(true);
            buttonE.SetActive(false);
        }

        //animate battery by switching between sprites
        frameTime += Time.deltaTime;
        if(frameTime >= animateFrameDuration){
            frameTime = frameTime % animateFrameDuration;
            currentFrame = (currentFrame + 1) % frameCount;
            switch(currentFrame){
                case 0:
                    batteryEmpty.GetComponent<Image>().sprite = battery1;
                    sanityEmpty.GetComponent<Image>().sprite = sanity1;
                    buttonQ.GetComponent<Image>().sprite = buttonQ1;
                    buttonEPressed.GetComponent<Image>().sprite = buttonEPressed1;
                    buttonQPressed.GetComponent<Image>().sprite = buttonQPressed1;
                    buttonE.GetComponent<Image>().sprite = buttonE1;
                    progressBarEmpty.GetComponent<Image>().sprite = progressBar1;
                    sanityBorder.GetComponent<Image>().sprite = sanityBorder1;
                    tabButton.GetComponent<Image>().sprite = tabButton1;
                    break;
                case 1:
                    batteryEmpty.GetComponent<Image>().sprite = battery2;
                    sanityEmpty.GetComponent<Image>().sprite = sanity2;
                    buttonQ.GetComponent<Image>().sprite = buttonQ2;
                    buttonE.GetComponent<Image>().sprite = buttonE2;
                    buttonEPressed.GetComponent<Image>().sprite = buttonEPressed2;
                    buttonQPressed.GetComponent<Image>().sprite = buttonQPressed2;
                    progressBarEmpty.GetComponent<Image>().sprite = progressBar2;
                    sanityBorder.GetComponent<Image>().sprite = sanityBorder2;
                    tabButton.GetComponent<Image>().sprite = tabButton2;
                    break;
                case 2:
                    batteryEmpty.GetComponent<Image>().sprite = battery3;
                    sanityEmpty.GetComponent<Image>().sprite = sanity3;
                    buttonQ.GetComponent<Image>().sprite = buttonQ3;
                    buttonE.GetComponent<Image>().sprite = buttonE3;
                    buttonEPressed.GetComponent<Image>().sprite = buttonEPressed3;
                    buttonQPressed.GetComponent<Image>().sprite = buttonQPressed3;
                    progressBarEmpty.GetComponent<Image>().sprite = progressBar3;
                    sanityBorder.GetComponent<Image>().sprite = sanityBorder3;
                    tabButton.GetComponent<Image>().sprite = tabButton3;
                    break;
            }
        }

        //enable or disable monster event UI
        bool monsterEventActive = playerObj.GetComponent<MonsterEvent>().eventActive;
        MonsterEventUI.SetActive(monsterEventActive);
    }
}
