using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeShell : MonoBehaviour
{
    bool cool = true;
    int damage;
    Coroutine dot = null;
    LayerMask targetLayer;

    private void Awake()
    {
        damage = transform.GetComponentInParent<FoxSubSkill>().damage;
        targetLayer = (1 << LayerMask.NameToLayer("Enemy")) | (1 << LayerMask.NameToLayer("Enemy"));
    }

    private void OnDisable()
    {
        if(dot != null)
        {
            StopCoroutine(dot);
        }        
    }

    private void OnTriggerStay(Collider other)
    {

        if((targetLayer | (1<<other.gameObject.layer)) != targetLayer && cool)
        {
            if(other.TryGetComponent<IHitable>(out IHitable hitable))
            {
                hitable.Hit(damage);
                cool = false;

                if(dot == null)
                {
                    dot = StartCoroutine(DoT());
                }
            }
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(DoT());
    }

    IEnumerator DoT()
    {
        yield return new WaitForSeconds(1f);
        cool = true;
        dot = null;
    }
}
