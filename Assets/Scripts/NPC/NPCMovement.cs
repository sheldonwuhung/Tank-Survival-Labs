using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class NPCMovement : MonoBehaviour
{
    public General generalScript;

    [NonSerialized] public float moveSpeed = 30f;
    [NonSerialized] public float originalSpeed = 30f;
    [NonSerialized] public float gunZRotation;

    public Vector2 movementInput;
    public float zRotation = 0f;

    public int stopDistance = 100;
    public int retreatDistance = 50;
    
    void Awake()
    {
        generalScript = GameObject.Find("Managers").GetComponent<General>();
    }
    
    public void Movement(Transform tr, Vector3 target)
    {
        movementInput = (target - transform.position).normalized;
        //Tank Rotation
        if (movementInput.sqrMagnitude > 0.01f) zRotation = generalScript.GetAngleFromPos(movementInput);
        tr.DORotate(new Vector3(0, 0, zRotation - 90), .1f);
        
        float distance = Vector3.Distance(tr.position, target);
        MovementDecisions(distance);
    }

    public void MoveTo(Rigidbody2D rb)
    {
        Vector2 pos = rb.position + Time.fixedDeltaTime * moveSpeed * movementInput;
        rb.MovePosition(pos);
    }

    public void MovementDecisions(float distance)
    {
        // NPC stops moving when within 50 to 100 units from target
        if (distance > retreatDistance && distance < stopDistance) moveSpeed = 0f;
        // NPC moves away from target if closer than 100 units
        else if (distance < retreatDistance) moveSpeed = -originalSpeed;
        else moveSpeed = originalSpeed;
    }

}