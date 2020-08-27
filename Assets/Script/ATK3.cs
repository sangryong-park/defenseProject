using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATK3 : MonoBehaviour
{
    public Transform target;
    public Vector3 lastTargetPos;

    public float speed = 10f;
    public GameObject impactEffect;
    public float dmg;
    public TrailRenderer TR;
    public float fatal;
    public string[] synergyNames;


    private void Awake()
    {
        if (gameObject.GetComponent<TrailRenderer>() != null)
            TR = GetComponent<TrailRenderer>();

    }

    public void Seek(Transform _target, Vector3 pos, float _atk, float _fatal, string[] synergyName)
    {

        this.target = _target;
        lastTargetPos = pos;
        dmg = _atk;
        fatal = _fatal;
        synergyNames = new string[3];
        synergyNames = synergyName;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {

            Vector3 dir = lastTargetPos - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                /*
                PoolManager.Instance.Destroy(gameObject);
                TR.Clear();
                return;
                */

                HitTarget();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);

            transform.LookAt(lastTargetPos);
        }
        else
        {
            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);

            transform.LookAt(target);

            lastTargetPos = target.position;
        }







    }

    public void HitTarget()
    {
        //맞는부분
     

        GameObject go1 = PoolManager.Instance.Instantiate("공격Prefab/마법폭발");
        go1.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        PoolManager.Instance.Destroy(go1, 0.5f);

        ATK1 attack = go1.GetComponent<ATK1>();
        if (attack != null)
        {
            //9치명타확률
            attack.Seek(dmg, fatal, synergyNames);

        }



        PoolManager.Instance.Destroy(gameObject);
        TR.Clear();


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (target == null)
            {
                //맞는부분
                GameObject go1 = PoolManager.Instance.Instantiate("공격Prefab/마법폭발");
                go1.transform.position = transform.position + new Vector3(0, 0.5f, 0);
                PoolManager.Instance.Destroy(go1, 0.5f);

                ATK1 attack = go1.GetComponent<ATK1>();
                if (attack != null)
                {
                    //9치명타확률
                    attack.Seek(dmg, fatal,synergyNames);

                }



                PoolManager.Instance.Destroy(gameObject);
                TR.Clear();



                Debug.Log("맞음");
            }




        }
        else
        {
            return;
        }

    }
}
