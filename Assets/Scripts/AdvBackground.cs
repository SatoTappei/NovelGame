using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AdvBackground : MonoBehaviour
{
    // ///////////////////////////////////////
    /* �C�x���g�p�̘g�A�C�x���g�̏��������� */
    // ///////////////////////////////////////

    Image _img;

    void Awake()
    {
        _img = GetComponent<Image>();
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    // �w�i�ύX
    public void ChangeBackground(string name)
    {
        Sprite sprite = Resources.Load<Sprite>(name);
        _img.sprite = sprite;
    }

    // ��ʗh�炷
    public void ShakeScreen(string _)
    {
        transform.DOShakePosition(0.5f, 30.0f);
    }

    // ���炷
    public void PlaySound(string name)
    {
        if (name == "�~�߂�") SoundManager.instance.StopBGM();
        else SoundManager.instance.Play(name);
    }
}
