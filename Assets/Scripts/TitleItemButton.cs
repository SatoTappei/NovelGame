using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TitleItemButton : MonoBehaviour
{
    // /////////////////////////
    /* �^�C�g���̊e�R���e���c�{�^���̋��� */
    // /////////////////////////

    [SerializeField] Color32 _idleColor;
    [SerializeField] Color32 _PopColor;
    EventTrigger _et;
    TextMeshProUGUI _tmp;

    void Awake()
    {
        _et = GetComponent<EventTrigger>();
        _tmp = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        //_tmp.color = _idleColor; // TitleSceneManager�̌�ɌĂ΂��ƐF���N���b�N�\���̐F�ɖ߂��Ă��܂��s�
    }

    void Update()
    {

    }

    // �T�C�Y��ς��邾���Ȃ̂ŃA�j���[�V�������g��Ȃ��ŃX�P�[����ύX����
    public void SetScale(string trigger)
    {
        if (trigger == "Enter")
        {
            _tmp.color = _PopColor;
            gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1);
            SoundManager.instance.Play("SE_�J�[�\���I��");
        }
        else if (trigger == "Exit")
        {
            _tmp.color = _idleColor;
            gameObject.transform.localScale = Vector3.one;
        }
        else if (trigger == "Down")
        {
            gameObject.transform.localScale = new Vector3(0.9f, 0.9f, 1);
        }
        else if (trigger == "Up" && gameObject.transform.localScale != Vector3.one)
        {
            gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1);
        }
    }

    // �{�^���Ƃ��ċ@�\�����邩�ǂ����؂�ւ���
    public void SetInteractable(bool b)
    {
        byte a = (byte)(b == true ? 255 : 127);
        TextMeshProUGUI tmp = GetComponent<TextMeshProUGUI>();
        Color32 c = tmp.color;
        tmp.color = new Color32(c.r, c.g, c.b, a);
        _et.enabled = b;
    }
}
