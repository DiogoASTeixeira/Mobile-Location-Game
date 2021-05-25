using UnityEngine;

public class WinStarScript : MonoBehaviour
{
    private float precisionStarAmount = 0.0f;
    private float timeStarAmount = 0.0f;
    private int nRightGuesses = 0;
    private int nTotalGuesses = 0;

    public UnityEngine.UI.Image TimeStarImg;
    public UnityEngine.UI.Image PrecisionStarImg;

    public void UpdateStarUI(float challengeTíme)
    {
        CalculateStarPoints(challengeTíme);
        FillStars();
        Debug.Log("Time: " + challengeTíme + "\n" +
            "Precision Star Amount: " + precisionStarAmount + "\n" +
            "Time Star Amount: " + timeStarAmount +"\n" +
            "Guesses: " + nRightGuesses + "/" + nTotalGuesses);
    }

    private void CalculateStarPoints(float challengeTíme)
    {
        if (challengeTíme <= 60 * 1.5f) // 1.5minutes
        {
            //5 stars
            timeStarAmount = 5 * 0.2f;
        }
        else if (challengeTíme <= 60 * 2.0f)
        {
            //4 stars
            timeStarAmount = 4 * 0.2f;
        }
        else if (challengeTíme <= 60 * 2.5f)
        {
            //3 stars
            timeStarAmount = 3 * 0.2f;
        }
        else if (challengeTíme <= 60 * 3.0f)
        {
            //2stars
            timeStarAmount = 2 * 0.2f;
        }
        else if (challengeTíme <= 60 * 3.5f)
        {
            //1stars
            timeStarAmount = 1 * 0.2f;
        }
        else
        {
            //0stars
            timeStarAmount = 0;
        }
        precisionStarAmount = (float)nRightGuesses / nTotalGuesses;
    }

    public void CorrectGuess()
    {
        nRightGuesses++;
        nTotalGuesses++;
    }

    public void WrongGuess(int multiplier = 1)
    {
        nTotalGuesses += multiplier;
    }

    private void FillStars()
    {
        TimeStarImg.fillAmount = timeStarAmount;
        PrecisionStarImg.fillAmount = precisionStarAmount;
    }

    public void ResetValues()
    {
        precisionStarAmount = 0.0f;
        timeStarAmount = 0.0f;
        nRightGuesses = 0;
        nTotalGuesses = 0;
    }
}
