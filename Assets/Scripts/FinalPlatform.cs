using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FinalPlatform : MonoBehaviour
{
    public static FinalPlatform Instance;
    public List<GameObject> FinalPlatforms = new List<GameObject>();
    public List<Enemy> enemies = new List<Enemy>();
    public bool game = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (StairController.Instance.counter3 != 1) return;
        if (Player.Instance.currentLevel == 3)
        {
            game = false;
            Player.Instance.transform.DOMove(FinalPlatforms[0].transform.position, .5f).OnComplete(() =>
            {
                UIManager.Instance.successPanel.SetActive(true);
            });
            Player.Instance.transform.DORotate(Player.Instance.transform.rotation * Vector3.up * 180, .5f);
        }
        else
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].currentLevel == 3)
                {
                    game = false;
                    enemies[i].transform.DOMove(FinalPlatforms[0].transform.position, .5f);
                    Player.Instance.transform.DORotate(enemies[i].transform.rotation * Vector3.up * 180, .5f);
                    if (Player.Instance.currentLevel == 2)
                    {
                        Player.Instance.transform.DOMove(FinalPlatforms[1].transform.position, .5f).OnComplete(() =>
                        {
                            UIManager.Instance.FailPanel.SetActive(true);
                        });;
                        Player.Instance.transform.DORotate(Player.Instance.transform.rotation * Vector3.up * 180, .5f);
                    }
                }
                
            }
        }
        
    }
}
