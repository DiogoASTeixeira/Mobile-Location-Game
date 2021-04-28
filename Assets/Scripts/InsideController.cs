using System;
using System.Collections;
using UnityEngine;

public class InsideController : MonoBehaviour
{
    private static readonly short LEAVES_PER_CHALLENGE = 3;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    public Camera camera;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    public GameControl Control;
    public IntroManager PanelManager;

    private LeafChallenge leafChallenge;
    private int leafSelected = -1;
    private int leafDetected = -1;

    public enum Difficulty { EASY, MEDIUM, HARD };
    private Difficulty difficulty = Difficulty.EASY;

    private void Start()
    {
        
    }

    private void OnCameraPanel()
    {
        CreateLeafChallenge();
        SetObjectsForChallenge();
    }

    public void CreateLeafChallenge()
    {
        //TODO verify if alreaddy init
        leafChallenge.init();

        // Create a list of only unfound Leaves
        Leaf[] tmpArray = Array.FindAll(Control.Leaves, leaf => !leaf.IsLeafFound());
        ArrayList leavesNotFound = new ArrayList(tmpArray);

        // Fill leafChallenge with random leaves to be found
        short loop_max = (short)Mathf.Min(leavesNotFound.Count, LEAVES_PER_CHALLENGE);
        for (short i = 0; i < loop_max; i++)
        {
            short rnd = (short)UnityEngine.Random.Range(0, leavesNotFound.Count);
            leafChallenge.leaves[i] = rnd;
            leavesNotFound.RemoveAt(rnd);
        }
    }

    private void SetObjectsForChallenge()
    {

    }
    public void CameraBtnPressed()
    {
        if(leafDetected == leafSelected && IsLeafOnScreenCenter(leafDetected))
        {
            //mark leaf as found
            //next leaf or found all 3 leafs panel
        }
    }

    private bool IsLeafOnScreenCenter(int leafNumber)
    {
        //TODO fix for new array
        Vector3 objPosition = new Vector3();// leavesInfo[leafNumber].centerPiece.transform.position;
        Vector3 screenPos = camera.WorldToScreenPoint(objPosition);
        int fifthX = camera.pixelWidth / 5;
        int fifthY = camera.pixelHeight / 5;
        bool a = fifthX * 2 < screenPos.x && screenPos.x < fifthX * 3
            && fifthY * 2 < screenPos.y && screenPos.y < fifthY * 3;
        return a;
    }

    public void SetDifficulty(Difficulty diff) { difficulty = diff; }
    public Difficulty GetDifficulty() { return difficulty; }

    private struct LeafChallenge
    {
        public short index;
        public ArrayList leaves;
        public bool isActive;

        internal void init()
        {
            index = 0;
            leaves = new ArrayList();
        }
    }
}
