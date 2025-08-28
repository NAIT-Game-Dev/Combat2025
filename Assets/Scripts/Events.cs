using UnityEngine.Events;

static public class MyEvents
{
    static public UnityEvent<int> AddScore = new UnityEvent<int>();
    static public UnityEvent<int> ActivateScores = new UnityEvent<int>();
}
