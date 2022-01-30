using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSlot : MonoBehaviour
{
    public bool isMax = false;
    public bool isHalf = false;
    public bool isQuarter = false;
    public bool isFilled = false;

    private void Update()
    {
        if(isMax)
        {
            GetComponent<Image>().color = Color.red;
        }
        else if(isHalf)
        {
            GetComponent<Image>().color = Color.green;
        }
        else if(isQuarter)
        {
            GetComponent<Image>().color = Color.cyan;
        }
    }
}
