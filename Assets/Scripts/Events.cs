using System;
using UnityEngine;
using UnityEngine.Events;

public static class Events
{
    //exemplos de como criar um evento
    //public static readonly Evt<string,int> onAddDictionary = new Evt<string,int>();
    //public static readonly Evt<Script> onScriptEvent = new Evt<Script>();
    //public static readonly Evt onPlayerBusyEvent = new Evt();
    //public static readonly Evt onPlayerBusyEventExit = new Evt();
    //public static readonly Evt<MonoBehaviour> onMissionComplete = new Evt<MonoBehaviour>();
    public static readonly Evt onEvent1 = new Evt();
    public static readonly Evt<int> onEvent2 = new Evt<int>();
}
public class Evt
{
    private event Action _action = delegate { };

    public void Invoke() => _action.Invoke();
    public void AddListener(Action listener) => _action += listener;
    public void RemoveListener(Action listener) => _action -= listener;
}
public class Evt<T>
{
    private event Action<T> _action = delegate { };

    public void Invoke(T param) => _action.Invoke(param);
    public void AddListener(Action<T> listener) => _action += listener;
    public void RemoveListener(Action<T> listener) => _action -= listener;
}

public class Evt<T0, T1>
{
    private event Action<T0, T1> _action = delegate { };

    public void Invoke(T0 param1, T1 param2) => _action.Invoke(param1, param2);
    public void AddListener(Action<T0, T1> listener) => _action += listener;
    public void RemoveListener(Action<T0, T1> listener) => _action -= listener;
}