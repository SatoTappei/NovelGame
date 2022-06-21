using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvSystemButton : MonoBehaviour
{
    // ///////////////////////////////////////
    /* �A�h�x���`���[�p�[�g�̃V�X�e���{�^�� */
    // ///////////////////////////////////////

    [SerializeField] Transform _borderTrans;

    Vector3 _defaultPos;
    Vector3 _borderDefPos;

    void Start()
    {
        _defaultPos = transform.position;
        _borderDefPos = _borderTrans.position;
    }

    void Update()
    {
        
    }

    // �{�^�����|�b�v���鉉�o
    public void SetPosition(bool isPop)
    {
        Vector3 up = isPop ? Vector3.up * 0.1f : Vector3.zero;
        transform.position = _defaultPos + up;
        
        // ���̑����͈ړ������Ȃ�
        _borderTrans.position = _borderDefPos;
    }
}
