using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{


    private Vector3 screenPoint;
    private Vector3 offset;

    public Draggable[] draggables;

    public BoxCollider myBox;
    public Vector3 startMousePos;
    public Vector3 currentMousePos;
 

    private void Start()
    {
        myBox = GetComponent<BoxCollider>();
    }
    // Start is called before the first frame update

    void OnMouseDown()
    {

        StageManager.Instance.CurrentUnitCheck();

        //정보창업데이트
        if (Input.mousePosition.x >= 540)
        {
            InfoWindow.Instance.window.GetComponent<RectTransform>().transform.localPosition = new Vector3(-330, -221.6f, 0);
            Debug.Log("" + Input.mousePosition.x);
        }
        else
        {
            InfoWindow.Instance.window.GetComponent<RectTransform>().transform.localPosition = new Vector3(330, -221.6f, 0);
            Debug.Log("" + Input.mousePosition.x);
        }



        InfoWindow.Instance.OpenWindow(gameObject.GetComponent<Character>().ATKrange_bonus,transform);
        InfoWindow.Instance.currentCharacter = gameObject;
        gameObject.GetComponent<Character>().SetInfoWindow();

        if (StageManager.Instance.isWaving && transform.parent.gameObject.tag == "UnitDropZone")
        {
            return;
        }

        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        //offset = gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        DragManager.Instance.startParent = transform.parent;

        draggables = FindObjectsOfType<Draggable>();

        for (int i = 0; i < draggables.Length; i++)
        {
            draggables[i].myBox.enabled = false;        
        }

        

        startMousePos = Input.mousePosition;

        if (transform.parent.GetComponent<DropZone>())
        {
            transform.parent.GetComponent<DropZone>().currentObj = null;
        }

    }

    void OnMouseDrag()
    {

      

        if(!DragManager.Instance.isDraging)
        StageManager.Instance.CurrentUnitCheck();
        //InfoWindow.Instance.window.SetActive(false);
        DragManager.Instance.isDraging = true;

        if(StageManager.Instance.isWaving && transform.parent.gameObject.tag == "UnitDropZone")
        {
            return;
        }

        Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
     
        // Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset*0.5f;
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint);
        transform.position = new Vector3(cursorPosition.x,1.5f, cursorPosition.z);
        DragManager.Instance.currentDragObject = gameObject;

        currentMousePos = Input.mousePosition;
        float dis = Vector3.Distance(startMousePos,currentMousePos);
        if( dis >= 100)
        {
            InfoWindow.Instance.CloseWindow();
        }

        if (transform.parent.GetComponent<DropZone>())
        {
            transform.parent.GetComponent<DropZone>().currentObj = null;
        }

        StoreManager.Instance.storeCards.SetActive(false);
        StoreManager.Instance.sellpriceText.gameObject.SetActive(true);
        StoreManager.Instance.sellpriceText.text = "판매 가격: " + 
            StoreManager.Instance.sellPrice[gameObject.GetComponent<Character>().Lv - 1]* gameObject.GetComponent<Character>().rating + "골드" ;

    }


    void OnMouseUp()
    {
        //원래내용

       

        if (StageManager.Instance.isWaving && transform.parent.gameObject.tag == "UnitDropZone")
        {
            return;
        }

        if (DragManager.Instance.overParent == DragManager.Instance.startParent || DragManager.Instance.overParent == null)
        {
            transform.position = DragManager.Instance.startParent.position + new Vector3(0, 1, 0);
        }
        else if (DragManager.Instance.overParent.GetComponent<DropZone>().currentObj == null)
        {

            if(StageManager.Instance.isWaving)
            {
                if (DragManager.Instance.overParent.gameObject.tag == "StoreDropZone")
                {
                    transform.parent.GetComponent<DropZone>().currentObj = null;
                    transform.SetParent(DragManager.Instance.overParent);
                    transform.position = DragManager.Instance.overParent.position + new Vector3(0, 1, 0);
                    transform.parent.GetComponent<DropZone>().currentObj = gameObject;

                }
                else
                {
                    transform.position = DragManager.Instance.startParent.position + new Vector3(0, 1, 0);

                }



            }
            else
            {

                if (StageManager.Instance.currentUintNum < StoreManager.Instance.uintMax)
                {

                    transform.parent.GetComponent<DropZone>().currentObj = null;
                    transform.SetParent(DragManager.Instance.overParent);
                    transform.position = DragManager.Instance.overParent.position + new Vector3(0, 1, 0);
                    transform.parent.GetComponent<DropZone>().currentObj = gameObject;
                }
                else
                {
                    if (DragManager.Instance.overParent.gameObject.tag == "StoreDropZone")
                    {
                        transform.parent.GetComponent<DropZone>().currentObj = null;
                        transform.SetParent(DragManager.Instance.overParent);
                        transform.position = DragManager.Instance.overParent.position + new Vector3(0, 1, 0);
                        transform.parent.GetComponent<DropZone>().currentObj = gameObject;

                    }
                    else
                    {
                        transform.position = DragManager.Instance.startParent.position + new Vector3(0, 1, 0);

                    }


                }


            }

           



        }
        else
        {

            if(StageManager.Instance.isWaving)
            {
                if (DragManager.Instance.overParent.gameObject.tag == "StoreDropZone")
                {
                    DragManager.Instance.overParent.GetComponent<DropZone>().currentObj.transform.SetParent(transform.parent);
                    DragManager.Instance.overParent.GetComponent<DropZone>().currentObj.transform.position =
                    transform.parent.transform.position + new Vector3(0, 1, 0);
                    transform.parent.GetComponent<DropZone>().currentObj =
                    DragManager.Instance.overParent.GetComponent<DropZone>().currentObj;
                    DragManager.Instance.overParent.GetComponent<DropZone>().currentObj.GetComponent<Draggable>().PlusCheck();

                    transform.SetParent(DragManager.Instance.overParent);
                    transform.position = DragManager.Instance.overParent.position + new Vector3(0, 1, 0);
                    transform.parent.GetComponent<DropZone>().currentObj = gameObject;

                }
                else
                {
                    transform.position = DragManager.Instance.startParent.position + new Vector3(0, 1, 0);

                }


            }
            else
            {
                DragManager.Instance.overParent.GetComponent<DropZone>().currentObj.transform.SetParent(transform.parent);
                DragManager.Instance.overParent.GetComponent<DropZone>().currentObj.transform.position =
                transform.parent.transform.position + new Vector3(0, 1, 0);
                transform.parent.GetComponent<DropZone>().currentObj =
                DragManager.Instance.overParent.GetComponent<DropZone>().currentObj;
                DragManager.Instance.overParent.GetComponent<DropZone>().currentObj.GetComponent<Draggable>().PlusCheck();

                transform.SetParent(DragManager.Instance.overParent);
                transform.position = DragManager.Instance.overParent.position + new Vector3(0, 1, 0);
                transform.parent.GetComponent<DropZone>().currentObj = gameObject;
            }

         

        }





        draggables = FindObjectsOfType<Draggable>();

        for (int i = 0; i < draggables.Length; i++)
        {
            draggables[i].myBox.enabled = true;
        }

        DragManager.Instance.currentDragObject = null;

        if (transform.parent.GetComponent<DropZone>())
        {
            transform.parent.GetComponent<DropZone>().currentObj = gameObject;
        }

        //PlusCheck();


        if (InfoWindow.Instance.window.activeSelf == false)
        {
            RangeCircle.Instance.GetComponent<LineRenderer>().enabled = false;
            gameObject.GetComponent<Character>().CloseStoneRange();
        }

        if (DragManager.Instance.isDraging)
        {
            SynergyManager.Instance.FindSynergy();
        }
        DragManager.Instance.isDraging = false;

        StageManager.Instance.CurrentUnitCheck();

        if (currentMousePos.y <= 400)
        {
            StageManager.Instance.currentGold += StoreManager.Instance.sellPrice[gameObject.GetComponent<Character>().Lv - 1]*gameObject.GetComponent<Character>().rating;
            gameObject.gameObject.transform.parent.GetComponent<DropZone>().currentObj = null;
            gameObject.transform.SetParent(null);
            RangeCircle.Instance.transform.SetParent(null);
            Destroy(gameObject);
            InfoWindow.Instance.CloseWindowBtn();
            
            SynergyManager.Instance.FindSynergy();
            StageManager.Instance.CurrentUnitCheck();


         

        }

        StoreManager.Instance.storeCards.SetActive(true);
        StoreManager.Instance.sellpriceText.gameObject.SetActive(false);

    }




    #region 웨이브시작시 마우스업
    public void WaveOnMouseUp()
    {
        //원래내용

        if (StageManager.Instance.isWaving && transform.parent.gameObject.tag == "UnitDropZone")
        {
            return;
        }

        if (DragManager.Instance.overParent == DragManager.Instance.startParent || DragManager.Instance.overParent == null)
        {
            transform.position = DragManager.Instance.startParent.position + new Vector3(0, 1, 0);
        }
        else if (DragManager.Instance.overParent.GetComponent<DropZone>().currentObj == null)
        {

            if (StageManager.Instance.isWaving)
            {
                if (DragManager.Instance.overParent.gameObject.tag == "StoreDropZone")
                {
                    transform.parent.GetComponent<DropZone>().currentObj = null;
                    transform.SetParent(DragManager.Instance.overParent);
                    transform.position = DragManager.Instance.overParent.position + new Vector3(0, 1, 0);
                    transform.parent.GetComponent<DropZone>().currentObj = gameObject;

                }
                else
                {
                    transform.position = DragManager.Instance.startParent.position + new Vector3(0, 1, 0);

                }



            }
            else
            {

                if (StageManager.Instance.currentUintNum < StoreManager.Instance.uintMax)
                {

                    transform.parent.GetComponent<DropZone>().currentObj = null;
                    transform.SetParent(DragManager.Instance.overParent);
                    transform.position = DragManager.Instance.overParent.position + new Vector3(0, 1, 0);
                    transform.parent.GetComponent<DropZone>().currentObj = gameObject;
                }
                else
                {
                    if (DragManager.Instance.overParent.gameObject.tag == "StoreDropZone")
                    {
                        transform.parent.GetComponent<DropZone>().currentObj = null;
                        transform.SetParent(DragManager.Instance.overParent);
                        transform.position = DragManager.Instance.overParent.position + new Vector3(0, 1, 0);
                        transform.parent.GetComponent<DropZone>().currentObj = gameObject;

                    }
                    else
                    {
                        transform.position = DragManager.Instance.startParent.position + new Vector3(0, 1, 0);

                    }


                }


            }





        }
        else
        {

            if (StageManager.Instance.isWaving)
            {
                if (DragManager.Instance.overParent.gameObject.tag == "StoreDropZone")
                {
                    DragManager.Instance.overParent.GetComponent<DropZone>().currentObj.transform.SetParent(transform.parent);
                    DragManager.Instance.overParent.GetComponent<DropZone>().currentObj.transform.position =
                    transform.parent.transform.position + new Vector3(0, 1, 0);
                    transform.parent.GetComponent<DropZone>().currentObj =
                    DragManager.Instance.overParent.GetComponent<DropZone>().currentObj;
                    DragManager.Instance.overParent.GetComponent<DropZone>().currentObj.GetComponent<Draggable>().PlusCheck();

                    transform.SetParent(DragManager.Instance.overParent);
                    transform.position = DragManager.Instance.overParent.position + new Vector3(0, 1, 0);
                    transform.parent.GetComponent<DropZone>().currentObj = gameObject;

                }
                else
                {
                    transform.position = DragManager.Instance.startParent.position + new Vector3(0, 1, 0);

                }


            }
            else
            {
                DragManager.Instance.overParent.GetComponent<DropZone>().currentObj.transform.SetParent(transform.parent);
                DragManager.Instance.overParent.GetComponent<DropZone>().currentObj.transform.position =
                transform.parent.transform.position + new Vector3(0, 1, 0);
                transform.parent.GetComponent<DropZone>().currentObj =
                DragManager.Instance.overParent.GetComponent<DropZone>().currentObj;
                DragManager.Instance.overParent.GetComponent<DropZone>().currentObj.GetComponent<Draggable>().PlusCheck();

                transform.SetParent(DragManager.Instance.overParent);
                transform.position = DragManager.Instance.overParent.position + new Vector3(0, 1, 0);
                transform.parent.GetComponent<DropZone>().currentObj = gameObject;
            }



        }





        draggables = FindObjectsOfType<Draggable>();

        for (int i = 0; i < draggables.Length; i++)
        {
            draggables[i].myBox.enabled = true;
        }

        DragManager.Instance.currentDragObject = null;

        if (transform.parent.GetComponent<DropZone>())
        {
            transform.parent.GetComponent<DropZone>().currentObj = gameObject;
        }

        //PlusCheck();


        if (InfoWindow.Instance.window.activeSelf == false)
        {
            RangeCircle.Instance.GetComponent<LineRenderer>().enabled = false;
            gameObject.GetComponent<Character>().CloseStoneRange();
        }

        if (DragManager.Instance.isDraging)
        {
            SynergyManager.Instance.FindSynergy();
        }
        DragManager.Instance.isDraging = false;

        StageManager.Instance.CurrentUnitCheck();

        StoreManager.Instance.storeCards.SetActive(true);
        StoreManager.Instance.sellpriceText.gameObject.SetActive(false);
    }
    #endregion
    //합치기 수정후 stageManager
    #region 합치기 수정전
    public void PlusCheck()
    {
        Debug.Log("합치기");
        //합치기
        if (transform.parent.tag == "StoreDropZone" || GetComponent<Character>().Lv >= 3)
        {
            return;
        }
        else
        {
            DropZone myDropZone = transform.parent.GetComponent<DropZone>();
             int myLv =  GetComponent<Character>().Lv;
            int myId = GetComponent<Character>().id;



            //위쪽일경우
            if (myDropZone.NeerInfo("위",myId,myLv))
            {
                if (myDropZone.neerUp.GetComponent<DropZone>().NeerInfo("위", myId, myLv))
                {
                    GetComponent<Character>().DestroyLv();
                    myDropZone.neerUp.transform.GetChild(1).GetComponent<Character>().LvUp();
                    myDropZone.neerUp.GetComponent<DropZone>().neerUp.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                }else
                if (myDropZone.neerUp.GetComponent<DropZone>().NeerInfo("왼", myId, myLv))
                {
                    GetComponent<Character>().DestroyLv();
                    myDropZone.neerUp.transform.GetChild(1).GetComponent<Character>().LvUp();
                    myDropZone.neerUp.GetComponent<DropZone>().neerLeft.transform.GetChild(1).GetComponent<Character>().DestroyLv();

                }
                else
                if (myDropZone.neerUp.GetComponent<DropZone>().NeerInfo("오", myId, myLv))
                {
                    GetComponent<Character>().DestroyLv();
                    myDropZone.neerUp.transform.GetChild(1).GetComponent<Character>().LvUp();
                    myDropZone.neerUp.GetComponent<DropZone>().neerRight.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                }
                else
                if (myDropZone.NeerInfo("왼", myId, myLv))
                {
                    GetComponent<Character>().LvUp();
                    myDropZone.neerUp.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                    myDropZone.neerLeft.transform.GetChild(1).GetComponent<Character>().DestroyLv();

                }
                else
                if (myDropZone.NeerInfo("오", myId, myLv))
                {
                    GetComponent<Character>().LvUp();
                    myDropZone.neerUp.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                    myDropZone.neerRight.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                }
                else
                if (myDropZone.NeerInfo("아래", myId, myLv))
                {
                    GetComponent<Character>().LvUp();
                    myDropZone.neerUp.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                    myDropZone.neerDown.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                }
            } //왼쪽
            else if (myDropZone.NeerInfo("왼", myId, myLv))
            {
                if (myDropZone.neerLeft.GetComponent<DropZone>().NeerInfo("왼", myId, myLv))
                {
                    GetComponent<Character>().DestroyLv();
                    myDropZone.neerLeft.transform.GetChild(1).GetComponent<Character>().LvUp();
                    myDropZone.neerLeft.GetComponent<DropZone>().neerLeft.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                }
                else
                if (myDropZone.neerLeft.GetComponent<DropZone>().NeerInfo("위", myId, myLv))
                {
                    GetComponent<Character>().DestroyLv();
                    myDropZone.neerLeft.transform.GetChild(1).GetComponent<Character>().LvUp();
                    myDropZone.neerLeft.GetComponent<DropZone>().neerUp.transform.GetChild(1).GetComponent<Character>().DestroyLv();

                }
                else
                if (myDropZone.neerLeft.GetComponent<DropZone>().NeerInfo("아래", myId, myLv))
                {
                    GetComponent<Character>().DestroyLv();
                    myDropZone.neerLeft.transform.GetChild(1).GetComponent<Character>().LvUp();
                    myDropZone.neerLeft.GetComponent<DropZone>().neerDown.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                }
                else
                if (myDropZone.NeerInfo("위", myId, myLv))
                {
                    GetComponent<Character>().LvUp();
                    myDropZone.neerUp.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                    myDropZone.neerLeft.transform.GetChild(1).GetComponent<Character>().DestroyLv();

                }
                else
                if (myDropZone.NeerInfo("아래", myId, myLv))
                {
                    GetComponent<Character>().LvUp();
                    myDropZone.neerDown.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                    myDropZone.neerLeft.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                }
                else
                if (myDropZone.NeerInfo("오", myId, myLv))
                {
                    GetComponent<Character>().LvUp();
                    myDropZone.neerLeft.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                    myDropZone.neerRight.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                }
            }
            //아래쪽
            else if (myDropZone.NeerInfo("아래", myId, myLv))
            {
                if (myDropZone.neerDown.GetComponent<DropZone>().NeerInfo("아래", myId, myLv))
                {
                    GetComponent<Character>().DestroyLv();
                    myDropZone.neerDown.transform.GetChild(1).GetComponent<Character>().LvUp();
                    myDropZone.neerDown.GetComponent<DropZone>().neerDown.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                }
                else
                if (myDropZone.neerDown.GetComponent<DropZone>().NeerInfo("왼", myId, myLv))
                {
                    GetComponent<Character>().DestroyLv();
                    myDropZone.neerDown.transform.GetChild(1).GetComponent<Character>().LvUp();
                    myDropZone.neerDown.GetComponent<DropZone>().neerLeft.transform.GetChild(1).GetComponent<Character>().DestroyLv();

                }
                else
                if (myDropZone.neerDown.GetComponent<DropZone>().NeerInfo("오", myId, myLv))
                {
                    GetComponent<Character>().DestroyLv();
                    myDropZone.neerDown.transform.GetChild(1).GetComponent<Character>().LvUp();
                    myDropZone.neerDown.GetComponent<DropZone>().neerRight.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                }
                else
                if (myDropZone.NeerInfo("왼", myId, myLv))
                {
                    GetComponent<Character>().LvUp();
                    myDropZone.neerDown.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                    myDropZone.neerLeft.transform.GetChild(1).GetComponent<Character>().DestroyLv();

                }
                else
                if (myDropZone.NeerInfo("오", myId, myLv))
                {
                    GetComponent<Character>().LvUp();
                    myDropZone.neerDown.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                    myDropZone.neerRight.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                }
                else
                if (myDropZone.NeerInfo("위", myId, myLv))
                {
                    GetComponent<Character>().LvUp();
                    myDropZone.neerDown.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                    myDropZone.neerUp.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                }
            }  //오른쪽
            else if (myDropZone.NeerInfo("오", myId, myLv))
            {
                if (myDropZone.neerRight.GetComponent<DropZone>().NeerInfo("오", myId, myLv))
                {
                    GetComponent<Character>().DestroyLv();
                    myDropZone.neerRight.transform.GetChild(1).GetComponent<Character>().LvUp();
                    myDropZone.neerRight.GetComponent<DropZone>().neerRight.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                }
                else
                if (myDropZone.neerRight.GetComponent<DropZone>().NeerInfo("위", myId, myLv))
                {
                    GetComponent<Character>().DestroyLv();
                    myDropZone.neerRight.transform.GetChild(1).GetComponent<Character>().LvUp();
                    myDropZone.neerRight.GetComponent<DropZone>().neerUp.transform.GetChild(1).GetComponent<Character>().DestroyLv();

                }
                else
                if (myDropZone.neerRight.GetComponent<DropZone>().NeerInfo("아래", myId, myLv))
                {
                    GetComponent<Character>().DestroyLv();
                    myDropZone.neerRight.transform.GetChild(1).GetComponent<Character>().LvUp();
                    myDropZone.neerRight.GetComponent<DropZone>().neerDown.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                }
                else
                if (myDropZone.NeerInfo("위", myId, myLv))
                {
                    GetComponent<Character>().LvUp();
                    myDropZone.neerUp.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                    myDropZone.neerRight.transform.GetChild(1).GetComponent<Character>().DestroyLv();

                }
                else
                if (myDropZone.NeerInfo("아래", myId, myLv))
                {
                    GetComponent<Character>().LvUp();
                    myDropZone.neerDown.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                    myDropZone.neerRight.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                }
                else
                if (myDropZone.NeerInfo("왼", myId, myLv))
                {
                    GetComponent<Character>().LvUp();
                    myDropZone.neerLeft.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                    myDropZone.neerRight.transform.GetChild(1).GetComponent<Character>().DestroyLv();
                }
            }




        }
    }

    #endregion

}





