using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{

    PlayerMovement playerMovement;
    public int[,] shopItems = new int [4,26];
    GameObject[] content;
    List<Transform> contentList = new List<Transform>();
    void Awake(){
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        content = GameObject.FindGameObjectsWithTag("Content");
        foreach(GameObject obj in content){
            Transform transform = obj.transform;
            contentList.Add(transform);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //ID's
        shopItems[1,1] = 1;
        shopItems[1,2] = 2;
        shopItems[1,3] = 3;
        shopItems[1,4] = 4;
        shopItems[1,5] = 5;
        shopItems[1,6] = 6;
        shopItems[1,7] = 7;
        shopItems[1,8] = 8;
        shopItems[1,9] = 9;
        shopItems[1,10] = 10;
        shopItems[1,11] = 11;
        shopItems[1,12] = 12;
        shopItems[1,13] = 13;
        shopItems[1,14] = 14;
        shopItems[1,15] = 15;
        shopItems[1,16] = 16;
        shopItems[1,17] = 17;
        shopItems[1,18] = 18;
        shopItems[1,19] = 19;
        shopItems[1,20] = 20;
        shopItems[1,21] = 21;
        shopItems[1,22] = 22;
        shopItems[1,23] = 23;
        shopItems[1,24] = 24;
        shopItems[1,25] = 25;

        //Price
        shopItems[2,1] = 0;
        shopItems[2,2] = 10;
        shopItems[2,3] = 10;
        shopItems[2,4] = 10;
        shopItems[2,5] = 10;
        shopItems[2,6] = 10;
        shopItems[2,7] = 10;
        shopItems[2,8] = 20;
        shopItems[2,9] = 20;
        shopItems[2,10] = 20;
        shopItems[2,11] = 20;
        shopItems[2,12] = 20;
        shopItems[2,13] = 20;
        shopItems[2,14] = 20;
        shopItems[2,15] = 50;
        shopItems[2,16] = 50;
        shopItems[2,17] = 50;
        shopItems[2,18] = 50;
        shopItems[2,19] = 50;
        shopItems[2,20] = 50;
        shopItems[2,21] = 50;
        shopItems[2,22] = 50;
        shopItems[2,23] = 50;
        shopItems[2,24] = 50;
        shopItems[2,25] = 50;

        //isBought
        shopItems[3,1] = PlayerPrefs.GetInt("item1");
        shopItems[3,2] = PlayerPrefs.GetInt("item2");
        shopItems[3,3] = PlayerPrefs.GetInt("item3");
        shopItems[3,4] = PlayerPrefs.GetInt("item4");
        shopItems[3,5] = PlayerPrefs.GetInt("item5");
        shopItems[3,6] = PlayerPrefs.GetInt("item6");
        shopItems[3,7] = PlayerPrefs.GetInt("item7");
        shopItems[3,8] = PlayerPrefs.GetInt("item8");
        shopItems[3,9] = PlayerPrefs.GetInt("item9");
        shopItems[3,10] = PlayerPrefs.GetInt("item10");
        shopItems[3,11] = PlayerPrefs.GetInt("item11");
        shopItems[3,12] = PlayerPrefs.GetInt("item12");
        shopItems[3,13] = PlayerPrefs.GetInt("item13");
        shopItems[3,14] = PlayerPrefs.GetInt("item14");
        shopItems[3,15] = PlayerPrefs.GetInt("item15");
        shopItems[3,16] = PlayerPrefs.GetInt("item16");
        shopItems[3,17] = PlayerPrefs.GetInt("item17");
        shopItems[3,18] = PlayerPrefs.GetInt("item18");
        shopItems[3,19] = PlayerPrefs.GetInt("item19");
        shopItems[3,20] = PlayerPrefs.GetInt("item20");
        shopItems[3,21] = PlayerPrefs.GetInt("item21");
        shopItems[3,22] = PlayerPrefs.GetInt("item22");
        shopItems[3,23] = PlayerPrefs.GetInt("item23");
        shopItems[3,24] = PlayerPrefs.GetInt("item24");
        shopItems[3,25] = PlayerPrefs.GetInt("item25");
        LoadButtons();
    }

    // Update is called once per frame
    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if(playerMovement.stars >= shopItems[2,ButtonRef.GetComponent<ShopButton>().itemID]){
            playerMovement.stars -= shopItems[2,ButtonRef.GetComponent<ShopButton>().itemID];
            playerMovement.starText.text = playerMovement.stars.ToString();
            playerMovement.shopStarText.text = playerMovement.stars.ToString();
            PlayerPrefs.SetInt("stars",playerMovement.stars);
            ResetButtons();
            Equip();
        }
    }
    public void Equip(){
        ResetButtons();
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        shopItems[3,ButtonRef.GetComponent<ShopButton>().itemID] = 2;
        PlayerPrefs.SetInt("item" + ButtonRef.GetComponent<ShopButton>().itemID.ToString(),2);
        ButtonRef.GetComponent<ShopButton>().priceText.text = "";
        ButtonRef.GetComponent<ShopButton>().buttonText.text = "Equipped";
        playerMovement.spriteRender.sprite = ButtonRef.GetComponent<ShopButton>().spriteImage.sprite;
    }
    public void CheckButton(){
        playerMovement.buttonAudio.Play();
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        if(PlayerPrefs.GetInt("item" + ButtonRef.GetComponent<ShopButton>().itemID.ToString()) == 0){
            Buy();
        }
        else{
            Equip();
        }
    }
    public void LoadButtons(){
        foreach(Transform child in contentList){
            
            Transform button = child.Find("EquipButton");
            if(button != null){
                int itemID = button.GetComponent<ShopButton>().itemID;
                if(PlayerPrefs.GetInt("item" + button.GetComponent<ShopButton>().itemID.ToString()) == 0){
                    button.GetComponent<ShopButton>().buttonText.text = "Buy";
                    button.GetComponent<ShopButton>().priceText.text = shopItems[2,itemID].ToString();
                }
                else if(PlayerPrefs.GetInt("item" + button.GetComponent<ShopButton>().itemID.ToString()) == 1){
                    button.GetComponent<ShopButton>().buttonText.text = "Owned";
                    button.GetComponent<ShopButton>().priceText.text = "";
                }
                else if(PlayerPrefs.GetInt("item" + button.GetComponent<ShopButton>().itemID.ToString()) == 2){
                    button.GetComponent<ShopButton>().buttonText.text = "Equipped";
                    playerMovement.spriteRender.sprite = button.GetComponent<ShopButton>().spriteImage.sprite;
                    button.GetComponent<ShopButton>().priceText.text = "";
                }
            }else Debug.Log(null);
            
        }
    }
    public void ResetButtons(){
        int numberReset = 0;
        foreach(Transform child in contentList){
            
            Transform button = child.Find("EquipButton");
            if(button != null){
                if(PlayerPrefs.GetInt("item" + button.GetComponent<ShopButton>().itemID.ToString()) == 2)
                {
                    PlayerPrefs.SetInt("item" + button.GetComponent<ShopButton>().itemID.ToString(),1);
                    button.GetComponent<ShopButton>().buttonText.text = "Owned";
                    button.GetComponent<ShopButton>().priceText.text = "";
                    numberReset++;
                }
            }
        }
    }
}
