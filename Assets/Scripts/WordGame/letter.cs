using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letter : MonoBehaviour
{
    // keeping info about what letter and index the button has
    public int index;
    public GameObject letterHandler;
    [HideInInspector] public bool on;
    private void Awake()
    {
        on = false;
    }
    public void SetLetterIndex() {
        // sending the info to the letter align script 
        // it will then trigger the animation and increment the slot with the letter
        // this buttom has
        letterHandler.GetComponent<alignLetters>().letterIndex = index;
    }
}
