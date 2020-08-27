using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class InfoBase
{
    public int id;

    public string name_;

    public int rating;

    public string synergy1;

    public string synergy2;

    public string synergy3;

    public string synergy4;

    public float ATK;

    public float ATKspeed;

    public float fatal;

    public float ATKrange;

    public int ATKtype;

    public float sp;

    public float magicATK;

    public string skill_info;

    public Sprite thisImage;

    public InfoBase(int Id, string Name, int Rating)
    {
        id = Id;
        name_ = Name;
        rating = Rating;
    }

    public InfoBase(int Id, string Name, int Rating, string Synergy1, string Synergy2, string Synergy3, string Synergy4, int atk, int AtkSpeed, int Fatal, int ATKRange, int ATKType, int Sp, int MagicATK, string Skill_info, Sprite ThisImage)
    {
        id = Id;
        name_ = Name;
        rating = Rating;
        synergy1 = Synergy1;
        synergy2 = Synergy2;
        synergy3 = Synergy3;
        synergy4 = Synergy4;
        ATK = atk;
        ATKspeed = AtkSpeed;
        fatal = Fatal;
        ATKrange = ATKRange;
        ATKtype = ATKType;
        sp = Sp;
        magicATK = MagicATK;
        skill_info = Skill_info;
        thisImage = ThisImage;

    }

}