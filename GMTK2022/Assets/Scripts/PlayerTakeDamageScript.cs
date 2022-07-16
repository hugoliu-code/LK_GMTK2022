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
}
