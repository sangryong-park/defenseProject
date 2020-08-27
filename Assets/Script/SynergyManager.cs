using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SynergyManager : MonoBehaviour
{

    static SynergyManager instance;
    public static SynergyManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SynergyManager>();
            }

            return instance;
        }

    }

    public string[] synerrgyInfos;

    public string[] synergy_names;
    public int[] synergy_nums;
    public int[] synergy_bonus;



    public GameObject[] synergyWindow;
    public GameObject[] synergyWindow_image;
    public GameObject[] synergyWindow_NameText;
    public GameObject[] synergyWindow_NumText;
    

    public GameObject pos1;
    public DropZone[] dropZones;
    public List<int> idList;

    struct SynergyOrder : IComparable<SynergyOrder>
    {
        public int id;

        public int order;

        public int CompareTo(SynergyOrder obj)
        {
            if (id == obj.id)
                return 0;
            return id > obj.id ? 1 : -1;
        }
    }
    void Awake()
    {
        synergy_nums = new int[10];
        synergy_bonus = new int[10];
        dropZones = new DropZone[25];


       

    }


    // Start is called before the first frame update
    void Start()
    {
        idList = new List<int>();
        for (int i = 0; i < 25; i++)
        {
            dropZones[i] = pos1.transform.GetChild(i).gameObject.GetComponent<DropZone>();

        }


        synergyWindow_image = new GameObject[synergyWindow.Length];
        synergyWindow_NameText = new GameObject[synergyWindow.Length];
        synergyWindow_NumText = new GameObject[synergyWindow.Length];


        for (int i = 0; i < synergyWindow.Length; i++)
        {
            synergyWindow_image[i] = synergyWindow[i].transform.GetChild(1).gameObject;
            synergyWindow_NameText[i] = synergyWindow[i].transform.GetChild(2).gameObject;
            synergyWindow_NumText[i] = synergyWindow[i].transform.GetChild(3).gameObject;
           
        }
        startUI();

        FindSynergy();

   
    }

    /*
    void Update()
    {
        
    }
    */
    public void FindSynergy()
    {
        idList.Clear();
        for (int i = 0; i < dropZones.Length; i++)
        {
            if(dropZones[i].currentObj != null)
            {
                idList.Add(dropZones[i].currentObj.GetComponent<Character>().id);
            }         
        }

        idList = idList.Distinct<int>().ToList<int>();

        foreach (var item in idList)
        {
            Debug.Log("" + item );
        }

        CurrentSynergyUpdate();
    }

    public void CurrentSynergyUpdate()
    {
        resetNum();
        foreach (int item in idList)
        {

            string[] _synergy = new string[3];
            _synergy[0] = InfoDatabase.infoList[item -1].synergy1;
            _synergy[1] = InfoDatabase.infoList[item -1].synergy2;
            _synergy[2] = InfoDatabase.infoList[item -1].synergy3;

            Debug.Log("" + _synergy[0] + _synergy[1] + _synergy[2]);
            for (int i = 0; i < 3; i++)
            {
                switch (_synergy[i])
                {
                    case "광전사":
                        synergy_nums[0]++;
            
                        break;
                    case "빙결":
                        synergy_nums[1]++;
             
                        break;                  
                    case "마법사":
                        synergy_nums[2]++;
                 
                        break;
                    case "기사":
                        synergy_nums[3]++;
                
                        break;
                    case "저격수":
                        synergy_nums[4]++;
               
                        break;
                    case "왕족":
                        synergy_nums[5]++;
                  
                        break;
                    case "도적":
                        synergy_nums[6]++;
               
                        break;
                    case "농부":
                        synergy_nums[7]++;
            
                        break;
                    case "귀족":
                        synergy_nums[8]++;
                 
                        break;
                    case "부족":
                        synergy_nums[9]++;
              
                        break;          
               
                    default:
                        break;
                }
            }
           
        }

        SynergyUiUpdate();
    }
    
    public void SynergyUiUpdate()
    {


        PriorityQueue<SynergyOrder> q = new PriorityQueue<SynergyOrder>();
        for (int i = 0; i < synergy_nums.Length; i++)
        {


            q.Push(new SynergyOrder() { id = synergy_nums[i] , order = i});
            if (synergy_nums[i] > 0)
            {
                synergyWindow[i].SetActive(true);
                synergyWindow_NumText[i].GetComponent<Text>().text = "" + synergy_nums[i];
                /*
                int num =  Mathf.Max(synergy_nums);


                if (synergy_nums[i] == num)
                {
                    synergyWindow[i].transform.SetAsFirstSibling();
                }    */              
              
            }
            else
            {
                synergyWindow[i].SetActive(false);
            }

            
        }

        for (int i = 0; i < synergy_nums.Length; i++)
        {
            synergyWindow[q.Pop().order].transform.SetAsLastSibling();
        }


        SetBonus();


    }



    public void resetNum()
    {
        for (int i = 0; i < synergy_nums.Length; i++)
        {
            synergy_nums[i] = 0;
        }


    }
    public void startUI()
    {
        for (int i = 0; i < synergyWindow_image.Length; i++)
        {
            synergyWindow_image[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("시너지이미지/" + synergy_names[i]);
            synergyWindow_NameText[i].GetComponent<Text>().text = "" + synergy_names[i];
        }
        
       // synergyWindow_NameText[i]
    }


    public void SetBonus()
    {

        for (int i = 0; i < 10; i++)
        {
            if (i == 5 || i == 8)
                continue;

            if (synergy_nums[i] < 6 && synergy_nums[i] >= 4)
            {
                synergy_bonus[i] = 2;
                synergyWindow_NumText[i].GetComponent<Text>().color = Color.blue;
            }
            else if (synergy_nums[i] < 4 && synergy_nums[i] >= 2)
            {
                synergy_bonus[i] = 1;
                synergyWindow_NumText[i].GetComponent<Text>().color = Color.yellow;
            }
            else if (synergy_nums[i] < 2 && synergy_nums[i] >= 0)
            {
                synergy_bonus[i] = 0;
                synergyWindow_NumText[i].GetComponent<Text>().color = Color.white;
            }
            else
            {
                synergy_bonus[i] = 3;
                synergyWindow_NumText[i].GetComponent<Text>().color = Color.red;
            }
        }

   
        if (synergy_nums[8] < 3 && synergy_nums[8] >= 2)
        {
            synergy_bonus[8] = 2;
            synergyWindow_NumText[8].GetComponent<Text>().color = Color.blue;
        }
        else if (synergy_nums[8] < 2 && synergy_nums[8] >= 1)
        {
            synergy_bonus[8] = 1;
            synergyWindow_NumText[8].GetComponent<Text>().color = Color.yellow;
        }
        else if (synergy_nums[8] < 1 && synergy_nums[8] >= 0)
        {
            synergy_bonus[8] = 0;
            synergyWindow_NumText[8].GetComponent<Text>().color = Color.white;
        }
        else
        {
            synergy_bonus[8] = 3;
            synergyWindow_NumText[8].GetComponent<Text>().color = Color.red;
        }

      
        if (synergy_nums[5] >= 2)
        {
            synergy_bonus[5] = 1;
            synergyWindow_NumText[5].GetComponent<Text>().color = Color.yellow;
        }else
        {
            synergy_bonus[5] = 0;
        }
       
      
    }

}





