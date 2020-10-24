using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class questionHandler : MonoBehaviour
{
    /// <summary>
    /// This handles the question or picture to be shown and
    /// if the answer is correnct or not
    /// </summary>

    // dynamically changing the hint as per the picture.
    public Text hint;
    // dynamically changing the image.
    public Image objImage;
    public GameObject letterAligner;
    public GameObject gameManager;

    // Registry data Structure to load all the sprites or images 
    public Sprite[] obj;

    [HideInInspector] public string word; // current Word
    [HideInInspector] public char[] currentAns; // current answer
    [HideInInspector] public bool reset; // Just to reset the question
    [HideInInspector] public bool win; // did the answer was right or wrong
    [HideInInspector] public bool done; // submitting the answer

    List<Sprite> picture; // list of images from Registry 
    List<string> words; // list of name of the images
    int currentWordIndex; // at what index the question is running
   
    // to calcutate correction percentage to give the stars
    int numCorrectLetter; 
    float percentage;

    private void Start()
    {

        percentage = 0;
        numCorrectLetter = 0;
        reset = win = done = false;
        words = new List<string>();
        picture = new List<Sprite>();
        objImage.color = Color.white;

        //loading the images and words in the list
        LoadWords();
        // randomly taking a image
        Init();
    }
    private void Update()
    {
        if (done) {
            // calculation the similarity percentage
            numCorrectLetter = 0;
            for (int i = 0; i < word.Length; i++) {
                numCorrectLetter += (word[i] == currentAns[i]) ? 1 : 0;
            }
            percentage = ((float)numCorrectLetter) / ((float)word.Length);
            
            // letting the GM know the number of stars and score to show on the screen
            gameManager.GetComponent<GameManager>().done = true;
            if (percentage < 0.3f) {
                gameManager.GetComponent<GameManager>().score = 1;
            }
            else if (percentage < 1) {
                gameManager.GetComponent<GameManager>().score = 2;
            }
            else {
                win = true;
                gameManager.GetComponent<GameManager>().score = 3;
            }
            done = false;
        }
        if (reset) {
            if (win)
            {
                // if the answer was correct the image will not show up again
                // by removeing them from the list

                words.RemoveAt(currentWordIndex);
                picture.RemoveAt(currentWordIndex);
                if (words.Count == 0) {
                    //Just in case if the Registry has less number of images and
                    //the list is over, than Reloadin all the images again.
                    LoadWords();
                }
                // randomly taking a index in list
                currentWordIndex = Random.Range(0, words.Count);
            }
            else {
                // if the answer was incorrect the image will will show up again randomly
                // adding 1 to the index and making sure it is in the Length of the list
                currentWordIndex = (currentWordIndex + 1) % words.Count;
            }
            // defining the current word, hint and image to the letter align script
            word = words[currentWordIndex];
            hint.text = word;
            objImage.sprite = picture[currentWordIndex];
            objImage.gameObject.GetComponent<Animator>().SetTrigger("now");
            currentAns = new char[word.Length];
            letterAligner.GetComponent<alignLetters>().word = word;
            letterAligner.GetComponent<alignLetters>().reset = true;

            reset = win = false;
        }
    }
    // randomly taking and defining a pic, hint and a word at initial 
    void Init() {
        currentWordIndex = Random.Range(0, words.Count);
        word = words[currentWordIndex];
        hint.text = word;
        objImage.sprite = picture[currentWordIndex];
        objImage.gameObject.GetComponent<Animator>().SetTrigger("now");
        currentAns = new char[word.Length];
        letterAligner.GetComponent<alignLetters>().word = word;
        letterAligner.GetComponent<alignLetters>().reset = true;
    }

    //Loading words and pic
    void LoadWords() {
        for (int i = 0; i < obj.Length; i++) {
            words.Add(obj[i].name.ToUpper());
            picture.Add(obj[i]);
        }
    }

}
