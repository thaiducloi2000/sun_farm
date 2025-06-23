using System;
using System.Collections.Generic;
using EventBus;
using Score;

public class LandManager : MonoBehaviourEventListener
{
    public struct UnclockLandRequest : IEventData
    {
        public int LandIndex;
        public Action<bool> OnUnclockResult;
    }

    private HashSet<int> _unlockedLandIndices = new();
    private HashSet<int> _occupiedLandIndices = new();

    protected override void RegisterEvents()
    {
        EventBus<GameplayEvent>.AddListener<RequestBuildAtLand>((int)EventId_Gameplay.RequestBuildAtLand, HandleBuildRequest);
        EventBus<GameplayEvent>.AddListener<UnclockLandRequest>((int)EventId_Gameplay.UnClockLand, HandleUnclockLandRequest);
    }

    protected override void UnregisterEvents()
    {
        EventBus<GameplayEvent>.RemoveListener<RequestBuildAtLand>((int)EventId_Gameplay.RequestBuildAtLand, HandleBuildRequest);
        EventBus<GameplayEvent>.RemoveListener<UnclockLandRequest>((int)EventId_Gameplay.UnClockLand, HandleUnclockLandRequest);
    }

    private void HandleBuildRequest(RequestBuildAtLand request)
    {
        if (!IsUnlocked(request.landIndex))
        {
            request.refuseBuildRespone?.Invoke();
            return;
        }

        if (IsOccupied(request.landIndex))
        {
            request.refuseBuildRespone?.Invoke();
            return;
        }

        request.canBuildRespone?.Invoke();
    }

    private void HandleUnclockLandRequest(UnclockLandRequest request)
    {
        if (IsUnlocked(request.LandIndex))
        {
            request.OnUnclockResult?.Invoke(false);
            return;
        }

        _unlockedLandIndices.Add(request.LandIndex);
        request.OnUnclockResult?.Invoke(true);
    }

    public void UnlockLand(int index)
    {
        _unlockedLandIndices.Add(index);
    }

    public void SetOccupied(int index)
    {
        _occupiedLandIndices.Add(index);
    }

    public void RemoveOccupied(int index)
    {
        _occupiedLandIndices.Remove(index);
    }

    private bool IsUnlocked(int index)
    {
        return _unlockedLandIndices.Contains(index);
    }

    private bool IsOccupied(int index)
    {
        return _occupiedLandIndices.Contains(index);
    }
}
