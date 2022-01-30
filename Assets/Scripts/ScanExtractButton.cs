using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScanExtractButton : MonoBehaviour
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
        if (gameManager.isExtracting)
        {
            text.text = "Scan";
        }
        else
        {
            text.text = "Extract";
        }
    }
}
