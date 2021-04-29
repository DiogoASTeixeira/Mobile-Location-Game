using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsideController : MonoBehaviour
{
    private static readonly short LEAVES_PER_CHALLENGE = 3;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    public Camera camera;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    public GameControl Control;
    public IntroManager PanelManager;
    public TMPro.TextMeshProUGUI CounterText;
    public GameObject LowerSection;
    public Image HalfLeaf;
    public Sprite[] HalfLeaves;

    LeafChallenge leafChallenge;
    private short leafDetected = -1;

    public enum Difficulty { EASY, MEDIUM, HARD };
    private Difficulty difficulty = Difficulty.EASY;

    private void Start()
    {
    }

    public void OnCameraPanel()
    {
        Init_ARCamera();
        CreateLeafChallenge();
        UpdateUICameraPanel();
    }

    public void CreateLeafChallenge()
    {
        //TODO verify if alreaddy init
        leafChallenge.Init();

        // Create a list of only unfound Leaves
        List<short> leavesNotFound = new List<short>(Control.Leaves.Length);
        for (short i = 0; i < Control.Leaves.Length; i++)
        {
            if(!Control.Leaves[i].IsLeafFound())
            {
                leavesNotFound.Add(i);
            }
        }

        // Fill leafChallenge with random leaves to be found
        short loop_max = (short)Mathf.Min(leavesNotFound.Count, LEAVES_PER_CHALLENGE);
        for (short i = 0; i < loop_max; i++)
        {
            int rnd = UnityEngine.Random.Range(0, leavesNotFound.Count);
            leafChallenge.leaves[i] = leavesNotFound[rnd];
            leavesNotFound.RemoveAt(rnd);
        }
        for(int i = 0; i < leavesNotFound.Count; i++) Debug.LogWarning(leavesNotFound[i]);
        for(int i = 0; i < leafChallenge.leaves.Length; i++) Debug.LogWarning(leafChallenge.leaves[i]);
    }

    private void Init_ARCamera()
    {
        camera.enabled = true;
        camera.gameObject.SetActive(true);
    }
    public void CameraBtnPressed()
    {
        if (leafDetected == leafChallenge.GetLeaf() )//&& IsLeafOnScreenCenter(leafDetected))
        {
            //mark leaf as found
            Control.Leaves[leafChallenge.GetLeaf()].FoundLeaf();

            //Quiz time
            ShowQuestion();

            //next leaf or found all 3 leafs panel
            if (leafChallenge.Next())
            {
                //Update UI
                UpdateUICameraPanel();
            }
        }
    }
    private void ShowQuestion()
    {
        CreateQuestion(leafChallenge.GetLeaf());
    }

    private void UpdateUICameraPanel()
    {
        CounterText.text = Control.NumberOfFoundLeaves().ToString() + " / " + Control.Leaves.Length.ToString();
        short leafIndex = leafChallenge.GetLeaf();
        HalfLeaf.sprite = HalfLeaves[leafIndex];
    }

    //TODO
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

    private void CreateQuestion(short leafIndex)
    {
        //leafChallenge.rightAnswer = Random.Range();
    }

    public void DetectedLeaf(int leafNumber) { leafDetected = (short)leafNumber; }
    public void LostDetectedLeaf() { leafDetected = -1; }
    public void SetDifficulty(Difficulty diff) { difficulty = diff; }
    public Difficulty GetDifficulty() { return difficulty; }

    public struct LeafChallenge
    {
        public short index;
        public short[] leaves;
        public bool isActive;

        public string[] answers;
        public short rightAnswer;
        internal void Init()
        {
            index = 0;
            leaves = new short[LEAVES_PER_CHALLENGE];
            answers = new string[3];
        }

        internal short GetLeaf()
        {
            return leaves[index];
        }

        internal bool Next()
        {
            index++;
            if (index >= LEAVES_PER_CHALLENGE)
            {
                index = 0;
                return false;
            }
            else return true;
        }
    }
}
