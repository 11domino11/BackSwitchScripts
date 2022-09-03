using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButton : MonoBehaviour
{

    public int itemID;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI buttonText;
    public Image spriteImage;

    ShopManager shopManager;

    // Update is called once per frame
    void Awake()
    {
        shopManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShopManager>();
    }
}
