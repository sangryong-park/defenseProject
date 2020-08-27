using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    static StageManager instance;
    public static StageManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<StageManager>();
            }

            return instance;
        }

    }

    public Transform enemyPrefab;
    public Transform spawnPoint;

    public GameObject passWaveAnim;
    public GameObject monsterApearEffect;

    public DropZone[] myDropZones;
    public DropZone[] plusDropZones;

    public bool isWaving;


    public float currentTimeScale;
    public float ttime;
    public int currentWave;
    //public int currentLv;
    public int currentGold;
    public int currentInterest;
    public int currentEnemyNum;

    public int currentUintNum;

    public int currentLeaderHp;
    public int LeaderMaxHp;


    public List<Character> plusList;

    // Start is called before the first frame update
    void Start()
    {
        plusList = new List<Character>();
        currentUintNum = 0;
        currentInterest = 0;
        currentTimeScale = 1;
        currentEnemyNum = -1;
        ttime = 20.5f;
        isWaving = false;
        currentWave = 1;
        currentGold = 300;

        LeaderMaxHp = 10;
        currentLeaderHp = LeaderMaxHp;

        CurrentUnitCheck();


    }

    // Update is called once per frame
    void Update()
    {
  
        if (ttime <= 0f && !isWaving)
        {
            if (DragManager.Instance.currentDragObject != null)
            {
                DragManager.Instance.currentDragObject.GetComponent<Draggable>().WaveOnMouseUp();
            }

          
            StartCoroutine(SpawnWave());
            isWaving = true;
            ttime = 20.5f;

           

        }

        if(!isWaving)
        {
            ttime -= Time.deltaTime;
        }

        if(currentEnemyNum == 0)
        {
            WaveClear();

            currentEnemyNum = -1;

            if (currentWave <= 29)
            {
                isWaving = false;
                currentWave++;

            }
            else
            {
                GameClear();
            }                 
        }

        if(currentGold >  40)
        {
            currentInterest = 4;
        }
        else
        {
            
            currentInterest = (currentGold / 10) * 1;
        }

  
    }

    public void WaveClear()
    {
        StoreManager.Instance.Xp_Up(2);

        if(!StoreManager.Instance.isLock)
        {
            StoreManager.Instance.Refresh_wave();
        }
        else
        {
            StoreManager.Instance.LockBtn();
        }

        Time.timeScale = 1;
        
        currentGold += 5;
        currentGold += (currentInterest + SynergyManager.Instance.synergy_bonus[7]*1);
    }


    IEnumerator SpawnWave()
    {
        currentEnemyNum = 30;
        Time.timeScale = currentTimeScale;


        passWaveAnim.GetComponent<Animator>().Play("스테이지 넘기는연출 실행");
        yield return new WaitForSeconds(2.0f);
       

        //waveIndex++;
        for (int i = 0; i < 30; i++)
        {
            
            monsterApearEffect.SetActive(true);
            yield return new WaitForSeconds(0.05f);
            SpawnEnemy();
            yield return new WaitForSeconds(0.8f);
            monsterApearEffect.SetActive(false);
        }


    }


    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public void Hit()
    {
        currentLeaderHp -= 1;
        if (currentLeaderHp <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Time.timeScale= 0;
        StageUIManager.Instance.gameOverWindow.SetActive(true);


        int dia = PlayerPrefs.GetInt("Dia");
        dia += currentWave;
        StageUIManager.Instance.gameOverText.text = "Game Over";
        StageUIManager.Instance.gameOverWaveText.text = "Wave " + StageManager.Instance.currentWave;
        StageUIManager.Instance.gameOverDiaText.text = "보상 : " + StageManager.Instance.currentWave;

        PlayerPrefs.SetInt("Dia",dia);
    }

    public void GameClear()
    {

        Time.timeScale = 0;
        StageUIManager.Instance.gameOverWindow.SetActive(true);
    

        int dia = PlayerPrefs.GetInt("Dia");
        dia += (currentWave + 10);
        StageUIManager.Instance.gameOverText.text = "Game Clear!!!";
        StageUIManager.Instance.gameOverWaveText.text = "Wave " + (StageManager.Instance.currentWave);
        StageUIManager.Instance.gameOverDiaText.text = "보상 : " + (StageManager.Instance.currentWave + 10);

        PlayerPrefs.SetInt("Dia", dia);
    }

    public void GameOverBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");

    }

    public void CurrentUnitCheck()
    {
        int num = 0;
        string plusId = "";
        for (int i = 0; i < myDropZones.Length; i++)
        {
           
            if(myDropZones[i].currentObj != null)
            {
                num++;
            }
        
        }

        currentUintNum = num;

        for (int i = 0; i < plusDropZones.Length; i++)
        {
            if (plusDropZones[i].currentObj != null)
            {
                plusId = (plusDropZones[i].currentObj.GetComponent<Character>().id).ToString()+ "Id" + (plusDropZones[i].currentObj.GetComponent<Character>().Lv).ToString()+ "Lv" ;
            }

        }

        Debug.Log("체크함" );
    }





    #region 합치기_3개버전
    /*
    public bool PlusCheck_buy(int Id)
    {
        plusList.Clear();
        for (int i = 0; i < plusDropZones.Length; i++)
        {
            if (plusDropZones[i].currentObj != null)
            {
                plusList.Add(plusDropZones[i].currentObj.GetComponent<Character>());

            }

        }

        List<Character> myList = new List<Character>();

        foreach (Character item in plusList)
        {
            if(item.Lv == 1 && item.id == Id)
            {
                myList.Add(item);
            }
        }

        if(myList.Count == 2)
        {
            myList[0].LvUp();
            myList[1].DestroyLv();
          
            return true;
        }
       
        return false;

    
    }

    public bool PlusCheck_DropZone( int Id )
    {
        plusList.Clear();
        for (int i = 0; i < plusDropZones.Length; i++)
        {
            if (plusDropZones[i].currentObj != null)
            {
                plusList.Add(plusDropZones[i].currentObj.GetComponent<Character>());

            }

        }

        List<Character> myList = new List<Character>();

        foreach (Character item in plusList)
        {
            if (item.Lv == 2 && item.id == Id)
            {
                myList.Add(item);
            }
        }

        if (myList.Count == 3)
        {
            myList[0].LvUp();
            myList[1].DestroyLv();
            myList[2].DestroyLv();

            return true;
        }

        return false;


    }*/
    #endregion
    #region 합치기_2개버전
    public bool PlusCheck_buy(int Id)
    {
        plusList.Clear();
        for (int i = 0; i < plusDropZones.Length; i++)
        {
            if (plusDropZones[i].currentObj != null)
            {
                plusList.Add(plusDropZones[i].currentObj.GetComponent<Character>());

            }

        }

        List<Character> myList = new List<Character>();

        foreach (Character item in plusList)
        {
            if (item.Lv == 1 && item.id == Id)
            {
                myList.Add(item);
            }
        }

        if (myList.Count == 1)
        {
            myList[0].LvUp();
           // myList[1].DestroyLv();

            return true;
        }

        return false;


    }

    public bool PlusCheck_DropZone(int Id , int lv)
    {

        plusList.Clear();
        for (int i = 0; i < plusDropZones.Length; i++)
        {
            if (plusDropZones[i].currentObj != null)
            {
                plusList.Add(plusDropZones[i].currentObj.GetComponent<Character>());

            }

        }

        List<Character> myList = new List<Character>();

        foreach (Character item in plusList)
        {
            if (item.Lv == lv && item.id == Id)
            {
                myList.Add(item);
            }
        }

        if (myList.Count == 2)
        {
            myList[0].LvUp();
            myList[1].DestroyLv();
           

            return true;
        }



        return false;


    }
    #endregion

}
