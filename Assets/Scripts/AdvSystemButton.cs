using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvSystemButton : MonoBehaviour
{
    // ///////////////////////////////////////
    /* アドベンチャーパートのシステムボタン */
    // ///////////////////////////////////////

    [SerializeField] Transform _borderTrans;

    Vector3 _defaultPos;
    Vector3 _borderDefPos;

    void Start()
    {
        _defaultPos = transform.position;
        _borderDefPos = _borderTrans.position;
    }

    void Update()
    {
        
    }

    // ボタンがポップする演出
    public void SetPosition(bool isPop)
    {
        Vector3 up = isPop ? Vector3.up * 0.1f : Vector3.zero;
        transform.position = _defaultPos + up;
        
        // 下の装飾は移動させない
        _borderTrans.position = _borderDefPos;
    }
}
