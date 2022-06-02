using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChapterNumButton : MonoBehaviour
{
    // /////////////////////
    /* シーン選択のボタン */
    // /////////////////////

    // 登録するチャプターデータ
    [SerializeField] ChapterDataSO _so;
    // 表示するテキスト
    TextMeshProUGUI _tmp;

    void Start()
    {
        _tmp = transform.GetComponentInChildren<TextMeshProUGUI>();
        _tmp.text = $"第{_so.Num}話 {_so.Title}";
        //gameObject.SetActive(false);
    }

    void Update()
    {
        
    }
}
