using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class CardController : MonoBehaviour
{
    private GunManager gm;
    private GunType gun;
    public int roll;
    private Animator anim;
    private bool isChoosen = false;
    private BoxCollider2D box;


    [SerializeField] Sprite[] borders;
    [SerializeField] SpriteRenderer gunIcon;
    [SerializeField] SpriteRenderer border;
    [SerializeField] TextMeshPro gunName;
    [SerializeField] TextMeshPro description;
    [SerializeField] TextMeshPro rarityField;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Start", true);
        gm = FindObjectOfType<GunManager>();
        gm.onChoseCard += FinishCard;
        box = GetComponent<BoxCollider2D>();
        SetupCard();
    }
    void SetupCard()
    {
        switch (roll)
        {
            case 1:
                gun = gm.commonGuns[UnityEngine.Random.Range(0, gm.commonGuns.Length)];
                break;
            case 2:
                int index = UnityEngine.Random.Range(0, gm.commonGuns.Length + gm.uncommonGuns.Length);
                if (index >= gm.commonGuns.Length)
                    gun = gm.uncommonGuns[index - gm.commonGuns.Length];
                else
                    gun = gm.commonGuns[index];
                break;
            case 3:
                index = UnityEngine.Random.Range(0, gm.uncommonGuns.Length + gm.rareGuns.Length);
                if (index >= gm.uncommonGuns.Length)
                    gun = gm.rareGuns[index - gm.uncommonGuns.Length];
                else
                    gun = gm.uncommonGuns[index];
                break;
            case 4:
                gun = gm.rareGuns[UnityEngine.Random.Range(0, gm.rareGuns.Length)];
                break;
            case 5:
                index = UnityEngine.Random.Range(0, gm.rareGuns.Length + gm.epicGuns.Length);
                if (index >= gm.rareGuns.Length)
                    gun = gm.epicGuns[index - gm.rareGuns.Length];
                else
                    gun = gm.rareGuns[index];
                break;
            case 6:
                index = UnityEngine.Random.Range(0, gm.epicGuns.Length + gm.legendaryGuns.Length);
                if (index >= gm.epicGuns.Length)
                    gun = gm.legendaryGuns[index - gm.epicGuns.Length];
                else
                    gun = gm.epicGuns[index];
                break;
        }
        border.sprite = borders[gun.rarity];
        gunName.text = gun.name;
        gunIcon.sprite = gun.cardArt;
        description.text = gun.description.Replace("\\n", "\n");
        switch (gun.rarity)
        {
            case 0:
                rarityField.text = "common";
                break;
            case 1:
                rarityField.text = "uncommon";
                break;
            case 2:
                rarityField.text = "rare";
                break;
            case 3:
                rarityField.text = "epic";
                break;
            case 4:
                rarityField.text = "legendary";
                break;

        }

    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isChoosen = true;
            gm.CardChoose(gun);
            box.enabled = false;

            Invoke("FinishCardSpecial", 1f);

        }
    }
    private void FinishCard(object sender, EventArgs e)
    {
        gm.onChoseCard -= FinishCard;
        if (!isChoosen)
        {
            box.enabled = false;
            anim.SetBool("Start", false);
        }
    }
    private void FinishCardSpecial()
    {
        anim.SetBool("Start", false);
    }
}
