using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DefaultNamespace.Command.Commands
{
    public static class SmoothTransition
    {
        public static async UniTask ChangeValueOverTime(Func<float> getValue, Action<float> setValue, float targetValue, float duration, CancellationToken ct)
        {
            float startValue = getValue();
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                if (ct.IsCancellationRequested) break;

                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / duration;
                setValue(Mathf.Lerp(startValue, targetValue, progress));

                await UniTask.Yield(PlayerLoopTiming.Update, ct);
            }

            setValue(targetValue);
        }
    }
}