using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace POWL.Input
{
    /// <summary>
    /// Handles input for both Mouse (PC) and Touch (iOS) using the New Input System.
    /// </summary>
    public class InputHandler : MonoBehaviour
    {
        public static InputHandler Instance { get; private set; }

        public event Action<Vector2> OnInteract;

        private InputAction _pressAction;
        private InputAction _positionAction;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            SetupInputActions();
        }

        private void OnEnable()
        {
            _pressAction.Enable();
            _positionAction.Enable();
        }

        private void OnDisable()
        {
            _pressAction.Disable();
            _positionAction.Disable();
        }

        private void SetupInputActions()
        {
            _pressAction = new InputAction(type: InputActionType.Button);
            _pressAction.AddBinding("<Mouse>/leftButton");
            _pressAction.AddBinding("<Touchscreen>/primaryTouch/press");

            _positionAction = new InputAction(type: InputActionType.Value, expectedControlType: "Vector2");
            _positionAction.AddBinding("<Mouse>/position");
            _positionAction.AddBinding("<Touchscreen>/primaryTouch/position");

            _pressAction.performed += ctx => HandlePress();
        }

        private void HandlePress()
        {
            Vector2 screenPos = _positionAction.ReadValue<Vector2>();

            OnInteract?.Invoke(screenPos);
        }
    }
}