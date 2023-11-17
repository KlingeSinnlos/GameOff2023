using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Character");
            if (player != null)
            {
                Collider2D playerCollider = player.GetComponent<Collider2D>();

                if (playerCollider != null && playerCollider.IsTouching(GetComponent<Collider2D>()))
                {
                    GameObject.FindGameObjectWithTag("DialogueUI").SetActive(true);
                }
            }

        }
    }
}
