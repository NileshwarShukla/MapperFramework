namespace MapperLibrary
{
    public delegate object Method<T>(T o);
   public enum Operation
    {
        Refer
      , Offset
      , Default,
        Hook,
        Basic
    }
}
