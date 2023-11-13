using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    [SerializeField] private GameObject price;
    [SerializeField] private GameObject equipButton;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private Animator anim;
    [SerializeField] private int skinId;
    [SerializeField] private bool[] skinPurchased;
    [SerializeField] private float[] skinPurchasedPrice;

    private void Start()
    {
        skinPurchased[0] = true;
    }

    public void NextSkin()
    {
        skinId++;

        if (skinId > 3)
        {
            skinId = 0;
        }
        SetupSkinInfo();
    }

    public void PrevSkin()
    {
        skinId--;

        if (skinId <= 0)
        {
            skinId = 3;
        }
        SetupSkinInfo();

    }

    private void SetupSkinInfo()
    {
        if (skinPurchased[skinId])
        {
            buyButton.SetActive(false);
            equipButton.SetActive(true);
        }
        else
        {
            buyButton.SetActive(true);
            equipButton.SetActive(false);
        }

        price.GetComponentInChildren<TextMeshProUGUI>().text = "Price: " + skinPurchasedPrice[skinId].ToString();
        anim.SetInteger("skinId", skinId);
    }

    public void Buy()
    {
        skinPurchased[skinId] = true;

        SetupSkinInfo();
    }

    public void Equip()
    {
        PlayerManager.instance.choosenSkinId = skinId;
    }

}
