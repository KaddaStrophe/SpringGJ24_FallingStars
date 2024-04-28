using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField]
    CharacterChannel characterEventChannel = default;
    [SerializeField]
    CharacterMotor character = default;

    public void StartGame() {
        characterEventChannel.RaiseCharacterStartMove(character);
    }

}
