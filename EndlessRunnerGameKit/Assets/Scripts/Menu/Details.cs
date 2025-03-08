using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Details : Panel
{
    [SerializeField] private Button closeButton = null;

    public override void Initialize()
    {
        if (IsInitialized)
        {
            return;
        }
        closeButton.onClick.AddListener(ClosePanel);
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
