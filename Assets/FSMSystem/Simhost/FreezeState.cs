using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FSMSystem.Simhost
{
    public class FreezeState : StateBase
    {
        public FreezeState(FSMManager fsm) : base(fsm)
        {
            stateID = StateID.FreezeState;
        }

        public override void Act(Data data)
        {
            Debug.Log($"{this.GetType() } -> Act()");
        }

        public override void Reason(Data data)
        {
            if (data.receiveState == ReceiveState.PressDefreeze)
            {
                fsm.UpdateStateByTransition(Transition.PressDefreeze);
            }

            if (data.receiveState == ReceiveState.PressReset)
            {
                fsm.UpdateStateByTransition(Transition.PressReset);
            }
        }

        public override void BeforeEnter()
        {
            base.BeforeEnter();
            Debug.Log($"{this.GetType() } -> BeforeEnter()");
        }

        public override void BeforeLeave()
        {
            base.BeforeLeave();
            Debug.Log($"{this.GetType() } -> BeforeLeave()");
        }
    }
}