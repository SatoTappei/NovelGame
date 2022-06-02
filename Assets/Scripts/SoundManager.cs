using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // ///////////////////////
    /* �T�E���h�}�l�[�W���[ */
    // ///////////////////////

    static public SoundManager instance;

    // �Đ�����T�E���h�̃f�[�^
    [Serializable]
    public class SoundData
    {
        public string key;
        public AudioClip audioClip;
        public float playedTime;
    }

    // �Đ�����T�E���h�f�[�^����
    [SerializeField] SoundData[] _soundDatas;
    // ���ɉ����点��܂ł̊Ԋu
    [SerializeField] float _distance;
    //���������ɖ点��悤�ɂ���������Ă���
    AudioSource[] _audioSources = new AudioSource[15];
    // ������Ŗ炷�����w�肷�邽��
    Dictionary<string, SoundData> _soundDic = new Dictionary<string, SoundData>();

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // AudioSource����������t����
        for (int i = 0; i < _audioSources.Length; i++)
            _audioSources[i] = gameObject.AddComponent<AudioSource>();

        // ������Ŏw��ł���悤�ɒǉ�����
        foreach (SoundData data in _soundDatas)
            _soundDic.Add(data.key, data);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // �n���ꂽKey����T�E���h���Đ�
    public void Play(string key)
    {
        if(_soundDic.TryGetValue(key,out var data))
        {
            if (Time.realtimeSinceStartup - data.playedTime < _distance) return;
            data.playedTime = Time.realtimeSinceStartup;

            if (key.IndexOf("BGM_") >= 0)
                PlayBGM(data.audioClip);
            else 
                Play(data.audioClip);
        }
        else
        {
            Debug.LogWarning("�o�^����Ă��܂���: " + key);
        }
    }

    // �n���ꂽ�N���b�v���Đ�
    void Play(AudioClip clip)
    {
        AudioSource source = GetAudioSource();
        if(source == null)
        {
            Debug.LogWarning("����点�܂���ł����BAudioSource������܂���");
            return;
        }
        source.clip = clip;
        source.Play();
    }

    // �n���ꂽBGM�̃N���b�v���Đ�
    void PlayBGM(AudioClip clip)
    {
        AudioSource source = _audioSources[_audioSources.Length - 1];
        source.clip = clip;
        source.volume = 0.3f;
        source.loop = true;
    }

    // �Đ�����BGM���~�߂�
    public void StopBGM() { _audioSources[_audioSources.Length - 1].Stop(); }

    // ����炷AudioSource���擾
    AudioSource GetAudioSource()
    {
        // ��Ԍ���AudioSource��BGM�Đ��p�Ɏ���Ă���
        for(int i = 0; i < _audioSources.Length - 1; i++)
        {
            if (_audioSources[i].isPlaying == false)
                return _audioSources[i];
        }
        return null;
    }
}
