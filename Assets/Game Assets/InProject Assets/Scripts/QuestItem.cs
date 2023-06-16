using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestItem : MonoBehaviour
{
    public GameObject ItemQuest;
    private Quest1 script1Reference;
    public TMP_Text QuestText;
    int QuestStadia;
    public TMP_Text Note;

    private void Update()
    {
        GameObject obj = GameObject.Find("RFA_Model");
        script1Reference = obj.GetComponent<Quest1>();

        Debug.Log("Stadia" + QuestStadia);
        QuestStadia = script1Reference.Stadia; 
    }




    private void OnTriggerEnter(Collider other)
    {


        Collider collider = ItemQuest.GetComponent<Collider>();


        if (QuestStadia == 1)
        {
            Note.gameObject.SetActive(true);
            Invoke("Note_OFF", 5f);
            QuestText.text = "Віднести зброю";
            QuestItem_OFF();
            script1Reference.UpdateStadia(2);

        }

    }

    private void Note_OFF()
    {
        Note.gameObject.SetActive(false);
    }

    private void QuestItem_OFF()
{
    ItemQuest.gameObject.SetActive(false);
}
}
