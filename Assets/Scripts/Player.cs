using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Camera cam;
    private Rigidbody rb;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        HideAndLockCursor();
    }

    // Method untuk hide cursor
    private void HideAndLockCursor() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void FixedUpdate()
    {
        // Input menentukan arah gerak (w,a,s,d)
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        // Script pergerakan agar mengikuti kamera
        Vector3 horizontalDirection = horizontalMove * cam.transform.right;
        Vector3 verticalDirection = verticalMove * cam.transform.forward;
        horizontalDirection.y = 0;
        verticalDirection.y = 0;

        // Menentukan move direction
        Vector3 moveDirection = horizontalDirection + verticalDirection;

        // Player movement
        rb.velocity = moveDirection * moveSpeed * Time.fixedDeltaTime;
    }
}
