using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float RunSpeed = 7;
    public float RotationSpeed = 250;
    public bool canControl = true;


    public Animator animator;

    private float x, y;

    void Update()
    {
        if (!canControl)
        {
            animator.SetFloat("VelX", 0);
            animator.SetFloat("VelY", 0);
            return;
        }

        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        transform.Rotate(0, x * Time.deltaTime * RotationSpeed, 0);
        transform.Translate(0, 0, y * Time.deltaTime * RunSpeed);

        animator.SetFloat("VelX", x);
        animator.SetFloat("VelY", y);
    }
}
