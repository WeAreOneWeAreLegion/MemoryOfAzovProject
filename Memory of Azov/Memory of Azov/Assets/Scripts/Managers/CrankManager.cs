using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankManager : MonoBehaviour {

    [Header("Crank Objects")]
    public List<DoorBell> crankPuzzles;

    public void Awake()
    {
        ChooseRandomCrank();
    }

    private void ChooseRandomCrank()
    {
        crankPuzzles.ForEach(x => x.isFakeBell = true);

        int randomBell = Random.Range(0, crankPuzzles.Count);

        crankPuzzles[randomBell].isFakeBell = false;
    }

}
