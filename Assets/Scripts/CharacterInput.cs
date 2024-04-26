using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour {
    public bool shouldCompress { get; set; }
    public bool shouldInflate { get; set; }

    PlayerInputActions inputActions;
    protected void OnEnable() {
        inputActions = new PlayerInputActions();
        inputActions.Enable();

        inputActions.Player.Compress.started += IntentToCompress;
        inputActions.Player.Compress.canceled += StopToCompress;
        inputActions.Player.Inflate.started += IntentToInflate;
        inputActions.Player.Inflate.canceled += StopToInflate;
    }


    protected void OnDisable() {
        inputActions.Disable();

        inputActions.Player.Compress.started -= IntentToCompress;
        inputActions.Player.Compress.canceled -= StopToCompress;
        inputActions.Player.Inflate.started -= IntentToInflate;
        inputActions.Player.Inflate.canceled -= StopToInflate;
    }

    void IntentToCompress(InputAction.CallbackContext context) => shouldCompress = true;
    void StopToCompress(InputAction.CallbackContext context) => shouldCompress = false;
    void IntentToInflate(InputAction.CallbackContext context) => shouldInflate = true;
    void StopToInflate(InputAction.CallbackContext context) => shouldInflate = false;
}
