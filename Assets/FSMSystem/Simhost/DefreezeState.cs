using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FSMSystem
{
    public class DefreezeState : StateBase
    {
        public DefreezeState(FSMManager fsm) : base(fsm)
        {
            stateID = StateID.DefreezeState;
        }

        public override void Act(Data data)
        {
            Debug.Log($"{this.GetType() } -> Act()");

        }

        public override void Reason(Data data)
        {
            if (data.receiveState == ReceiveState.PressFreeze)
            {
                fsm.UpdateStateByTransition(Transition.PressFreeze);
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