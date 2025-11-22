public interface IInputActivater 
{
    void EnableInput();
    void DisableInput();
    bool IsInputEnabled { get; set; }
}
