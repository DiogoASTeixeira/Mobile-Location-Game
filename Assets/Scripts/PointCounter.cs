using UnityEngine;

public class PointCounter : MonoBehaviour
{
    private static readonly float STARTING_POINTS = 2000.0f;

    private static readonly int CORRECT_LEAF_POINTS = 300;
    private static readonly int WRONG_LEAF_POINTS = 50;
    private static readonly int CORRECT_ANSWER_POINTS = 150;
    private static readonly int WRONG_ANSWER_POINTS = 100;
    private static readonly float POINTS_PER_SECOND = 11.0f;

    private float counter = STARTING_POINTS;
    private float counter2 = 0;
    private bool isCounting = false;
    private void Update()
    {
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        // se o tempo chegar a 0 fica 0
        if(isCounting && counter >= 0)
            counter -= Time.deltaTime * POINTS_PER_SECOND;
    }

    public void StartCounter() => isCounting = true;

    public void StopCounter() => isCounting = false;
    public void zeroCounter() => counter2 = 0;
    public void CorrectLeaf(float diffMult) => counter2 += CORRECT_LEAF_POINTS * diffMult;
    public void WrongLeaf(short multiplier = 1) => counter2 -= WRONG_LEAF_POINTS * multiplier;
    public void CorrectAnswer() => counter2 += CORRECT_ANSWER_POINTS;
    public void WrongAnswer() => counter2 -= WRONG_ANSWER_POINTS;

    public void ResetCounter() => counter = STARTING_POINTS;

    //counter é do tempo
    public int GetCounter() => (int)counter;
    //counter2 é das perguntas e deteções
    public int GetCounter2() => (int)counter2;
    public int FinalCounter() => (int)counter + (int)counter2;
}
