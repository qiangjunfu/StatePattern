using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FSMSystem.AI
{
    public class AttackState : StateBase<GameObject>
    {
        private Transform player;
        private float seeEnemyDis = 3;


        public AttackState(FSMManager<GameObject> fsmManager) : base(fsmManager)
        {
            stateID = StateID.AttackState;
            player = GameObject.Find("Player").transform;
        }

        public override void Act(GameObject npc)
        {
            Debug.Log("AttackState-----------");
        }

        public override void Reason(GameObject npc)
        {
            if (player == null)
            {
                Debug.LogError($"ChaseState -> Reason() -> player == null");
                return;
            }
            if (Vector3.Distance(player.position, npc.transform.position) > seeEnemyDis)
            {
                fsm.UpdateStateByTransition (Transition.SeeEnemy);
            }
        }
        public override void BeforeEnter()
        {
            base.BeforeEnter();
        }

        public override void BeforeLeave()
        {
            base.BeforeLeave();
        }
    }
}