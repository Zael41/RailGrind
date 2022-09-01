using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float sprintMultiplier = 1.5f;

    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;
    public Camera cam;

    private float fovTargetNormal = 60f;
    private float fovTargetSprint = 80f;

    private GameObject currentRail;

    Vector3 velocity;
    bool isGrounded;
    bool isSprinting;
    bool railGrinding;

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
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = Vector3.zero;
        move = transform.right * x + transform.forward * z;
        MovementControl(move);
        currentDirection = characterController.velocity;
        JumpControl();
    }

    void MovementControl(Vector3 move)
    {
        Physics.SyncTransforms();
        if (railGrinding)
        {
            /*float distanceToStart = Vector3.Distance(transform.position, currentRail.transform.GetChild(0).transform.position);
            float distanceToEnd = Vector3.Distance(transform.position, currentRail.transform.GetChild(1).transform.position);
            if (distanceToStart < distanceToEnd) targetPoint = 0;
            else targetPoint = 1;*/
            transform.position = Vector3.MoveTowards(transform.position, currentRail.transform.GetChild(targetPoint).transform.position, 25 * Time.deltaTime);
            if (Vector3.Distance(transform.position, currentRail.transform.GetChild(targetPoint).transform.position) < 0.5f)
            {
                railGrinding = false;
                //GetComponent<CharacterController>().enabled = true;
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

    void JumpControl()
    {
        if (railGrinding) return;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
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
        //GetComponent<CharacterController>().enabled = false;
    }
}
