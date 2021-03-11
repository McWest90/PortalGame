using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] enemyList;
    public Transform spawnPoint; // create empty object and place here
    public bool showGizmos = true;
    private bool enabled = true;
    
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && enabled)
        {
            for (int i = 0; i < enemyList.Length; i++)
            {
                int rnd = Random.Range(0, enemyList.Length - 1);
                Instantiate(enemyList[rnd], spawnPoint.position, Quaternion.identity);
            }
            enabled = false;
        }
    }

    void OnDrawGizmosSelected() {
        // Drawing spawn zone
        if(showGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(spawnPoint.position, 2);
        }
    }


}
