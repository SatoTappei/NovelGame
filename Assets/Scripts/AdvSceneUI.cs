using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvSceneUI : MonoBehaviour
{
    // /////////////////////////////////////
    /* �A�h�x���`���[�p�[�g��UI�𑀍삷�� */
    // /////////////////////////////////////

    // �\����\����؂�ւ���䎌�g
    [SerializeField] GameObject _lineItem;
    // �o�b�N���O�̐e�Ɠo�^����q
    [SerializeField] GameObject _backLogChild;
    [SerializeField] Transform _backLogParent;
    // �o�b�N���O���|�b�v���Ă���A�j���[�V����
    [SerializeField] Animator _backLogAnim;
    // �o�b�N���O�̔w�i
    [SerializeField] GameObject _backLogBack;
    // "�^�C�g���ɖ߂�"�̊m�F��ʂ��|�b�v���Ă���A�j���[�V����
    [SerializeField] Animator _returnTitleAnim;
    // "�^�C�g���ɖ߂�"�̊m�F��ʂ̔w�i
    [SerializeField] GameObject _rt_ConfirmPopBack;
    // �V�[���X�L�b�v�̊m�F��ʂ��|�b�v���Ă���A�j���[�V����
    [SerializeField] Animator _sceneSkipAnim;
    // �V�[���X�L�b�v�̊m�F��ʂ̔w�i
    [SerializeField] GameObject _ss_ConfirmPopBack;

    // �䎌�g�̕\����\����؂�ւ���
    public bool _LineItemActive { set => _lineItem.SetActive(value); }

    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }

    // ������
    void Init()
    {
        // �o�b�N���O�Ɗm�F��ʂ̔w�i���\���ɂ���
        _backLogBack.SetActive(false);
        _rt_ConfirmPopBack.SetActive(false);
        _ss_ConfirmPopBack.SetActive(false);
    }

    // �o�b�N���O�ɑ䎌��ǉ�
    public void AddBackLog(string name, string line)
    {
        GameObject go = Instantiate(_backLogChild);
        go.GetComponent<BackLogChild>().SetText(name, line);
        go.transform.SetParent(_backLogParent);
        go.transform.localScale = Vector3.one;
    }

    // �o�b�N���O�̕\����؂�ւ�
    public void ActiveBackLog(bool isPop) => Active(isPop, _backLogAnim, _backLogBack);

    // �^�C�g���ɖ߂�̊m�F�|�b�v�A�b�v�\����؂�ւ�
    public void ActiveReturnTitlePop(bool isPop) => Active(isPop, _returnTitleAnim, _rt_ConfirmPopBack);

    // �V�[���X�L�b�v�̊m�F�|�b�v�A�b�v�̕\����؂�ւ�
    public void ActiveSkipPop(bool isPop) => Active(isPop, _sceneSkipAnim, _ss_ConfirmPopBack);

    // �\���̐؂�ւ�
    void Active(bool isPop, Animator anim, GameObject back)
    {
        string trigger = isPop == true ? "Pop" : "Close";
        anim.SetTrigger(trigger);
        back.SetActive(isPop);
    }
}
