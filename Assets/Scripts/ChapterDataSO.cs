using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ChapterDataSO : ScriptableObject
{
    // /////////////////////
    /* �`���v�^�[�̃f�[�^ */
    // /////////////////////

    [Header("�b�ԍ�")]
    [SerializeField] int num;
    [Header("�^�C�g��")]
    [SerializeField] string title;
    [Header("�䎌���������e�L�X�g")]
    [SerializeField] TextAsset textAsset;

    public int Num { get => num; }
    public string Title { get => title; }
    public TextAsset TextAsset { get => textAsset; }
}
