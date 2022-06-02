using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChapterNumButton : MonoBehaviour
{
    // /////////////////////
    /* �V�[���I���̃{�^�� */
    // /////////////////////

    // �o�^����`���v�^�[�f�[�^
    [SerializeField] ChapterDataSO _so;
    // �\������e�L�X�g
    TextMeshProUGUI _tmp;

    void Start()
    {
        _tmp = transform.GetComponentInChildren<TextMeshProUGUI>();
        _tmp.text = $"��{_so.Num}�b {_so.Title}";
        //gameObject.SetActive(false);
    }

    void Update()
    {
        
    }
}
