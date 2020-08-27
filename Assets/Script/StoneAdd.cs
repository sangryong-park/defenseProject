using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneAdd : MonoBehaviour
{

  

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.GetComponent<Character>())
        {
            return;
        }

        if (other.gameObject.GetComponent<Character>().cha_prefab_.tag == "Stone")
        {
            return;
        }

        other.gameObject.GetComponent<Character>().stonenum = 0.1f;


        /*
        if(DragManager.Instance.currentDragObject != null)
        {
            if(DragManager.Instance.currentDragObject == transform.parent.parent.gameObject)
            {
                return;
            }
        }

        */
        


        if (other.gameObject.tag == "Character" && transform.parent.parent.GetComponent<Character>().cha_prefab_.tag == "Stone")
        {

            if (transform.parent.parent.GetComponent<Character>().id == 26)
            {
                if(other.gameObject.GetComponent<Character>().stoneAtk <= transform.parent.parent.GetComponent<Character>().ATK_bonus)
                other.gameObject.GetComponent<Character>().stoneAtk = transform.parent.parent.GetComponent<Character>().ATK_bonus;   
            }

            if (transform.parent.parent.GetComponent<Character>().id == 27)
            {
                if (other.gameObject.GetComponent<Character>().stoneSpeed <= transform.parent.parent.GetComponent<Character>().ATKspeed_Bonus)
                    other.gameObject.GetComponent<Character>().stoneSpeed = transform.parent.parent.GetComponent<Character>().ATKspeed_Bonus;
            }

            if (transform.parent.parent.GetComponent<Character>().id == 28)
            {
                if (other.gameObject.GetComponent<Character>().stoneFatal <= transform.parent.parent.GetComponent<Character>().Fatal_bonus)
                    other.gameObject.GetComponent<Character>().stoneFatal = transform.parent.parent.GetComponent<Character>().Fatal_bonus;
            }

        }
    }

    /*
    private void OnTriggerExit(Collider other)
    {

        if (!other.gameObject.GetComponent<Character>())
        {
            return;
        }

        if (other.gameObject.GetComponent<Character>().cha_prefab_.tag == "Stone")
        {
            return;
        }

        if (other.gameObject.tag == "Character" && transform.parent.parent.GetComponent<Character>().cha_prefab_.tag == "Stone")
        {

            if (transform.parent.parent.GetComponent<Character>().id == 26)
            {

                other.gameObject.GetComponent<Character>().stoneAtk = 0;
            }

            if (transform.parent.parent.GetComponent<Character>().id == 27)
            {
                other.gameObject.GetComponent<Character>().stoneSpeed = 0;
            }

            if (transform.parent.parent.GetComponent<Character>().id == 28)
            {
                other.gameObject.GetComponent<Character>().stoneFatal = 0;
            }

        }
    }*/

    

}
