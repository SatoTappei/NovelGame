using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    // /////////////////////
    /* ゲームマネージャー */
    // /////////////////////

    // セーブデータとして扱うクラス
    public class ChapterFlag
    {
        public int clear; // クリア済みチャプター
        public int read;  // 最後に読み込んだチャプター
    }

    public static GameManager instance;

    // 画面クリック時に発生させるエフェクト
    [SerializeField] GameObject _clickEff;
    // 最後の話番号 <- 第n話という使い方
    [SerializeField] int _maxChapterNum;
    // シーン切り替え時に次のシーンに進むかどうか <- シーン選択の場合はfalse
    public bool _IsStoryMode { get; set; }
    // セーブデータがあるか
    public bool _ExistSaveData { get; private set; }
    // セーブデータのインスタンス
    ChapterFlag _flag = new ChapterFlag();

    public int _MaxChapterNum { get => _maxChapterNum; }
    public ChapterFlag _Flag { get => _flag; private set => _flag = value; }

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
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
            PlayClickEff();
    }

    // 画面上をクリックした際のエフェクトを生成
    public void PlayClickEff()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(_clickEff, new Vector3(pos.x, pos.y, 10f), Quaternion.identity);
    }

    // データをセーブ
    public void Save()
    {
        using (StreamWriter sw = new StreamWriter(Application.dataPath + "/saveData.json", append: false))
        {
            string data = JsonUtility.ToJson(_Flag);
            sw.Write(data);
            sw.Flush();
            sw.Close();
        }
    }

    // データをロード
    public void Load()
    {
        try
        {
            // 各フラグをリセット
            _Flag.clear = 0;
            _Flag.read = 0;

            using (StreamReader sr = new StreamReader(Application.dataPath + "/saveData.json"))
            {
                string data = sr.ReadLine();
                sr.Close();
                _Flag = JsonUtility.FromJson<ChapterFlag>(data);
                _ExistSaveData = true;
            }
        }
        catch(FileNotFoundException fnfe)
        {
            Debug.LogWarning("セーブデータが見つかりませんでした: " + fnfe);
            _ExistSaveData = false;
        }
    }
}
