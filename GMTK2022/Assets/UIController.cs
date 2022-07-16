using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIController : MonoBehaviour
{
    private GameController gc;
    private GunManager gm;
    private GunController gunController;
    private Canvas canvas;
    private int storedHealth;
    [SerializeField] int UIHealthOffset;
    [SerializeField] Vector2 initialHealthPosition;
    [SerializeField] Image gun;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] GameObject[] health;
    [SerializeField] Animator anim;
    void Start()
    {
        gc = FindObjectOfType<GameController>();
        gm = FindObjectOfType<GunManager>();
        gunController = FindObjectOfType<GunController>();
        canvas = FindObjectOfType<Canvas>();
        storedHealth = gc.health;
        gun.sprite = gm.currentGun.cardArt;

        gunController.onReload += ReloadTextMove;
        gunController.onReloadFinish += ReloadTextMoveTwo;

        SetUpHealth();
    }

    private void ReloadTextMove(object sender, EventArgs e)
    {
        anim.SetBool("isFinished", false);
    }
    private void ReloadTextMoveTwo(object sender, EventArgs e)
    {
        anim.SetBool("isFinished", true);
    }
    void SetUpHealth()
    {
        for(int a = 0; a < gc.health; a++)
        {
            health[a].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gc.health < storedHealth && gc.health >= 0)
        {
            health[storedHealth - 1].GetComponent<Animator>().SetBool("isFinished", true);
            storedHealth--;
        }
        ammoText.text = gunController.currentAmmo + "/" + gm.currentGun.maxAmmo;
    }
}
