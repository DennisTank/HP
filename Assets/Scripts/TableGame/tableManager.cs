using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tableManager : MonoBehaviour
{
    /// <summary>
    /// to manage the Table game canvas transitions
    /// number selection, table making, result showing
    /// </summary>
    public GameObject SwtGam;

    public Animator numSel;
    public Animator mainTable;
    public GameObject tableMkr;

    public GameObject result;
    public Text cInc, ans;

    // to go back to select numbers after the table is completed.
    [HideInInspector] public bool toNumSel;
    private void Awake()
    {
        toNumSel = false;
    }
    private void Update()
    {

        if (toNumSel) {
            backToNumSel();
            toNumSel = false;
        }   
    }
    void backToNumSel() {
        // to go back to select numbers after the table is completed.
        mainTable.SetBool("on", false);
        CancelInvoke("stHoldNS");
        Invoke("stHoldNS", 1f);
    }
    // show the result if its correct or not
    public void showResult(string cInc, string ans)
    {
        this.cInc.text = cInc;
        this.ans.text = ans;
        result.GetComponent<Animator>().SetTrigger("now");
    }
    // to reset the Table game
    public void reset() {
        numSel.SetBool("on", true);
        tableMkr.GetComponent<tableMaker>().option[0].GetComponent<numberOption>().ON=false;
    }
    // Pointer functions
    public void numSelect(int num) {
        tableMkr.GetComponent<tableMaker>().currentTable = num;     
        numSel.SetBool("on",false);
        CancelInvoke("stHoldMT");
        Invoke("stHoldMT", 0.5f);
    }
    public void QuitToMM() {
        tableMkr.GetComponent<tableMaker>().option[0].GetComponent<numberOption>().ON=false;
        toNumSel = true;
        CancelInvoke("QuitHold");
        Invoke("QuitHold",0.5f);
    }
    // Invoking methods
    void stHoldNS() {
        numSel.SetBool("on", true);
    }
    void stHoldMT() {
        tableMkr.GetComponent<tableMaker>().set = true;
        mainTable.SetBool("on", true);
    }
    void QuitHold() {
        SwtGam.GetComponent<SwitchGame>().quit = true;
    }
}
