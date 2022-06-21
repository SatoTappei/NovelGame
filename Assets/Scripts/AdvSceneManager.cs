using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class AdvSceneManager : MonoBehaviour
{
    // ///////////////////////////////////
    /* �A�h�x���`���[�p�[�g�̃��C������ */
    // ///////////////////////////////////

    [SerializeField] AdvEventManager advEventManager;
    [SerializeField] AdvSceneUI advSceneUI;
    [SerializeField] FadeManager fadeManager;
    // ���O�g
    [SerializeField] Text _nameText;
    // �䎌�g
    [SerializeField] Text _lineText;
    // 1������\�����銴�o
    [SerializeField] float _distance;
    // �Đ�����`���v�^�[�̃f�[�^
    ChapterDataSO _chapterData;
    // �\�����镶�������X�V���ꂽ����
    float _lastTypeTime;
    // ���݂̑䎌�ԍ�
    int _lineNum;
    // �\�����镶����
    int _charaCount;
    // ���ݕ\�������䎌�S��
    string _currentLine;
    // �I�[�g���[�h��
    bool _isAuto;
    // �䎌�g���B��Ă��邩
    bool _isHide;
    // �t�F�[�h�A�E�g���� <- �A���N���b�N�ŕ����񎟂̃V�[���ւ̈ړ��������Ă΂�Ȃ��悤�ɂ���
    bool _isFading; 
    // �L�������A�N����C�x���g�A�䎌�����ꂼ��i�[����
    List<string> _names = new List<string>();
    List<string> _events = new List<string>();
    List<string> _lines = new List<string>();

    void Start()
    {
        fadeManager.FadeIn();
        Init();
        Advance();
        //Debug.Log("�ō����B�_:�`���v�^�[" + GameManager.instance._Flag._clear);
        //Debug.Log("���ݓǂݍ���ł���:�`���v�^�[" + GameManager.instance._Flag._read);
    }

    void Update()
    {
        // �I�[�g���[�h�A�䎌���S���\������Ă���A�䎌���Ō�܂ŕ\������Ă���n�b��
        if (_isAuto && _charaCount == _currentLine.Length && 
            Time.realtimeSinceStartup - _lastTypeTime > 1.0f)
        {

            if (_lineNum == _names.Count)
            {
                // �Ō�̑䎌�������ꍇ�̓V�[���ڍs�̏���
                MoveScene();
            }
            else
            {
                // ����ȊO�̏ꍇ�͎��̑䎌��
                Advance();
            }
        }

        // �g���B��Ă��Ȃ��A�}�E�X���䎌�g���N���b�N�����ꍇ
        if (!_isHide && Input.GetMouseButtonDown(0) && HitLineFrame())
        {
            // �I�[�g���[�h���Ȃ����
            if (_isAuto) _isAuto = false;

            // �䎌���S���\������Ă����
            if (_charaCount == _currentLine.Length)
            {
                if(_lineNum == _names.Count)
                {
                    // �Ō�̑䎌�������ꍇ�̓V�[���ڍs�̏���
                    MoveScene();
                }
                else
                {
                    // ����ȊO�̏ꍇ�͎��̑䎌��
                    Advance();
                }
            }
            else
            {
                // �䎌���\������Ă��Ȃ����"�\�����镶�����̃J�E���g"���ő�ɂ��� <- ���̌�ɌĂ΂�鏈���Ńe�L�X�g���̂��X�V����Ƃ����_�ɒ���
                _charaCount = _currentLine.Length;
            }
        }
        // �䎌��`��
        TypingLine();

        // ���݂̑䎌���S�ĕ\�����I����Ă�����s���I�h�A�C�R����\������
        if (_lineText.text.Length == _currentLine.Length &&
            !advSceneUI._PeriodIcon)
            advSceneUI._PeriodIcon = true;
    }

    // ������
    void Init()
    {
        // �`���v�^�[�f�[�^�����[�h���ăe�L�X�g�𕪊�
        string name = $"Chapter{GameManager.instance._Flag.read}";
        _chapterData = LoadChapterData(name);
        SplitText(_chapterData.TextAsset);

        // �^�C�g�����Z�b�g
        advSceneUI._TitlePopText = _chapterData.Title;
    }

    // ���݂̃V�[���œǂݍ��ރ`���v�^�[�f�[�^�����[�h
    ChapterDataSO LoadChapterData(string name)
    {
        ChapterDataSO so = Resources.Load(name, typeof(ChapterDataSO)) as ChapterDataSO;
        if (!so) Debug.LogWarning("ChapterData��������܂���ł���: " + name);
        return so;
    }

    // �e�L�X�g�𕪊����ă��X�g�Ɋi�[����
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

    // ���̑䎌�ɐi��
    void Advance()
    {
        // �s���I�h�A�C�R��������
        advSceneUI._PeriodIcon = false;

        // �C�x���g�̎��s�ƁA������\������䎌�̃Z�b�g
        advEventManager.RunEvent(_events[_lineNum]);
        SetNextLine();

        // �䎌�ԍ����V�[���̑䎌���𒴂��Ȃ��悤��Clamp�Ő�����
        _lineNum++;
        _lineNum = Mathf.Clamp(_lineNum, 0, _names.Count);
    }

    // ���̑䎌���Z�b�g
    void SetNextLine()
    {
        _lineText.text = "";
        _nameText.text = _names[_lineNum];
        _currentLine = _lines[_lineNum];

        //�o�b�N���O�ɑ䎌��ǉ�
        advSceneUI.AddBackLog(_names[_lineNum], _lines[_lineNum]);

        // �\������镶���������Z�b�g
        _charaCount = 0;
    }

    // �\������䎌�̕����𑝂₷
    void TypingLine()
    {
        // �O�̕�����`�悵�Ă�����̊Ԋu���󂢂Ă��Ȃ��ꍇ�A��������"�S���̃e�L�X�g���\������Ă���ꍇ"��return����
        if (Time.realtimeSinceStartup - _lastTypeTime < _distance ||
            _lineText.text.Length == _currentLine.Length)
            return;
        _lastTypeTime = Time.realtimeSinceStartup;

        // �\�����镶�������䎌�̕������𒴂��Ȃ��悤��Clamp�Ő�����
        _charaCount++;
        _charaCount = Mathf.Clamp(_charaCount, 0, _currentLine.Length);
        _lineText.text = _currentLine.Substring(0, _charaCount);
    }

    // �䎌�g���N���b�N�������ǂ���
    bool HitLineFrame()
    {
        PointerEventData p = new PointerEventData(EventSystem.current);
        List<RaycastResult> rr = new List<RaycastResult>();
        p.position = Input.mousePosition;
        EventSystem.current.RaycastAll(p, rr);

        // �{�^�����N���b�N�����ꍇ�͂������̏��������������̂�false��Ԃ�
        foreach (RaycastResult r in rr)
            if (r.gameObject.tag == "LineSystemButton")
                return false;
        return true;
    }

    // �䎌�g�̕\����؂�ւ�
    public void SwitchHideFrame(bool isHide)
    {
        // �䎌�g���\������Ă��鎞�ɌĂ΂ꂽ�ꍇ��return����
        if (!_isHide && isHide == false) return;

        SoundManager.instance.Play("SE_����2");
        advSceneUI._LineItem = !isHide;
        _isHide = isHide;
        // �䎌�g�𑀍삷��ƃI�[�g���[�h����
        _isAuto = false;
    }

    // �X�L�b�v�̃|�b�v�A�b�v�̕\����؂�ւ�
    public void SwitchSkipPop(bool isPop) => Active(isPop, advSceneUI.ActiveSkipPop);

    // �o�b�N���O�̕\����؂�ւ�
    public void SwitchBackLog(bool isPop) => Active(isPop, advSceneUI.ActiveBackLog);

    // �^�C�g���ɖ߂�̃|�b�v�A�b�v�̕\����؂�ւ�
    public void SwitchReturnTitlePop(bool isPop) => Active(isPop, advSceneUI.ActiveReturnTitlePop);

    // �\���̐؂�ւ�
    void Active(bool isPop, UnityAction<bool> func)
    {
        string key = isPop == true ? "SE_����2" : "SE_�L�����Z��";
        SoundManager.instance.Play(key);
        func.Invoke(isPop);
        // �I�[�g���[�h������
        _isAuto = false;
    }

    // �I�[�g���[�h�؂�ւ�
    public void SwitchAutoMode()
    {
        SoundManager.instance.Play("SE_����2");
        _isAuto = !_isAuto;
    }

    // �X�L�b�v�@�\
    public void SkipScene()
    {
        SoundManager.instance.Play("SE_����2");
        MoveScene();
    }

    // �^�C�g���ɖ߂�
    public void ReturnTitleScene()
    {
        SoundManager.instance.Play("SE_����2");
        fadeManager.FadeOut("TitleScene");
    }

    // �Z�[�u�{�^��
    public void ClickedSaveButton()
    {
        SoundManager.instance.Play("SE_�Z�[�u");
        GameManager.instance.Save();
    }

    // �V�[���ړ�
    public void MoveScene()
    {
        if (_isFading) return;
        _isFading = true;

        // 2��ȏ�Q�Ƃ���̂ŕϐ��ɓ���Ă���
        int clear = GameManager.instance._Flag.clear;
        int max = GameManager.instance._MaxChapterNum;

        // "�͂��߂���"��������"�Â�����"���A���̃`���v�^�[�������
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
            // �S�N��������Z�[�u���ă^�C�g���ɖ߂�
            GameManager.instance.Save();
            fadeManager.FadeOut("TitleScene");
        }
    }
}
