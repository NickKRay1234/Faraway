using UnityEngine;

namespace DefaultNamespace.Command.Commands
{
    public interface IPlayer
    {
        Rigidbody Rigidbody { get; }
        Transform Transform { get; }
    }
}