using DefaultNamespace.Command.Commands;
using UnityEngine;

public class RepeatGame : MonoBehaviour
{
    [SerializeField] private BehaviourGameUI _repeat;
    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>()) _repeat.RepeatGame();
    }
}
