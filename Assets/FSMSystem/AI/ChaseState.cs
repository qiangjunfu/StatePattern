using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSMSystem.AI
{
    public class ChaseState : StateBase<GameObject>
    {
        private Transform player;
        /// <summary>
        /// 移动速度
        /// </summary>
        private float moveSpeed = 5;
        /// <summary>
        /// 平滑旋转速度
        /// </summary>
        private float rotSoomth = 5;
        /// <summary>
        /// 丢失距离
        /// </summary>
        private float lostDis = 8;
        /// <summary>
        /// 追上距离
        /// </summary>
        private float chaseUpDis = 3;


        public ChaseState(FSMManager<GameObject> fsmManager) : base(fsmManager)
        {
            stateID = StateID.ChaseState;
            player = GameObject.Find("Player").transform;
        }

        public override void Act(GameObject npc)
        {
            Debug.Log("ChaseState-----------");
            Vector3 dir = player.position - npc.transform.position;
            dir.y = 0;
            Quaternion q = Quaternion.LookRotation(dir);
            npc.transform.rotation = Quaternion.Lerp(npc.transform.rotation, q, rotSoomth * Time.deltaTime);
            //npc.transform.LookAt(player.position);
            npc.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        }

        public override void Reason(GameObject npc)
        {
            if (player == null)
            {
                Debug.LogError($"ChaseState -> Reason() -> player == null");
                return;
            }
            if (Vector3.Distance(player.position, npc.transform.position) >= lostDis)
            {
                fsm.UpdateStateByTransition(Transition.LostEnemy);
            }
            if (Vector3.Distance(player.position, npc.transform.position) <= chaseUpDis)
            {
                fsm.UpdateStateByTransition(Transition.ChaseUpEnemy);
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