using TMPro;
using UnityEngine;

public class TouchScreenDebugger : MonoBehaviour {
    [SerializeField]
    CharacterInput input = default;
    [SerializeField]
    TextMeshProUGUI fingerCountDisplay = default;

    protected void FixedUpdate() {
        fingerCountDisplay.text = input.touchCount.ToString();
    }
}
