using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleSceneManager : MonoBehaviour
{
    // ///////////////////////////
    /* タイトル画面のメイン処理 */
    // ///////////////////////////

    [SerializeField] FadeManager fadeManager;
    // ボタンをクリックしたときにポップするパネルのアニメーション
    [SerializeField] Animator _popPanelAnim;
    // ボタンをクリックしたときにポップするパネルの背景
    [SerializeField] GameObject _popPanelBack;
    // 確認画面のアニメーション
    [SerializeField] Animator _confirmPopAnim;
    // 確認画面の背景
    [SerializeField] GameObject _confirmPopBack;
    // 確認画面で表示されるテキスト
    [SerializeField] TextMeshProUGUI _confirmPopText;
    // "つづきから"のボタンのコンポーネント
    [SerializeField] TitleItemButton _continueButton;
    // シーン選択のボタンのコンポーネント
    [SerializeField] TitleItemButton _sceneSelectButton;
    // シーン選択の各シーンを選択するボタンの親
    [SerializeField] GameObject _sceneSelectItem;
    // 設定の各オブジェクトの親
    [SerializeField] GameObject _settingItem;
    // チャプターボタンクリックでここに保存、再生しますか？ -> YESでGameManagerに渡す
    int _chapterNumBuffer;

    void Awake()
    {
        // セーブデータを読み込む
        GameManager.instance.Load(); // ビルド後に上手く動作しないので一時的に消している
        //Debug.Log($"セーブデータをロード クリア{GameManager.instance._Flag.clear} リード{GameManager.instance._Flag.read}");
    }

    void Start()
    {
        fadeManager.FadeIn();
        Init();
    }

    void Update()
    {
        
    }

    // 初期化
    public void Init()
    {
        //セーブデータがない場合は"つづきから"とシーン選択をグレーアウト
        if (!GameManager.instance._ExistSaveData)
        {
            _continueButton.SetInteractable(false);
            _sceneSelectButton.SetInteractable(false);
        }

        _sceneSelectItem.SetActive(false);
        _settingItem.SetActive(false);
        _popPanelBack.SetActive(false);
        _confirmPopBack.SetActive(false);
    }

    // "はじめから"をクリックした際の処理
    public void ClickedBeginingButton()
    {
        StartCoroutine(enumerator());

        IEnumerator enumerator()
        {
            _popPanelBack.SetActive(true);
            GameManager.instance._Flag.read = 0;
            GameManager.instance._IsStoryMode = true;
            SoundManager.instance.Play("SE_決定");
            yield return new WaitForSeconds(0.5f);
            fadeManager.FadeOut("AdvScene");
        }
    }

    // "つづきから"をクリックした際の処理
    public void ClickedContinueButton()
    {
        StartCoroutine(enumerator());

        IEnumerator enumerator()
        {
            _popPanelBack.SetActive(true);
            GameManager.instance._IsStoryMode = true;
            SoundManager.instance.Play("SE_決定");
            yield return new WaitForSeconds(0.5f);
            fadeManager.FadeOut("AdvScene");
        }
    }

    // "シーン選択"をクリックした際の処理
    public void ClickedSceneSelectButton()
    {
        ActiveItemPanel(true);
        ActiveItems(dispItem: _sceneSelectItem, hideItems: _settingItem);

        // 進行状況に応じてボタンを表示
        for (int i = 0; i < _sceneSelectItem.transform.childCount; i++)
        {
            GameObject child = _sceneSelectItem.transform.GetChild(i).gameObject;
            child.SetActive(i <= GameManager.instance._Flag.clear ? true : false);
        }
    }

    // "設定"をクリックした際の処理
    public void ClickedSettingButton()
    {
        ActiveItemPanel(true);
        ActiveItems(dispItem: _settingItem, hideItems: _sceneSelectItem);
    }

    // "ゲーム終了"をクリックした際の処理
    public void ClickedExitButton()
    {
        _popPanelBack.SetActive(true);
        // TODO: クリックした後に他の場所をクリックさせないために透明の背景を出す
        // TODO: SEの再生
        // TODO: SEが終わったらゲーム終了
    }

    // シーン選択画面でチャプター番号をクリックした際の処理
    public void ClickedChapterNumButton(ChapterDataSO so)
    {
        ActiveConfirmPop(true);
        _chapterNumBuffer = so.Num;
        _confirmPopText.text = $"{so.Title}\nを再生しますか？";
    }

    // 1つのオブジェクトのみ表示して残りを非表示にする
    void ActiveItems(GameObject dispItem, params GameObject[] hideItems)
    {
        dispItem.SetActive(true);
        foreach (GameObject go in hideItems)
            go.SetActive(false);
    }

    // チャプターセレクトと設定のパネルの表示を切り替え
    void ActiveItemPanel(bool isPop) => Active(isPop, _popPanelAnim, _popPanelBack);

    // 確認用のポップの表示を切り替え
    public void ActiveConfirmPop(bool isPop) => Active(isPop, _confirmPopAnim, _confirmPopBack);

    // 表示の切り替え
    void Active(bool isPop,Animator anim,GameObject back)
    {
        string key = isPop == true ? "SE_決定2" : "SE_キャンセル";
        string trigger = isPop == true ? "Pop" : "Close";
        SoundManager.instance.Play(key);
        anim.SetTrigger(trigger);
        back.SetActive(isPop);
    }

    // チャプターセレクトから指定したチャプターを再生
    public void PlaySelectedChapter()
    {
        StartCoroutine(enumerator());

        IEnumerator enumerator()
        {
            GameManager.instance._Flag.read = _chapterNumBuffer;
            GameManager.instance._IsStoryMode = false;
            SoundManager.instance.Play("SE_決定");
            yield return new WaitForSeconds(0.5f);
            fadeManager.FadeOut("AdvScene");
        }
    }
}
