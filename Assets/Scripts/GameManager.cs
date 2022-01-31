using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GridSlot gridSlot;
    [SerializeField]
    private GridSlot[,] gridArray;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private int gridSize = 32;
    [SerializeField]
    private int numOfMaxResources = 8;
    private int[] acceptableNumbers = new int[6];
    private int randomNum, pickedRow, pickedColumn;
    public bool isHidden = false;
    public bool isExtracting = false;
    private int scanAttempts = 6;
    private int extractAttempts = 3;
    private int score = 0;
    public TMP_Text scoreText;
    public TMP_Text scansLeft;
    public TMP_Text extractsLeft;
    public TMP_Text messageBox;
    public GameObject restartButton;


    void Start()
    {
        acceptableNumbers = new int[] { 2, 7, 12, 17, 22, 27 };

        Vector3 tempPos = transform.position;

        //create tile grid
        gridArray = new GridSlot[gridSize, gridSize];

        for(int row=0;row<gridSize;row++)
        {
            for(int column=0; column < gridSize; column++)
            {
                gridArray[row, column] = Instantiate(gridSlot,tempPos, transform.rotation);
                gridArray[row, column].gameObject.transform.SetParent(canvas.transform);
                gridArray[row, column].row = row;
                gridArray[row, column].column = column;
                tempPos.y -= 50;
            }
            tempPos.y = transform.position.y;
            tempPos.x += 50;
        }

        SetResources();
        isHidden = true;
        //set UI texts
        messageBox.text = "You Are in Scan Mode!";
        scoreText.text = "Resources Gathered: " + score;
        extractsLeft.text = "Extracts Left: " + extractAttempts;
        scansLeft.text = "Scans Left: " + scanAttempts;
    }

    public void OnHideUnhideClick()
    {
        if (extractAttempts > 0)
        {
            isHidden = !isHidden;
            if (isHidden)
            {
                for (int row = 0; row < gridSize; row++)
                {
                    for (int column = 0; column < gridSize; column++)
                    {
                        gridArray[row, column].SetHiddenColor();
                    }
                }
            }
        }
    }

    public void OnScanExtractClick()
    {
        if (extractAttempts > 0)
        {
            if (isExtracting)
            {
                messageBox.text = "You Are in Scan Mode!";
            }
            else
            {
                messageBox.text = "You Are in Extract Mode!";
            }

            isExtracting = !isExtracting;
        }
    }

    public void OnRestartClicked()
    {
        SceneManager.LoadScene("Minigame");
    }

    void SetResources()
    {
        while (numOfMaxResources > 0)
        {
            randomNum = Random.Range(0, 6);
            pickedRow = acceptableNumbers[randomNum];
            randomNum = Random.Range(0, 6);
            pickedColumn = acceptableNumbers[randomNum];
            if(!gridArray[pickedRow, pickedColumn].isFilled)
            {
                gridArray[pickedRow, pickedColumn].isFilled = true;

                // set max resource
                gridArray[pickedRow, pickedColumn].isMax = true;

                // set half resources
                for(int row=pickedRow-1;row<pickedRow+2;row++)
                {
                    for(int column = pickedColumn-1;column<pickedColumn+2;column++)
                    {
                        gridArray[row, column].isHalf = true;
                    }
                }

                // set quarter resources
                for (int row = pickedRow - 2; row < pickedRow + 3; row++)
                {
                    for (int column = pickedColumn - 2; column < pickedColumn + 3; column++)
                    {
                        gridArray[row, column].isQuarter = true;
                    }
                }
                numOfMaxResources--;
            }
        }
    }

    public void ScanReveal(int rowRef, int columnRef)
    {
        if (scanAttempts > 0)
        {
            //find surrounding tiles to reveal
            for (int row = rowRef - 1; row < rowRef + 2; row++)
            {
                for (int column = columnRef - 1; column < columnRef + 2; column++)
                {
                    if (row < 0 || row > 31 || column < 0 || column > 31)
                    {

                    }
                    else
                    {
                        if (gridArray[row, column].isMax)
                        {
                            gridArray[row, column].SetMaxColor();
                        }
                        else if (gridArray[row, column].isHalf)
                        {
                            gridArray[row, column].SetHalfColor();
                        }
                        else if (gridArray[row, column].isQuarter)
                        {
                            gridArray[row, column].SetQuarterColor();
                        }
                        else
                        {
                            gridArray[rowRef, columnRef].SetBlankColor();
                        }
                    }

                }
            }
            scanAttempts--;
            scansLeft.text = "Scans Left: " + scanAttempts;
            messageBox.text = "Scan Succesful";
        }
    }

    public void ExtractSlot(int rowRef, int columnRef)
    {
        if (extractAttempts > 0)
        {
            if (gridArray[rowRef, columnRef].isMax)
            {
                gridArray[rowRef, columnRef].SetHitColor();
                gridArray[rowRef, columnRef].isMax = false;
                gridArray[rowRef, columnRef].isHalf = false;
                gridArray[rowRef, columnRef].isQuarter = false;
                score += 2000;
                messageBox.text = "Hit Max Resource!";

            }
            else if (gridArray[rowRef, columnRef].isHalf)
            {
                gridArray[rowRef, columnRef].SetHitColor();
                gridArray[rowRef, columnRef].isMax = false;
                gridArray[rowRef, columnRef].isHalf = false;
                gridArray[rowRef, columnRef].isQuarter = false;
                score += 1000;
                messageBox.text = "Hit Half Resource!";
            }
            else if (gridArray[rowRef, columnRef].isQuarter)
            {
                gridArray[rowRef, columnRef].SetHitColor();
                gridArray[rowRef, columnRef].isMax = false;
                gridArray[rowRef, columnRef].isHalf = false;
                gridArray[rowRef, columnRef].isQuarter = false;
                score += 500;
                messageBox.text = "Hit Quarter Resource!";
            }
            else
            {
                gridArray[rowRef, columnRef].SetBlankColor();
                messageBox.text = "No Resource Found!";
            }

            for (int row = rowRef - 2; row < rowRef + 3; row++)
            {
                for (int column = columnRef - 2; column < columnRef + 3; column++)
                {
                    if (gridArray[row, column].isMax)
                    {
                        gridArray[row, column].isMax = false;
                        gridArray[row, column].isHalf = true;
                        gridArray[row, column].SetHalfColor();
                    }
                    else if (gridArray[row, column].isHalf)
                    {
                        gridArray[row, column].isHalf = false;
                        gridArray[row, column].isQuarter = true;
                        gridArray[row, column].SetQuarterColor();
                    }
                    else if (gridArray[row, column].isQuarter)
                    {
                        gridArray[row, column].isQuarter = false;
                        gridArray[row, column].SetBlankColor();
                    }
                }
            }
            extractAttempts--;
            scoreText.text = "Resources Gathered: " + score;
            extractsLeft.text = "Extracts Left: " + extractAttempts;
            //end game
            if(extractAttempts <1)
            {
                scoreText.text = " ";
                messageBox.text = "In Total You Gathered " + score+ " Resources";
                restartButton.SetActive(true);
            }
        }
    }
}
