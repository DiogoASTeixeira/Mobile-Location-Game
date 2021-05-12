using UnityEngine;

public class PointCounter : MonoBehaviour
{
    private static readonly int CORRECT_LEAF_POINTS = 300;
    private static readonly int WRONG_LEAF_POINTS = 50;
    private static readonly int CORRECT_ANSWER_POINTS = 150;
    private static readonly int WRONG_ANSWER_POINTS = 100;
    private static readonly float POINTS_PER_SECOND = 11.0f;

    private float counter = 2000.0f;
    private bool isCounting = false;
    private void Update()
    {
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        if(isCounting)
            counter -= Time.deltaTime * POINTS_PER_SECOND;
    }

    public void StartCounter() => isCounting = true;

    public void StopCounter() => isCounting = false;
    public void CorrectLeaf() => counter += CORRECT_LEAF_POINTS;
    public void WrongLeaf() => counter -= WRONG_LEAF_POINTS;
    public void CorrectAnswer() => counter += CORRECT_ANSWER_POINTS;
    public void WrongAnswer() => counter -= WRONG_ANSWER_POINTS;

    public int getCounter() => (int)counter;
}
