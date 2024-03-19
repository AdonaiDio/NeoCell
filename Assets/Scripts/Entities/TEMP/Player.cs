using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    /*private CharacterController controller;
    private Controls controls;

    private UnityEngine.Vector2 playerMovement; // receive move inputs
    private UnityEngine.Vector2 aim; //receive aim inputs
    [SerializeField] private float moveSpeed = 5f;
    private GameObject body;
    public void Awake()
    {
        controller = GetComponent<CharacterController>();
        controls = new Controls();
        body = this.transform.Find("Body").gameObject;
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
    private void Update()
    {
        HandleInput();
        HandleMovement();
        //HandleRotation();
    }
    private void HandleInput()
    {
        playerMovement = controls.Player.Move.ReadValue<UnityEngine.Vector2>();
        aim = controls.Player.Aim.ReadValue<UnityEngine.Vector2>();
    }
    private void HandleMovement()
    {
        UnityEngine.Vector3 move = new UnityEngine.Vector3(playerMovement.x, 0, playerMovement.y);
        controller.Move(move * Time.deltaTime * moveSpeed);
    }*/

    //private void HandleRotation()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(aim);
    //    UnityEngine.Plane groundPlane = new UnityEngine.Plane(UnityEngine.Vector3.up, UnityEngine.Vector3.zero);
    //    float rayDistance;
    //    if (groundPlane.Raycast(ray, out rayDistance))
    //    {
    //        UnityEngine.Vector3 point = ray.GetPoint(rayDistance);
    //        LookAt(point);
    //    }
    //}
    //private void LookAt(UnityEngine.Vector3 lookPoint)
    //{
    //    UnityEngine.Vector3 heightCorrectedPoint = new UnityEngine.Vector3(lookPoint.x, transform.position.y, lookPoint.z);
    //    body.transform.LookAt(heightCorrectedPoint);
    //}

}
