using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AdvEventManager : MonoBehaviour
{
    // /////////////////////////////////////
    /* アドベンチャーパートのイベント処理 */
    // /////////////////////////////////////

    // 演出用のクラス
    [Serializable]
    public class EventData
    {
        public string key;
        public StrEvent method;
    }
    // UnityEventのInvokeから引数を渡すためにUnityEventを継承したクラス
    [Serializable]
    public class StrEvent : UnityEvent<string> { }

    // イベント名で呼び出せる演出
    [SerializeField] EventData[] _eventsDatas;
    Dictionary<string, StrEvent> _eventDic = new Dictionary<string, StrEvent>();

    void Awake()
    {
        // 呼び出せるようにDictionary型に格納
        foreach (EventData ed in _eventsDatas)
            _eventDic.Add(ed.key, ed.method);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //イベントの実行
    public void RunEvent(string eventStr)
    {
        // 文字列を分解してイベント名に分ける
        string[] eventNames = eventStr.Split(';');

        foreach(string name in eventNames)
        {
            // イベント名が1文字以上なら
            if(name.Length >= 1)
            {
                // ! <- Dictionary型のキーと、対応するメソッドに渡す引数を分ける記号
                // イベント名に!があればキーと引数に分ける
                string key = name.IndexOf('!') >= 0 ? name.Substring(0, name.IndexOf('!')) : name;
                string arg = name.IndexOf('!') >= 0 ? name.Substring(name.IndexOf('!') + 1) : "";

                _eventDic.TryGetValue(key, out StrEvent strEvent);
                if (strEvent == null) Debug.LogWarning("イベントが見つかりません: " + key);
                else strEvent.Invoke(arg);
            }
        }
    }
}
