using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenu : Panel
{
    [SerializeField] private Button returnButton = null;
    [SerializeField] private Button detailsButton = null;

    public override void Initialize()
    {
        if (IsInitialized)
        {
            return;
        }
        returnButton.onClick.AddListener(ClosePanel);
        detailsButton.onClick.AddListener(OpenDetails);
        base.Initialize();
    }
    
    public override void Open()
    {
        base.Open();
    }

    public void OpenDetails()
    {
        PanelManager.Open("details");
    }

    public void ClosePanel()
    {
        Close();
    }
}
