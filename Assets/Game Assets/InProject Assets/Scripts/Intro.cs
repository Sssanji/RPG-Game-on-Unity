using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;
    public TMP_Text text4;
    public TMP_Text text5;
   
    
    // Update is called once per frame
    void Start()
    {
       
        Invoke("Text1_ON", 1.5f);
        Invoke("Text1_OFF", 5f);

        Invoke("Text2_ON", 6.5f);
        Invoke("Text2_OFF", 11f);

        Invoke("Text3_ON", 17.5f);
        Invoke("Text3_OFF", 21.5f);

        Invoke("Text4_ON", 23.5f);
        Invoke("Text4_OFF", 28f);

        Invoke("Text5_ON", 29.5f);
        Invoke("Text5_OFF", 34f);

        Invoke("Audio_OFF", 34.9f);

        Invoke("NextScene", 37.9f);
    }


    private void Text1_ON()
    {
        text1.gameObject.SetActive(true);
    }

    private void Text1_OFF()
    {
        text1.gameObject.SetActive(false);
    }

    private void Text2_ON()
    {
        text2.gameObject.SetActive(true);
    }

    private void Text2_OFF()
    {
        text2.gameObject.SetActive(false);
    }

    private void Text3_ON()
    {
        text3.gameObject.SetActive(true);
    }

    private void Text3_OFF()
    {
        text3.gameObject.SetActive(false);
    }

    private void Text4_ON()
    {
        text4.gameObject.SetActive(true);
    }

    private void Text4_OFF()
    {
        text4.gameObject.SetActive(false);
    }

    private void Text5_ON()
    {
        text5.gameObject.SetActive(true);
    }

    private void Text5_OFF()
    {
        text5.gameObject.SetActive(false);
    }

    private void Audio_OFF()
    {
       
        GetComponent<MusicVolume>().enabled = true;

    }

    private void NextScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
