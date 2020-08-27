using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastBtn : MonoBehaviour
{
    public GameObject fastImage;
    public bool isfast;
    

    public void Fast_Btn_()
    {
        if (StageManager.Instance.isWaving)
        {
            if (!isfast)
            {
                isfast = true;
                fastImage.SetActive(true);
                Time.timeScale = 2f;
                StageManager.Instance.currentTimeScale = 2f;

            }
            else
            {
                isfast = false;
                fastImage.SetActive(false);
                Time.timeScale = 1;
                StageManager.Instance.currentTimeScale = 1;

            }
        }else
        {
            if (!isfast)
            {
                isfast = true;
                fastImage.SetActive(true);
            
                StageManager.Instance.currentTimeScale = 2;

            }
            else
            {
                isfast = false;
                fastImage.SetActive(false);
            
                StageManager.Instance.currentTimeScale = 1;

            }
        }
    }



}
