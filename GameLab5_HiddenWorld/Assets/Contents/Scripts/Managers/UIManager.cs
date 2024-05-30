using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region SINGLETON
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance)
                return _instance;
            _instance = GameObject.FindFirstObjectByType<UIManager>();
            if (!_instance)
                Debug.LogError("MISSING UIMANAGER");
            return _instance;
        }
    }
    #endregion
    public enum UITypes 
    {
        None,
        Gameplay,
        Win,
        GameOver,
        CutScene,
        MainMenu,
        Options,
        Pause
    }
    private Dictionary<UITypes, UIBase> registeredUI = new();

    private void Awake()
    {
        foreach (UIBase uiBase in GetComponentsInChildren<UIBase>(true))
            registeredUI.Add(uiBase.UIType, uiBase);
        SetUI(UITypes.None);
    }

    public void OpenUI(UITypes uiType)
    {
        registeredUI[uiType]?.IsActive(true);
    }

    public void CloseUI(UITypes uiType)
    {
        registeredUI[uiType]?.IsActive(false);
    }
    public void SetUI(UITypes uiType)
    {
        foreach(KeyValuePair<UITypes, UIBase> kvp in registeredUI)
            registeredUI[kvp.Key].IsActive(kvp.Key == uiType);
    }

    public void UpdateCutSceneText(string text)
    {
        ((UICutScene)registeredUI[UITypes.CutScene]).UpdateCutSceneText(text);
    }
}
