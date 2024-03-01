using UnityEngine;
using UnityEngine.InputSystem;



    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private InputActionAsset Controls;
        [SerializeField] private string actionMapName = "Player";
        [SerializeField] private string move = "Move";
        [SerializeField] private string aim = "Aim";
        public Vector2 rotate;
        private InputAction moveAction;
        private InputAction aimAction;

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
            moveAction = Controls.FindActionMap("Player").FindAction("Move");
            aimAction = Controls.FindActionMap("Player").FindAction("Aim");
            RegisterInputActions();

        }
        void RegisterInputActions()
        {
            moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
            moveAction.canceled += context => MoveInput = Vector2.zero;
            aimAction.performed += context => AimInput = context.ReadValue<Vector2>();
            aimAction.canceled += context => AimInput = Vector2.zero;
        }

        private void OnEnable()
        {
            moveAction.Enable();
            aimAction.Enable();
        }
        private void OnDisable()
        {
            moveAction.Disable();
            aimAction.Disable();
        }
        public Vector2 MoveInput { get; private set; }

        public Vector2 AimInput { get; private set; }
    }
