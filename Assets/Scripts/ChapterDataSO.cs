using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ChapterDataSO : ScriptableObject
{
    // /////////////////////
    /* チャプターのデータ */
    // /////////////////////

    [Header("話番号")]
    [SerializeField] int num;
    [Header("タイトル")]
    [SerializeField] string title;
    [Header("台詞を書いたテキスト")]
    [SerializeField] TextAsset textAsset;

    public int Num { get => num; }
    public string Title { get => title; }
    public TextAsset TextAsset { get => textAsset; }
}
