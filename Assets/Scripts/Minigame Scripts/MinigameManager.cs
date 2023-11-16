using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MyBox;

public class MinigameManager : MonoBehaviour
{
    [ReadOnly]public int roundAmount;
    [ReadOnly]public int roundCurrent;
    [ReadOnly] public List<Examination.Baggage> baggageInScene;
    public Examination examination;

    private void Update()
    {
         if (Input.GetKeyDown(KeyCode.Z)) {
            SceneManager.LoadScene("DialogueMinigame");
        }
    }
    [ButtonMethod]
    private void StartMinigame() {
        Debug.Log("Minigame will be summoned (implementation needed)");
        baggageInScene.Add(examination.AddBaggage(examination.rounds[0].baggageToBeAdded[0]));
    }
}
