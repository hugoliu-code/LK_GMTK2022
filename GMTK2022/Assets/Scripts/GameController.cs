using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameController : MonoBehaviour
{
    public int health = 5;
    public int currentLevel = 1;
    public GameController instance;
    public bool fin = false;

    //string[] jazz = { "GunSelection", "StartMenu" };
    //private static FMOD.Studio.EventInstance Music;

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

        //if (jazz.Contains(SceneManager.GetActiveScene().name))
        //{
         //   Progress("Misc");
         //   Music = FMODUnity.RuntimeManager.CreateInstance("event:/OST/Music");
        //    Music.start();
        //    Music.release();
        //}

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

    //public void Progress (string LevelType)
    //{
    //    Music.setParameterByNameWithLabel("Level Type", LevelType);
    //}
    //
    //private void OnDestroy()
    //{
     //   Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    //}
}
