using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AdvBackground : MonoBehaviour
{
    // ///////////////////////////////////////
    /* イベント用の枠、イベントの処理を持つ */
    // ///////////////////////////////////////

    Image _img;

    void Awake()
    {
        _img = GetComponent<Image>();
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    // 背景変更
    public void ChangeBackground(string name)
    {
        Sprite sprite = Resources.Load<Sprite>(name);
        _img.sprite = sprite;
    }

    // 画面揺らす
    public void ShakeScreen(string _)
    {
        transform.DOShakePosition(0.5f, 30.0f);
    }

    // 音鳴らす
    public void PlaySound(string name)
    {
        if (name == "止める") SoundManager.instance.StopBGM();
        else SoundManager.instance.Play(name);
    }
}
