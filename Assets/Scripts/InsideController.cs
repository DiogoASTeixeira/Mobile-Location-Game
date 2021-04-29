using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Utils;

public class InsideController : MonoBehaviour
{
    private static readonly short LEAVES_PER_CHALLENGE = 3;
    public WaitDelegate m_MethodToCall;


#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    public Camera camera;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    public GameControl Control;
    public IntroManager PanelManager;
    public Sprite[] HalfLeaves;
    public TMPro.TextMeshProUGUI CounterText;
    public GameObject LowerSection;
    public GameObject QuestionBox;
    public InfoPanelBehaviour InfoPanel;
    public Image HalfLeaf;
    public Image[] QuestionBtn;
    //public TMPro.TextMeshProUGUI[] AnswerBtnText;

    LeafChallenge leafChallenge;
    private short leafDetected = -1;

    [HideInInspector]
    public bool answered = true;

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
            int rnd = Random.Range(0, leavesNotFound.Count);
            leafChallenge.leaves[i] = leavesNotFound[rnd];
            leavesNotFound.RemoveAt(rnd);
        }
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
            ShowQuestion(leafChallenge.GetLeaf());

            /*
            //next leaf or found all 3 leafs panel
            if (leafChallenge.Next())
            {
                //Update UI
                UpdateUICameraPanel();
            }
            */
        }
    }
    private void ShowQuestion(short leafIndex)
    {
        LowerSection.SetActive(false);
        CreateQuestion(leafIndex);
        QuestionBox.SetActive(true);
        answered = false;

        //prepare InfoPanel
        InfoPanel.Setup(leafIndex);
    }

    private void UpdateUICameraPanel()
    {
        CounterText.text = Control.NumberOfFoundLeaves().ToString() + " / " + Control.Leaves.Length.ToString();
        short leafIndex = leafChallenge.GetLeaf();
        HalfLeaf.sprite = HalfLeaves[leafIndex];
    }

    private void CreateQuestion(short leafIndex)
    {
        leafChallenge.rightAnswer = (short)Random.Range(0, 3);
        HashSet<short> exclude = new HashSet<short>() { leafIndex };
        for(short i = 0; i < 3; i++)
        {
            if (i == leafChallenge.rightAnswer)
            {
                QuestionBtn[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text
        = Control.Leaves[leafIndex].scientificName;
            }
            else
            {
                //Creates the list of valid indexes
                IEnumerable<int> range = Enumerable.Range(0, Control.Leaves.Length-1).Where(x => !exclude.Contains((short)x));
                // gets index of a leaf that is not the right one or one that has been 
                int range_index = Random.Range(0, Control.Leaves.Length - exclude.Count - 1);
                short rnd_index = (short)range.ElementAt(range_index);

                QuestionBtn[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text
                    = Control.Leaves[rnd_index].scientificName;
                //adds picked leaf to the excluded set
                exclude.Add(rnd_index);
            }
        }
    }
    public void AnswerQuestion(int answer)
    {
        if (!answered)
        {
            answered = true;
            if (answer == leafChallenge.rightAnswer)
            {
                // TODO Award Points
            }
            else
            {
                // Wrond answer turns red
                QuestionBtn[answer].color = new Color32(231, 94, 90, 255);
            }
            // Correct answer Turns green
            QuestionBtn[leafChallenge.rightAnswer].color = new Color32(106, 142, 78, 255);

            m_MethodToCall = ShowInfoPanel;
            StartCoroutine(WaitAndCall(2, m_MethodToCall));
        }
    }

    private void ShowInfoPanel()
    {
        InfoPanel.gameObject.SetActive(true);
        Debug.LogWarning("DONE");
    }
    //TODO detect only in frame
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
