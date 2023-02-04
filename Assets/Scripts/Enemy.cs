using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public Type type;

    [SerializeField] private float radius;
    [SerializeField] private Transform collectPos;
    [SerializeField] private List<Stair> stairs;
    [SerializeField] private Material enemyColor;

    public Animator animator;
    public NavMeshAgent navMeshAgent;
    public bool haveTarget = false;
    public Vector3 targetPosition;
    public List<Transform> stairNonActiveSteps = new List<Transform>();
    
    private List<Step> targets = new List<Step>();
    public List<Step> collectSteps = new List<Step>();
    private Stair myStair;
    private List<Stair> levelStair;
    public int currentLevel=0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        LevelUp();
    }

    private void Update()
    {
        if (!haveTarget && targets.Count > 0 && FinalPlatform.Instance.game)
        {
            ChoseTarget();
        }

        if (!FinalPlatform.Instance.game)
        {
            haveTarget = false;
            navMeshAgent.speed = 0;
        }
        
        if (navMeshAgent.speed == 0)
        {
            animator.SetBool("isRun", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var step = other.GetComponent<Step>();
        if (step != null && step.type == this.type)
        {
            step.GetComponent<BoxCollider>().enabled = false;
            step.GetComponent<Rigidbody>().isKinematic = false;
            
            step.transform.SetParent(collectPos);
            var newPos = collectPos.transform.localPosition + Vector3.up * collectSteps.Count / 8;
            step.transform.DOLocalMove(newPos, .5f).OnComplete(() =>
            {
                foreach (var trail in step.transform.GetComponentsInChildren<TrailRenderer>())
                {
                    trail.enabled = false;
                }
            });
            step.transform.DOLocalRotate(Vector3.zero, .2f);

            collectSteps.Add(step);
            targets.Remove(step);
            haveTarget = false;
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        var stairStep = other.gameObject.GetComponent<StairStep>();
        if (stairStep != null && stairStep.type != this.type)
        {
            if (collectSteps.Count > 0)
            {
                var temp = collectSteps[collectSteps.Count - 1];
                temp.transform.SetParent(null);
                temp.transform.DOMove(stairStep.transform.position, .1f).OnComplete(() =>
                {
                    collectSteps.RemoveAt(collectSteps.Count - 1);
                    Destroy(temp);
                });
                stairStep.GetComponentInChildren<MeshRenderer>().material = enemyColor;
                stairStep.GetComponentInChildren<MeshRenderer>().enabled = true;
                stairStep.type = this.type;
                if (stairStep.lastStairStep)
                {
                    currentLevel++;
                    StairController.Instance.RandomSelectStair(currentLevel,this.type);
                    LevelUp();
                    haveTarget = false;
                }
            }
            else
            {
                haveTarget = false;
            }
        }
    }

    private void ChoseTarget()
    {
        var goToStair = Random.Range(0, 2); 
        if (goToStair == 0 && collectSteps.Count > 5)
        {
            targetPosition = myStair.stairSteps[myStair.stairSteps.Count-1].transform.position;
            
            // stairNonActiveSteps.Clear();
            // foreach (var item in myStair.stairSteps)
            // {
            //     if (item.type == Type.None)
            //     {
            //         stairNonActiveSteps.Add(item.transform);
            //     }
            // }
            //
            // if (collectSteps.Count >= stairNonActiveSteps.Count)
            // {
            //     targetPosition = stairNonActiveSteps[stairNonActiveSteps.Count-1].position;
            //     // Debug.Log(type + "Target:" +stairNonActiveSteps[stairNonActiveSteps.Count-1].gameObject.name+ "NonActiveStepCount :" + stairNonActiveSteps.Count);
            // }
            // else
            // {
            //     targetPosition = stairNonActiveSteps[collectSteps.Count].position;
            //     // Debug.Log(type + " :" +stairNonActiveSteps[collectSteps.Count].gameObject.name + "NonActiveStepCount :" + stairNonActiveSteps.Count);
            // }
        }
        else
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
            List<Transform> ourColors = new List<Transform>();

            foreach (var hitcollider in hitColliders)
            {
                var step = hitcollider.GetComponent<Step>();
                if (step == null) break;
                if (step.type == this.type)
                {
                    ourColors.Add(hitcollider.transform);
                }
            }

            if (ourColors.Count > 0)
            {
                targetPosition = ourColors[0].position;
            }
            else
            {
                var randomTarget = Random.Range(0, targets.Count);
                targetPosition = targets[randomTarget].transform.position;
            }
        }

        navMeshAgent.SetDestination(targetPosition);
        animator.SetBool("isRun", true);
        haveTarget = true;
    }

    private void LevelUp()
    {
        switch (type)
        {
            case Type.Orange:
                switch (currentLevel)
                {
                    case 0:
                        targets = StepController.Instance.OrangeStep;
                        myStair = StairController.Instance.OrangeStair;
                        StepController.Instance.OpenLevelSteps(targets);
                        break;
                    case 1:
                        if (StairController.Instance.counter < 3)
                        {
                            StairController.Instance.counter++;
                            targets = StepController.Instance.OrangeStep2;
                            myStair = StairController.Instance.OrangeStair;
                            StepController.Instance.OpenLevelSteps(targets);
                        }
                        else
                        {
                            gameObject.SetActive(false);
                            navMeshAgent.speed =0;
                        }
                        break;
                    case 2:
                        if (StairController.Instance.counter2 < 2)
                        {
                            StairController.Instance.counter2++;
                            targets = StepController.Instance.OrangeStep3;
                            myStair = StairController.Instance.OrangeStair;
                            StepController.Instance.OpenLevelSteps(targets);
                        }
                        else
                        {
                            gameObject.SetActive(false);
                            navMeshAgent.speed =0;
                        }
                        break;
                    case 3:
                        if (StairController.Instance.counter3 < 1)
                        {
                            StairController.Instance.counter3++;
                            navMeshAgent.speed = 0;
                        }
                        else
                        {
                            gameObject.SetActive(false);
                            navMeshAgent.speed = 0;
                        }
                        break;
                }
                break;
            case Type.Red:
                switch (currentLevel)
                {
                    case 0:
                        targets = StepController.Instance.RedSteps;
                        myStair = StairController.Instance.RedStair;
                        StepController.Instance.OpenLevelSteps(targets);
                        break;
                    case 1:
                        if (StairController.Instance.counter < 3)
                        {
                            StairController.Instance.counter++;
                            targets = StepController.Instance.RedSteps2;
                            myStair = StairController.Instance.RedStair;
                            StepController.Instance.OpenLevelSteps(targets);
                        }
                        else
                        {
                            gameObject.SetActive(false);
                            navMeshAgent.speed =0;
                        }
                        break;
                    case 2:
                        if (StairController.Instance.counter2 < 2)
                        {
                            StairController.Instance.counter2++;
                            targets = StepController.Instance.RedSteps3;
                            myStair = StairController.Instance.RedStair;
                            StepController.Instance.OpenLevelSteps(targets);
                        }
                        else
                        {
                            gameObject.SetActive(false);
                            navMeshAgent.speed =0;
                        }
                        break;
                    case 3:
                        if (StairController.Instance.counter3 < 1)
                        {
                            StairController.Instance.counter3++;
                            navMeshAgent.speed = 0;
                        }
                        else
                        {
                            gameObject.SetActive(false);
                            navMeshAgent.speed = 0;
                        }
                        break;
                }
                break;
            case Type.Purple:
                switch (currentLevel)
                {
                    case 0:
                        targets = StepController.Instance.PurpleSteps;
                        myStair = StairController.Instance.PurpleStair;
                        StepController.Instance.OpenLevelSteps(targets);
                        break;
                    case 1:
                        if (StairController.Instance.counter < 3)
                        {
                            StairController.Instance.counter++;
                            targets = StepController.Instance.PurpleSteps2;
                            myStair = StairController.Instance.PurpleStair;
                            StepController.Instance.OpenLevelSteps(targets);
                        }
                        else
                        {
                            gameObject.SetActive(false);
                            navMeshAgent.speed =0;
                        }
                        break;
                    case 2:
                        if (StairController.Instance.counter2 < 2)
                        {
                            StairController.Instance.counter2++;
                            targets = StepController.Instance.PurpleSteps3;
                            myStair = StairController.Instance.PurpleStair;
                            StepController.Instance.OpenLevelSteps(targets);
                        }
                        else
                        {
                            gameObject.SetActive(false);
                            navMeshAgent.speed =0;
                        }
                        break;
                    case 3:
                        if (StairController.Instance.counter3 < 1)
                        {
                            StairController.Instance.counter3++;
                            navMeshAgent.speed = 0;
                        }
                        else
                        {
                            gameObject.SetActive(false);
                            navMeshAgent.speed = 0;
                        }
                        break;
                }
                break;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}