using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairStep : MonoBehaviour
{
   public Type type;
   public BoxCollider obstacleCollider;
   public MeshRenderer mesh;
   public bool lastStairStep;

   private void Start()
   {
      obstacleCollider = GetComponent<BoxCollider>();
      mesh = GetComponent<MeshRenderer>();
   }
}
