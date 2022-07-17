using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int health = 5;
    public int currentLevel = 1;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameController");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    public void AddHealth()
    {
        if (health < 6)
        {
            health++;
        }
    }
    public void ResetController()
    {
        health = 2;
        currentLevel = 1;
    }
}
