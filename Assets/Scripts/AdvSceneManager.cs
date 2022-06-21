using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class AdvSceneManager : MonoBehaviour
{
    // ///////////////////////////////////
    /* アドベンチャーパートのメイン処理 */
    // ///////////////////////////////////

    [SerializeField] AdvEventManager advEventManager;
    [SerializeField] AdvSceneUI advSceneUI;
    [SerializeField] FadeManager fadeManager;
    // 名前枠
    [SerializeField] Text _nameText;
    // 台詞枠
    [SerializeField] Text _lineText;
    // 1文字を表示する感覚
    [SerializeField] float _distance;
    // 再生するチャプターのデータ
    ChapterDataSO _chapterData;
    // 表示する文字数が更新された時間
    float _lastTypeTime;
    // 現在の台詞番号
    int _lineNum;
    // 表示する文字数
    int _charaCount;
    // 現在表示される台詞全文
    string _currentLine;
    // オートモードか
    bool _isAuto;
    // 台詞枠が隠れているか
    bool _isHide;
    // フェードアウト中か <- 連続クリックで複数回次のシーンへの移動処理が呼ばれないようにする
    bool _isFading; 
    // キャラ名、起こるイベント、台詞をそれぞれ格納する
    List<string> _names = new List<string>();
    List<string> _events = new List<string>();
    List<string> _lines = new List<string>();

    void Start()
    {
        fadeManager.FadeIn();
        Init();
        Advance();
        //Debug.Log("最高到達点:チャプター" + GameManager.instance._Flag._clear);
        //Debug.Log("現在読み込んでいる:チャプター" + GameManager.instance._Flag._read);
    }

    void Update()
    {
        // オートモード、台詞が全部表示されている、台詞が最後まで表示されてからn秒後
        if (_isAuto && _charaCount == _currentLine.Length && 
            Time.realtimeSinceStartup - _lastTypeTime > 1.0f)
        {

            if (_lineNum == _names.Count)
            {
                // 最後の台詞だった場合はシーン移行の処理
                MoveScene();
            }
            else
            {
                // それ以外の場合は次の台詞へ
                Advance();
            }
        }

        // 枠が隠れていない、マウスが台詞枠をクリックした場合
        if (!_isHide && Input.GetMouseButtonDown(0) && HitLineFrame())
        {
            // オートモード中なら解除
            if (_isAuto) _isAuto = false;

            // 台詞が全部表示されていれば
            if (_charaCount == _currentLine.Length)
            {
                if(_lineNum == _names.Count)
                {
                    // 最後の台詞だった場合はシーン移行の処理
                    MoveScene();
                }
                else
                {
                    // それ以外の場合は次の台詞へ
                    Advance();
                }
            }
            else
            {
                // 台詞が表示されていなければ"表示する文字数のカウント"を最大にする <- この後に呼ばれる処理でテキスト自体を更新するという点に注意
                _charaCount = _currentLine.Length;
            }
        }
        // 台詞を描画
        TypingLine();

        // 現在の台詞が全て表示し終わっていたらピリオドアイコンを表示する
        if (_lineText.text.Length == _currentLine.Length &&
            !advSceneUI._PeriodIcon)
            advSceneUI._PeriodIcon = true;
    }

    // 初期化
    void Init()
    {
        // チャプターデータをロードしてテキストを分割
        string name = $"Chapter{GameManager.instance._Flag.read}";
        _chapterData = LoadChapterData(name);
        SplitText(_chapterData.TextAsset);

        // タイトルをセット
        advSceneUI._TitlePopText = _chapterData.Title;
    }

    // 現在のシーンで読み込むチャプターデータをロード
    ChapterDataSO LoadChapterData(string name)
    {
        ChapterDataSO so = Resources.Load(name, typeof(ChapterDataSO)) as ChapterDataSO;
        if (!so) Debug.LogWarning("ChapterDataが見つかりませんでした: " + name);
        return so;
    }

    // テキストを分割してリストに格納する
    void SplitText(TextAsset asset)
    {
        string[] strs = asset.text.Split(',');

        foreach (string s in strs)
        {
            if (s.IndexOf("@name") >= 0) _names.Add(s.Remove(0, 8));
            else if (s.IndexOf("@event") >= 0) _events.Add(s.Remove(0, 9));
            else if (s.IndexOf("@line") >= 0) _lines.Add(s.Remove(0, 8));
        }
    }

    // 次の台詞に進む
    void Advance()
    {
        // ピリオドアイコンを消す
        advSceneUI._PeriodIcon = false;

        // イベントの実行と、今から表示する台詞のセット
        advEventManager.RunEvent(_events[_lineNum]);
        SetNextLine();

        // 台詞番号がシーンの台詞数を超えないようにClampで整える
        _lineNum++;
        _lineNum = Mathf.Clamp(_lineNum, 0, _names.Count);
    }

    // 次の台詞をセット
    void SetNextLine()
    {
        _lineText.text = "";
        _nameText.text = _names[_lineNum];
        _currentLine = _lines[_lineNum];

        //バックログに台詞を追加
        advSceneUI.AddBackLog(_names[_lineNum], _lines[_lineNum]);

        // 表示される文字数をリセット
        _charaCount = 0;
    }

    // 表示する台詞の文字を増やす
    void TypingLine()
    {
        // 前の文字を描画してから一定の間隔が空いていない場合、もしくは"全部のテキストが表示されている場合"はreturnする
        if (Time.realtimeSinceStartup - _lastTypeTime < _distance ||
            _lineText.text.Length == _currentLine.Length)
            return;
        _lastTypeTime = Time.realtimeSinceStartup;

        // 表示する文字数が台詞の文字数を超えないようにClampで整える
        _charaCount++;
        _charaCount = Mathf.Clamp(_charaCount, 0, _currentLine.Length);
        _lineText.text = _currentLine.Substring(0, _charaCount);
    }

    // 台詞枠をクリックしたかどうか
    bool HitLineFrame()
    {
        PointerEventData p = new PointerEventData(EventSystem.current);
        List<RaycastResult> rr = new List<RaycastResult>();
        p.position = Input.mousePosition;
        EventSystem.current.RaycastAll(p, rr);

        // ボタンをクリックした場合はそっちの処理をさせたいのでfalseを返す
        foreach (RaycastResult r in rr)
            if (r.gameObject.tag == "LineSystemButton")
                return false;
        return true;
    }

    // 台詞枠の表示を切り替え
    public void SwitchHideFrame(bool isHide)
    {
        // 台詞枠が表示されている時に呼ばれた場合はreturnする
        if (!_isHide && isHide == false) return;

        SoundManager.instance.Play("SE_決定2");
        advSceneUI._LineItem = !isHide;
        _isHide = isHide;
        // 台詞枠を操作するとオートモード解除
        _isAuto = false;
    }

    // スキップのポップアップの表示を切り替え
    public void SwitchSkipPop(bool isPop) => Active(isPop, advSceneUI.ActiveSkipPop);

    // バックログの表示を切り替え
    public void SwitchBackLog(bool isPop) => Active(isPop, advSceneUI.ActiveBackLog);

    // タイトルに戻るのポップアップの表示を切り替え
    public void SwitchReturnTitlePop(bool isPop) => Active(isPop, advSceneUI.ActiveReturnTitlePop);

    // 表示の切り替え
    void Active(bool isPop, UnityAction<bool> func)
    {
        string key = isPop == true ? "SE_決定2" : "SE_キャンセル";
        SoundManager.instance.Play(key);
        func.Invoke(isPop);
        // オートモードを解除
        _isAuto = false;
    }

    // オートモード切り替え
    public void SwitchAutoMode()
    {
        SoundManager.instance.Play("SE_決定2");
        _isAuto = !_isAuto;
    }

    // スキップ機能
    public void SkipScene()
    {
        SoundManager.instance.Play("SE_決定2");
        MoveScene();
    }

    // タイトルに戻る
    public void ReturnTitleScene()
    {
        SoundManager.instance.Play("SE_決定2");
        fadeManager.FadeOut("TitleScene");
    }

    // セーブボタン
    public void ClickedSaveButton()
    {
        SoundManager.instance.Play("SE_セーブ");
        GameManager.instance.Save();
    }

    // シーン移動
    public void MoveScene()
    {
        if (_isFading) return;
        _isFading = true;

        // 2回以上参照するので変数に入れておく
        int clear = GameManager.instance._Flag.clear;
        int max = GameManager.instance._MaxChapterNum;

        // "はじめから"もしくは"つづきから"かつ、次のチャプターがあれば
        if (GameManager.instance._IsStoryMode &&
            GameManager.instance._Flag.read < max)
        {
            clear++;
            GameManager.instance._Flag.clear = Mathf.Clamp(clear, 0, max);
            GameManager.instance._Flag.read++;
            fadeManager.FadeOut("AdvScene");
        }
        else
        {
            // 全クリしたらセーブしてタイトルに戻る
            GameManager.instance.Save();
            fadeManager.FadeOut("TitleScene");
        }
    }
}
