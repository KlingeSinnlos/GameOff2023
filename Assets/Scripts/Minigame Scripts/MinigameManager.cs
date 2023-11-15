using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Z)) {
            StartMinigame();
         }
    }

    void StartMinigame() {
        Debug.Log("Minigame will be summoned (implementation needed)");
    }
}
