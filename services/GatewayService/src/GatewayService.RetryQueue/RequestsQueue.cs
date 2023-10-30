namespace GatewayService.RetryQueue;

public class RequestsQueue : IRequestsQueue
{
    private readonly List<Action> _actions;

    public RequestsQueue()
    {
        _actions = new List<Action>();
    }
    public void AddRequest(Action requestAction)
    {
        _actions.Add(requestAction);
    }

    public void RemoveRequest(Action requestAction)
    {
        _actions.Remove(requestAction);
    }

    public List<Action> GetRequests()
    {
        return _actions;
    }
}