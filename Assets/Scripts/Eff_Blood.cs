using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eff_Blood : MonoBehaviour
{
    // /////////////////
    /* 血のエフェクト */
    // /////////////////

    void Start()
    {
        StartCoroutine(Enumerator());
    }

    void Update()
    {
        
    }

    IEnumerator Enumerator()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
