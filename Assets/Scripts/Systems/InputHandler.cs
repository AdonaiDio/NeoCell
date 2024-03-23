using UnityEngine;
using UnityEngine.InputSystem;



public class InputHandler : MonoBehaviour
{
    [SerializeField] private InputActionAsset Controls;
    [SerializeField] private string playerActionMapName = "Player";
    [SerializeField] private string menuActionMapName = "UI";
    
    [SerializeField] private string move = "Move";
    [SerializeField] private string aim = "Aim";
    [SerializeField] private string open = "OpenInventory";


    public Vector2 rotate;
    public InputAction moveAction;
    public InputAction aimAction;
    public InputAction inventoryMenuAction;


    public static InputHandler Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        moveAction = Controls.FindActionMap(playerActionMapName).FindAction(move);
        aimAction = Controls.FindActionMap(playerActionMapName).FindAction(aim);
        inventoryMenuAction = Controls.FindActionMap(menuActionMapName).FindAction(open);
        RegisterInputActions();

    }
    void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;
        aimAction.performed += context => AimInput = context.ReadValue<Vector2>();
        aimAction.canceled += context => AimInput = Vector2.zero;
        inventoryMenuAction.performed += context => InventoryMenuInput = true;       
        inventoryMenuAction.canceled += context => InventoryMenuInput = false;
        
    }

    private void OnEnable()
    {
        moveAction.Enable();
        aimAction.Enable();
        inventoryMenuAction.Enable();
    }
    private void OnDisable()
    {
        moveAction.Disable();
        aimAction.Disable();
        inventoryMenuAction.Disable();
    }
    
    public Vector2 MoveInput { get; private set; }

    public Vector2 AimInput { get; private set; }
    public bool InventoryMenuInput { get; private set; }
   
}
