using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATK1 : MonoBehaviour
{
    public float DMG;
    public float fatal;
    string[] synergyNames;


    public void Seek(float dmg ,float faTal,string[] synergyName)
    {
        DMG = dmg;
        fatal = faTal;
        synergyNames = new string[3];
        synergyNames = synergyName;
    }
        



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GameObject effectIns = PoolManager.Instance.Instantiate("공격Prefab/BulletImpactEffect");
            effectIns.transform.position = other.transform.position + new Vector3(0,0.3f,0);
            other.GetComponent<Enemy>().Hit(DMG, fatal, synergyNames);
            PoolManager.Instance.Destroy(effectIns, 0.15f);
        }
        else
        {
            return;
        }
    }

}
