using FSMSystem;
using FSMSystem.Simhost;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Test : MonoBehaviour
{
    [SerializeField] private Data data;
    private FSMManager fsm;


    private void Awake()
    {
        data = new Data();
    }


    void Start()
    {
        fsm = new FSMManager();

        StateBase defreezeState = new DefreezeState(fsm);
        defreezeState.AddTransition(Transition.PressFreeze, StateID.FreezeState);
        defreezeState.AddTransition(Transition.PressReset, StateID.ResetState);

        StateBase freezeState = new FreezeState(fsm);
        freezeState.AddTransition(Transition.PressDefreeze, StateID.DefreezeState);
        freezeState.AddTransition(Transition.PressReset, StateID.ResetState);

        StateBase resetState = new ResetState(fsm);
        resetState.AddTransition(Transition.PressDefreeze, StateID.DefreezeState);
        resetState.AddTransition(Transition.PressFreeze, StateID.FreezeState);


        fsm.AddState(defreezeState);
        fsm.AddState(freezeState);
        fsm.AddState(resetState);

        fsm.InitState(freezeState);
    }



    void Update()
    {
        fsm.Update(data);
    }
}


[System.Serializable]
public class Data
{
    [SerializeField] public ReceiveState receiveState;

    public Data()
    {
        receiveState = ReceiveState.PressFreeze;
    }

}

public enum ReceiveState
{
    Null,
    /// <summary>
    /// 按下解冻
    /// </summary>
    PressDefreeze,
    /// <summary>
    /// 按下冻结
    /// </summary>
    PressFreeze,
    /// <summary>
    /// 按下重置
    /// </summary>
    PressReset
}
