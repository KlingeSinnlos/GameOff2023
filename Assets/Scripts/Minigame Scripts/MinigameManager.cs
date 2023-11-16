using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour
{
    private void Update()
    {
         if (Input.GetKeyDown(KeyCode.Z)) {
            StartMinigame();
         }
    }

    private void StartMinigame() {
        SceneManager.LoadScene("DialogueMinigame");
        Debug.Log("Minigame will be summoned (implementation needed)");
    }
}
