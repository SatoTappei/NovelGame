using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BackLogChild : MonoBehaviour
{
    // ///////////////////////////////////////////////////////
    /* アドベンチャーパートのバックログ内に生成される台詞枠 */
    // ///////////////////////////////////////////////////////

    [SerializeField] TextMeshProUGUI _nameText;
    [SerializeField] TextMeshProUGUI _lineText;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // 渡された台詞をセット
    public void SetText(string name,string line)
    {
        _nameText.text = name;
        _lineText.text = line;
    }
}
