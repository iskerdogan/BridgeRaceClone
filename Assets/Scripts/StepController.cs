using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StepController : MonoBehaviour
{
    public static StepController Instance;
    
    [SerializeField] private GameObject plane1;
    [SerializeField] private Step stepPrefab;
    [SerializeField] private List<Step> plane1Steps = new List<Step>();
    [SerializeField] private List<Step> plane2Steps = new List<Step>();
    [SerializeField] private List<Step> plane3Steps = new List<Step>();
    [SerializeField] private List<Material> materials = new List<Material>();
    
    private List<Step> orangeSteps = new List<Step>();
    private List<Step> purpleSteps = new List<Step>();
    private List<Step> redSteps = new List<Step>();
    private List<Step> blueSteps = new List<Step>();
    
    private List<Step> orangeSteps2 = new List<Step>();
    private List<Step> purpleSteps2 = new List<Step>();
    private List<Step> redSteps2 = new List<Step>();
    private List<Step> blueSteps2 = new List<Step>();
    
    private List<Step> orangeSteps3 = new List<Step>();
    private List<Step> purpleSteps3 = new List<Step>();
    private List<Step> redSteps3 = new List<Step>();
    private List<Step> blueSteps3 = new List<Step>();
    
    private List<Vector3> transformorangeSteps = new List<Vector3>();
    private List<Vector3> transformpurpleSteps = new List<Vector3>();
    private List<Vector3> transformredSteps = new List<Vector3>();
    private List<Vector3> transformblueSteps = new List<Vector3>();
    
    private List<Vector3> transformorangeSteps2 = new List<Vector3>();
    private List<Vector3> transformpurpleSteps2 = new List<Vector3>();
    private List<Vector3> transformredSteps2 = new List<Vector3>();
    private List<Vector3> transformblueSteps2 = new List<Vector3>();
    
    private List<Vector3> transformorangeSteps3 = new List<Vector3>();
    private List<Vector3> transformpurpleSteps3 = new List<Vector3>();
    private List<Vector3> transformredSteps3 = new List<Vector3>();
    private List<Vector3> transformblueSteps3 = new List<Vector3>();

    public List<Step> OrangeStep => orangeSteps;
    public List<Step> PurpleSteps => purpleSteps;
    public List<Step> RedSteps => redSteps;
    public List<Step> BlueSteps => blueSteps;
    
    public List<Step> OrangeStep2 => orangeSteps2;
    public List<Step> PurpleSteps2 => purpleSteps2;
    public List<Step> RedSteps2 => redSteps2;
    public List<Step> BlueSteps2 => blueSteps2;
    
    public List<Step> OrangeStep3 => orangeSteps3;
    public List<Step> PurpleSteps3 => purpleSteps3;
    public List<Step> RedSteps3 => redSteps3;
    public List<Step> BlueSteps3 => blueSteps3;

    private void Awake()
    {
        Instance = this;
        
        LevelStepSetup();
        Level2StepSetup();
        Level3StepSetup();
    }

    private void Start()
    {
        OpenLevelSteps(blueSteps);
    }

    private void Update()
    {
        CheckStepCount();
    }

    public void OpenLevelSteps(List<Step> colorStepsList)
    {
        foreach (var step in colorStepsList)
        {
            step.gameObject.SetActive(true);
        }
    }
    
    private void LevelStepSetup()
    {
        foreach (var step in plane1Steps)
        {
            var color = materials[Random.Range(0, materials.Count)];
            step.GetComponent<MeshRenderer>().material = color;
            step.gameObject.SetActive(false);
            switch (color.name)
            {
                case "Orange":
                    step.type = Type.Orange;
                    orangeSteps.Add(step);
                    transformorangeSteps.Add(step.transform.position);
                    break;
                case "Blue":
                    step.type = Type.Blue;
                    blueSteps.Add(step);
                    transformblueSteps.Add(step.transform.position);
                    break;
                case "Purple":
                    step.type = Type.Purple;
                    purpleSteps.Add(step);
                    transformpurpleSteps.Add(step.transform.position);
                    break;
                case "Red":
                    step.type = Type.Red;
                    redSteps.Add(step);
                    transformredSteps.Add(step.transform.position);
                    break;
            }
        }
    }
    
    private void Level2StepSetup()
    {
        foreach (var step in plane2Steps)
        {
            var color = materials[Random.Range(0, materials.Count)];
            step.GetComponent<MeshRenderer>().material = color;
            step.gameObject.SetActive(false);
            switch (color.name)
            {
                case "Orange":
                    step.type = Type.Orange;
                    orangeSteps2.Add(step);
                    transformorangeSteps2.Add(step.transform.position);
                    break;
                case "Blue":
                    step.type = Type.Blue;
                    blueSteps2.Add(step);
                    transformblueSteps2.Add(step.transform.position);
                    break;
                case "Purple":
                    step.type = Type.Purple;
                    purpleSteps2.Add(step);
                    transformpurpleSteps2.Add(step.transform.position);
                    break;
                case "Red":
                    step.type = Type.Red;
                    redSteps2.Add(step);
                    transformredSteps2.Add(step.transform.position);
                    break;
            }
        }
    }
    
    private void Level3StepSetup()
    {
        foreach (var step in plane3Steps)
        {
            var color = materials[Random.Range(0, materials.Count)];
            step.GetComponent<MeshRenderer>().material = color;
            step.gameObject.SetActive(false);
            switch (color.name)
            {
                case "Orange":
                    step.type = Type.Orange;
                    orangeSteps3.Add(step);
                    transformorangeSteps3.Add(step.transform.position);
                    break;
                case "Blue":
                    step.type = Type.Blue;
                    blueSteps3.Add(step);
                    transformblueSteps3.Add(step.transform.position);
                    break;
                case "Purple":
                    step.type = Type.Purple;
                    purpleSteps3.Add(step);
                    transformpurpleSteps3.Add(step.transform.position);
                    break;
                case "Red":
                    step.type = Type.Red;
                    redSteps3.Add(step);
                    transformredSteps3.Add(step.transform.position);
                    break;
            }
        }
    }

    private void CheckStepCount()
    {
        if (orangeSteps.Count == 0)
        {
            foreach (var stepTransform in transformorangeSteps)
            {
                var temp = Instantiate(stepPrefab);
                temp.transform.position = stepTransform;
                temp.GetComponent<MeshRenderer>().material = materials[2];
                temp.type = Type.Orange;
                orangeSteps.Add(temp);
            }
        }
        if (orangeSteps2.Count == 0)
        {
            foreach (var stepTransform in transformorangeSteps2)
            {
                var temp = Instantiate(stepPrefab);
                temp.transform.position = stepTransform;
                temp.GetComponent<MeshRenderer>().material = materials[2];
                temp.GetComponent<Step>().type = Type.Orange;
                orangeSteps2.Add(temp);
            }
        }
        if (orangeSteps3.Count == 0)
        {
            foreach (var stepTransform in transformorangeSteps3)
            {
                var temp = Instantiate(stepPrefab);
                temp.transform.position = stepTransform;
                temp.GetComponent<MeshRenderer>().material = materials[2];
                temp.GetComponent<Step>().type = Type.Orange;
                orangeSteps3.Add(temp);
            }
        }
        if (redSteps.Count == 0)
        {
            foreach (var stepTransform in transformredSteps)
            {
                var temp = Instantiate(stepPrefab);
                temp.transform.position = stepTransform;
                temp.GetComponent<MeshRenderer>().material = materials[1];
                temp.type = Type.Red;
                redSteps.Add(temp);
            }
        }
        if (redSteps2.Count == 0)
        {
            foreach (var stepTransform in transformredSteps2)
            {
                var temp = Instantiate(stepPrefab);
                temp.transform.position = stepTransform;
                temp.GetComponent<MeshRenderer>().material = materials[1];
                temp.type = Type.Red;
                redSteps2.Add(temp);
            }
        }
        if (redSteps3.Count == 0)
        {
            foreach (var stepTransform in transformredSteps3)
            {
                var temp = Instantiate(stepPrefab);
                temp.transform.position = stepTransform;
                temp.GetComponent<MeshRenderer>().material = materials[1];
                temp.type = Type.Red;
                redSteps3.Add(temp);
            }
        }
        
        if (blueSteps.Count == 0)
        {
            foreach (var stepTransform in transformblueSteps)
            {
                var temp = Instantiate(stepPrefab);
                temp.transform.position = stepTransform;
                temp.GetComponent<MeshRenderer>().material = materials[3];
                temp.type = Type.Blue;
                blueSteps.Add(temp);
            }
        }
        if (blueSteps2.Count == 0)
        {
            foreach (var stepTransform in transformblueSteps2)
            {
                var temp = Instantiate(stepPrefab);
                temp.transform.position = stepTransform;
                temp.GetComponent<MeshRenderer>().material = materials[3];
                temp.type = Type.Blue;
                blueSteps2.Add(temp);
            }
        }
        if (blueSteps3.Count == 0)
        {
            foreach (var stepTransform in transformblueSteps3)
            {
                var temp = Instantiate(stepPrefab);
                temp.transform.position = stepTransform;
                temp.GetComponent<MeshRenderer>().material = materials[3];
                temp.type = Type.Blue;
                blueSteps3.Add(temp);
            }
        }
        
        if (purpleSteps.Count == 0)
        {
            foreach (var stepTransform in transformpurpleSteps)
            {
                var temp = Instantiate(stepPrefab);
                temp.transform.position = stepTransform;
                temp.GetComponent<MeshRenderer>().material = materials[0];
                temp.type = Type.Purple;
                purpleSteps.Add(temp);
            }
        }
        
        if (purpleSteps2.Count == 0)
        {
            foreach (var stepTransform in transformpurpleSteps2)
            {
                var temp = Instantiate(stepPrefab);
                temp.transform.position = stepTransform;
                temp.GetComponent<MeshRenderer>().material = materials[0];
                temp.type = Type.Purple;
                purpleSteps2.Add(temp);
            }
        }
        
        if (purpleSteps3.Count == 0)
        {
            foreach (var stepTransform in transformpurpleSteps3)
            {
                var temp = Instantiate(stepPrefab);
                temp.transform.position = stepTransform;
                temp.GetComponent<MeshRenderer>().material = materials[0];
                temp.type = Type.Purple;
                purpleSteps3.Add(temp);
            }
        }
    }
}
