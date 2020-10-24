using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class alignLetters : MonoBehaviour
{
    /// <summary>
    /// Randomly aligning the letters
    /// managing push and pop of letters
    /// </summary>

    // data storage
    public DB_Script db;

    public GameObject questHandler;

    // taking the slots positions and the letter positions
    public GameObject[] slots;
    public GameObject[] letters;

    // basic informations
    [HideInInspector] public string word;
    [HideInInspector] public bool reset;
    [HideInInspector] public int letterIndex;
    [HideInInspector] public int currentSlot;

    // sub-informations
    int currentWordHight;
    List<int> list;

    private void Awake()
    {
        reset = false;
        currentWordHight = -1;
        list = new List<int>();
    }
    private void Update()
    {
        // if all the slots are filled and the word is formed
        // show the result and than new question
        if (currentWordHight == word.Length-1) {           
            questHandler.GetComponent<questionHandler>().done = true;
            currentWordHight = -1;
        }
        // reset the positions of the buttons, also assigning them the letters randomly
        if (reset) {
            currentWordHight = -1;
            list.Clear();
            // turing all the slots inactive again
            InactiveAll();

            for (int i = 0; i < word.Length; i++) list.Add(i);

            int x = 0;
            for(int i=0; i < word.Length;i++) {

                x = list[Random.Range(0, list.Count)];

                letters[x].GetComponent<letter>().index = x;
                letters[x].GetComponent<letter>().on = true;
                letters[x].GetComponentInChildren<Text>().text = word[i].ToString();
                letters[x].GetComponent<Animator>().SetBool("on",true);

                list.Remove(x);
            }
            reset = false;
        }
    }
    public void InactiveAll() {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<Animator>().SetBool("on", false);
        }
        for (int i = 0; i < letters.Length; i++)
        {
            letters[i].GetComponent<Animator>().SetBool("on", false);
        }
    }
    // Pointer Functions for the letter holding buttons and slots
    public void letterButton() {
        if (letters[letterIndex].GetComponent<letter>().on) {
            // after incrementing slot, making the last slot inactive 
            if (currentWordHight >= 0) slots[currentWordHight].GetComponent<slot>().on = false;
            // current word length
            currentWordHight++;
            // pushing Answer
            questHandler.GetComponent<questionHandler>().currentAns[currentWordHight] = letters[letterIndex].GetComponentInChildren<Text>().text[0];
            // set letter
            slots[currentWordHight].GetComponentInChildren<Text>().text = letters[letterIndex].GetComponentInChildren<Text>().text;
            slots[currentWordHight].GetComponent<slot>().letterIndex = letters[letterIndex].GetComponent<letter>().index;

            // current slot activation
            slots[currentWordHight].GetComponent<slot>().on = true;
            // slot animation
            slots[currentWordHight].GetComponent<Animator>().SetBool("on",true);
            // letter animation
            letters[letterIndex].GetComponent<Animator>().SetBool("on",false);

            // storing data
            db.DB.allData.numLetterPressed += 1;
            db.DB.allData.letters += letters[letterIndex].GetComponentInChildren<Text>().text;
            db.Save();
        }
    }
    public void Slote() {
        // if the pressed slot is not leading then do nothing
        if (currentSlot != currentWordHight) return;
        // checking for activaiton Just in case
        if (slots[currentSlot].GetComponent<slot>().on) {
            // after decrementing the slot, making the last slot active and current inactive
            if (currentSlot > 0) { 
                slots[currentSlot - 1].GetComponent<slot>().on = true; 
            }
            slots[currentSlot].GetComponent<slot>().on = false;
            //slots animation
            slots[currentSlot].GetComponent<Animator>().SetBool("on",false);
            // letter animation from using index
            letters[slots[currentSlot].GetComponent<slot>().letterIndex].GetComponent<Animator>().SetBool("on",true);
            // popping Answers
            questHandler.GetComponent<questionHandler>().currentAns[currentWordHight] = '\0';
            currentWordHight--;
        }
    }
}
