using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tableMaker : MonoBehaviour
{
    /// <summary>
    /// Getting the current table number and making the table
    /// randomly setting up options 
    /// showing results
    /// </summary>

    public GameObject tblMng;
    public Text[] num;
    public GameObject[] option;

    // basic current info
    [HideInInspector]public int currentTable;
    [HideInInspector] public int answer;
    [HideInInspector] public bool set;
    [HideInInspector] public bool done;
    // sub-info
    int count,correctAns;

    void Update()
    {
        if (done) {
            // set the answer and show the result
            num[2].text = answer.ToString();
            if (answer == correctAns)
            {
                tblMng.GetComponent<tableManager>().showResult("Correct", currentTable.ToString() + "X" + count.ToString() + "=" + correctAns.ToString());
            }
            else
            {
                tblMng.GetComponent<tableManager>().showResult("Incorrect", currentTable.ToString() + "X" + count.ToString() + "=" + correctAns.ToString());
            }
            // if the table is completed then to --> Number Selection
            if (count == 10)
            {
                CancelInvoke("toNumSel");
                Invoke("toNumSel",2.5f);
            }
            else {        
                CancelInvoke("resultHold");
                Invoke("resultHold",2.5f);
            }
            done = false;
        }
        if (set) {
            // Initial Setup
            count = 1;
            num[0].text = currentTable.ToString();
            num[1].text = count.ToString();
            num[2].text = " ";
            correctAns = setValues(currentTable,count);
            set = false;
        }
    }
    // finding currect answer
    // randomly setting it at a button
    // giving random numbers (nearby to answers) to other buttons
    int setValues(int n1,int n2) {
        int ans = n1 * n2;
        int x = Random.Range(0, 10) % (option.Length);
        option[x].GetComponentInChildren<Text>().text = ans.ToString();
        option[x].GetComponent<numberOption>().value = ans;
        for (int i = 0; i < option.Length - 1; i++) {
            x = (x + 1) % (option.Length);
            ans = (n1 * n2) + Random.Range(1, 5);
            option[x].GetComponentInChildren<Text>().text = ans.ToString();
            option[x].GetComponent<numberOption>().value = ans;
        }
        option[0].GetComponent<numberOption>().ON=true;
        return n1 * n2; 
    }
    //Invoking methods
    void resultHold() {
        // reseting the correct answer and text on questions 
        num[2].text = " ";
        count++;
        num[1].text =count.ToString(); 
        correctAns = setValues(currentTable, count);
    }
    void toNumSel() {
        tblMng.GetComponent<tableManager>().toNumSel = true;
    }
}
