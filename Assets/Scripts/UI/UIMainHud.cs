using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Experimental.GlobalIllumination;

public class UIMainHud :MonoBehaviour
{
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
        SetMaxValue += SetMaxHP;
        SetValue += changeHP;
        ChangeScore += ChangeScoreText;
        score = 0;
        pauseMenu.SetActive(false);
        btnPause.onClick.AddListener(OnPauseClick);
        //add as a list
        for(int i=0;i<lsbtnPauseMenu.Count;i++)
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
                Debug.Log("Setting");
                break;
            default:
                Debug.Log("Quit");
                break;
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
