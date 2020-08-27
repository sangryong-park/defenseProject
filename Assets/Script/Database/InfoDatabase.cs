using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoDatabase : MonoBehaviour
{

    static InfoDatabase instance;
    public static InfoDatabase Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InfoDatabase>();
            }

            return instance;
        }

    }

    public static List<InfoBase> infoList = new List<InfoBase>();



    private void Awake()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("데이터테이블");



        // public InfoBase(int Id, string Name, int Rating, string Synergy1, string Synergy2, string Synergy3, string Synergy4, float atk, float AtkSpeed, float Fatal, float ATKRange, float Sp, float MagicATK, string Skill_info, Sprite ThisImage)
        
        for (var i = 0; i < 33; i++)
        {
             Sprite myImage = Resources.Load<Sprite>("캐릭터카드이미지/" + data[i]["id"]);
        
            // Debug.Log("index " + (i).ToString() + " : " + data[i]["name"] + " " + data[i]["rating"] + " " + data[i]["synergy1"]);

            
            infoList.Add(new InfoBase((int)data[i]["id"], (string)data[i]["name"], (int)data[i]["rating"], (string)data[i]["synergy1"],
                (string)data[i]["synergy2"], (string)data[i]["synergy3"], (string)data[i]["synergy4"],(int)data[i]["ATK"],
                (int)data[i]["ATKspeed"], (int)data[i]["fatal"], (int)data[i]["ATKrange"], (int)data[i]["ATKtype"], (int)data[i]["sp"],
                1,(string)data[i]["skill_info"], myImage));

          //  Debug.Log(""+i);

        }
        
        


    }
}
