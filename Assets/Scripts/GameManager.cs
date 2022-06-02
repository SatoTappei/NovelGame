using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    // /////////////////////
    /* �Q�[���}�l�[�W���[ */
    // /////////////////////

    // �Z�[�u�f�[�^�Ƃ��Ĉ����N���X
    public class ChapterFlag
    {
        public int clear; // �N���A�ς݃`���v�^�[
        public int read;  // �Ō�ɓǂݍ��񂾃`���v�^�[
    }

    public static GameManager instance;

    // ��ʃN���b�N���ɔ���������G�t�F�N�g
    [SerializeField] GameObject _clickEff;
    // �Ō�̘b�ԍ� <- ��n�b�Ƃ����g����
    [SerializeField] int _maxChapterNum;
    // �V�[���؂�ւ����Ɏ��̃V�[���ɐi�ނ��ǂ��� <- �V�[���I���̏ꍇ��false
    public bool _IsStoryMode { get; set; }
    // �Z�[�u�f�[�^�����邩
    public bool _ExistSaveData { get; private set; }
    // �Z�[�u�f�[�^�̃C���X�^���X
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

    // ��ʏ���N���b�N�����ۂ̃G�t�F�N�g�𐶐�
    public void PlayClickEff()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(_clickEff, new Vector3(pos.x, pos.y, 10f), Quaternion.identity);
    }

    // �f�[�^���Z�[�u
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

    // �f�[�^�����[�h
    public void Load()
    {
        try
        {
            // �e�t���O�����Z�b�g
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
            Debug.LogWarning("�Z�[�u�f�[�^��������܂���ł���: " + fnfe);
            _ExistSaveData = false;
        }
    }
}
