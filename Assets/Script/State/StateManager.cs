using System;
using System.Collections.Generic;
using UniRx;

public class StateManager
{
    readonly Stack<State> stateStack = new();

    readonly Subject<State> onChangeStateSubject = new();
    public IObservable<State> OnChangeState => onChangeStateSubject;

    public void Initialize()
    {
        stateStack.Clear();
        GoNextState(new Main());
    }

    public void GoNextState(State state)
    {
        stateStack.Push(state);
        onChangeStateSubject.OnNext(state);
    }

    public void GoPrevState()
    {
        if (stateStack.Count == 1)
            return;
        
        stateStack.Pop();
        onChangeStateSubject.OnNext(stateStack.Peek());
    }
}