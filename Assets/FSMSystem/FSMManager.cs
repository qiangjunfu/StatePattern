using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSMSystem
{
    public class FSMManager
    {
        /// <summary>
        /// 当前状态ID
        /// </summary>
        private StateID currentStateID;
        /// <summary>
        /// 当前状态
        /// </summary>
        private StateBase currentState;
        /// <summary>
        /// 存储的状态字典
        /// </summary>
        private Dictionary<StateID, StateBase> stateDic = new Dictionary<StateID, StateBase>();



        public void Update(Data data)
        {
            currentState.Act(data);
            currentState.Reason(data);
        }

        public void InitState(StateBase state)
        {
            if (currentState == null)
            {
                currentState = state;
                currentStateID = state.StateID;
                currentState.BeforeEnter();
            }
        }

        /// <summary>
        /// 更新状态通过转换条件
        /// </summary>
        public void UpdateStateByTransition(Transition trans)
        {
            if (trans == Transition.NullTransition)
            {
                Debug.LogError($"{this.GetType().Name } -> UpdateStateByTransition -> trans == Transition.NullTransition");
                return;
            }

            StateID id = currentState.GetStateIdByTrans(trans);
            if (id == StateID.NullStateID)
            {
                Debug.LogError($"{this.GetType().Name } -> UpdateStateByTransition -> id == StateID.NullStateID");
                return;
            }
            if (!stateDic.ContainsKey(id))
            {
                Debug.LogError($"{this.GetType().Name } -> UpdateStateByTransition -> !stateDic.ContainsKey(id)");
                return;
            }

            currentState.BeforeLeave();
            StateBase state = stateDic[id];
            currentState = state;
            currentStateID = state.StateID;
            currentState.BeforeEnter();
        }

        /// <summary>
        /// 添加状态
        /// </summary>
        public void AddState(StateBase state)
        {
            if (state == null)
            {
                Debug.LogError($"{this.GetType().Name } -> AddState() -> state == null ");
                return;
            }

            if (currentState == null)
            {
                //提取出来 由单独方法执行初始化状态
                //currentState = state;
                //currentStateID = state.StateID;
                //currentState.BeforeEnter();
            }

            if (stateDic.ContainsKey(state.StateID))
            {
                Debug.LogError($"{this.GetType().Name } -> AddState() -> stateDic.ContainsKey(state.StateID) : {state.StateID }");
                return;
            }

            stateDic.Add(state.StateID, state);
        }

        /// <summary>
        /// 移除状态
        /// </summary>
        public void RemoveState(StateID stateID)
        {
            if (stateID == StateID.NullStateID)
            {
                Debug.LogError($"{this.GetType().Name } -> RemoveState() -> stateID == StateID.NullStateID");
                return;
            }
            if (stateDic.ContainsKey(stateID) == false)
            {
                Debug.LogError($"{this.GetType().Name } -> RemoveState() -> stateDic .ContainsKey(stateID) == false ：{stateID}");
                return;
            }
            stateDic.Remove(stateID);
        }


    }
}