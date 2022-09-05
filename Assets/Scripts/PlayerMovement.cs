using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float dashSpeed = 15f;
    public float dashTime = 0.5f;
    public float sprintMultiplier = 1.5f;

    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;
    public Camera cam;

    private float fovTargetNormal = 60f;
    private float fovTargetSprint = 80f;

    private GameObject currentRail;

    Vector3 velocity;
    Vector3 upAxis;
    bool isGrounded;
    bool isSprinting;
    bool isDashing;
    bool railGrinding;
    bool doubleJump;

    Vector3 currentDirection;
    int targetPoint;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        upAxis = new Vector3(0f, -gravity, 0f);
        ChangeGravity();
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if ((isGrounded && velocity.y < 0 && Mathf.Sign(gravity) == -1) || (isGrounded && velocity.y > 0 && Mathf.Sign(gravity) == 1))
        {
            velocity.y = 2f * -upAxis.normalized.y;
            doubleJump = true;
            isDashing = false;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = Vector3.zero;
        move = transform.right * x + transform.forward * z;
        MovementControl(move);
        currentDirection = characterController.velocity;
        AirControl();
    }

    void ChangeGravity()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            gravity = -gravity;
            this.transform.Translate(Vector3.up * 1.75f, Space.Self);
            this.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
        }
    }

    void MovementControl(Vector3 move)
    {
        Physics.SyncTransforms();
        if (railGrinding)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentRail.transform.GetChild(targetPoint).transform.position, 25 * Time.deltaTime);
            if (Vector3.Distance(transform.position, currentRail.transform.GetChild(targetPoint).transform.position) < 0.5f)
            {
                railGrinding = false;
                velocity.y = upAxis.normalized.y * Mathf.Sqrt(jumpHeight * 2 * -upAxis.normalized.y * gravity);
                doubleJump = true;
                isDashing = false;
            }
            return;
        }
        if (isSprinting && isGrounded)
        {
            //cam.fieldOfView = 90f;
            if (cam.fieldOfView < fovTargetSprint)
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fovTargetSprint, 10 * Time.deltaTime);
            }
            characterController.Move(move * speed * Time.deltaTime * sprintMultiplier);
        }
        else
        {
            if (cam.fieldOfView > fovTargetNormal)
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fovTargetNormal, 10 * Time.deltaTime);
            }
            characterController.Move(move * speed * Time.deltaTime);
        }
    }

    void AirControl()
    {
        if (railGrinding) return;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = upAxis.normalized.y * Mathf.Sqrt(jumpHeight * 2 * -upAxis.normalized.y * gravity);
        }

        if (Input.GetButtonDown("Jump") && !isGrounded && doubleJump)
        {
            velocity.y = upAxis.normalized.y * Mathf.Sqrt(jumpHeight * 2 * -upAxis.normalized.y * gravity);
            doubleJump = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && !isGrounded)
        {
            isDashing = true;
            StartCoroutine(dashCoroutine());
        }
        
        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }

    private IEnumerator dashCoroutine()
    {
        velocity.x = this.transform.forward.x * dashSpeed;
        velocity.z = this.transform.forward.z * dashSpeed;
        yield return new WaitForSeconds(dashTime);
        velocity.x -= velocity.x;
        velocity.z -= velocity.z;
        yield break;
    }

    public void RailGrind(GameObject collider)
    {
        currentRail = collider;
        targetPoint = -1;
        Vector3 forwardRail = currentRail.transform.GetChild(0).transform.position - currentRail.transform.GetChild(1).transform.position;
        Vector3 backwardsRail = currentRail.transform.GetChild(1).transform.position - currentRail.transform.GetChild(0).transform.position;
        Debug.Log(Vector3.Angle(currentDirection, forwardRail));
        if (Mathf.Abs(Vector3.Angle(currentDirection, forwardRail)) < Mathf.Abs(Vector3.Angle(currentDirection, backwardsRail)))
        {
            targetPoint = 0;
        }
        else
        {
            targetPoint = 1;
        }
        railGrinding = true;
    }
}
