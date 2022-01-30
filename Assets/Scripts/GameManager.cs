using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    int[] numbers = new int[6];
    int randomNum, pickedRow, pickedColumn;
    // Start is called before the first frame update
    void Start()
    {
        numbers = new int[] { 2, 7, 12, 17, 22, 27 };

        Vector3 tempPos = transform.position;

        gridArray = new GridSlot[gridSize, gridSize];
        for(int row=0;row<gridSize;row++)
        {
            for(int column=0; column < gridSize; column++)
            {
                gridArray[row, column] = Instantiate(gridSlot,tempPos, transform.rotation);
                gridArray[row, column].gameObject.transform.SetParent(canvas.transform);
                tempPos.y -= 50;
            }
            tempPos.y = transform.position.y;
            tempPos.x += 50;
        }
        SetResources();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetResources()
    {
        while (numOfMaxResources > 0)
        {
            randomNum = Random.Range(0, 6);
            pickedRow = numbers[randomNum];
            randomNum = Random.Range(0, 6);
            pickedColumn = numbers[randomNum];
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
                Debug.Log("set row: " + pickedRow + " column: " + pickedColumn);
            }
        }

    }
}
