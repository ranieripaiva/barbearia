namespace BarberBossI.Exception.ExceptionsBase;
public abstract class BarberBossIException : SystemException
{
    protected BarberBossIException(string message) : base(message)
    {
        
    }

    public abstract int StatusCode {  get;  }

    public abstract List<string> GetErrors();
}
