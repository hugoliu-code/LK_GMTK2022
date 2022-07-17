using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CardController : MonoBehaviour
{
    private GunManager gm;
    private GunType gun;
    public int roll;
    private Animator anim;
    
    [SerializeField] Sprite[] borders;
    [SerializeField] SpriteRenderer gunIcon;
    [SerializeField] SpriteRenderer border;
    [SerializeField] TextMeshPro gunName;
    [SerializeField] TextMeshPro description;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Start", true);
        gm = FindObjectOfType<GunManager>();
        SetupCard();
    }
    void SetupCard()
    {
        switch (roll)
        {
            case 1:
                gun = gm.commonGuns[Random.Range(0, gm.commonGuns.Length)];
                break;
            case 2:
                int index = Random.Range(0,gm.commonGuns.Length + gm.uncommonGuns.Length);
                if (index >= gm.commonGuns.Length)
                    gun = gm.uncommonGuns[index-gm.uncommonGuns.Length];
                else
                    gun = gm.commonGuns[index];
                break;
            case 3:
                index = Random.Range(0, gm.uncommonGuns.Length + gm.rareGuns.Length);
                if (index >= gm.uncommonGuns.Length)
                    gun = gm.rareGuns[index-gm.rareGuns.Length];
                else
                    gun = gm.uncommonGuns[index];
                break;
            case 4:
                gun = gm.rareGuns[Random.Range(0, gm.rareGuns.Length)];
                break;
            case 5:
                index = Random.Range(0, gm.rareGuns.Length + gm.epicGuns.Length);
                if (index >= gm.rareGuns.Length)
                    gun = gm.epicGuns[index- gm.epicGuns.Length];
                else
                    gun = gm.rareGuns[index];
                break;
            case 6:
                index = Random.Range(0, gm.epicGuns.Length + gm.legendaryGuns.Length);
                if (index >= gm.epicGuns.Length)
                    gun = gm.legendaryGuns[index- gm.legendaryGuns.Length];
                else
                    gun = gm.epicGuns[index];
                break;
        }
        border.sprite = borders[gun.rarity];   

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
