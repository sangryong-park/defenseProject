using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{


    public int ModeNum
    {
        get
        {
            return PlayerPrefs.GetInt("ModeNum");
        }
        set
        {
            PlayerPrefs.SetInt("ModeNum", value);
        }
    }

    public int Dia
    {
        get
        {
            return PlayerPrefs.GetInt("Dia", 0);
        }
        set
        {
            PlayerPrefs.SetInt("Dia", value);
        }
    }

    public GameObject charecters;
    public GameObject modeBtns;
    public GameObject touchBtn;

    public GameObject choiceWindow;
    public GameObject leaderChoiceWindow;
    public GameObject[] ModeText;
   


    public Text diaText;

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        diaText.text = "" + Dia;
    }

    public void ModeChoice( int num)
    {
        ModeNum = num;

        for (int i = 0; i < ModeText.Length; i++)
        {
            ModeText[i].SetActive(false);
        }
        ModeText[num].SetActive(true);
        choiceWindow.SetActive(true);
        modeBtns.SetActive(false);
    }

    public void LeaderChoiceWindowOpen()
    {
        choiceWindow.SetActive(false);
        leaderChoiceWindow.SetActive(true);
    }

    public void CloseLeaderWindow()
    {
        choiceWindow.SetActive(false);
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Game");
    }

    public void Touch()
    {
        charecters.SetActive(false);
        modeBtns.SetActive(true);
        touchBtn.SetActive(false);
    }
}

