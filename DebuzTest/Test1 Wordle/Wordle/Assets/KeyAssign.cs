using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyAssign : MonoBehaviour
{
    [SerializeField]private GameController controller;
    public static Dictionary<string,Image> ImageMap = new Dictionary<string,Image>();
    void Start()
    {
        Button button = GetComponent<Button>();
        Image image = GetComponent<Image>();
        ImageMap[button.name] = image;
        button.onClick.AddListener(() => controller.OnKeyboardClick(button));
    }
}
