using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    // /////////////////////////
    /* �G�t�F�N�g�������Ŕj�� */
    // /////////////////////////

    // ����
    [SerializeField] float _lifeTime;

    void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    void Update()
    {

    }
}
