using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    public int damageAmount = 5;

    

    private void OnTriggerEnter(Collider other)
    {
        Collider collider = GameObject.Find("SWArmR").GetComponent<Collider>();
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerDamage>().TakeDamagePlayer(damageAmount);
          
        }
    }


   
}