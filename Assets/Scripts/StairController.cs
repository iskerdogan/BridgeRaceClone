using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class StairController : MonoBehaviour
{
    public static StairController Instance;
    
    
    [SerializeField] private List<Stair> levelOneStairts;
    [SerializeField] private List<Stair> levelTwoStairts;
    [SerializeField] private List<Stair> levelThreeStairts;
    [SerializeField] private List<GameObject> allPLayers;
    
    private List<Stair> levelStairs = new List<Stair>();
    private List<Stair> level2Stairs = new List<Stair>();
    private List<Stair> level3Stairs = new List<Stair>();
    private Stair orangeStair;
    private Stair purpleStair;
    private Stair redStair;
    private Stair blueStair;

    public int counter;
    public int counter2;
    public int counter3;
    public Stair OrangeStair => orangeStair;
    public Stair PurpleStair => purpleStair;
    public Stair RedStair => redStair;
    public Stair BlueStair => blueStair;

    private void Awake()
    {
        Instance = this;
        
        for (int i = 0; i < levelOneStairts.Count; i++)
        {
            levelStairs.Add(levelOneStairts[i]);
        }
        for (int i = 0; i < levelTwoStairts.Count; i++)
        {
            level2Stairs.Add(levelTwoStairts[i]);
        }
        for (int i = 0; i < levelThreeStairts.Count; i++)
        {
            level3Stairs.Add(levelThreeStairts[i]);
        }
    }

    private void Start()
    {
        RandomSelectStairStart(levelStairs);
    }

    private void RandomSelectStairStart(List<Stair> currentStairs)
    {
        for (int i = 0; i < levelOneStairts.Count; i++)
        {
            var temp = i;
            var randomStair = currentStairs[Random.Range(0, currentStairs.Count)];
            randomStair.type = allPLayers[temp].GetComponent<Player>() != null ? allPLayers[temp].GetComponent<Player>().type : allPLayers[temp].GetComponent<Enemy>().type;
            switch (randomStair.type)
            {
                case Type.Blue:
                    blueStair = randomStair;
                    break;
                case Type.Red:
                    redStair = randomStair;
                    break;
                case Type.Orange:
                    orangeStair = randomStair;
                    break;
                case Type.Purple:
                    purpleStair = randomStair;
                    break;
            }
            currentStairs.Remove(randomStair);
        }
    }

    public void RandomSelectStair(int currentLevel,Type type)
    {
        Stair randomStair;
        switch (currentLevel)
        {
            case 1:
                randomStair = level2Stairs[Random.Range(0, level2Stairs.Count)];
                randomStair.type = type;
                switch (randomStair.type)
                {
                    case Type.Red:
                        redStair = randomStair;
                        break;
                    case Type.Orange:
                        orangeStair = randomStair;
                        break;
                    case Type.Purple:
                        purpleStair = randomStair;
                        break;
                }
                level2Stairs.Remove(randomStair);
                break;
            case 2:
                randomStair = level3Stairs[0];
                // randomStair.type = type;
                switch (type)
                {
                    case Type.Red:
                        redStair = randomStair;
                        break;
                    case Type.Orange:
                        orangeStair = randomStair;
                        break;
                    case Type.Purple:
                        purpleStair = randomStair;
                        break;
                }
                break;
        }
        
    }
}
