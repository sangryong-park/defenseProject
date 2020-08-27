using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ModeManager : MonoBehaviour
{

    public GameObject[] map;
    // Start is called before the first frame update
    void Start()
    {
        map[0].SetActive(false);
        map[1].SetActive(false);
        switch (PlayerPrefs.GetInt("ModeNum"))
        {
            case 0:
                map[0].SetActive(true);         
                break;
            case 1:
                map[1].SetActive(true);
                break;
            case 2:
                map[0].SetActive(true);
                break;
        }
    }


    public void TitleBtn()
    {
        SceneManager.LoadScene("title");
    }
}
