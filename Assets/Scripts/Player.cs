using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Rigidbody rb;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // Input menentukan arah gerak (w,a,s,d)
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        // Menentukan move direction
        Vector3 moveDirection = new Vector3(horizontalMove,0,verticalMove);

        // Player movement
        rb.velocity = moveDirection * moveSpeed * Time.fixedDeltaTime;
    }
}
