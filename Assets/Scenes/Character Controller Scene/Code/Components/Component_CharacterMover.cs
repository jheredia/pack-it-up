using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Component_CharacterMover : MonoBehaviour
{
    private const float TIME_TO_REACH_TOP_SPEED = 0.1f;
    
    public SO_MovementRules rules;
    public Rigidbody2D rb;
    private Vector2 currentVelocity;
    private Vector2 inputVector;
    
    public bool canMove { get; private set; } = true;
    public bool canControlMovement { get; private set; } = true;

    private bool isReceivingInput = false;

    private void FixedUpdate()
    {
        MovementLogic();
    }

    private void MovementLogic()
    {
        if (canMove && isReceivingInput) ApplyAcceleration();
        else if (!canMove) StopVelocity();
        else ApplyDeceleration();
    }

    private void ApplyAcceleration()
    {
        // Obtain the actual current velocity of the Rigidbody2D
        currentVelocity = rb.velocity;

        // Determine the target velocity based on input
        Vector2 targetVelocity = inputVector.normalized * rules.topSpeed;

        // Calculate the acceleration rate based on the time to reach top speed
        float accelerationRate = rules.acceleration / TIME_TO_REACH_TOP_SPEED;

        // Accelerate towards the target velocity
        currentVelocity = Vector2.MoveTowards(currentVelocity, targetVelocity, accelerationRate * Time.fixedDeltaTime);
        rb.velocity = currentVelocity;
    }

    private void ApplyDeceleration()
    {
        // Decelerate based on momentum
        float decelerationFactor = 1 - (rules.momentum); // At 0 momentum, factor is 1 (instant stop). At 1 momentum, factor is 0 (no deceleration).
        currentVelocity = Vector2.Lerp(currentVelocity, Vector2.zero, decelerationFactor);
        rb.velocity = currentVelocity;
    }

    private void StopVelocity()
    {
        rb.velocity = Vector2.zero;
    }

    public void ToggleAllMovement()
    {
        canMove = !canMove;
    }

    public void TogglePlayerControlledMovement()
    {
        isReceivingInput = false;
        canControlMovement = !canControlMovement;
    }

    public void AcceptVector2Input(Vector2 input)
    {

        if (canControlMovement)
        {
            isReceivingInput = true;
            inputVector = input;
        }
    }

    public void AcceptCancelMovement() { isReceivingInput = false; }
}
