using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    static DragManager instance;
    public static DragManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DragManager>();
            }

            return instance;
        }

    }

    public bool isDraging;

    public Transform startParent;
    public Transform overParent;

    public GameObject currentDragObject;



}
