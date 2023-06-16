using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class PlayerDamage : MonoBehaviour
{
    private int HP = 100;
    public Animator animator;
    public Slider healthBar;



    void Update()
    {
        healthBar.value = HP;
       
    }


    public void TakeDamagePlayer(int damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)
        {
            
          animator.SetTrigger("death");
            GetComponent<Collider>().enabled = false;

            Invoke("Death", 5f);
            healthBar.gameObject.SetActive(false);
        }

      

    }

    public void Death()
    {
        SceneManager.LoadScene("Death");
    }
   
}
