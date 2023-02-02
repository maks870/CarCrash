using System;

public interface ITrainingWaiter
{
    public void SubscribeWaitAction(Action endWaitAction);
    public void UnsubscribeWaitAction(Action endWaitAction);
}
