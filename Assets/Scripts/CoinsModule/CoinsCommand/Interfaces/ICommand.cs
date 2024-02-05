public interface ICommand
{
    void Execute(Player player);
    void Cancel();
}