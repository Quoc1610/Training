using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class UIMainHud : MonoBehaviour
{
    private GameSave gameSave;
    [SerializeField] private Slider hpSlider;
    public EventHandler<int> SetMaxValue;
    public EventHandler<int> SetValue;
    public EventHandler<int> ChangeScore;
    [SerializeField] private Button btnPause;
    [SerializeField] private TextMeshProUGUI txtHealth;
    [SerializeField] private TextMeshProUGUI txtScore;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private List<Button> lsbtnPauseMenu;
    
    private int score;

    public void Setup()
    {
        gameSave= GameSave.GetInstance();
        SetMaxValue += SetMaxHP;
        SetValue += changeHP;
        ChangeScore += ChangeScoreText;
        score = 0;
        pauseMenu.SetActive(false);
        btnPause.onClick.AddListener(OnPauseClick);
        //add as a list
        for (int i = 0; i < lsbtnPauseMenu.Count; i++)
        {
            int a = i;
            lsbtnPauseMenu[i].onClick.AddListener(() => OnPauseButtonClicked(a));
        }
    }

    public void OnPauseClick()
    {
        UIManager.GetInstance().PauseGame();
        pauseMenu.SetActive(true);
    }

    public void OnPauseButtonClicked(int index)
    {
        Debug.Log($"Button clicked with index: {index}");

        switch (index)
        {
            case 0:
                Debug.Log("Resuming game");
                UIManager.GetInstance().ResumeGame();
                pauseMenu.SetActive(false);
                break;
            case 1:
                Debug.Log("Save");
                //SaveGame();
                gameSave.SaveGame();
                //hpSlider.value=gameSave.
                break;
            case 2:
                Debug.Log("Load");
                //LoadGame();
                gameSave.LoadGame();
                hpSlider.value=gameSave.save.Hp;
                txtHealth.text = gameSave.save.Hp.ToString();
                break;
            default:
                //Load Next level
                AllManager.GetInstance().levelCount+=1;
                Debug.Log("Load Scence "+"level "+(AllManager.GetInstance().levelCount+1));
                SceneManager.LoadScene("level"+(AllManager.GetInstance().levelCount+1));
                
                
                return;
        }
    }

    private void ChangeScoreText(object sender, int amount)
    {
        score += amount;
        txtScore.text = "SCORE: " + score;
    }

    private void SetMaxHP(object sender, int hp)
    {
        hpSlider.maxValue = hp;
        txtHealth.text = hp + " / " + hp;
    }

    private void changeHP(object sender, int hp)
    {
        hpSlider.value += hp;
        txtHealth.text = hpSlider.value + " / " + hpSlider.maxValue;
    }
}