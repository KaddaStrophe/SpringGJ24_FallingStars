using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScene : MonoBehaviour {
    public void StartGame() {
        SceneManager.LoadScene("Game");
    }
}
