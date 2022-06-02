using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    // ///////////////////////////////////////////
    /* ��ʂ��t�F�[�h�����ăV�[���`�F���W������ */
    // ///////////////////////////////////////////

    Image _image;
    bool _isFading = false;
    byte _fadeSpeed = 15;

    void Awake()
    {
        _image = GetComponent<Image>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // �t�F�[�h�C��
    public void FadeIn()
    {
        if (_isFading) return;
        StartCoroutine(In());

        IEnumerator In()
        {
            byte a = 255;

            _isFading = true;
            while (true)
            {
                _image.color = new Color32(0, 0, 0, a);
                if (a <= 0) break;
                a -= _fadeSpeed;
                yield return null;
            }
            _isFading = false;
            _image.enabled = false;
        }
    }

    // �t�F�[�h�A�E�g������A�V�[�������[�h
    public void FadeOut(string name)
    {
        if (_isFading) return;
        StartCoroutine(Out());

        IEnumerator Out()
        {
            byte a = 0;

            _image.enabled = true;
            _isFading = true;
            while (true)
            {
                _image.color = new Color32(0, 0, 0, a);
                if (a >= 255) break;
                a += _fadeSpeed;
                yield return null;
            }
            _isFading = false;
            SceneManager.LoadScene(name);
        }
    }

}
