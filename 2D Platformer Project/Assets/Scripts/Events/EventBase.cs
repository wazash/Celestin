using System;

public class EventBase
{
    private event Action action = delegate { };

    public void Invoke()
    {
        action.Invoke();
    }

    public void AddListener(Action listener)
    {
        action += listener;
    }

    public void RemoveListener(Action listener)
    {
        action -= listener;
    }
}
public class EventBase<T1>
{
    private event Action<T1> action = delegate { };

    public void Invoke(T1 param)
    {
        action.Invoke(param);
    }

    public void AddListener(Action<T1> listener)
    {
        action += listener;
    }

    public void RemoveListener(Action<T1> listener)
    {
        action -= listener;
    }
}
public class EventBase<T1, T2>
{
    private event Action<T1, T2> action = delegate { };

    public void Invoke(T1 param1, T2 param2)
    {
        action.Invoke(param1, param2);
    }

    public void AddListener(Action<T1, T2> listener)
    {
        action += listener;
    }

    public void RemoveListener(Action<T1, T2> listener)
    {
        action -= listener;
    }
}