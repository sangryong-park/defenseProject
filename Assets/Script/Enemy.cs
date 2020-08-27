using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject stunEffect;
    public GameObject slowEffect;
    public int wavepointIndex = 0;
    public GameObject canvas;
    public GameObject sliderObj;
    public Slider hpSlider;

    public GameObject myObj;
  

    public Transform target;



    public float baseSpeed = 1.7f;
    public float CurrentSpeed;
    public float maxHp;
    public float currentHp;
    public int giveToGold;


    public bool isDie;

    public bool isFly;

    public bool isIce;
    public bool isStun;

    public float stunCool;

    public GameObject flyText;

    private void Start()
    {
        
        CurrentSpeed = baseSpeed;
        float num2 = StageManager.Instance.currentWave * StageManager.Instance.currentWave;
        float num3 = StageManager.Instance.currentWave * StageManager.Instance.currentWave * StageManager.Instance.currentWave;

        switch (PlayerPrefs.GetInt("ModeNum"))
        {
            case 0:
                maxHp = 10 + (num2*4) + (num3*0.4f);
                break;
            case 1:
                maxHp = 10 + (num2*6) + (num3*0.6f);
                break;
            case 2:
                maxHp = 10 + (num2*6) + (num3*0.6f);
                break;
        }

        GameObject clone = Instantiate((GameObject)Resources.Load("EnemyPrefab/" + StageManager.Instance.currentWave));
        clone.transform.position = transform.position;
        clone.transform.parent = myObj.transform;

        if (clone.tag == "Fly")
        {
            myObj.transform.position = myObj.transform.position + new Vector3(0, 1, 0);
            sliderObj.transform.position = sliderObj.transform.position + new Vector3(0, 1, 0);

            maxHp = maxHp * 0.5f;
            flyText.SetActive(true);

        }else
        {
            flyText.SetActive(false);
        }


        currentHp = maxHp;
        target = WayPoint.points[0];
        myObj.transform.LookAt(target);

        hpSlider.maxValue = maxHp;
        hpSlider.value = currentHp;






        
    }

    private void Update()
    {

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * CurrentSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position , target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }
       
        if(stunCool >= 0)
        {
            stunCool -= Time.deltaTime;
        }

     
    }


    void GetNextWayPoint()
    {

        if (wavepointIndex >= WayPoint.points.Length - 1)
        {
            wavepointIndex = 0;
        }else
        {
            wavepointIndex++;
            
        }
        target = WayPoint.points[wavepointIndex];
        myObj.transform.LookAt(target);
        
     //   canvas.transform.Rotate(0, +90, 0,0);
    }

    public void Hit(float _atk ,float _fatal , string[] synergyName)
    {
        List<string> synergys = new List<string>();
        for (int i = 0; i < synergyName.Length; i++)
        {
            synergys.Add(synergyName[i]);
        }

        
        int cri = Random.Range(1, 101);
        GameObject pop;
        if(cri > _fatal)
        {
            currentHp -= (int)_atk;
            pop =  PoolManager.Instance.Instantiate("데미지/데미지");
            pop.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "" + (int)_atk;
     
        }
        else
        {
            int dmg = ((2 * (int)_atk) + ((int)((2 * _atk) * 0.2f * SynergyManager.Instance.synergy_bonus[8])));
            currentHp -= dmg;
            pop = PoolManager.Instance.Instantiate("데미지/크리티컬데미지");
            pop.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "" + dmg;
            
        }
        pop.transform.position = transform.position;
        PoolManager.Instance.Destroy(pop,0.35f);
        hpSlider.maxValue = maxHp;
        hpSlider.value = currentHp;
        if(currentHp <= 0)
        {
            Die();
        }

        //얼음
        if(synergys.Contains("빙결") && SynergyManager.Instance.synergy_nums[1] >= 2 )
        {
            StartCoroutine("IceAtk");
        }
        //마법사 스턴
        if (synergys.Contains("마법사") && SynergyManager.Instance.synergy_nums[2] >= 2)
        {
            int ran = Random.Range(0,100);

            int lvPlus = 0;
            if(SynergyManager.Instance.synergy_bonus[2] == 3)
            {
                lvPlus = 5;
            }

            if(ran > (99 - SynergyManager.Instance.synergy_bonus[2]*5 - lvPlus))
            {
                StartCoroutine("StunAtk");
            }
 
        }
        //도적
        



    }

    public void Die()
    {
        if (isDie)
            return;
        StageManager.Instance.currentEnemyNum--;
        isDie = true;
   
        /*
        if(StageManager.Instance.currentWave >= 20)
        {
            StageManager.Instance.currentGold += 5;
        }
        else if(StageManager.Instance.currentWave >= 10)
        {
            StageManager.Instance.currentGold += 3;
        }
        else
        {
            StageManager.Instance.currentGold += 2;
        }*/

        Destroy(gameObject);
    }





    
    IEnumerator IceAtk()
    {
        
        if (!isIce && !isStun)
        {
            isIce = true;
            CurrentSpeed = baseSpeed - (baseSpeed*0.1f * SynergyManager.Instance.synergy_bonus[1]);
            slowEffect.SetActive(true);

            yield return new WaitForSeconds(5);

            if(!isStun)
            {
                CurrentSpeed = baseSpeed;
            }

           
            isIce = false;
            slowEffect.SetActive(false);


        }
    }

    IEnumerator StunAtk()
    {

        if (!isStun && stunCool <= 0)
        {
            isIce = false;
            slowEffect.SetActive(false);


            isStun = true;
            CurrentSpeed = 0;
            stunEffect.SetActive(true);

            yield return new WaitForSeconds(3);
            CurrentSpeed = baseSpeed;
            isStun = false;
            stunEffect.SetActive(false);
            stunCool = 1;


        }
    }



}
