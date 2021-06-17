using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public event System.Action OnNewLevelLoaded;

    float currentLevel = 1;
    public float CurrentLevel
    {
        get
        {
            return currentLevel;
        }
    }

    [SerializeField]
    Image imageToMatch;
    [SerializeField]
    IceCreamMachine machine;
    

    [SerializeField]
    Level[] levels = new Level[8];
    Level loadedLevel;

    List<int> targetOrderOfPieces;              //This is a list of 1's and 2's generated in this script based
                                                //on the data from current Level.
                                                //We compare this to ordrOfpieces list that we generated in
                                                //the IceCreamMachine script.

    private void Start()
    {
        LoadLevel();
    }


    public void LoadLevel()
    {
        //This method is called once at the start of the game and then by the "Next Level" button.

        OnNewLevelLoaded?.Invoke();
        currentLevel++;
        machine.ResetMachine();
        
        loadedLevel = levels[Random.Range(0, levels.Length)];       // keeps loading random levels from the list of
                                                                    // 8 levels we set in inspector. This can go on 
                                                                    // for infinite number of levels. We can add new
                                                                    // levels by simply adding more elements to the 
                                                                    // levels array in the inspector.
        imageToMatch.sprite = loadedLevel.imageToMatch;
    }


    public int CalculateMatchPercentage()
    {
        // This method generates a list of 1's and 2's based on the percentages of ice cream types in the Level
        // data type. Then, this list is compared with a similar list from IceCreamMachine.
        // The number of pieces and the order of pieces are both taken into account.

        targetOrderOfPieces = new List<int>();
        float matchPercentage = 0;
        float step = 100f / machine.orderOfPieces.Count;


        int targetNumberOfBlueIceCream = (int)((machine.orderOfPieces.Count * loadedLevel.blueCreamPercentage) / 100);
        int targetNumberOfGreenIceCream =  (int)(machine.orderOfPieces.Count * loadedLevel.greenCreamPercentage) / 100;

        if (loadedLevel.firstIceCream == Level.IceCreamTypes.blue)
        {
            for (int j = 0; j < targetNumberOfBlueIceCream; j++)
            {
                targetOrderOfPieces.Add(1); // 1 == blue
            }
            for (int k = targetNumberOfBlueIceCream; k < machine.orderOfPieces.Count; k++)
            {
                targetOrderOfPieces.Add(2); // 2 == green
            }
        }
        if (loadedLevel.firstIceCream == Level.IceCreamTypes.green)
        {
            for (int l = 0; l < targetNumberOfGreenIceCream; l++)
            {
                targetOrderOfPieces.Add(2); // 2 == green
            }
            for (int m = targetNumberOfGreenIceCream; m < machine.orderOfPieces.Count; m++)
            {
                targetOrderOfPieces.Add(1); // 1 == blue
            }
        }


        //comparing
        for (int i = 0; i < targetOrderOfPieces.Count; i++)
        {
            if (targetOrderOfPieces[i] == machine.orderOfPieces[i])
            {
                matchPercentage += step;
            }
        }

        return (int)matchPercentage;
    }
}
