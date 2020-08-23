namespace MapperLibrary
{
    public interface IHooks<T>
    {
       CustomActions<T> CustomHooks { get; set; }
        void Add(Method<T> delegat);
    }
}
