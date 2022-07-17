using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClearScript : MonoBehaviour
{
    public int enemiesLeft;
    private void Start()
    {
        enemiesLeft += GameObject.FindGameObjectsWithTag("BasicEnemy").Length;
        enemiesLeft += GameObject.FindGameObjectsWithTag("ShootEnemy").Length;
        enemiesLeft += GameObject.FindGameObjectsWithTag("StrongEnemy").Length;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && enemiesLeft <= 0)
        {
            FindObjectOfType<GameController>().currentLevel++;
            FindObjectOfType<LevelTraversal>().LevelNav("GunSelection");
        }
    }
}
