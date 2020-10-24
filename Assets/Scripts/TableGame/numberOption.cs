using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class numberOption : MonoBehaviour
{
    /// <summary>
    /// Options for the current table question
    /// </summary>

    // a static boolean to affect all the buttons (script holders)
    public static bool on; 
    public GameObject tableMkr;
    // current value or the number or the answer
    [HideInInspector] public int value;

    private void Awake()
    {
        on = false;
    }
    private void Update()
    {
        GetComponent<Animator>().SetBool("on",on);
    }
    // Pointer function to set the answer 
    public void SetAns() {
        tableMkr.GetComponent<tableMaker>().answer = value;
        tableMkr.GetComponent<tableMaker>().done = true;
        on = false;
    }
    // set for static variable
    public bool ON {
        set { on = value; }
    }
}
