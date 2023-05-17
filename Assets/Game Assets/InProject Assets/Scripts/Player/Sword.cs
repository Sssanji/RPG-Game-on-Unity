using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int damageAmount = 20;
    


    private void OnTriggerEnter(Collider other)
    {
        Collider collider = GameObject.Find("Sword_1").GetComponent<Collider>();
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyScript>().TakeDamage(damageAmount);
            collider.enabled = false;
        }
    }
}