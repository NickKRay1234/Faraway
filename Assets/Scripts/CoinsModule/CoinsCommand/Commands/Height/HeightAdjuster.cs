using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class HeightAdjuster : IHeightAdjuster
{
    /// Implementation of the IHeightAdjuster interface to smoothly change the player's height
    public async Task AdjustHeightSmoothly(Player player, float targetHeight, float duration, CancellationToken ct)
    {
        if (player == null)
            throw new ArgumentNullException(nameof(player));
        
        Vector3 startPosition = player.transform.position;
        Vector3 targetPosition = new Vector3(startPosition.x, targetHeight, startPosition.z);
        float elapsedTime = 0;
        
        while (elapsedTime < duration)
        {
            if (ct.IsCancellationRequested) 
                break;
            
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            player.transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
            await UniTask.Yield(PlayerLoopTiming.Update, ct);
        }
        
        if (!ct.IsCancellationRequested) 
            player.transform.position = targetPosition;
    }
}