using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Hero : MonoBehaviour, ISizeBuff, IStrengthBuff, IWeightLiftingBuff, IMoveSpeedBuff
{

    [SerializeField]
    public HeroArm heroArmL;
    [SerializeField]
    public HeroArm heroArmR;

    [SerializeField]
    Rigidbody rb;

    [Header("Movement")]
    [SerializeField]
    InputAction movement;
    [SerializeField]
    private Vector2 moveInput;
    public float moveForce;
    public float moveBrake = 1f;
    public float maxMoveSpeed;
    public float torqueSprint;
    public Vector3 facing;


    [Header("Throw")]
    public float throwStrength;

    public Collider[] heroCols;

    [Header("HoldingWeight")]
    public float maxHoldingWeight;
    public UnityEvent<HeroArm.ArmType> onArmBrokenEvent;
    public UnityEvent<HeroArm> onArmBrokenObjEvent;

    [Header("Buff")]
    public int levelMoveSpeed;
    public float[] levelMoveSpeedAttr;
    public int levelThrowPower;
    public float[] levelThrowPowerAttr;
    public int levelWeightLifting;
    public float[] levelWeightLiftingAttr;
    public int levelSize;
    public float[] levelSizeAttr;

    void Start()
    {
        rb.maxAngularVelocity = 10000f;
    }

    void FixedUpdate()
    {
        float rbSpeed = rb.linearVelocity.magnitude;
        if (Mathf.Abs(moveInput.magnitude) > 0.1f)
        {
            if (rbSpeed < maxMoveSpeed)
            {
                Vector3 targetDir = new Vector3(moveInput.x, 0f, moveInput.y);
                if (rbSpeed > 0.1f && Vector3.Angle(targetDir, rb.linearVelocity) > 90f)
                {
                    rb.AddForce(-moveBrake * moveForce * rb.linearVelocity, ForceMode.Acceleration);
                    //rb.linearVelocity = Vector3.zero;
                }
                rb.AddForce(targetDir * moveForce, ForceMode.Acceleration);
            }
        }
        else if (rbSpeed > 0.1f)
        {
            rb.AddForce(-moveBrake * moveForce * rb.linearVelocity, ForceMode.Acceleration);
            //rb.linearVelocity = Vector3.zero;
        }

        float angleOffset = Vector3.SignedAngle(transform.forward, facing, Vector3.up);
        float springCoef = Mathf.InverseLerp(-180f, 180f, angleOffset) * 2f - 1;
        //float damperCoef = Mathf.InverseLerp(0f, rb.maxAngularVelocity, rb.angularVelocity.y);
        float finalCoef = torqueSprint * springCoef;

        //Vector3 finalTorque = Vector3.up * torqueSprint * springCoef + rb.angularVelocity * torqueDamper;
        Vector3 finalTorque = Vector3.up * finalCoef;

        if (Mathf.Abs(angleOffset) < 5f)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            rb.AddTorque(finalTorque, ForceMode.Acceleration);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();


        //TODO: temp
        moveInput *= -1;

        if (moveInput.magnitude > 0.1f)
        {
            facing = new Vector3(moveInput.x, 0f, moveInput.y);
        }

        Debug.Log(moveInput);
    }

    public void ThrowObjects()
    {
        DisableCollider();

        Vector3 dir = transform.forward;
        Vector3 force = dir * throwStrength;

        heroArmL.ThrowObjects(force);
        heroArmR.ThrowObjects(force);

        Invoke("EnableCollider", 2f);
    }

    public void ReleaseObject()
    {
        DisableCollider();

        heroArmL.ReleaseObjects();
        heroArmR.ReleaseObjects();

        Invoke("EnableCollider", 2f);
    }

    public void EnableCollider()
    {
        for (int i = 0; i < heroCols.Length; i++)
        {
            heroCols[i].enabled = false;
        }
    }

    public void DisableCollider()
    {
        for (int i = 0; i < heroCols.Length; i++)
        {
            heroCols[i].enabled = false;
        }
    }

    public void OnArmBroken(HeroArm.ArmType armType)
    {
        onArmBrokenEvent.Invoke(armType);

        switch (armType)
        {
            case HeroArm.ArmType.Left: onArmBrokenObjEvent?.Invoke(heroArmL); break;
            case HeroArm.ArmType.Right: onArmBrokenObjEvent?.Invoke(heroArmR); break;
        }
    }
     
    public void AddWeightLevel(int level)
    {
        levelWeightLifting += level;

        int usedLevel = levelWeightLifting;
        if (usedLevel > levelWeightLiftingAttr.Length - 1)
        {
            usedLevel = levelWeightLiftingAttr.Length - 1;
        }

        maxHoldingWeight = levelWeightLiftingAttr[usedLevel];
    }

    public void RemoveWeightLevel(int level)
    { 
        levelWeightLifting -= level;

        int usedLevel = levelWeightLifting;
        if (usedLevel < 0)
        {
            usedLevel = 0;
            Debug.LogWarning("Level less than zero!!");
        }

        maxHoldingWeight = levelWeightLiftingAttr[usedLevel];
    }

    public void AddStrengthLevel(int level)
    {
        levelThrowPower += level;

        int usedLevel = levelThrowPower;
        if (usedLevel > levelThrowPowerAttr.Length - 1)
        {
            usedLevel = levelThrowPowerAttr.Length - 1;
        }

        throwStrength = levelThrowPowerAttr[usedLevel];
    }

    public void RemoveStrengthLevel(int level)
    { 
        levelThrowPower -= level;

        int usedLevel = levelThrowPower;
        if (usedLevel < 0)
        {
            usedLevel = 0;
            Debug.LogWarning("Level less than zero!!");
        }

        throwStrength = levelThrowPowerAttr[usedLevel];
    }

    public void AddMoveSpeedLevel(int level)
    {
        levelMoveSpeed += level;

        int usedLevel = levelMoveSpeed;
        if (usedLevel > levelMoveSpeedAttr.Length - 1)
        {
            usedLevel = levelMoveSpeedAttr.Length - 1;
        }

        maxMoveSpeed = levelMoveSpeedAttr[usedLevel];
    }

    public void RemoveMoveSpeedLevel(int level)
    { 
        levelMoveSpeed -= level;

        int usedLevel = levelThrowPower;
        if (usedLevel < 0)
        {
            usedLevel = 0;
            Debug.LogWarning("Level less than zero!!");
        }

        maxMoveSpeed = levelMoveSpeedAttr[usedLevel];
    }

    public void AddSizeLevel(int level)
    {
        levelSize += level;

        int usedLevel = levelSize;
        if (usedLevel > levelSizeAttr.Length - 1)
        {
            usedLevel = levelSizeAttr.Length - 1;
        }

        transform.localScale = Vector3.one * levelSizeAttr[usedLevel];
    }

    public void RemoveSizeLevel(int level)
    { 
        levelSize -= level;

        int usedLevel = levelSize;
        if (usedLevel < 0)
        {
            usedLevel = 0;
            Debug.LogWarning("Level less than zero!!");
        }

        transform.localScale = Vector3.one * levelSizeAttr[usedLevel];
    }
}
