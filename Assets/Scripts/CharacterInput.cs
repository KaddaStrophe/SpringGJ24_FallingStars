using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour {
    public bool shouldCompress { get; set; }
    //public bool shouldInflate { get; set; }
    //public bool shouldNormalize { get; set; }

    PlayerInputActions inputActions;
    protected void OnEnable() {
        inputActions = new PlayerInputActions();
        inputActions.Enable();

        inputActions.Player.Compress.started += IntentToCompress;
        inputActions.Player.Compress.canceled += StopToCompress;
        //    inputActions.Player.Inflate.started += IntentToBreak;
        //    inputActions.Player.Inflate.canceled += StopToBreak;
    }


    protected void OnDisable() {
        inputActions.Disable();

        inputActions.Player.Compress.started -= IntentToCompress;
        inputActions.Player.Compress.canceled -= StopToCompress;
        //    inputActions.Player.Break.started -= IntentToBreak;
        //    inputActions.Player.Break.canceled -= StopToBreak;
        //    inputActions.Player.Turn.started -= IntentToTurn;
        //    inputActions.Player.Turn.canceled -= StopToTurn;
    }

    void IntentToCompress(InputAction.CallbackContext context) => shouldCompress = true;
    void StopToCompress(InputAction.CallbackContext context) => shouldCompress = false;
    //void IntentToBreak(InputAction.CallbackContext context) => shouldInflate = true;
    //void StopToBreak(InputAction.CallbackContext context) => shouldInflate = false;
    //void IntentToTurn(InputAction.CallbackContext context) {
    //    if (context.ReadValue<float>() < 0) {
    //        shouldTurnLeft = true;
    //    }
    //    if (context.ReadValue<float>() > 0) {
    //        shouldTurnRight = true;
    //    }
    //}
    //void StopToTurn(InputAction.CallbackContext context) {
    //    shouldTurnLeft = false;
    //    shouldTurnRight = false;
    //}
}
