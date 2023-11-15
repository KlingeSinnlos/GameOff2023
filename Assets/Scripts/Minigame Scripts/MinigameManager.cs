using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour
{
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Z)) {
            StartMinigame();
         }
    }

    void StartMinigame() {
        SceneManager.LoadScene("DialogueMinigame");
        Debug.Log("Minigame will be summoned (implementation needed)");
    }
}
