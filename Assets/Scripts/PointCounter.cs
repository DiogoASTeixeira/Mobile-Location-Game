using UnityEngine;

public class PointCounter : MonoBehaviour
{
    private static readonly int AWARD_POINTS = 30;
    private float counter = 0.0f;
    private bool isCounting = false;
    private void Update()
    {
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        if(isCounting)
            counter += Time.deltaTime;
    }

    public void StartCounter() => isCounting = true;

    public void StopCounter() => isCounting = false;

    public void AwardPoints() => counter += AWARD_POINTS;
    public void TakePoints() => counter -= AWARD_POINTS;

    public int getCounter() => (int)counter;
}
