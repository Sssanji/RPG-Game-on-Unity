using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    private int HP = 100;
    public Animator animator;
    public Slider healthBar;
    public Slider healthBarPlayer;
    public NavMeshAgent navMeshAgent;
    
    void Update()
    {
        healthBar.value = HP;
    }


    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if(HP <= 0)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator.SetTrigger("death");
            GetComponent<Collider>().enabled = false;
            navMeshAgent.enabled = false;
            GetComponent<EnemyAI>().enabled = false;
            healthBar.gameObject.SetActive(false);
        }

        else
        {
            animator.SetTrigger("damage");
        }

    }


   
}
