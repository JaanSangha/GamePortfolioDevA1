using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSlot : MonoBehaviour
{
    public bool isMax, isHalf, isQuarter, isFilled = false;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if (!gameManager.isHidden)
        {
            if (isMax)
            {
                GetComponent<Image>().color = new Color(0.275f, 0.008f, 0.4f, 1f);
            }
            else if (isHalf)
            {
                GetComponent<Image>().color = new Color(0.871f, 0.227f, 0.580f, 1f);
            }
            else if (isQuarter)
            {
                GetComponent<Image>().color = new Color(0.227f, 0.816f, 0.871f, 1f);
            }
        }
        else
            GetComponent<Image>().color = new Color(0.467f,0.710f,0.660f,1f);
    }
}
