using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

using System.Xml.XPath;
using Unity.VisualScripting;



public class Cell : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float HPMax; //Max HP
    [SerializeField] private GameObject body;

    private float HP; //Used to store current HP

    [SerializeField] private Camera camera;

    [SerializeField] private Weapon currentWeapon;


    [SerializeField] private float shootTimer = 0.5f;
    [SerializeField] private float currentExperience, maxExperience, level;
    public float experienceToFillBar; // for XP bar calc



    private CharacterController controller;
    private Controls controls;


    private UnityEngine.Vector2 playerMovement; // receive move inputs
    private UnityEngine.Vector2 aim; //receive aim inputs
    private UnityEngine.Vector3 playerVelocity; //physics handling







    public void Awake()
    {
        controller = GetComponent<CharacterController>();
        controls = new Controls();

        HP = HPMax; //Start at max HP
    }
    private void OnEnable()
    {
        controls.Enable();
        //Receive XP change from Experience Manager
    }
    private void OnDisable()
    {
        controls.Disable();

    }


    void Update()
    {
        if (HP <= 0)
        {

            //Application.Quit(); //Close App

            UnityEditor.EditorApplication.isPlaying = false; //Close editor
        }
        HandleInput();
        HandleMovement();
        HandleRotation();



        currentWeapon.Shoot();


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
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    }

    private void HandleRotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(aim);
        UnityEngine.Plane groundPlane = new UnityEngine.Plane(UnityEngine.Vector3.up, UnityEngine.Vector3.zero);
        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            UnityEngine.Vector3 point = ray.GetPoint(rayDistance);
            LookAt(point);
        }
    }
    private void LookAt(UnityEngine.Vector3 lookPoint)
    {
        UnityEngine.Vector3 heightCorrectedPoint = new UnityEngine.Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        body.transform.LookAt(heightCorrectedPoint);


    }

    public void LoseHP()
    {
        HP--;
        float hpToFillBar = HP / HPMax;
        Events.onHpLostCell.Invoke(hpToFillBar);
        //hpToFillBar = HP/HPMax //Variable used to fill on HPBarCell

    }
}



