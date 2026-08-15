public interface IEvent
{
    string EventID { get; set; }
    void EventStart();
    void EventEnd();
}