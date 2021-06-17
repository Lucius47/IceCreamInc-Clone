using UnityEngine;
using DG.Tweening;
using BezierSolution;
using System.Collections.Generic;

public class IceCreamMachine : BezierWalkerWithSpeed
{
    public event System.Action OnLevelCompleted;        //Subscribed to by the LevelManager to show lvlCompleted popup.


    [SerializeField]
    Transform creamFilter;                              //Transform from where the ice cream pieces instantiate.
    [SerializeField]
    Transform iceCreamCone;                             //This is used as a parent transform for all the ice cream pieces generated.
    

    GameObject iceCreamPrefab;                          //which ice cream to generate. This is set by the GenerateCreamButton.
    GameObject iceCreamGO;


    [HideInInspector]
    public List<int> orderOfPieces;                     // 1 == blue piece, 2 == green piece


    bool iceCreamFinished = false;
    bool machineIsRunning = false;

    
    public float durationOfFallingPiece = 2.5f;
    public float movementSpeedOfMachine = 1.5f;

    float initialHeight;                                //Ice cream machine's y position is set to this every frame.
                                                        //otherwise, it follows along the curve.


    void Start()
    {
        initialHeight = transform.position.y;
    }


    protected override void Update()
    {
        transform.position = new Vector3(transform.position.x, initialHeight, transform.position.z);

        if (machineIsRunning && !iceCreamFinished)
        {
            base.Update();                              //Machine's movement occurs in base Update. We need this
                                                        //IF statement to control the movement.
            
            transform.position = new Vector3(transform.position.x, initialHeight, transform.position.z);
            
        }
        else
        {
            return;
        }
    }

    private void FixedUpdate()
    {
        //Ice cream pieces are generated in FixedUpdate, each with exactly 20ms delay between them. This way, 
        //the ice cream looks the same regardless of framerate.

        if(iceCreamPrefab != null && machineIsRunning && !iceCreamFinished)
        {
            CountPieces(iceCreamPrefab);

            iceCreamGO = Instantiate(iceCreamPrefab, creamFilter.position, transform.rotation);

            iceCreamGO.transform.DOMove((spline.GetPoint(NormalizedT)), durationOfFallingPiece);

            iceCreamGO.transform.SetParent(iceCreamCone);

        }



    }
    public void StartIceCream(GameObject _iceCreamPrefab)
    {
        iceCreamPrefab = _iceCreamPrefab;
        machineIsRunning = true;
    }

    public void StopIceCream()
    {
        iceCreamPrefab = null;
        machineIsRunning = false;
    }

    public void BezierWalkerPathCompleted()
    {
        //Called by the OnPathCompleted() in inspector of BezierWalkerWithSpeed.

        iceCreamFinished = true;
        OnLevelCompleted?.Invoke();
    }

    public void ResetMachine()
    {
        orderOfPieces = new List<int>();
        NormalizedT = 0;
        iceCreamFinished = false;

        foreach (Transform child in iceCreamCone)
        {
            Destroy(child.gameObject);                  // Destory all previous ice cream pieces.
                                                        // When the "Next Level" button is pressed within
                                                        // 3 seconds after the level is completed, the ice cream 
                                                        // pieces are destroyed before their Tween is finished.
                                                        // This causes DGTween to issue a low-level warning.
                                                        // However it does not have any significant effect.
        }
    }

    void CountPieces(GameObject _iceCreamPiece)
    {
        // Add 1 to list for blue ice cream piece and 2 for green piece.
        // We can add more entries like 3, 4, 5 and so on for more ice cream types.
        // We could also use strings instead of ints.

        if (_iceCreamPiece.CompareTag("BlueIceCreamPiece"))
        {
            orderOfPieces.Add(1);
        }
        if (_iceCreamPiece.CompareTag("GreenIceCreamPiece"))
        {
            orderOfPieces.Add(2);
        }
    }

}
