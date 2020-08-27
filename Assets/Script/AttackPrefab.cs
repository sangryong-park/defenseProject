using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPrefab : MonoBehaviour
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
        if(gameObject.GetComponent<TrailRenderer>() != null)
        TR = GetComponent<TrailRenderer>();
     
    }

    public void Seek(Transform _target, Vector3 pos , float _atk , float _fatal , string[] synergyName)
    {
        synergyNames = new string[3];
        synergyNames = synergyName;
        this.target = _target;
        lastTargetPos = pos;
        dmg = _atk;
        fatal = _fatal;
       
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {

            Vector3 dir = lastTargetPos - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                PoolManager.Instance.Destroy(gameObject);
                TR.Clear();
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

        GameObject effectIns = PoolManager.Instance.Instantiate("공격Prefab/BulletImpactEffect");
        effectIns.transform.position = target.transform.position + new Vector3(0, 0.3f, 0);
        target.GetComponent<Enemy>().Hit(dmg , fatal, synergyNames);
        PoolManager.Instance.Destroy(effectIns, 0.15f);
       


        PoolManager.Instance.Destroy(gameObject);
        TR.Clear();


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if(target == null)
            {

                GameObject effectIns = PoolManager.Instance.Instantiate("공격Prefab/BulletImpactEffect");
                effectIns.transform.position = target.transform.position + new Vector3(0, 0.3f, 0);
                target.GetComponent<Enemy>().Hit(dmg, fatal,synergyNames);
                PoolManager.Instance.Destroy(effectIns, 0.15f);


                PoolManager.Instance.Destroy(gameObject);
                TR.Clear();



                Debug.Log("맞음");
            }
               

           
       
        }else
        {
            return;
        }
        
    }


}
