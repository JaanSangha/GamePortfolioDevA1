using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSlot : MonoBehaviour
{
    public bool isMax, isHalf, isQuarter, isFilled = false;
    private GameManager gameManager;

    public int row, column;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        GetComponent<Image>().color = new Color(0.467f, 0.710f, 0.660f, 1f);
    }
    private void Update()
    {
        if (!gameManager.isHidden)
        {
            if (isMax)
            {
                SetMaxColor();
            }
            else if (isHalf)
            {
                SetHalfColor();
            }
            else if (isQuarter)
            {
                SetQuarterColor();
            }
        }
    }

    public void OnClicked()
    {
        if (gameManager.isExtracting)
        {
            gameManager.ExtractSlot(row, column);
        }
        else
        {
            gameManager.ScanReveal(row, column);
        }
    }

    public void SetMaxColor()
    {
        GetComponent<Image>().color = new Color(0.275f, 0.008f, 0.4f, 1f);
    }

    public void SetHalfColor()
    {
        GetComponent<Image>().color = new Color(0.871f, 0.227f, 0.580f, 1f);
    }

    public void SetQuarterColor()
    {
        GetComponent<Image>().color = new Color(0.227f, 0.816f, 0.871f, 1f);
    }

    public void SetBlankColor()
    {
        GetComponent<Image>().color = new Color(0.69f, 0.69f, 0.69f, 1f);
    }

    public void SetHitColor()
    {
        GetComponent<Image>().color = new Color(1.0f, 0.0f, 0.0f, 1f);
    }

    public void SetHiddenColor()
    {
        GetComponent<Image>().color = new Color(0.467f, 0.710f, 0.660f, 1f);
    }
}
