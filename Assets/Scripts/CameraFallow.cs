using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    [SerializeField] 
    private PlayerMovement player;

	[SerializeField] 
    private Vector3 offset; 

    private void LateUpdate()
	{
		var vector = player.transform.position + offset;
		transform.position = Vector3.Lerp(transform.position, vector,1.5f);
	}
}
