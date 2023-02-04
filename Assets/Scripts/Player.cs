using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public Type type = Type.Blue;
    
    [SerializeField] private Transform collectPos;
    [SerializeField] private Material playerColor;

    public bool obstacleStep = false;
    public Vector3 obstacleStepPos;
    private List<Step> collectSteps = new List<Step>();
    public int currentLevel = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void OnCollisionEnter(Collision other)
    {
        var stairStep = other.gameObject.GetComponent<StairStep>();
        if (stairStep != null)
        {
            if (collectSteps.Count > 0 && stairStep.type != this.type && !GetComponent<PlayerMovement>().isDirectionZNegative)
            {
                if (stairStep.lastStairStep)
                {
                    currentLevel++;
                    SelectTargetListByLevel();
                }
                // stairStep.obstacleCollider.enabled = false;
                var temp = collectSteps[collectSteps.Count - 1];
                temp.transform.SetParent(null);
                temp.transform.DOMove(stairStep.transform.position, .1f);
                Destroy(collectSteps[collectSteps.Count - 1]);
                collectSteps.RemoveAt(collectSteps.Count - 1);
                
                stairStep.GetComponentInChildren<MeshRenderer>().material = playerColor;
                stairStep.GetComponentInChildren<MeshRenderer>().enabled = true;
                stairStep.type = this.type;
            }

            if (collectSteps.Count == 0 && stairStep.type != type)
            {
                obstacleStep = true;
                obstacleStepPos = transform.position;
            }

            if (stairStep.type == type)
            {
                obstacleStep = false;
            }
        }
        
        var enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy!=null)
        {
            if (collectSteps.Count > enemy.collectSteps.Count)
            {
                enemy.navMeshAgent.speed = 0;
                foreach (var step in  enemy.collectSteps)
                {
                    step.transform.SetParent(null);
                    step.GetComponent<MeshRenderer>().material.color = Color.gray;
                    step.transform.position = enemy.transform.position + Vector3.up * 5;
                    step.transform
                        .DOJump(
                            new Vector3(transform.position.x + Random.Range(-1f, 1f), 0.68f,
                                transform.position.z + Random.Range(-1f, 1f)), 2, 1, .5f);

                    step.type = Type.None;
                    step.GetComponent<BoxCollider>().enabled = true;
                }
                enemy.collectSteps.Clear();
                switch (enemy.currentLevel)
                {
                    case 0:
                        Debug.Log("level 0 çalıştı");
                        enemy.transform.DOJump(new Vector3(-transform.position.x/6,0.7499045f,-transform.position.z/6) ,2,1,1f).OnComplete(
                            () =>
                            {
                                enemy.navMeshAgent.speed = 3.5f;
                                enemy.haveTarget = false;
                            });
                        break;
                    case 1:
                        Debug.Log("level 1 çalıştı");
                        enemy.transform.DOJump(new Vector3(-transform.position.x/200,7.749905f,-transform.position.z/200) ,2,1,1f).OnComplete(
                            () =>
                            {
                                enemy.navMeshAgent.speed = 3.5f;
                                enemy.haveTarget = false;
                            });
                        break;
                    case 2:
                        Debug.Log("level 2 çalıştı");
                        enemy.transform.DOJump(new Vector3(-transform.position.x/50,14.7525f,-transform.position.z/50) ,2,1,1f).OnComplete(
                            () =>
                            {
                                enemy.navMeshAgent.speed = 3.5f;
                                enemy.haveTarget = false;
                            });
                        break;
                }

                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var step = other.GetComponent<Step>();
        if (step != null && step.type == this.type || step.type == Type.None)
        {
            if (step.type == Type.None)
            { 
                step.GetComponent<MeshRenderer>().material = playerColor;
            }
            obstacleStep = false;
            step.GetComponent<BoxCollider>().enabled = false;
            step.GetComponent<Rigidbody>().isKinematic = false;
            step.transform.SetParent(collectPos);
            var newPos = collectPos.transform.localPosition + Vector3.up * collectSteps.Count / 8;
            step.transform.DOLocalJump(newPos, 1,1,.5f).OnComplete(() =>
            {
                foreach (var trail in step.transform.GetComponentsInChildren<TrailRenderer>())
                {
                    trail.enabled = false;
                }
            });
            step.transform.DOLocalRotate(Vector3.zero, .2f);
            collectSteps.Add(step);
            if (step.type == Type.Blue)
            {
                switch (currentLevel)
                {
                    case 0:
                        StepController.Instance.BlueSteps.Remove(step);
                        break;
                    case 1:
                        StepController.Instance.BlueSteps2.Remove(step);
                        break;
                    case 2:
                        StepController.Instance.BlueSteps3.Remove(step);
                        break;
                }
            }
        }
    }
    
    private void SelectTargetListByLevel()
    {
        switch (currentLevel)
        {
            case 1:
                StairController.Instance.counter++;
                StepController.Instance.OpenLevelSteps(StepController.Instance.BlueSteps2);
                break;
            case 2:
                StairController.Instance.counter2++;
                StepController.Instance.OpenLevelSteps(StepController.Instance.BlueSteps3);
                break;
            case 3:
                StairController.Instance.counter3++;
                break;
        }
    }

}
