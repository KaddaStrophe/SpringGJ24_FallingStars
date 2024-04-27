using UnityEngine;

[CreateAssetMenu(fileName = "EC_CharacterChannel", menuName = "ScriptableObjects/EventChannel/CharacterChannel")]
public class CharacterChannel : ScriptableObject {

    public delegate void CharacterResizeCallback(CharacterMotor character);
    public CharacterResizeCallback OnCharacterResize;

    public void RaiseCharacterResize(CharacterMotor character) {
        OnCharacterResize?.Invoke(character);
    }
}