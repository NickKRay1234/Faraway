using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DefaultNamespace.Command.Commands
{
    /// Base class for speed change commands, inherits from AbstractCommand.
    public class SpeedParentCommand : AbstractCommand
    {
        protected float _startSpeed;
        protected float _maxSpeed;
        protected float _minSpeed;
        protected float _timeToReachMaxSpeed;
        protected float _durationHalf;

        protected SpeedParentCommand(CommandContext context) : base(context)
        {
        }

        /// Asynchronous method for smoothly changing the speed from one value to another.
        protected void ChangeSpeedOverTime(float fromSpeed, float toSpeed, float duration,
            CancellationToken ct)
        {
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                if (ct.IsCancellationRequested)
                    break;

                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / duration;
                _context.Settings.ForwardSpeed = Mathf.Lerp(fromSpeed, toSpeed, progress);
            }

            if (!ct.IsCancellationRequested)
                _context.Settings.ForwardSpeed = toSpeed;
        }
        
        protected void ResetCancellationToken()
        {
            if (!_cancellationTokenSource.IsCancellationRequested)
                _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
        }
    }
}