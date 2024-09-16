using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerDisplay : MonoBehaviour
{
    [SerializeField]private Image selector;
    [SerializeField]private TextMeshProUGUI[] text;
    public TextMeshProUGUI[] Text { get => text; set => text = value; }

    public void Hilight(bool hilight)
    {
        selector.color = new Color(0, 1, 1, 0f);
        if (hilight)selector.color = new Color(0, 1, 1, 40/255.0f);
    }

    public string toString()
    {
        string res = string.Empty;
        for (int i = 0; i < text.Length; i++)
        {
            res += text[i].text.ToString();
        }
        return res;
    }
}
