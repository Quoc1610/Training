using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public UIMainHud uiMainHud;
    
    public static UIManager _instance { get; private set; }


    public static UIManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = GameObject.FindAnyObjectByType<UIManager>();
        }
        return _instance;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
    private void Awake()
    {
        uiMainHud.Setup();
    }

    private void Update()
    {
        
    }

    
}
