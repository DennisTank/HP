using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGame : MonoBehaviour
{
    /// <summary>
    ///  To Switch between the games 
    /// </summary>
    public GameObject MainMenu;
    public GameObject wordApp;
    public GameObject tableApp;

    public GameManager gm;
    public tableManager tm;

    [HideInInspector] public bool quit;

    private void Awake()
    {
        quit = false;
        MainMenu.SetActive(true);
        wordApp.SetActive(false);
        tableApp.SetActive(false);
    }
    private void Update()
    {
        if (quit) {
            QuitToMM();
            quit = false;
        }
    }
    void QuitToMM() {
        wordApp.SetActive(false);
        tableApp.SetActive(false);
        MainMenu.SetActive(true);
    }
    public void WordGameOpt() {
        MainMenu.SetActive(false);
        wordApp.SetActive(true);
        gm.FullReset();
    }
    public void TableGameOpt() {
        MainMenu.SetActive(false);
        tableApp.SetActive(true);
        tm.reset();
    }
    
}
