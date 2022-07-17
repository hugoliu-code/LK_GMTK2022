using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageHandler : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    public void TakeDamage(int damage)
    {
        if (enemy.CompareTag("BasicEnemy")){
            enemy.GetComponent<BasicEnemyController>().TakeDamage(damage);
        }
        if (enemy.CompareTag("ShootingEnemy")) { 
        
        }
    }
}
