using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private float rotationSpeed;

    [SerializeField] private DynamicJoystick dynamicJoystick;

    private Animator animator;
    public bool isDirectionZNegative;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        var inputX = dynamicJoystick.Horizontal;
        var inputZ = dynamicJoystick.Vertical;

        if (inputX != 0 && inputZ != 0 && FinalPlatform.Instance.game)
        {
            animator.SetBool("isRun", true);
            Vector3 direction = Vector3.forward * inputZ + Vector3.right * inputX;
            if (direction.z<0)
            {
                isDirectionZNegative = true;
            }
            else
            {
                isDirectionZNegative = false;
            }
            if (Player.Instance.obstacleStep && Player.Instance.obstacleStepPos.z <= transform.position.z)
            {
                var dirZ = Mathf.Clamp(direction.z,-1,0);
                transform.position += new Vector3(direction.x, direction.y, dirZ) * speed * Time.deltaTime;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                    rotationSpeed * Time.deltaTime);
            }
            else
            {
                transform.position += direction * speed * Time.deltaTime;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                    rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            animator.SetBool("isRun", false);
        }
    }
}