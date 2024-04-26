using UnityEngine;

public class CharacterInput : MonoBehaviour {
    //public bool shouldContract { get; set; }
    //public bool shouldInflate { get; set; }
    //public bool shouldNormalize { get; set; }

    //PlayerInputActions inputActions;
    //protected void OnEnable() {
    //    inputActions = new PlayerInputActions();
    //    inputActions.Enable();

    //    inputActions.Player.Contract.started += IntentToBoost;
    //    inputActions.Player.Contract.canceled += StopToBoost;
    //    inputActions.Player.Inflate.started += IntentToBreak;
    //    inputActions.Player.Inflate.canceled += StopToBreak;
    //}


    //protected void OnDisable() {
    //    inputActions.Disable();

    //    inputActions.Player.Boost.started -= IntentToBoost;
    //    inputActions.Player.Boost.canceled -= StopToBoost;
    //    inputActions.Player.Break.started -= IntentToBreak;
    //    inputActions.Player.Break.canceled -= StopToBreak;
    //    inputActions.Player.Turn.started -= IntentToTurn;
    //    inputActions.Player.Turn.canceled -= StopToTurn;
    //}

    //void IntentToBoost(InputAction.CallbackContext context) => shouldContract = true;
    //void StopToBoost(InputAction.CallbackContext context) => shouldContract = false;
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
