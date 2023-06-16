using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle : MonoBehaviour
{
    public GameObject InventoryScreen;
    private bool isObjectVisible = false; 

    private void Start()
    {
        InventoryScreen.SetActive(false); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            ToggleObjectVisibility(); 
        }
    }

    private void ToggleObjectVisibility()
    {
        isObjectVisible = !isObjectVisible; 

        InventoryScreen.SetActive(isObjectVisible); 
    }
}
