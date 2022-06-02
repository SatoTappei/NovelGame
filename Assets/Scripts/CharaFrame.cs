using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharaFrame : MonoBehaviour
{
    // ///////////////////////////////////////
    /* イベント用の枠、イベントの処理を持つ */
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

    // 立ち絵表示
    public void DispSprite(string name)
    {

        //img.sprite = Resources.Load<Sprite>(name);
        //img.color = new Color32(255, 255, 255, 255);
    }

    // 立ち絵削除
    public void DeleteSprite(string _)
    {

        //img.sprite = null;
        //img.color = new Color32(255, 255, 255, 0);
    }

    // 立ち絵下げる
    public void LowerSprite(string _)
    {

        //img.color = new Color32(100, 100, 100, 255);
    }

    // 立ち絵揺らす
    public void ShakeSprite(string _)
    {

    }

    // 画面揺らす
    public void ShakeScreen(string _)
    {

    }

    // エフェクト生成
    public void GenerateEffect(string name)
    {

    }

    // 背景変更
    public void ChangeBackground(string name)
    {

    }
}
