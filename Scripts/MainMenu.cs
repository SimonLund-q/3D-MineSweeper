using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public static int width = 5;
    public static int height = 5;
    public static int depth = 5;

    public TMP_Text widthInput;
    public TMP_Text heightInput;
    public TMP_Text depthInput;
    
    public TMP_Text finalScore;
    public TMP_Text highScore;
    public bool result;

    public AudioSource sfx;
    public AudioClip sfx1;
    public float countDown;

    void Start()
    {
        prossesScore();
        getHighScore();
    }
    void getHighScore(){
        highScore.text = "HighScore: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
    void prossesScore(){
        if(Generation.score > PlayerPrefs.GetInt("HighScore", Generation.score) && Generation.lives > 0){
            PlayerPrefs.SetInt("HighScore", Generation.score);
            getHighScore();
        }
        
    }
    public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void quit(){
        Application.Quit();
        Debug.Log("Quit");
    }
    private string GetNumbers(String InputString)
        {
            String Result = "";
            string Numbers = "0123456789";
            int i = 0;

            for (i = 0; i < InputString.Length; i++)
            {
                if(Numbers.Contains(InputString.ElementAt(i)))
                {
                    Result += InputString.ElementAt(i);
                }
            }
            return Result;
        }

    private int CheckWithErrors(string str)
    {
        int number;
        string resultString = GetNumbers(str);
        try {
            number = Int32.Parse(resultString);  
        } catch(FormatException) {
            number = 5;
            Debug.Log("Not a number");
        }
        return number;
    }


    // Update is called once per frame
    void Update()
    {
        countDown = countDown - Time.deltaTime;
        if (countDown <= 0f){
            sfx.clip = sfx1;
            sfx.Play();
            countDown = 72f;
        }
        
        width = CheckWithErrors(widthInput.text);
        height = CheckWithErrors(heightInput.text);
        depth = CheckWithErrors(depthInput.text);

        if (Generation.lives > 0){
            finalScore.text = "Score = " + Generation.score; 
        } else if (Generation.lives < 0){
            finalScore.text = "You lost : Out of lives"; 
        }
    }
}
