using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController cc;
    PlayerInput P_Input;
    public float MaxSpeed;

    public float GravityDownForce = 20f;//why 20?
    public float MaxSpeedInAir = 10f;
    public float MovementSharpnessOnGround = 15;
    public float AccelSpeedInAir = 25f;

    public Vector3 FSprintMod; // = new Vector3(1, 1, 2);

    public float JumpForce = 9f;
    public LayerMask GroundCheckLayers = -1; 
    public float GroundCheckDistance = 0.05f;
    public float LastTimeJumped;
    const float k_JumpGroundingPreventionTime = 0.2f;
    const float k_GroundCheckDistanceInAir = 0.07f;
    Vector3 GroundNormal;
    Vector3 CharacterVelocity;
    public bool IsGrounded { get; private set; }
    public bool HasJumpedThisFrame { get; private set; }

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        P_Input = GetComponent<PlayerInput>();
    }
    void Update()
    {
        HasJumpedThisFrame = false;
        bool wasGrounded = IsGrounded;
        GroundCheck();

        //landing
        //variation of late update?
        if (IsGrounded && !wasGrounded)
        {
            //fall damage 
            //sound effects
        }
        HandleCharMovement();
    }

    private void GroundCheck()
    {
        //make ground check in air small prevent snap to ground
        //double check because skin width is very small in general
        float chosenGroundCheckDistance = 
            IsGrounded ? (cc.skinWidth + GroundCheckDistance) : k_GroundCheckDistanceInAir;

        //reset values before check
        IsGrounded = false;
        GroundNormal = Vector3.up;

        //prevent pre-grounding
        if (Time.time >= LastTimeJumped + k_JumpGroundingPreventionTime)
        {
            //if grounded get ground normal w capsule cast
            //could I use a radius/diameter circle thing?
            if (Physics.CapsuleCast(GetCapBottomHemi(), GetCapTopHemi(cc.height), cc.radius, Vector3.down, out RaycastHit hit, chosenGroundCheckDistance, GroundCheckLayers, QueryTriggerInteraction.Ignore))
            {
                //upward direction from surface
                GroundNormal = hit.normal;

                //if ground normal = character up and slope lower than controlers hit
                if (Vector3.Dot(hit.normal, transform.up)> 0f && IsNormalUnderSlopeLimit(GroundNormal))
                {
                    IsGrounded = true;
                    
                    //ground snapping
                    if (hit.distance> cc.skinWidth)
                    {
                        cc.Move(Vector3.down * hit.distance);
                    }
                }
            }
        }

    }
    Vector3 GetCapBottomHemi()
    {
        return transform.position + (transform.up * cc.radius);
    }
    Vector3 GetCapTopHemi(float atHeight)
    {
        return transform.position + (transform.up * (atHeight - cc.radius));
    }
    bool IsNormalUnderSlopeLimit(Vector3 normal)
    {
        return Vector3.Angle(transform.up, normal) <= cc.slopeLimit;
    }
    public Vector3 GetDirReorientOnSlope(Vector3 direction, Vector3 slopeNormal)
    {
        Vector3 directionRight = Vector3.Cross(direction, transform.up);
        return Vector3.Cross(slopeNormal, directionRight).normalized;
    }

    void HandleCharMovement()
    {
        bool isSprinting = P_Input.GetSprintInput();

        //float speedModifier = isSprinting ? SprintSpeedModifier : 1f;
        Vector3 forwardSprint = isSprinting ? FSprintMod : new Vector3(1, 1, 1);
        //convert move input to worldspace
        Vector3 worldspaceMoveInput = transform.TransformVector(Vector3.Scale(P_Input.GetMovementInputs(), forwardSprint));

        //grounded
        if (IsGrounded)
        {
            Vector3 targetVelocity = worldspaceMoveInput * MaxSpeed; // * speedModifier;
            targetVelocity = GetDirReorientOnSlope(targetVelocity.normalized, GroundNormal) * targetVelocity.magnitude;

            //interp between current velocity and target velocity
            CharacterVelocity = Vector3.Lerp(CharacterVelocity, targetVelocity, MovementSharpnessOnGround * Time.deltaTime);

            //jumping
            if (IsGrounded && P_Input.GetJumpInput())
            {
                //crouching and obstruction
                //if (setCrouchingState(false,false))

                //cancel vertical velocity
                //FIX can this be vec(1,0,1)?
                CharacterVelocity = new Vector3(CharacterVelocity.x, 0f, CharacterVelocity.z);

                //add jumpforce
                CharacterVelocity += Vector3.up * JumpForce;

                //remember jump for snapping
                LastTimeJumped = Time.time;
                HasJumpedThisFrame = true;

                //force grounding to false
                IsGrounded = false;
                GroundNormal = Vector3.up;
            }
        }
        //handle air movement
        else
        {
            CharacterVelocity += worldspaceMoveInput * AccelSpeedInAir * Time.deltaTime;

            //limit horizontal air speed
            float verticalVelocity = CharacterVelocity.y;
            Vector3 horizontalVelocity = Vector3.ProjectOnPlane(CharacterVelocity, Vector3.up);
            horizontalVelocity = Vector3.ClampMagnitude(horizontalVelocity, MaxSpeedInAir);
            CharacterVelocity = horizontalVelocity + (Vector3.up * verticalVelocity);

            //apply gravity
            CharacterVelocity += Vector3.down * GravityDownForce * Time.deltaTime;
        }

        //apply velocity as char movement
        Vector3 capBottomBeforeMove = GetCapBottomHemi();
        Vector3 CapTopBeforeMove = GetCapTopHemi(cc.height);
        cc.Move(CharacterVelocity * Time.deltaTime);
    }
}
