using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static Utils;

public class InsideController : MonoBehaviour
{
    private static readonly short LEAVES_PER_CHALLENGE = 3;
    public WaitDelegate m_MethodToCall;


#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    public Camera camera;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    private GameControl Control;
    //public PointCounter PointCounter;
    public IntroManager PanelManager;
    public Sprite[] HalfLeaves;
    public Sprite[] HalfLeavesNormal;
    public Sprite[] HalfLeavesHard;
    public Sprite[] feedbackIconsSprites;
    //public Sprite[] FullLeaves;
    public GameObject[] modal;
    public GameObject playAgain;

    //Panel 2 (Camera)
    public TMPro.TextMeshProUGUI CounterText;
    public GameObject LowerSection;
    public GameObject QuestionBox;
    public InfoPanelBehaviour InfoPanel;
    public Image HalfLeaf;
    public Image[] QuestionBtn;
    public Button SkipBtn;
    public GameObject Skip;

    //Panel 3
    public TMPro.TextMeshProUGUI LeavesLeftText;
    public TMPro.TextMeshProUGUI PointsText;
    public WinStarScript WinStarsScript;

    LeafChallenge leafChallenge;
    private short leafDetected = -1;

    [HideInInspector]
    public bool answered = true;

    public enum Difficulty { EASY, MEDIUM, HARD };
    private Difficulty difficulty = Difficulty.EASY;

    //DEBUG 
    public int points = 0;
    public int points2 = 0;

    // Routines
    public GameObject[] feedbackIcons;
    public GameObject[] rightIcons;
    public GameObject[] wrongIcons;

    private void Start()
    {
        Control = GameControl.control;
    }

    private void Update()
    {
        points = Control.PointCounter.GetCounter();
        points2 = Control.PointCounter.GetCounter2();
        if (Control.NumberOfFoundLeaves() > 3) playAgain.SetActive(false);
    }

    public void OnCameraPanel()
    {
        if (!Control.HasFoundAllLeaves())
        {
            Init_ARCamera();
            CreateLeafChallenge();
            UpdateUICameraPanel();
            Control.PointCounter.StartCounter();
            StartCoroutine("HideSkipTemp");

        }
        else
        {
            // skip panel if all leafs found
            PanelManager.ShowNextPanel();
            Control.NavBar.SetActive(true);
        }
    }

    public void CreateLeafChallenge()
    {
        leafChallenge.Init();
        WinStarsScript.ResetValues();

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

    // Acts as if the leaf was found, but takes away points equal to 5 erros
    public void SkipChallenge()
    {
        Control.PointCounter.WrongLeaf(5);
        WinStarsScript.WrongGuess(5);
        Control.Leaves[leafChallenge.GetLeaf()].FoundLeaf();
        Debug.Log("Leaf SKipped: " + Control.Leaves[leafChallenge.GetLeaf()].speciesName);
        ShowQuestion(leafChallenge.GetLeaf());
    }

    public void NextLeafChallenge()
    {
        ResetQuestionBtns();
        SkipBtn.gameObject.SetActive(true);
        //next leaf or found all 3 leafs panel
        if (leafChallenge.Next())
        {
            //Update UI
            UpdateUICameraPanel();
            LowerSection.SetActive(true);
            QuestionBox.SetActive(false);
            Control.PointCounter.StartCounter();
            StartCoroutine("HideSkipTemp");

        }
        else
        {
            // n?o castiga o tempo por ter errado
            if (Control.PointCounter.GetCounter2() < 0)
            {
                Control.PointCounter.zeroCounter();
            }
            // show end panel
            WinStarsScript.UpdateStarUI(Time.time - leafChallenge.TimeStart);
            Control.NavBar.SetActive(true);
            LeavesLeftText.text = "Total Leaves Found: " + Control.NumberOfFoundLeaves() + " / " + Control.Leaves.Length;
            //Its the final counter tununu
            PointsText.text = "Pontos " + Control.PointCounter.FinalCounter();
            PanelManager.ShowNextPanel();
            Control.SaveGame();
        }
    }

    private void ResetQuestionBtns()
    {
        foreach(Image btn in QuestionBtn)
        {
            btn.color = new Color32(255, 255, 255, 255);
        }
    }

    public void RepeatChallenge()
    {
        if (!Control.HasFoundAllLeaves())
        {
            /*
            Control.NavBar.SetActive(false);
            CreateLeafChallenge();
            //UpdateUICameraPanel();
            LowerSection.SetActive(true);
            QuestionBox.SetActive(false);
            //Control.PointCounter.StartCounter();
            SkipBtn.gameObject.SetActive(true);

            PanelManager.ShowPreviousPanel();
            PanelManager.ShowPreviousPanel();
            */
            UnityEngine.SceneManagement.SceneManager.LoadScene("Inside");
        }
    }

    private void Init_ARCamera()
    {
        camera.enabled = true;
        camera.gameObject.SetActive(true);
        rightIcons = GameObject.FindGameObjectsWithTag("right");
        wrongIcons = GameObject.FindGameObjectsWithTag("wrong");
        feedbackIcons = GameObject.FindGameObjectsWithTag("found");

        foreach (GameObject feedbackIcon in feedbackIcons)
        {
            feedbackIcon.gameObject.SetActive(true);
        }
        foreach (GameObject wrongIcon in wrongIcons)
        {
            wrongIcon.gameObject.SetActive(false);
        }
        foreach (GameObject rightIcon in rightIcons)
        {
            rightIcon.gameObject.SetActive(false);
        }
    }

    
    public void CameraBtnPressed()
    {
        if (leafDetected == leafChallenge.GetLeaf() )//&& IsLeafOnScreenCenter(leafDetected))
        {
            //mark leaf as found
            Control.Leaves[leafChallenge.GetLeaf()].FoundLeaf();
            /*
            if (leafChallenge.GetLeaf() == 0) {
                Control.bordo_found = true;
               
            }
            if (leafChallenge.GetLeaf() == 1) {
                Control.eugenia_found = true;
            }
            if (leafChallenge.GetLeaf() == 2) {
                Control.carvalho_found = true;
            }
            if (leafChallenge.GetLeaf() == 3) {
                Control.pilriteiro_found = true;
            }
            if (leafChallenge.GetLeaf() == 4) {
                Control.ambar_found = true;
            }
            if (leafChallenge.GetLeaf() == 5) {
                Control.azevinho_found = true;
            }
            */
            Control.PointCounter.CorrectLeaf(GetDifficultyMultiplier());
            WinStarsScript.CorrectGuess();
            //Quiz time
            //ShowQuestion(leafChallenge.GetLeaf());
            StartCoroutine("ShowCorrectFeedback");
        }
        else
        {
            Control.PointCounter.WrongLeaf();
            WinStarsScript.WrongGuess();

            StartCoroutine("ShowWrongFeedback");
            Debug.Log(leafChallenge.GetLeaf());
        }
    }

    IEnumerator ShowCorrectFeedback()
    {
        //desliga o icon ? do desconhecido
        
        foreach (GameObject feedbackIcon in feedbackIcons)
        {
            feedbackIcon.gameObject.SetActive(false);
        }
        //liga o icon do certo
        
        foreach (GameObject rightIcon in rightIcons)
        {
            rightIcon.gameObject.SetActive(true);
        }
        //espera 2 segundos
        yield return new WaitForSeconds(1f);
        //liga o icon ? do desconhecido de novo
        foreach (GameObject feedbackIcon in feedbackIcons)
        {
            feedbackIcon.gameObject.SetActive(true);
        }
        //desliga o icon do certo
        foreach (GameObject rightIcon in rightIcons)
        {
            rightIcon.gameObject.SetActive(false);
        }
        ShowQuestion(leafChallenge.GetLeaf());
        yield return null;
    }
    IEnumerator ShowWrongFeedback()
    {
        HalfLeaf.sprite = feedbackIconsSprites[0];

        //desliga o icon ? do desconhecido
        foreach (GameObject feedbackIcon in feedbackIcons)
        {
            feedbackIcon.gameObject.SetActive(false);
        }
        //liga o icon do errado
        foreach (GameObject wrongIcon in wrongIcons)
        {
            wrongIcon.gameObject.SetActive(true);
        }
        //espera 2 segundos
        yield return new WaitForSeconds(1f);
        //liga o icon ? do desconhecido de novo
        foreach (GameObject feedbackIcon in feedbackIcons)
        {
            feedbackIcon.gameObject.SetActive(true);
        }
        //desliga o icon do erradp
        foreach (GameObject wrongIcon in wrongIcons)
        {
            wrongIcon.gameObject.SetActive(false);
        }

        UpdateUICameraPanel();
        yield return null;
    }
    IEnumerator HideSkipTemp()
    {
        Skip.SetActive(false);
        yield return new WaitForSeconds(5f);
        Skip.SetActive(true);


        yield return null;
    }

    private void ShowQuestion(short leafIndex)
    {
        LowerSection.SetActive(false);
        CreateQuestion(leafIndex);
        QuestionBox.SetActive(true);
        answered = false;
        SkipBtn.gameObject.SetActive(false);

        //Hide ? Icon in Question
        foreach (GameObject feedbackIcon in feedbackIcons)
        {
            feedbackIcon.gameObject.SetActive(false);
        }

        //prepare InfoPanel
        InfoPanel.Setup(leafIndex);
    }

    private void UpdateUICameraPanel()
    {
        CounterText.text = leafChallenge.index + " / " + LEAVES_PER_CHALLENGE;
        short leafIndex = leafChallenge.GetLeaf();
        switch(difficulty)
        {
            case Difficulty.EASY:
                {
                    HalfLeaf.sprite = HalfLeaves[leafIndex];
                    break;
                }
            case Difficulty.MEDIUM:
                {
                    HalfLeaf.sprite = HalfLeavesNormal[leafIndex];
                    break;
                }
            case Difficulty.HARD:
            default:
                {
                    HalfLeaf.sprite = HalfLeavesHard[leafIndex];
                    break;
                }
        }
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
            Control.PointCounter.StopCounter();
            if (answer == leafChallenge.rightAnswer)
            {
                Control.PointCounter.CorrectAnswer();
                WinStarsScript.CorrectGuess();
            }
            else
            {
                // Wrond answer turns red
                QuestionBtn[answer].color = new Color32(231, 94, 90, 255);
                Control.PointCounter.WrongAnswer();
                WinStarsScript.WrongGuess();
            }
            // Correct answer Turns green
            QuestionBtn[leafChallenge.rightAnswer].color = new Color32(106, 142, 78, 255);

            


            m_MethodToCall = ShowInfoPanel;
            StartCoroutine(WaitAndCall(2, m_MethodToCall));
        }
    }

    private void ShowInfoPanel()
    {
        int which = leafChallenge.GetLeaf();

        for (int i = 0; i < modal.Length; i++)
        {
            // only the one matching i == which will be on, all others will be off
            modal[i].SetActive(i == which);
        }
        //Show ? Icon in Question
        foreach (GameObject feedbackIcon in feedbackIcons)
        {
            feedbackIcon.gameObject.SetActive(true);
        }
        InfoPanel.gameObject.SetActive(true);
        Debug.LogWarning("DONE");
    }

    public void StopCounter()
    {
        Control.PointCounter.StopCounter();
    }

    private float GetDifficultyMultiplier()
    {
        switch(difficulty)
        {
            case Difficulty.HARD: return 2.0f;
            case Difficulty.MEDIUM: return 1.5f;
            case Difficulty.EASY:
            default: return 1.0f;
        }
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

        public float TimeStart;
        internal void Init()
        {
            index = 0;
            leaves = new short[LEAVES_PER_CHALLENGE];
            answers = new string[3];
            TimeStart = Time.time;
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
