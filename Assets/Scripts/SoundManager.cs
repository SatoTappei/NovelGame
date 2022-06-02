using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // ///////////////////////
    /* サウンドマネージャー */
    // ///////////////////////

    static public SoundManager instance;

    // 再生するサウンドのデータ
    [Serializable]
    public class SoundData
    {
        public string key;
        public AudioClip audioClip;
        public float playedTime;
    }

    // 再生するサウンドデータたち
    [SerializeField] SoundData[] _soundDatas;
    // 次に音が鳴らせるまでの間隔
    [SerializeField] float _distance;
    //複数同時に鳴らせるようにたくさんつけておく
    AudioSource[] _audioSources = new AudioSource[15];
    // 文字列で鳴らす音を指定するため
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

        // AudioSourceをたくさん付ける
        for (int i = 0; i < _audioSources.Length; i++)
            _audioSources[i] = gameObject.AddComponent<AudioSource>();

        // 文字列で指定できるように追加する
        foreach (SoundData data in _soundDatas)
            _soundDic.Add(data.key, data);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // 渡されたKeyからサウンドを再生
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
            Debug.LogWarning("登録されていません: " + key);
        }
    }

    // 渡されたクリップを再生
    void Play(AudioClip clip)
    {
        AudioSource source = GetAudioSource();
        if(source == null)
        {
            Debug.LogWarning("音を鳴らせませんでした。AudioSourceが足りません");
            return;
        }
        source.clip = clip;
        source.Play();
    }

    // 渡されたBGMのクリップを再生
    void PlayBGM(AudioClip clip)
    {
        AudioSource source = _audioSources[_audioSources.Length - 1];
        source.clip = clip;
        source.volume = 0.3f;
        source.loop = true;
    }

    // 再生中のBGMを止める
    public void StopBGM() { _audioSources[_audioSources.Length - 1].Stop(); }

    // 音を鳴らすAudioSourceを取得
    AudioSource GetAudioSource()
    {
        // 一番後ろのAudioSourceはBGM再生用に取っておく
        for(int i = 0; i < _audioSources.Length - 1; i++)
        {
            if (_audioSources[i].isPlaying == false)
                return _audioSources[i];
        }
        return null;
    }
}
