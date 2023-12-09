using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Action OnPowerUpStart;
    public Action OnPowerUpStop;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private Camera cam;
    [SerializeField] private float powerUpDuration;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private int health;
    private bool isPowerUpActive;

    private Rigidbody rb;
    private Coroutine powerUpCoroutine;


    public void PickPowerUp()
    {
        if(powerUpCoroutine != null)
        {
            StopCoroutine(powerUpCoroutine);
        }
        powerUpCoroutine = StartCoroutine(StartPowerUp());
    }
    private IEnumerator StartPowerUp()
    {
        isPowerUpActive = true;
        if (OnPowerUpStart != null)
        {
            OnPowerUpStart();
        }
        yield return new WaitForSeconds(powerUpDuration);
        isPowerUpActive = false;
        if (OnPowerUpStop != null)
        {
            OnPowerUpStop();
        }
    }

    private void UpdateUI() {
        healthText.text = "Health: " + health;
    }

    public void Dead() {
        health -= 1;
        if (health > 0)
        {
            transform.position = respawnPoint.position;
        }
        else
        {
            health = 0;
            Debug.Log("Lose");
        }
        UpdateUI();
    }
    private void Awake() 
    {
        UpdateUI();
        rb = GetComponent<Rigidbody>();
        HideAndLockCursor();
    }

    // Method untuk hide cursor
    private void HideAndLockCursor() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
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
        rb.velocity = moveDirection * moveSpeed * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision other) {
        if (isPowerUpActive)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<Enemy>().Dead();
            }
        }
    }
}
