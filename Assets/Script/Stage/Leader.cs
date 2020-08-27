using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            StageManager.Instance.Hit();
            other.gameObject.GetComponent<Enemy>().Die();
         
        }
    }
}
