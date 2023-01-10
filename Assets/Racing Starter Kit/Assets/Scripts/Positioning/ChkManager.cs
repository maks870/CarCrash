using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChkManager : MonoBehaviour
{ 
    [SerializeField] private Text PositionDisplay; 
    [SerializeField] private RaceFinish raceFinish;  
    [SerializeField] private Text LapCounter;
    [SerializeField] private Transform cars;
    private int LapsSelected;

    
    public static List<int> nChk = new List<int>();//player values & car pos game object
    public static List<int> nLapsP = new List<int>();
    public static List<double> nDistP = new List<double>();
    public static List<GameObject> UnsortedCarPosList = new List<GameObject>();
    public static List<GameObject> CarPosList = new List<GameObject>();
    public  List<GameObject> PublicCarPosList = new List<GameObject>();
    public static List<double> scoreP = new List<double>();
    public static List<int> pNum = new List<int>();
    public static int posMax;
    public static int nBots;
    private bool added = false;

    //IMPORTANT NOTE: if you change the player's car, remember to have a gameobject called CarPos
    //If you change or add AI bots cars, those need a gameobject called CarPosAI with a box collider and IsTrigger checked
    //AI cars need to have it's number next to CarPosAI name in the gameobject. (i.e: CarPosAI9, CarPosAI10, and so on)
    //Each of these game objects needs a box collider with the IsTrigger option checked

    private GameObject[] UnparentChks;

    private void Awake()
    {
        nBots = cars.childCount - 1;
    }

    public void Start()
    {
        scoreP.Clear();
        pNum.Clear();
        nChk.Clear();
        nDistP.Clear();
        nLapsP.Clear();
        UnsortedCarPosList.Clear();
        CarPosList.Clear();
        PublicCarPosList.Clear();
        //when the race starts:
        ChkTrigger.startDis = false;//distance isn't measured because no one has passed checkpoints yet
        PositionDisplay.text = "--";//so we won't show player's position until it triggers a checkpoint


        foreach (Transform go in cars)
        {
            GameObject car = go.GetComponentInChildren<ChkTrigger>().gameObject;
            UnsortedCarPosList.Add(car);
        }

        for (int i = 0; i < UnsortedCarPosList.Count; i++)
        {
            added = false;
            for (int j = 0; j < CarPosList.Count; j++)
            {
                if (UnsortedCarPosList[i].name.CompareTo(CarPosList[j].name) < 0)
                {
                    CarPosList.Insert(j, UnsortedCarPosList[i]);
                    added = true;
                    break;
                }
            }
            if (!added)
                CarPosList.Add(UnsortedCarPosList[i]);
        }

        for (int i = 0; i < UnsortedCarPosList.Count; i++)
        {
            added = false;
            for (int j = 0; j < PublicCarPosList.Count; j++)
            {
                if (UnsortedCarPosList[i].name.CompareTo(PublicCarPosList[j].name) < 0)
                {
                    PublicCarPosList.Insert(j, UnsortedCarPosList[i]);
                    added = true;
                    break;
                }
            }
            if (!added)
                PublicCarPosList.Add(UnsortedCarPosList[i]);
        }

        //here we add a checkpoint, distance, laps, score and player number position for all cars in the scene
        for (int i = 0; i <= nBots; i++) // Player 1 included
        {
            nChk.Add(0);
            nDistP.Add(0);
            nLapsP.Add(0);
            scoreP.Add(0);
            pNum.Add(i + 1); // This sets the number of player in the race
        }
        //unparent checkpoints gameobject so we can get precise transforms values to compare later
        UnparentChks = GameObject.FindGameObjectsWithTag("chk");
        foreach (GameObject go in UnparentChks)
        {
            go.transform.parent = null;
        }
    }

    public void Update()
    {
        //detect how many laps the player has selected for the race
        LapsSelected = LapsSelectedManager.nLaps;

        //final score comparison between all players:
        for (int i = 0; i <= nBots; i++) // Player 1 included
        {
            scoreP[i] = nDistP[i] + (nChk[i] * 100) + (nLapsP[i] * 10000);
        }

        // Show player positions 
        if (ChkTrigger.startDis)
        {
            posPlayer(1);
            PositionDisplay.text = (posMax) + CardinalPos(posMax) + " Place";
        }

        //if the player completes all the selected laps
        if (LapsSelected < nLapsP[0])
        {
            raceFinish.Finish();//the race finish will trigger, ending the race
        }
        //show the amount of completed laps in the game UI
        LapCounter.text = Convert.ToString(nLapsP[0]);
    }
    public string CardinalPos(int i)
    {
        //depending the race position, the display in the canvas will show these letters
        string s;
        switch (i)
        {
            case 1:
                s = "st";//i.e: 1st (first) position
                break;
            case 2:
                s = "nd";//2nd (second)
                break;
            case 3:
                s = "rd";//3rd (third)
                break;
            default:
                s = "th";
                break;
        }
        return s;
    }
    //get the maximum position comparing cars scores
    public static void posPlayer(int nPlayer)
    {
        posMax = 1;

        for (int i = 0; i < scoreP.Count; i++)
        {
            if (scoreP[nPlayer - 1] < scoreP[i])
            {
                posMax = posMax + 1;
            }
        }
    }
}