using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    // /////////////////////////
    /* エフェクトを自動で破壊 */
    // /////////////////////////

    // 寿命
    [SerializeField] float _lifeTime;

    void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    void Update()
    {

    }
}
