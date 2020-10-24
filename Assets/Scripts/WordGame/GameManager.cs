using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Just to manage the canvas transition and animations
    /// Hint, Pause, Quit, Result
    /// Also to show the scores ( aka num of stars )
    /// </summary>

    // data storage
    public DB_Script db;

    public GameObject SwitchMM;
    public GameObject questHandler;
    public GameObject letterAlign;

    public GameObject hintBtn;
    public GameObject PauseMenu;
    public GameObject QuitMenu;
    public GameObject HintScreen;
    public GameObject result;
    public GameObject ScoreBoard;

    [HideInInspector] public bool reset;
    [HideInInspector] public bool done;
    [HideInInspector] public int score;

    private void Awake()
    {
        reset = false;
        hintBtn.SetActive(false);
        Invoke("hintOn", 30);
    }
    private void Update()
    {
        // Showing the starts and triggering the animation
        if (done) {
            result.GetComponent<Animator>().SetBool("win", (score == 3) ? true : false);
            result.GetComponent<Animator>().SetBool("on", true);
            CancelInvoke("StarsON");
            Invoke("StarsON", 0.1f);
            if (score != 3) {
                HintScreen.GetComponent<Animator>().SetTrigger("now");
            }
            done = false;
        }
        // Resetting the Hint timmer, for 30 secs
        if (reset) {
            hintBtn.SetActive(false);
            CancelInvoke("hintOn");
            Invoke("hintOn", 30);
            reset = false;
        }
    }
    public void FullReset() {
        hintBtn.SetActive(false);
        CancelInvoke("hintOn");
        Invoke("hintOn", 30);
        questHandler.GetComponent<questionHandler>().reset = true;
    }
    // UIs button Pointer Functions
    public void Pause() {
        PauseMenu.GetComponent<Animator>().SetBool("now",true);
    }
    public void Resume() {
        PauseMenu.GetComponent<Animator>().SetBool("now", false);
    }
    public void Quit() {
        PauseMenu.GetComponent<Animator>().SetBool("now", false);
        QuitMenu.GetComponent<Animator>().SetBool("now",true);
    }
    public void QuitNo() {
        QuitMenu.GetComponent<Animator>().SetBool("now", false);
    }
    public void QuitYes() {
        letterAlign.GetComponent<alignLetters>().InactiveAll();
        QuitMenu.GetComponent<Animator>().SetBool("now", false);
        CancelInvoke("quitTimer");
        Invoke("quitTimer", 1);
    }
    public void Hint() {
        HintScreen.GetComponent<Animator>().SetTrigger("now");
        CancelInvoke("hintOff");
        Invoke("hintOff",2.2f);

        // data storage
        db.DB.allData.numHintPressed += 1;
        db.Save();
    }
    public void nextBtn() {
        ScoreBoard.GetComponent<Animator>().SetInteger("stars", 0);
        CancelInvoke("StarsOFF");
        Invoke("StarsOFF", 0.5f);
        result.GetComponent<Animator>().SetBool("on", false);
        CancelInvoke("ResetQ");
        Invoke("ResetQ",0.5f);
    }
    // invoking methods
    void ResetQ()
    {
        // to reset the question
        reset = true;
        questHandler.GetComponent<questionHandler>().reset = true;
    }
    void StarsON()
    {
        ScoreBoard.SetActive(true);
        ScoreBoard.GetComponent<Animator>().SetInteger("stars", score);
    }
    void StarsOFF()
    {
        ScoreBoard.SetActive(false);
    }
    //hint active after 30sec
    void hintOn()
    {
        hintBtn.SetActive(true);
    }
    // hint inactive after used ones
    void hintOff()
    {
        hintBtn.SetActive(false);
    }
    void quitTimer() {
        SwitchMM.GetComponent<SwitchGame>().quit = true;
    }
}
