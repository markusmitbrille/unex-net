public interface IIdentifiable<T> where T : Identity
{
    T ID { get; }
}
