using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

    public ParticleSystem myRatingRarticle;
    public bool isStart;



    // public static List<InfoBase> infoList = new List<InfoBase>();

    [Header("스톤")]
    public GameObject[] stones;
    public float stoneAtk;
    public float stoneSpeed;
    public float stoneFatal;
    public float stonenum;

    [Header("***")]


    public bool rote;
    public float rotateTime = 1;
    public Animator[] animType;
    public Animator myAnim;
    public Transform target;

    public Text nameText;

    public GameObject[] cha_prefab;

    public int Lv;


    public int id;

    public string name_;

    public int rating;

    public string synergy1;

    public string synergy2;

    public string synergy3;

    public string synergy4;

    public string[] synergy_names;
   

    public float ATK;
    public float ATK_bonus
    {
        get
        {
            int lvPlus = 1;
            switch (Lv)
            {
                case 1:
                    lvPlus = 1;
                    break;
                case 2:
                    lvPlus = 3;
                    break;
                case 3:
                    lvPlus = 9;
                    break;
                case 4:
                    lvPlus = 27 + (int)(27.0f * (0.5f * SynergyManager.Instance.synergy_bonus[5]));
                    break;
            }

            List<string> synergys = new List<string>();
            for (int i = 0; i < synergy_names.Length; i++)
            {
                synergys.Add(synergy_names[i]);
            }

            float stone = ((100 + stoneAtk) * 0.01f);
            float sb = 0;
            if (stoneAtk != 0 && ATK <= 20)
                sb = 1;


            if(id == 27 || id == 28)
            {
                return 0;
            }

            if (id == 26)
            {

                if(Lv == 1)
                {
                    return 5;
                }
                else if(Lv == 2)
                {
                    return 5;
                }
                else if(Lv == 3)
                {
                    return 10;
                }

                return 5;
            }
            else
            {


                if (synergys.Contains("기사") && SynergyManager.Instance.synergy_nums[3] >= 2)
                {

                    return (((ATK * lvPlus) + (10 * SynergyManager.Instance.synergy_bonus[3] * Lv * Lv)) * stone) + sb;

                }
                else
                {
                    return ((ATK * lvPlus) * stone) + sb;
                }

            }




        }
    }

    public float ATKspeed;
    public float ATKspeed_Bonus
    {
        get
        {

            if (id == 27)
            {

                if (Lv == 1)
                {
                    return 10;
                }
                else if (Lv == 2)
                {
                    return 10;
                }
                else if (Lv == 3)
                {
                    return 20;
                }

                return 10;
            }




            List<string> synergys = new List<string>();
            for (int i = 0; i < synergy_names.Length; i++)
            {
                synergys.Add(synergy_names[i]);
            }

            if (synergys.Contains("광전사") && SynergyManager.Instance.synergy_nums[0] >= 2)
            {
                return ATKspeed - ((0.15f)*ATKspeed * SynergyManager.Instance.synergy_bonus[0]) - stoneSpeed*0.01f;
            }
            else
            {
                return ATKspeed - stoneSpeed*0.01f;
            }
  
        }
    }



    public float fatal;
    public float Fatal_bonus
    {
        get
        {


            if (id == 28)
            {

                if (Lv == 1)
                {
                    return 15;
                }
                else if (Lv == 2)
                {
                    return 15;
                }
                else if (Lv == 3)
                {
                    return 30;
                }

                return 15;
            }




            List<string> synergys = new List<string>();
            for (int i = 0; i < synergy_names.Length; i++)
            {
                synergys.Add(synergy_names[i]);
            }
            if (SynergyManager.Instance.synergy_nums[9] >= 2 && cha_prefab_.tag != "Stone")
            {
                return fatal + 10*SynergyManager.Instance.synergy_bonus[9] + stoneFatal;
            }
            else
            {
                return fatal + stoneFatal;
            }
        }
    }
         

    public float ATKrange;
    public float ATKrange_bonus
    {
        get
        {
            return ATKrange + 0.5f * SynergyManager.Instance.synergy_bonus[4];
        }
    }

    public int ATKtype;

    public int SellPrice
    {
        get
        {
            return (rating * 1 * Lv);
        }
    }

    public float sp;

    public float magicATK;

    public string skill_info;

    public Sprite thisImage;

    public int myid;

    public int timasdsd;

    public float atkCool;


    public GameObject cha_prefab_;

    public GameObject atkPrefab;
    public Transform firePoint;

    public Transform RotatePos;

    public float buffAttackSpeed;

   // public string[] synergys;

   

    public void SetStart(int _myid)
    {
        Lv = 1;

        synergy_names = new string[3];
        
        myid = _myid-1;

       // infoList = InfoDatabase.infoList;

        this.id = InfoDatabase.infoList[myid].id;
        this.name_ = InfoDatabase.infoList[myid].name_;
        this.rating = InfoDatabase.infoList[myid].rating;
        this.synergy1 = InfoDatabase.infoList[myid].synergy1;
        this.synergy2 = InfoDatabase.infoList[myid].synergy2;
        this.synergy3 = InfoDatabase.infoList[myid].synergy3;
        this.synergy4 = InfoDatabase.infoList[myid].synergy4;
        this.synergy_names[0] = InfoDatabase.infoList[myid].synergy1;
        this.synergy_names[1] = InfoDatabase.infoList[myid].synergy2;
        this.synergy_names[2] = InfoDatabase.infoList[myid].synergy3;
        this.ATK = InfoDatabase.infoList[myid].ATK;
        this.ATKspeed = InfoDatabase.infoList[myid].ATKspeed;
        this.fatal = InfoDatabase.infoList[myid].fatal;
        this.ATKrange = InfoDatabase.infoList[myid].ATKrange;
        this.ATKtype = InfoDatabase.infoList[myid].ATKtype;
        this.sp = InfoDatabase.infoList[myid].sp;
        this.magicATK = InfoDatabase.infoList[myid].magicATK;
        this.skill_info = InfoDatabase.infoList[myid].skill_info;
        this.thisImage = InfoDatabase.infoList[myid].thisImage;

       

        //  this.synergys[0] = infoList[myid].synergy1;
        //  this.synergys[1] = infoList[myid].synergy2;
        //  this.synergys[2] = infoList[myid].synergy3;
        //  this.synergys[3] = infoList[myid].synergy4;

        cha_prefab_ = Instantiate(Resources.Load<GameObject>("유닛BasePrefab/" + id), RotatePos);
        cha_prefab_.transform.position = transform.position;

        cha_prefab_.SetActive(true);
        cha_prefab_.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        myAnim = cha_prefab_.GetComponent<Animator>();

        if (ATKtype == 1 && cha_prefab_.tag != "Stone")
        {

            ATKrange -= 0.5f;
        }


        buffAttackSpeed = 1;
        RotatePos.transform.rotation = new Quaternion(0, 180, 0, 0);

        InvokeRepeating("UpdateTarget", 1, 0.5f);

        isStart = true;
        nameText.text = "" + "Lv" + Lv + "\n" + name_;

      

        switch (rating)
        {
            default:
                break;
        }

        ParticleSystem.MainModule psMain = myRatingRarticle.main;

        switch (rating)
        {
            case 1:
                psMain.startColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                break;
            case 2:
                psMain.startColor = new Color(0, 1, 0, 0.5f);
                break;
            case 3:
                psMain.startColor = new Color(0, 0, 1, 0.5f);
                break;
            case 4:
                psMain.startColor = new Color(1, 0, 0, 0.5f);
                break;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void UpdateTarget()
    {



        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= ATKrange_bonus)
        {
            target = nearestEnemy.transform;
        }else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {



        if (cha_prefab_.tag == "Stone")
            return;

        if(stonenum >= 0)
        {
            stonenum -= Time.deltaTime;
        }else
        {
            stoneAtk = 0;
            stoneFatal = 0;
            stoneSpeed = 0;
        }


        if (transform.parent != null && transform.parent.gameObject.tag != "StoreDropZone" && DragManager.Instance.currentDragObject != gameObject  && isStart )
        {
            if (target != null)
            {
              
                    Vector3 dir = target.transform.position - RotatePos.transform.position;
                    RotatePos.transform.rotation = Quaternion.Slerp(RotatePos.transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
                    rotateTime = 2;
               
            }
            else
            {
                rotateTime -= Time.deltaTime;
                if(rotateTime <= 0)
                {
                    RotatePos.transform.rotation = Quaternion.Slerp(RotatePos.transform.rotation, new Quaternion(0, 180, 0, 0), 0.1f * Time.deltaTime);
                }


                if(atkCool == ATKspeed_Bonus)
                atkCool = 0.01f;
            }

            if (atkCool >= 0)
            {
                atkCool -= Time.deltaTime;
            }
            else
            {

                UpdateTarget();
                StartCoroutine("Atk");
                //0공격속도
                
                 atkCool = ATKspeed_Bonus;
               
            }
        }
    }

 

    IEnumerator Atk()
    {

        if (target != null)
        {
          

            Vector3 targetPos = target.position + new Vector3(0,0,0);
            yield return new WaitForSeconds(0.2f);


            string path = "";
            switch (ATKtype)
            {
                case 1 :
                    
                    if(target== null)
                    {
                        break;
                    }
                 
                    if (target.gameObject.GetComponent<Enemy>().myObj.transform.GetChild(0).tag != "Fly")
                    {
                        SoundManager.Instance.Play( "PlayerAttack기사", Define.Sound.Effect);
                        myAnim.Play("attack");
                        path = "공격Prefab/근접";
                        GameObject go1 = PoolManager.Instance.Instantiate(path);
                        go1.transform.position = transform.position + new Vector3(0, 0.5f, 0);
                        go1.transform.localScale = new Vector3(ATKrange_bonus - 0.5f, ATKrange_bonus - 0.5f, ATKrange_bonus - 0.5f);
                        PoolManager.Instance.Destroy(go1, 0.35f);

                        ATK1 attack = go1.GetComponent<ATK1>();
                        if (attack != null)
                        {
                            //9치명타확률
                            attack.Seek(ATK_bonus, Fatal_bonus, synergy_names);

                        }
                    }

                    

                    break;
                case 2:
                    myAnim.Play("attack");
                    path = "공격Prefab/마법";
                    GameObject go2 = PoolManager.Instance.Instantiate(path);
                    ATK3 attack2 = go2.GetComponent<ATK3>();

                    go2.transform.position = FindFirePos().position;
                    go2.transform.rotation = FindFirePos().rotation;

                    if (attack2 != null)
                    {
                        //9치명타확률
                        attack2.Seek(target, targetPos, ATK_bonus, Fatal_bonus,synergy_names);

                    }

                    break;
                case 3:
                    myAnim.Play("attack");
                    SoundManager.Instance.Play( "SupporterAttack궁수",Define.Sound.Effect);
                    path = "공격Prefab/궁수";
                    GameObject go3 = PoolManager.Instance.Instantiate(path);
                    AttackPrefab attack3 = go3.GetComponent<AttackPrefab>();

                    go3.transform.position = FindFirePos().position;
                    go3.transform.rotation = FindFirePos().rotation;

                    if (attack3 != null)
                    {
                        //9치명타확률

                        if (target == null)
                        {
                            break;
                        }

                        if (target.gameObject.GetComponent<Enemy>().myObj.transform.GetChild(0).tag == "Fly")
                        {
                            attack3.Seek(target, targetPos, ATK_bonus*2, Fatal_bonus, synergy_names);
                        }
                        else
                        {
                            attack3.Seek(target, targetPos, ATK_bonus, Fatal_bonus, synergy_names);
                        }

                            

                    }
                  

                    path = "공격Prefab/ATK";
                    break;
                default:
                    path = "공격Prefab/ATK";
                    break;
            }

            //도적
            List<string> synergys = new List<string>();
            for (int i = 0; i < synergy_names.Length; i++)
            {
                synergys.Add(synergy_names[i]);
            }

            if (synergys.Contains("도적") && SynergyManager.Instance.synergy_nums[6] >= 2)
            {
                int num = Random.Range(0, 100);

                if (num > (99 - 3 * SynergyManager.Instance.synergy_bonus[6]))
                {
                    StageManager.Instance.currentGold++;
                }
            }
        }
       
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ATKrange_bonus);
    }


    public void SetInfoWindow()
    {
        InfoWindow.Instance.name_.text = name_;
        // this.rating = infoList[myid].rating;

        if(id == 26)
        {
            InfoWindow.Instance.ATK.text = "공격력 : " + (int)ATK_bonus + "%";
        }
        else
        {
            InfoWindow.Instance.ATK.text = "공격력 : " + (int)ATK_bonus;
        }
       
        //공격속도
        InfoWindow.Instance.ATKspeed.text = "공격속도 : " + ATKspeed_Bonus;
        InfoWindow.Instance.magicATK.text = "마법공격력 : " + magicATK;
        InfoWindow.Instance.fatal.text = "치명타확률 : " + Fatal_bonus;

        if(id == 26 || id == 27 || id == 28)
        {
            InfoWindow.Instance.ATKrange.text = "(같은 스톤은 중복이 불가능합니다.)";
           // InfoWindow.Instance.ATKrange.text = "(같은스톤중복x)";
        }
        else
        {
            InfoWindow.Instance.ATKrange.text = "사거리 : " + ATKrange_bonus;
        }

       
        InfoWindow.Instance.skill_info.text = "스킬정보 : " + skill_info;
        InfoWindow.Instance.thisImage.sprite = thisImage;
        InfoWindow.Instance.sellPriceText.text = "" + SellPrice; 

        SetSynergyImage();
        SetRatingCardColor();
    }

    public void SetSynergyImage()
    {

        for (int i = 0; i < 3; i++)
        {
            InfoWindow.Instance.synergy_images[i].gameObject.SetActive(true);
            InfoWindow.Instance.synergy_texts[i].gameObject.SetActive(true);
            if (synergy_names[i] != "")
            {
                InfoWindow.Instance.synergy_images[i].sprite = Resources.Load<Sprite>("시너지이미지/" + synergy_names[i]);
            }
            else
            {
                InfoWindow.Instance.synergy_images[i].gameObject.SetActive(false);
                InfoWindow.Instance.synergy_texts[i].gameObject.SetActive(false);
            }

            InfoWindow.Instance.synergy_texts[i].text = "" + synergy_names[i];
        }

    }

    public void SetRatingCardColor()
    {
        switch (rating)
        {
            case 1:
                InfoWindow.Instance.window.gameObject.GetComponent<Image>().color = Color.white;
  
                break;
            case 2:
                InfoWindow.Instance.window.gameObject.GetComponent<Image>().color = new Color(0.5f, 1, 0.5f);
          
                break;
            case 3:
                InfoWindow.Instance.window.gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 1);
      
                break;
            case 4:
                InfoWindow.Instance.window.gameObject.GetComponent<Image>().color = new Color(1, 0.5f, 0.5f);
            
                break;
            default:
                break;
        }
    }

    public void DestroyLv()
    {
        if (Lv >= 3 && cha_prefab_.tag == "Stone")
        {
            return;
        }

        if (Lv >= 4)
            return;

        gameObject.transform.parent.GetComponent<DropZone>().currentObj = null;
        RangeCircle.Instance.transform.SetParent(null);
        InfoWindow.Instance.CloseWindowBtn();
       

        Destroy(gameObject);

    }

    //3개버전
    public void LvUp()
    {

        if(Lv >= 3 && cha_prefab_.tag == "Stone")
        {
            return;
        }
       

        if (Lv >= 4)
            return;

        Lv++;
        cha_prefab_.transform.localScale = new Vector3(0.40f + 0.15f * Lv, 0.50f + 0.15f * Lv, 0.50f + 0.15f * Lv);
        //GetComponent<Draggable>().PlusCheck();
        StageManager.Instance.PlusCheck_DropZone(id, Lv);

        nameText.text = "" + "Lv" + Lv + "\n" + name_;

        GameObject effect = Instantiate((GameObject)Resources.Load("Prefab/렙업이팩트"));
        effect.transform.position = transform.position + new Vector3(0, 0.3f, 0);
        effect.transform.SetParent(transform);
        Destroy(effect, 1f);

        InfoWindow.Instance.CloseWindowBtn();

    }

    /* 두개버전
    public void LvUp()
    {
        Lv++;

        if (Lv == 2)
        {
            cha_prefab_.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            //GetComponent<Draggable>().PlusCheck();
            StageManager.Instance.PlusCheck_DropZone(id);
        }
        else
        {
            cha_prefab_.transform.localScale = new Vector3(1, 1, 1);
        }

        nameText.text = "" + "Lv" + Lv + "\n" + name_;

    }*/

    public Transform FindFirePos()
    {

        Transform firePos = cha_prefab_
            .transform.Find("RigPelvis").Find("RigSpine1").Find("RigSpine2")
            .Find("RigRibcage").Find("RigRArm1").Find("RigRArm2").Find("RigRArmPalm").transform;

        return firePos;
    }


    #region 스톤관련

    public void ShowStoneRange()
    {
        if (cha_prefab_.tag != "Stone")
            return;

        CloseStoneRange();
        stones[Lv-1].SetActive(true);

        for (int i = 0; i < stones[Lv - 1].transform.childCount; i++)
        {
            stones[Lv - 1].transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().enabled = true;
        }

        


    }

    public void CloseStoneRange()
    {
        if (cha_prefab_.tag != "Stone")
            return;

        for (int i = 0; i < stones.Length; i++)
        {
            for (int j = 0; j < stones[i].transform.childCount; j++)
            {
                stones[i].transform.GetChild(j).gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    #endregion


}
