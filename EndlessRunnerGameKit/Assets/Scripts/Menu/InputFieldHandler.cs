using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldHandler : MonoBehaviour
{
    [SerializeField] public TMP_InputField inputField;

    public void OnClick()
    {
        // 如果 InputField 的交互性未被禁用，则激活并唤起键盘
        if (inputField != null && inputField.interactable)
        {
            inputField.ActivateInputField();
            TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        }
    }
}