using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenu : Panel
{
    [SerializeField] private Button returnButton = null;

    public override void Initialize()
    {
        if (IsInitialized)
        {
            return;
        }
        returnButton.onClick.AddListener(ClosePanel);
        base.Initialize();
    }
    
    public override void Open()
    {
        base.Open();
    }

    public void ClosePanel()
    {
        Close();
    }
}
