using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //Définition des variables par défaut
    //Variables de vitesse Marche et Sprint / Puissance du saut
    [SerializeField] private float speed = 8f;
    [SerializeField] private float sprintSpeed = 12f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float airMultiplier = 0.4f;
    //
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform orientation;
    
    //Définitions des variables pratiques
    //
    private float moveX, moveZ;
    private Vector3 moveDirection;
    //
    private bool grounded = false;
    private bool running = false;
    private int nbSaut = 1;
    
    public void onSprintInput(InputAction.CallbackContext context)
    {
        running = context.performed;
    }
    public void onMoveInput(InputAction.CallbackContext context)
    {
        moveX = context.ReadValue<Vector2>().x;
        moveZ = context.ReadValue<Vector2>().y;
    }

    public void onJumpInput(InputAction.CallbackContext context)
    {
        if (context.performed && nbSaut > 0)
        {
            jump();
            nbSaut --;
        }
    }
    private void jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        //Calcule les directions de mouvements en temps réel
        moveDirection = orientation.forward * moveX + orientation.right * moveZ;
        //Sur terre, sinon si : dans les airs
        if (grounded)
        {
            moveDirection *= running ? sprintSpeed : speed;
            rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
            rb.AddForce(moveDirection * Time.deltaTime);
        }
        else if (!grounded)
        {
            moveDirection *= running ? sprintSpeed : speed;
            rb.AddForce(moveDirection.normalized * speed * 10f * airMultiplier, ForceMode.Force);
            rb.AddForce(moveDirection * Time.deltaTime);
        }
    }

    private void Update()
    {
        //Emet un rayon invisible, constate sa taille avant colision
        if (Physics.Raycast(transform.position, Vector3.down, 1.2f))
        {      
            //Si la distance est entre 0 et 1 (taille de la capsule) : il se trouve au sol
            grounded = true;
            nbSaut = 1;
        }
        else
        {
            //Sinon il ne se trouve pas au sol
            grounded = false;
        }
    }
}