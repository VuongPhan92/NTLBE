namespace Infrastructure.Decorator
{
    public interface ICommandHandler<TCommand>
    {
        void Handle(TCommand command);
    }
}