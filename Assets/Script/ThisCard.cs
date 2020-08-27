using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThisCard : MonoBehaviour
{

    public List<InfoBase> thisCard = new List<InfoBase>();

    

    public Text nameText;
    public Text costText;
    public Image CardImage;
    public Image CardColor;

    public Image[] synergy_images;


    public Text[] synergy_texts;
    


    private int cardId;

    public int id;

    public string name_;


    public int rating;
    public int cost;

    public int Cost
    {
        get
        {
           // return cost * 10 - (1 * SynergyManager.Instance.synergy_bonus[4]);
            return cost * 1;
        }
    }


    public Sprite thisImage;

    public string[] synergy_names;

    private void Start()
    {
        synergy_names = new string[3];
        CardColor = GetComponent<Image>();
        NewCard();
     
    }

    public void NewCard()
    {
        //id = Random.Range(0, 41);

        int num = Random.Range(0, 101);

       // Debug.Log(""+ num);
        if(num < StoreManager.Instance.card4Percentage)
        {
            id = Random.Range(29, 34);
        }
        else if(num < StoreManager.Instance.card4Percentage + StoreManager.Instance.card3Percentage)
        {
            id = Random.Range(21, 29);
        }
        else if (num < StoreManager.Instance.card4Percentage + StoreManager.Instance.card3Percentage + StoreManager.Instance.card2Percentage)
        {
            id = Random.Range(11, 21);
        }
        else if (num < StoreManager.Instance.card4Percentage + StoreManager.Instance.card3Percentage + StoreManager.Instance.card2Percentage + StoreManager.Instance.card1Percentage)
        {
            id = Random.Range(1, 11);
        }
        Debug.Log(id);

        // id = Random.Range(1, 41);

        foreach (InfoBase item in InfoDatabase.infoList)
        {
            if (id == item.id)
            {
                cardId = InfoDatabase.infoList.IndexOf(item);

            }
        }

        thisCard[0] = InfoDatabase.infoList[cardId];


        id = thisCard[0].id;
        name_ = thisCard[0].name_;
        rating = thisCard[0].rating;

        cost = thisCard[0].rating;
        thisImage = thisCard[0].thisImage;
        synergy_names[0] = thisCard[0].synergy1;
        synergy_names[1] = thisCard[0].synergy2;
        synergy_names[2] = thisCard[0].synergy3;

        nameText.text = "" + name_;
        costText.text = "" + Cost;
        CardImage.sprite = thisImage;

        SetSynergyImage();
        SetColor();


       
    }

    public void Buy_Card()
    {

        if(StageManager.Instance.currentGold >= Cost)
        {
            
            if(StageManager.Instance.PlusCheck_buy(id))
            {
                gameObject.SetActive(false);
                StageManager.Instance.currentGold -= Cost;
            }
            else
            {
                for (int i = 0; i < StoreManager.Instance.storeDropZones.Length; i++)
                {
                    if (StoreManager.Instance.storeDropZones[i].GetComponent<DropZone>().currentObj == null)
                    {

                        GameObject clone = Instantiate(Resources.Load("캐릭터PreFab/1")) as GameObject;
                        clone.transform.SetParent(StoreManager.Instance.storeDropZones[i].transform);
                        clone.transform.position = clone.transform.parent.transform.position + new Vector3(0, 1, 0);
                        clone.GetComponent<Character>().SetStart(id);
                        StoreManager.Instance.storeDropZones[i].GetComponent<DropZone>().currentObj = clone;
                        gameObject.SetActive(false);
                        StageManager.Instance.currentGold -= Cost;

                        break;
                    }

                }
            }



            StageManager.Instance.CurrentUnitCheck();
        }
 
    }

    public void SetSynergyImage()
    {

        for (int i = 0; i < 3; i++)
        {
            synergy_images[i].gameObject.SetActive(true);
            synergy_texts[i].gameObject.SetActive(true);
            if(synergy_names[i] != "")
            {
                synergy_images[i].sprite = Resources.Load<Sprite>("시너지이미지/" + synergy_names[i]);
            }else
            {
                synergy_images[i].gameObject.SetActive(false);
                synergy_texts[i].gameObject.SetActive(false);
            }
            
            synergy_texts[i].text = "" + synergy_names[i];
        }

    



    }

    public void SetColor()
    {
        switch (cost)
        {
            case 1:
                CardColor.color = Color.white;
                break;
            case 2:
                CardColor.color = new Color(0.5f,1,0.5f);
                break;
            case 3:
                CardColor.color = new Color(0.5f, 0.5f, 1);
                break;
            case 4:
                CardColor.color = new Color(1, 0.5f, 0.5f);
                break;
            default:
                break;
        }
    }



    #region 합치기 수정후
    public void PlusCheck()
    {

    }
    #endregion

}





