using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slot : MonoBehaviour
{
    /// <summary>
    /// This keeps the info about slot's activeness 
    /// (i.e. can it be press to undo the selection)
    /// and also the index of the letter to which the letter must go back.
    /// </summary>
    
    // selfs index
    public int slotIndex;
    // refrence to handler GO
    public GameObject letterHandler;
    // activation boolean
    [HideInInspector] public bool on;
    // letters index
    [HideInInspector] public int letterIndex;
    private void Awake()
    {
        on = false;
    }
    private void Update()
    {
        // if it is the last or the highest slot than its active and red
        GetComponent<Image>().color = (on) ? Color.red : Color.white;
    }
    public void SetSlotIndex()
    {
        letterHandler.GetComponent<alignLetters>().currentSlot = slotIndex;
    }
}
