using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    [SerializeField] public GameObject successPanel;
    [SerializeField] public GameObject FailPanel;

    private void Awake()
    {
        Instance = this;
        successPanel.SetActive(false);
        FailPanel.SetActive(false);
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
