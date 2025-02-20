using Scene.Stack;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : BaseUI
{
    Button startButton;
    Button exitButton;
    
    protected override UIState GetUIState()
    {
        return UIState.Home;
    }
    
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        
        startButton = transform.Find("StartButton").GetComponent<Button>();
        exitButton = transform.Find("ExitButton").GetComponent<Button>();
        
        Debug.Log(startButton);
        Debug.Log(exitButton);
        
        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickStartButton()
    {
        Debug.Log("OnClickStartButton");
        uiManager.OnClickStart();
    }

    public void OnClickExitButton()
    {
        uiManager.OnClickExit();
    }


}