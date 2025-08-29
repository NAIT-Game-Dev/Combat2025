using UnityEngine.Events;

static public class MyEvents
{
    static public UnityEvent<int> AddScore = new UnityEvent<int>();
    static public UnityEvent<int> ActivateScores = new UnityEvent<int>();
    static public UnityEvent GameOver = new UnityEvent();
    static public UnityEvent Replay = new UnityEvent();
    static public UnityEvent OpenLobby = new UnityEvent();
}
