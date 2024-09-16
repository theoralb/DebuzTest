using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField]private AnswerDisplay[] answerDisplay;
    [SerializeField]private TextMeshProUGUI Hint;
    [SerializeField]private GameObject finishDialog;
    [SerializeField]private TextMeshProUGUI correctAnswer;
    [SerializeField]private TextMeshProUGUI resultText;
    private string answer="";
    private int currentAnswer = 0;
    private int currentChar = 0;
    private bool canEnter = true;
    private List<string> fiveLetterWords = new List<string>
    {
        "apple", "brave", "chase", "drink", "eager",
        "faith", "giant", "house", "ideal", "jolly",
        "karma", "lucky", "magic", "noble", "ocean",
        "peace", "quest", "raise", "shine", "tiger",
        "urban", "vivid", "whale", "xenon", "youth",
        "zebra", "adapt", "bloom", "clear", "dream",
        "enjoy", "flame", "grape", "happy", "ivory",
        "jolly", "knock", "light", "merry", "nurse",
        "olive", "piano", "quilt", "river", "sweet",
        "teach", "union", "valve", "witty", "xylem",
        "yield", "azure", "bless", "crowd", "debut",
        "equip", "fancy", "grasp", "hover", "input",
        "joint", "kneel", "lemon", "model", "never",
        "order", "party", "quick", "rumor", "slice",
        "title", "uncle", "vital", "world", "xerox",
        "young", "zonal", "align", "beach", "craft",
        "ditch", "event", "flock", "globe", "horse",
        "image", "joint", "knife", "lunch", "medal",
        "nifty", "occur", "plant", "quiet", "rouge",
        "score", "tribe", "ultra", "voice", "waste"
    };

    private void Start()
    {
        ResetGame();
    }

    public void ResetGame()
    {
        int rnd = Random.Range(0, fiveLetterWords.Count);
        answer = fiveLetterWords[rnd].ToUpper();
        fiveLetterWords.RemoveAt(rnd);
        Hint.text = answer;
        currentAnswer = 0;
        currentChar = 0;
        UnHilightAll();
        answerDisplay[currentAnswer].Hilight(true);
        for (int i = 0; i < answerDisplay.Length; i++)
        {
            for (int j = 0; j < answerDisplay[i].Text.Length; j++)
            {
                answerDisplay[i].Text[j].text = "";
                answerDisplay[i].Text[j].transform.parent.Find("Image").GetComponent<Image>().color = new Color(0f, 0f, 0f);
            }
        }

        foreach (Image image in KeyAssign.ImageMap.Values) {
            image.color = new Color(26/255f, 26/255f, 26/255f);
        }

        finishDialog.SetActive(false);
        canEnter = true;
    }

    public void Terminate()
    {
        Application.Quit();
    }



    IEnumerator CheckAnswer()
    {

        string userAnswer = answerDisplay[currentAnswer].toString();
        for (int i = 0; i <5; i++)
        {
            answerDisplay[currentAnswer].Text[i].transform.parent.Find("Image").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            if (i > userAnswer.Length-1)
            {
                continue;
            }
            for (int j = 0; j < 5; j++)
            {
                if (userAnswer[i] == answer[j])
                {
                    answerDisplay[currentAnswer].Text[i].transform.parent.Find("Image").GetComponent<Image>().color = new Color(0.5f,0.5f,0);
                    KeyAssign.ImageMap["Button_"+ userAnswer[i]].color = new Color(0.5f, 0.5f, 0);
                    if (i == j)
                    {
                        answerDisplay[currentAnswer].Text[i].transform.parent.Find("Image").GetComponent<Image>().color = new Color(0, 0.5f, 0);
                        KeyAssign.ImageMap["Button_" + userAnswer[i]].color = new Color(0, 0.5f, 0);
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(0.15f);
        }

        if(userAnswer == answer)
        {
            correctAnswer.text = answer;
            resultText.text = "You win\n"+ (currentAnswer+1) + " trys";
            finishDialog.SetActive(true);
            yield break;
        }

        if(currentAnswer == 5)
        {
            correctAnswer.text = answer;
            resultText.text = "You Lose\n" + (currentAnswer+1) + " trys";
            finishDialog.SetActive(true);
            yield break;
        }


        yield return null;
        UnHilightAll();
        currentAnswer++;
        answerDisplay[currentAnswer].Hilight(true);
        currentChar = 0;
        canEnter = true;
    }

    public void UnHilightAll()
    {
        for (int i = 0; i < answerDisplay.Length; i++)
        {
            answerDisplay[i].Hilight(false);
        }
    }

    public void  OnKeyboardClick(Button button)
    {
        if (!canEnter) return;
        string input = button.name.Split('_')[1];
        if(input == "Enter")
        {
            canEnter = false;
            StartCoroutine(CheckAnswer());
            return;

        }
        if (input == "Backspace")
        {
            answerDisplay[currentAnswer].Text[currentChar].text = "";
            currentChar--;
            if (currentChar <= 0)
            {
                currentChar = 0;
            }
            return;
        }

        answerDisplay[currentAnswer].Text[currentChar].text = input;
        currentChar++;
        if (currentChar > 4)
        {
            currentChar = 4;
        }

    }
}
