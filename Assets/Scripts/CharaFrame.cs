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
        _img.sprite = Resources.Load<Sprite>(name);
        _img.color = new Color32(255, 255, 255, 255);
    }

    // �����G�폜
    public void DeleteSprite(string _)
    {
        _img.sprite = null;
        _img.color = new Color32(255, 255, 255, 0);
    }

    // �����G������
    public void LowerSprite(string _)
    {

        _img.color = new Color32(100, 100, 100, 255);
    }

    // �����G���˂�
    public void JumpSprite(string _)
    {
        transform.DOPunchPosition(Vector3.up * 30.0f, 0.5f);
    }

    // �����G�h�炷
    public void ShakeSprite(string _)
    {
        transform.DOShakePosition(0.5f, 30.0f);
    }

    // �G�t�F�N�g����
    public void GenerateEffect(string name)
    {
        GameObject eff = Resources.Load<GameObject>(name);
        Vector3 pos = transform.position;
        Instantiate(eff, new Vector3(pos.x + 2.5f, pos.y + 7.8f, 10), Quaternion.identity);
    }
}
