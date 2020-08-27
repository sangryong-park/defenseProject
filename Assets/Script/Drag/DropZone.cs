using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    public MeshRenderer MR;
    public GameObject currentObj;

    public GameObject neerUp;
    public GameObject neerDown;
    public GameObject neerLeft;
    public GameObject neerRight;

   

    private void Start()
    {
        Invoke("ExitRigidbody", 1.0f);
     //   Debug.Log("" + transform.position);
    }

    private void OnMouseOver()
    {
        if (DragManager.Instance.isDraging)
        {
            MR.material.color = Color.green;
            DragManager.Instance.overParent = transform;
        }     
    }


    private void OnMouseExit()
    {

        MR.material.color = Color.white;

        StageUIManager.Instance.debugText.text = "current Exit : " + gameObject.name;
        DragManager.Instance.overParent = null;

    }


   
    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.transform.localPosition == (transform.localPosition + new Vector3(0,0,1)))
        {
            neerUp = collision.gameObject;
        }
        if (collision.gameObject.transform.localPosition == (transform.localPosition + new Vector3(0, 0, -1)))
        {
            neerDown = collision.gameObject;
        }
        if (collision.gameObject.transform.localPosition == (transform.localPosition + new Vector3(-1, 0, 0)))
        {
            neerLeft = collision.gameObject;
        }
        if (collision.gameObject.transform.localPosition == (transform.localPosition + new Vector3(1, 0, 0)))
        {
            neerRight = collision.gameObject;
        }

      //  Debug.Log("접촉 " + gameObject.name + " 가 " + gameObject.transform.localPosition +
      //      " 좌표인데 "  + collision.gameObject.name + " 얘랑 얘위치는 " + collision.gameObject.transform.localPosition);
    }

    public void ExitRigidbody()
    {
        Rigidbody tmp = GetComponent<Rigidbody>();
        Destroy(tmp);
    }

    /*
    public int NeerInfo(string dir)
    {
       
        int num = 0;
        switch (dir)
        {
            
            case "위" :
                if (neerUp == null ||  neerUp.transform.childCount == 1)
                    return 0;
                num = neerUp.transform.GetChild(1).GetComponent<Character>().id + neerUp.transform.GetChild(1).GetComponent<Character>().Lv;
                break;
            case "아래":
                if (neerDown == null || neerDown.transform.childCount == 1)
                    return 0;
                num = neerDown.transform.GetChild(1).GetComponent<Character>().id + neerDown.transform.GetChild(1).GetComponent<Character>().Lv;
                break;
            case "왼":
                if (neerLeft == null || neerLeft.transform.childCount == 1)
                    return 0;
                num = neerLeft.transform.GetChild(1).GetComponent<Character>().id + neerLeft.transform.GetChild(1).GetComponent<Character>().Lv;
                break;
            case "오":
                if (neerRight == null || neerRight.transform.childCount == 1)
                    return 0;
                num = neerRight.transform.GetChild(1).GetComponent<Character>().id + neerRight.transform.GetChild(1).GetComponent<Character>().Lv;
                break;
        }
        return num;
    }*/

    public bool NeerInfo(string dir , int myId , int myLv)
    {

        bool num = false;
        switch (dir)
        {

            case "위":
                if (neerUp == null || neerUp.transform.childCount == 1)
                    return false;

                if (neerUp.transform.GetChild(1).GetComponent<Character>().id == myId && neerUp.transform.GetChild(1).GetComponent<Character>().Lv == myLv)
                    num = true;
                break;
            case "아래":
                if (neerDown == null || neerDown.transform.childCount == 1)
                    return false;


                if (neerDown.transform.GetChild(1).GetComponent<Character>().id == myId && neerDown.transform.GetChild(1).GetComponent<Character>().Lv == myLv)
                    num = true;
                break;
            case "왼":
                if (neerLeft == null || neerLeft.transform.childCount == 1)
                    return false;


                if (neerLeft.transform.GetChild(1).GetComponent<Character>().id == myId && neerLeft.transform.GetChild(1).GetComponent<Character>().Lv == myLv)
                    num = true;
                break;
            case "오":
                if (neerRight == null || neerRight.transform.childCount == 1)
                    return false;


                if (neerRight.transform.GetChild(1).GetComponent<Character>().id == myId && neerRight.transform.GetChild(1).GetComponent<Character>().Lv == myLv)
                    num = true;
                break;
        }
        return num;
    }





}
