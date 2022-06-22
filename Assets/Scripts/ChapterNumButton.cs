using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterNumButton : MonoBehaviour
{
    // /////////////////////
    /* シーン選択のボタン */
    // /////////////////////

    // 登録するチャプターデータ
    [SerializeField] ChapterDataSO _so;
    // 表示するテキスト
    Text _tmp;

    void Start()
    {
        _tmp = transform.GetComponentInChildren<Text>();
        _tmp.text = $"第{_so.Num + 1}話 {_so.Title}";
    }

    void Update()
    {
        
    }
}
