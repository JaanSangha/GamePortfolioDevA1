using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HideUnhideButton : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;
    private GameManager gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        if (gameManager.isHidden)
        { 
            text.text = "Unhide"; 
        }
        else
        {
            text.text = "Hide";
        }
    }
}
