using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private Button btnPlay;
    
    public void On_PlayClicked()
    {
        SceneManager.LoadScene("level1");
    }
}
