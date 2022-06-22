using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterNumButton : MonoBehaviour
{
    // /////////////////////
    /* �V�[���I���̃{�^�� */
    // /////////////////////

    // �o�^����`���v�^�[�f�[�^
    [SerializeField] ChapterDataSO _so;
    // �\������e�L�X�g
    Text _tmp;

    void Start()
    {
        _tmp = transform.GetComponentInChildren<Text>();
        _tmp.text = $"��{_so.Num + 1}�b {_so.Title}";
    }

    void Update()
    {
        
    }
}
