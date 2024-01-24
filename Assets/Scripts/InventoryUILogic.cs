using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUILogic : MonoBehaviour
{

    public GameObject inventory;

    public GameObject batteryCount;
    public GameObject pillsCount;
    public GameObject keyCount;

    public Sprite number0;
    public Sprite number1;
    public Sprite number2;
    public Sprite number3;
    public Sprite number4;
    public Sprite number5;
    public Sprite number6;
    public Sprite number7;
    public Sprite number8;
    public Sprite number9;

    public GameObject playerObj;
    public GameObject inventoryAudioNode;

    private Animator inventoryAnimator;
    private bool inventoryOpen = false;

    private Sprite Int2Sprite(int num){
        switch(num) {
            case 0: return number0;
            case 1: return number1;
            case 2: return number2;
            case 3: return number3;
            case 4: return number4;
            case 5: return number5;
            case 6: return number6;
            case 7: return number7;
            case 8: return number8;
            case 9: return number9;
            default: return number0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        inventoryAnimator = inventory.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //set item counts
        batteryCount.GetComponent<Image>().sprite = Int2Sprite(playerObj.GetComponent<PlayerLogic>().batteryCount);
        pillsCount.GetComponent<Image>().sprite = Int2Sprite(playerObj.GetComponent<PlayerLogic>().pillCount);
        keyCount.GetComponent<Image>().sprite = Int2Sprite(playerObj.GetComponent<PlayerLogic>().keyCount);


        if(!playerObj.GetComponent<PlayerLogic>().dead && Input.GetKeyDown(KeyCode.Tab)) {
            if(inventoryOpen) {
                inventoryAnimator.SetTrigger("closeInventory");
                inventoryAudioNode.GetComponent<AudioSource>().Play(0);
                inventoryOpen = false;
            }
            else {
                inventoryAnimator.SetTrigger("openInventory");
                inventoryAudioNode.GetComponent<AudioSource>().Play(0);
                inventoryOpen = true;
            }
        }
    }
}
