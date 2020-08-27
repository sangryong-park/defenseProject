using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBtn : MonoBehaviour
{

    public GameObject stopWindow;



    public void Stop_btn()
    {
        stopWindow.SetActive(true);
        Time.timeScale = 0;
    }

    public void Stop_btn2()
    {
        stopWindow.SetActive(false);
        Time.timeScale = StageManager.Instance.currentTimeScale;
    }

}
