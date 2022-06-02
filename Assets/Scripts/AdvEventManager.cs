using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AdvEventManager : MonoBehaviour
{
    // /////////////////////////////////////
    /* �A�h�x���`���[�p�[�g�̃C�x���g���� */
    // /////////////////////////////////////

    // ���o�p�̃N���X
    [Serializable]
    public class EventData
    {
        public string key;
        public StrEvent method;
    }
    // UnityEvent��Invoke���������n�����߂�UnityEvent���p�������N���X
    [Serializable]
    public class StrEvent : UnityEvent<string> { }

    // �C�x���g���ŌĂяo���鉉�o
    [SerializeField] EventData[] _eventsDatas;
    Dictionary<string, StrEvent> _eventDic = new Dictionary<string, StrEvent>();

    void Awake()
    {
        // �Ăяo����悤��Dictionary�^�Ɋi�[
        foreach (EventData ed in _eventsDatas)
            _eventDic.Add(ed.key, ed.method);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //�C�x���g�̎��s
    public void RunEvent(string eventStr)
    {
        // ������𕪉����ăC�x���g���ɕ�����
        string[] eventNames = eventStr.Split(';');

        foreach(string name in eventNames)
        {
            // �C�x���g����1�����ȏ�Ȃ�
            if(name.Length >= 1)
            {
                // ! <- Dictionary�^�̃L�[�ƁA�Ή����郁�\�b�h�ɓn�������𕪂���L��
                // �C�x���g����!������΃L�[�ƈ����ɕ�����
                string key = name.IndexOf('!') >= 0 ? name.Substring(0, name.IndexOf('!')) : name;
                string arg = name.IndexOf('!') >= 0 ? name.Substring(name.IndexOf('!') + 1) : "";

                _eventDic.TryGetValue(key, out StrEvent strEvent);
                if (strEvent == null) Debug.LogWarning("�C�x���g��������܂���: " + key);
                else strEvent.Invoke(arg);
            }
        }
    }
}
