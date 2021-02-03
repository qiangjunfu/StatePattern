//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace FSMSystem
//{
//    public class PatrolState : StateBase
//    {
//        private List<Transform> wayPointList;
//        private Transform player;
//        private int index;
//        private float moveSpeed = 5;
//        private float rotSoomth = 10;
//        private float relativeDis = 5;


//        public PatrolState(FSMManager fsmManager) : base(fsmManager)
//        {
//            stateID = StateID.PatrolState;

//            wayPointList = new List<Transform>();
//            Transform[] trans = GameObject.Find("WayPoints").GetComponentsInChildren<Transform>();
//            for (int i = 0; i < trans.Length; i++)
//            {
//                if (i != 0)
//                {
//                    wayPointList.Add(trans[i]);
//                }
//            }
//            player = GameObject.Find("Player").transform;
//        }

//        public override void Act(GameObject npc)
//        {
//            Debug.Log("PatrolState-----------");
//            //npc.transform.LookAt(wayPointList[index].position);
//            Vector3 dir = wayPointList[index].position - npc.transform.position;
//            dir.y = 0;
//            Quaternion q = Quaternion.LookRotation(dir);
//            npc.transform.rotation = Quaternion.Lerp(npc.transform.rotation, q, rotSoomth * Time.deltaTime);
//            npc.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
//            if (Vector3.Distance(npc.transform.position, wayPointList[index].position) < 2)
//            {
//                index++;
//                index %= wayPointList.Count;
//            }
//        }

//        public override void Reason(GameObject npc)
//        {
//            if (player == null)
//            {
//                Debug.LogError($"PatrolState -> Reason() -> player == null");
//                return;
//            }
//            if (Vector3.Distance(player.position, npc.transform.position) < relativeDis)
//            {
//                fsmManager.ChangeState(Transition.SeeEnemy);
//            }
//        }

//        public override void BeforeEnter()
//        {
//            base.BeforeEnter();
//        }

//        public override void BeforeLeave()
//        {
//            base.BeforeLeave();
//        }
//    }
//}