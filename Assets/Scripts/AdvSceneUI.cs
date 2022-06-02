using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvSceneUI : MonoBehaviour
{
    // /////////////////////////////////////
    /* アドベンチャーパートのUIを操作する */
    // /////////////////////////////////////

    // 表示非表示を切り替える台詞枠
    [SerializeField] GameObject _lineItem;
    // バックログの親と登録する子
    [SerializeField] GameObject _backLogChild;
    [SerializeField] Transform _backLogParent;
    // バックログがポップしてくるアニメーション
    [SerializeField] Animator _backLogAnim;
    // バックログの背景
    [SerializeField] GameObject _backLogBack;
    // "タイトルに戻る"の確認画面がポップしてくるアニメーション
    [SerializeField] Animator _returnTitleAnim;
    // "タイトルに戻る"の確認画面の背景
    [SerializeField] GameObject _rt_ConfirmPopBack;
    // シーンスキップの確認画面がポップしてくるアニメーション
    [SerializeField] Animator _sceneSkipAnim;
    // シーンスキップの確認画面の背景
    [SerializeField] GameObject _ss_ConfirmPopBack;

    // 台詞枠の表示非表示を切り替える
    public bool _LineItemActive { set => _lineItem.SetActive(value); }

    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }

    // 初期化
    void Init()
    {
        // バックログと確認画面の背景を非表示にする
        _backLogBack.SetActive(false);
        _rt_ConfirmPopBack.SetActive(false);
        _ss_ConfirmPopBack.SetActive(false);
    }

    // バックログに台詞を追加
    public void AddBackLog(string name, string line)
    {
        GameObject go = Instantiate(_backLogChild);
        go.GetComponent<BackLogChild>().SetText(name, line);
        go.transform.SetParent(_backLogParent);
        go.transform.localScale = Vector3.one;
    }

    // バックログの表示を切り替え
    public void ActiveBackLog(bool isPop) => Active(isPop, _backLogAnim, _backLogBack);

    // タイトルに戻るの確認ポップアップ表示を切り替え
    public void ActiveReturnTitlePop(bool isPop) => Active(isPop, _returnTitleAnim, _rt_ConfirmPopBack);

    // シーンスキップの確認ポップアップの表示を切り替え
    public void ActiveSkipPop(bool isPop) => Active(isPop, _sceneSkipAnim, _ss_ConfirmPopBack);

    // 表示の切り替え
    void Active(bool isPop, Animator anim, GameObject back)
    {
        string trigger = isPop == true ? "Pop" : "Close";
        anim.SetTrigger(trigger);
        back.SetActive(isPop);
    }
}
