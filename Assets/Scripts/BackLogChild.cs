using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackLogChild : MonoBehaviour
{
    // ///////////////////////////////////////////////////////
    /* �A�h�x���`���[�p�[�g�̃o�b�N���O���ɐ��������䎌�g */
    // ///////////////////////////////////////////////////////

    [SerializeField] Text _nameText;
    [SerializeField] Text _lineText;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // �n���ꂽ�䎌���Z�b�g
    public void SetText(string name,string line)
    {
        _nameText.text = name;
        _lineText.text = line;
    }
}
