using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharaFrame : MonoBehaviour
{
    // ///////////////////////////////////////
    /* �C�x���g�p�̘g�A�C�x���g�̏��������� */
    // ///////////////////////////////////////

    Image _img;
    Vector3 _defaultPos;

    void Awake()
    {
        _img = GetComponent<Image>();
        _defaultPos = transform.localPosition;
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    // �����G�\��
    public void DispSprite(string name)
    {

        //img.sprite = Resources.Load<Sprite>(name);
        //img.color = new Color32(255, 255, 255, 255);
    }

    // �����G�폜
    public void DeleteSprite(string _)
    {

        //img.sprite = null;
        //img.color = new Color32(255, 255, 255, 0);
    }

    // �����G������
    public void LowerSprite(string _)
    {

        //img.color = new Color32(100, 100, 100, 255);
    }

    // �����G�h�炷
    public void ShakeSprite(string _)
    {

    }

    // ��ʗh�炷
    public void ShakeScreen(string _)
    {

    }

    // �G�t�F�N�g����
    public void GenerateEffect(string name)
    {

    }

    // �w�i�ύX
    public void ChangeBackground(string name)
    {

    }
}
