using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamageScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PlayerController player;
    public void TakeDamage()
    {
        player.TakeDamage();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BasicEnemy") || collision.CompareTag("Spikes"))
        {
            player.TakeDamage();
        }
    }
}
