using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerMovement : NPCMovement
{
    void Awake()
    {
        moveSpeed = 40f;
        generalScript = GameObject.Find("Managers").GetComponent<General>();
    }
    public void Movement(Transform tr, Transform topGunTR)
    {
        movementInput = Vector2.zero;
        
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) movementInput.y += 1;
        if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) movementInput.y -= 1;
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) movementInput.x += 1;
        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) movementInput.x -= 1;

        movementInput = movementInput.normalized;
        
        //Tank Rotation
        if (movementInput.sqrMagnitude > 0.01f) zRotation = generalScript.GetAngleFromPos(movementInput);
        tr.DORotate(new Vector3(0, 0, zRotation - 90), .1f);

        //Gun Rotation
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gunZRotation = generalScript.GetAngleBetweenTwoVectors(mouseWorldPosition, topGunTR.position);
        topGunTR.DORotate(new Vector3(0, 0, gunZRotation + 90), 0.1f);
        
        // if (movementInput.x != 0) sr.flipX = movementInput.x > 0.01f ? false : true;
    }

}
