using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour {
    public bool shouldCompress { get; set; }
    public bool shouldInflate { get; set; }

    public int touchCount = 0;

    PlayerInputActions inputActions;
    protected void OnEnable() {
        inputActions = new PlayerInputActions();
        inputActions.Enable();

        inputActions.Player.Compress.started += IntentToCompress;
        inputActions.Player.Compress.canceled += StopToCompress;
        inputActions.Player.Inflate.started += IntentToInflate;
        inputActions.Player.Inflate.canceled += StopToInflate;
        //inputActions.Player.TouchInteract.started += IntentToTouch;
        //inputActions.Player.TouchInteract.canceled += StopToTouch;
    }

    //private void StopToTouch(InputAction.CallbackContext context) => throw new NotImplementedException();
    //private void IntentToTouch(InputAction.CallbackContext context) => throw new NotImplementedException();

    protected void OnDisable() {
        inputActions.Disable();

        inputActions.Player.Compress.started -= IntentToCompress;
        inputActions.Player.Compress.canceled -= StopToCompress;
        inputActions.Player.Inflate.started -= IntentToInflate;
        inputActions.Player.Inflate.canceled -= StopToInflate;
    }

    //void IntentToCompress(InputAction.CallbackContext context) => shouldCompress = true;
    void IntentToCompress(InputAction.CallbackContext context) {
        touchCount = Input.touchCount;
        shouldCompress = true;
    }
    void StopToCompress(InputAction.CallbackContext context) {
        touchCount = Input.touchCount;
        shouldCompress = false;
    }
    void IntentToInflate(InputAction.CallbackContext context) {
        touchCount = Input.touchCount;
        shouldInflate = true;
    }
    void StopToInflate(InputAction.CallbackContext context) {
        touchCount = Input.touchCount;
        shouldInflate = false;
    }
}
