namespace MapperLibrary
{
    internal class Default<T> : Extensions<T>, IOperations<T>
    {
        public override object AssignValue(T o)
        {
            return null;
        }
    }
}