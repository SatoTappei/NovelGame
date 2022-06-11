using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleSceneManager : MonoBehaviour
{
    // ///////////////////////////
    /* �^�C�g����ʂ̃��C������ */
    // ///////////////////////////

    [SerializeField] FadeManager fadeManager;
    // �{�^�����N���b�N�����Ƃ��Ƀ|�b�v����p�l���̃A�j���[�V����
    [SerializeField] Animator _popPanelAnim;
    // �{�^�����N���b�N�����Ƃ��Ƀ|�b�v����p�l���̔w�i
    [SerializeField] GameObject _popPanelBack;
    // �m�F��ʂ̃A�j���[�V����
    [SerializeField] Animator _confirmPopAnim;
    // �m�F��ʂ̔w�i
    [SerializeField] GameObject _confirmPopBack;
    // �m�F��ʂŕ\�������e�L�X�g
    [SerializeField] TextMeshProUGUI _confirmPopText;
    // "�Â�����"�̃{�^���̃R���|�[�l���g
    [SerializeField] TitleItemButton _continueButton;
    // �V�[���I���̃{�^���̃R���|�[�l���g
    [SerializeField] TitleItemButton _sceneSelectButton;
    // �V�[���I���̊e�V�[����I������{�^���̐e
    [SerializeField] GameObject _sceneSelectItem;
    // �ݒ�̊e�I�u�W�F�N�g�̐e
    [SerializeField] GameObject _settingItem;
    // �`���v�^�[�{�^���N���b�N�ł����ɕۑ��A�Đ����܂����H -> YES��GameManager�ɓn��
    int _chapterNumBuffer;

    void Awake()
    {
        // �Z�[�u�f�[�^��ǂݍ���
        GameManager.instance.Load(); // �r���h��ɏ�肭���삵�Ȃ��̂ňꎞ�I�ɏ����Ă���
        //Debug.Log($"�Z�[�u�f�[�^�����[�h �N���A{GameManager.instance._Flag.clear} ���[�h{GameManager.instance._Flag.read}");
    }

    void Start()
    {
        fadeManager.FadeIn();
        Init();
    }

    void Update()
    {
        
    }

    // ������
    public void Init()
    {
        //�Z�[�u�f�[�^���Ȃ��ꍇ��"�Â�����"�ƃV�[���I�����O���[�A�E�g
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

    // "�͂��߂���"���N���b�N�����ۂ̏���
    public void ClickedBeginingButton()
    {
        StartCoroutine(enumerator());

        IEnumerator enumerator()
        {
            _popPanelBack.SetActive(true);
            GameManager.instance._Flag.read = 0;
            GameManager.instance._IsStoryMode = true;
            SoundManager.instance.Play("SE_����");
            yield return new WaitForSeconds(0.5f);
            fadeManager.FadeOut("AdvScene");
        }
    }

    // "�Â�����"���N���b�N�����ۂ̏���
    public void ClickedContinueButton()
    {
        StartCoroutine(enumerator());

        IEnumerator enumerator()
        {
            _popPanelBack.SetActive(true);
            GameManager.instance._IsStoryMode = true;
            SoundManager.instance.Play("SE_����");
            yield return new WaitForSeconds(0.5f);
            fadeManager.FadeOut("AdvScene");
        }
    }

    // "�V�[���I��"���N���b�N�����ۂ̏���
    public void ClickedSceneSelectButton()
    {
        ActiveItemPanel(true);
        ActiveItems(dispItem: _sceneSelectItem, hideItems: _settingItem);

        // �i�s�󋵂ɉ����ă{�^����\��
        for (int i = 0; i < _sceneSelectItem.transform.childCount; i++)
        {
            GameObject child = _sceneSelectItem.transform.GetChild(i).gameObject;
            child.SetActive(i <= GameManager.instance._Flag.clear ? true : false);
        }
    }

    // "�ݒ�"���N���b�N�����ۂ̏���
    public void ClickedSettingButton()
    {
        ActiveItemPanel(true);
        ActiveItems(dispItem: _settingItem, hideItems: _sceneSelectItem);
    }

    // "�Q�[���I��"���N���b�N�����ۂ̏���
    public void ClickedExitButton()
    {
        _popPanelBack.SetActive(true);
        // TODO: �N���b�N������ɑ��̏ꏊ���N���b�N�����Ȃ����߂ɓ����̔w�i���o��
        // TODO: SE�̍Đ�
        // TODO: SE���I�������Q�[���I��
    }

    // �V�[���I����ʂŃ`���v�^�[�ԍ����N���b�N�����ۂ̏���
    public void ClickedChapterNumButton(ChapterDataSO so)
    {
        ActiveConfirmPop(true);
        _chapterNumBuffer = so.Num;
        _confirmPopText.text = $"{so.Title}\n���Đ����܂����H";
    }

    // 1�̃I�u�W�F�N�g�̂ݕ\�����Ďc����\���ɂ���
    void ActiveItems(GameObject dispItem, params GameObject[] hideItems)
    {
        dispItem.SetActive(true);
        foreach (GameObject go in hideItems)
            go.SetActive(false);
    }

    // �`���v�^�[�Z���N�g�Ɛݒ�̃p�l���̕\����؂�ւ�
    void ActiveItemPanel(bool isPop) => Active(isPop, _popPanelAnim, _popPanelBack);

    // �m�F�p�̃|�b�v�̕\����؂�ւ�
    public void ActiveConfirmPop(bool isPop) => Active(isPop, _confirmPopAnim, _confirmPopBack);

    // �\���̐؂�ւ�
    void Active(bool isPop,Animator anim,GameObject back)
    {
        string key = isPop == true ? "SE_����2" : "SE_�L�����Z��";
        string trigger = isPop == true ? "Pop" : "Close";
        SoundManager.instance.Play(key);
        anim.SetTrigger(trigger);
        back.SetActive(isPop);
    }

    // �`���v�^�[�Z���N�g����w�肵���`���v�^�[���Đ�
    public void PlaySelectedChapter()
    {
        StartCoroutine(enumerator());

        IEnumerator enumerator()
        {
            GameManager.instance._Flag.read = _chapterNumBuffer;
            GameManager.instance._IsStoryMode = false;
            SoundManager.instance.Play("SE_����");
            yield return new WaitForSeconds(0.5f);
            fadeManager.FadeOut("AdvScene");
        }
    }
}
