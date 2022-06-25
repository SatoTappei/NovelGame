using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] Text _confirmPopText;
    // "つづきから"のボタンのコンポーネント
    [SerializeField] TitleItemButton _continueButton;
    // シーン選択のボタンのコンポーネント
    [SerializeField] TitleItemButton _sceneSelectButton;
    // シーン選択のオブジェクトの親
    [SerializeField] GameObject _sceneSelectItem;
    // シーン選択のチャプター番号ボタンの親
    [SerializeField] Transform _chapterNumButton;
    // "おまけ"ボタンのコンポーネント
    [SerializeField] TitleItemButton _omakeButton;
    // おまけの各オブジェクトの親
    [SerializeField] GameObject _omakeItem;
    // 各コンテンツのボタンのアニメ
    [SerializeField] Animator _beginingButtonAnim;
    [SerializeField] Animator _continueButtonAnim;
    [SerializeField] Animator _sceneSelectButtonAnim;
    [SerializeField] Animator _omakeButtonAnim;
    [SerializeField] Animator _exitButtonAnim;
    // チャプターボタンクリックでここに保存、再生しますか？ -> YESでGameManagerに渡す
    int _chapterNumBuffer;

    void Awake()
    {
        // セーブデータを読み込む
        
        //Debug.Log($"セーブデータをロード クリア{GameManager.instance._Flag.clear} リード{GameManager.instance._Flag.read}");
    }

    void Start()
    {
        GameManager.instance.Load(); // ビルド後に上手く動作しないので一時的に消している
        fadeManager.FadeIn();
        Init();
    }

    void Update()
    {
        
    }

    // 初期化
    public void Init()
    {
        bool isNoSaveData = !GameManager.instance._ExistSaveData;

        //セーブデータがない場合は"つづきから"とシーン選択をグレーアウト
        if (isNoSaveData)
        {
            _continueButton.SetInteractable(false);
            _sceneSelectButton.SetInteractable(false);
        }

        _sceneSelectItem.SetActive(false);
        _omakeItem.SetActive(false);
        _popPanelBack.SetActive(false);
        _confirmPopBack.SetActive(false);

        SoundManager.instance.Play("BGM_タイトル");

        StartCoroutine(Enumerator());

        IEnumerator Enumerator()
        {
            _beginingButtonAnim.SetTrigger("FadeIn");
            yield return new WaitForSeconds(0.1f);
            _continueButtonAnim.SetTrigger(isNoSaveData ? "Gray" : "FadeIn");
            yield return new WaitForSeconds(0.1f);
            _sceneSelectButtonAnim.SetTrigger(isNoSaveData ? "Gray" : "FadeIn");
            yield return new WaitForSeconds(0.1f);
            _omakeButtonAnim.SetTrigger("FadeIn");
            yield return new WaitForSeconds(0.1f);
            _exitButtonAnim.SetTrigger("FadeIn");
        }
    }

    // "はじめから"をクリックした際の処理
    public void ClickedBeginingButton()
    {
        StartCoroutine(Enumerator());

        IEnumerator Enumerator()
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
        StartCoroutine(Enumerator());

        IEnumerator Enumerator()
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
        ActiveItems(dispItem: _sceneSelectItem, hideItems: _omakeItem);

        // 進行状況に応じてボタンを表示
        for (int i = 0; i < _chapterNumButton.childCount; i++)
        {
            GameObject child = _chapterNumButton.GetChild(i).gameObject;
            child.SetActive(i <= GameManager.instance._Flag.clear ? true : false);
        }
    }

    // "おまけ"をクリックした際の処理
    public void ClickedOmakeButton()
    {
        ActiveItemPanel(true);
        ActiveItems(dispItem: _omakeItem, hideItems: _sceneSelectItem);
    }

    // "ゲーム終了"をクリックした際の処理
    public void ClickedExitButton()
    {
        StartCoroutine(Enumerator());

        IEnumerator Enumerator()
        {
            _popPanelBack.SetActive(true);
            SoundManager.instance.Play("SE_キャンセル");
            yield return new WaitForSeconds(0.5f);
            Application.Quit();
        }
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
        StartCoroutine(Enumerator());

        IEnumerator Enumerator()
        {
            GameManager.instance._Flag.read = _chapterNumBuffer;
            GameManager.instance._IsStoryMode = false;
            SoundManager.instance.Play("SE_決定");
            yield return new WaitForSeconds(0.5f);
            fadeManager.FadeOut("AdvScene");
        }
    }
}
