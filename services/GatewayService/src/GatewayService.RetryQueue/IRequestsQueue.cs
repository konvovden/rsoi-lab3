namespace GatewayService.RetryQueue;

public interface IRequestsQueue
{
    void AddRequest(Action requestAction);
    void RemoveRequest(Action requestAction);
    List<Action> GetRequests();
}