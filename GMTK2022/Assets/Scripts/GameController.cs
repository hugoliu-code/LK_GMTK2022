using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int health = 5;
    public int currentLevel = 1;
    public GameController instance;
    public bool fin = false;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //if (instance != null)
        //{
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    instance = this;
        //}
        //DontDestroyOnLoad(this);
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
        fin = false;
    }
    private void Update()
    {
        if(health <= 0 && fin == false)
        {
            fin = true;
            FindObjectOfType<LevelTraversal>().LevelNav("DeathScreen");
        }
    }
}
