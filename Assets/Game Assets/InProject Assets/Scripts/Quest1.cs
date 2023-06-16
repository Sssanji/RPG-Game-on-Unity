using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
public class Quest1 : MonoBehaviour
{

    public GameObject QuestItem;
    public int Stadia = 0;
    public bool KeyPress = false;
    public GameObject QuestGiver;
    public GameObject Player;
    public TMP_Text Note;
    public TMP_Text QuestText;
    void Update()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {


        Collider colliderNPC = QuestGiver.GetComponent<Collider>();
        if (Stadia == 0)
        {

            Note.gameObject.SetActive(true);
            Invoke("Note_OFF", 5f);
            Stadia = 1;
            QuestText.text = "Знайти зброю";
        }


        if (Stadia == 2)
        {
           
            QuestText.text = "";
            Stadia = 3;

        }
    }




        private void Note_OFF()
    {
        Note.gameObject.SetActive(false);
    }


    private void KeyDown()
    {
        KeyPress = false;
    }

    public void UpdateStadia(int newValue)
    {
        Stadia = newValue;
    }

}


/*Collider colliderNPC = QuestGiver.GetComponent<Collider>();
if (Stadia == 0)
{

    Note.gameObject.SetActive(true);
    Invoke("Note_OFF", 5f);
    Stadia = 1;
    QuestText.text = "Найти оружие";
}

if (Stadia == 1)
{
    Collider colliderQuestItem = QuestItem.GetComponent<Collider>();
    QuestText.text = "Доставить оружие";
    QuestItem_OFF();
    Stadia = 2;

}

if (Stadia == 2)
{
    Collider colliderQuestGiver = QuestGiver.GetComponent<Collider>();
    QuestText.text = "";
    Stadia = 3;

}
    }*/