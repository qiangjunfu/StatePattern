using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSMSystem
{
    /// <summary>
    /// 状态
    /// </summary>
    public enum StateID
    {
        NullStateID,
        /********* Simhost ********/
        /// <summary>
        /// 解冻状态
        /// </summary>
        DefreezeState,
        /// <summary>
        /// 冻结状态
        /// </summary>
        FreezeState,
        /// <summary>
        /// 重置状态
        /// </summary>
        ResetState,

        /********* AI ********/
        /// <summary>
        /// 巡逻状态
        /// </summary>
        PatrolState,
        /// <summary>
        /// 追击状态
        /// </summary>
        ChaseState,
        /// <summary>
        /// 攻击状态
        /// </summary>
        AttackState
    }
    /// <summary>
    /// 转换条件
    /// </summary>
    public enum Transition
    {
        NullTransition,
        /********* Simhost ********/
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
        PressReset,

        /********* AI ********/
        /// <summary>
        /// 看见敌人
        /// </summary>
        SeeEnemy,
        /// <summary>
        /// 丢失敌人
        /// </summary>
        LostEnemy,
        /// <summary>
        /// 追上敌人
        /// </summary>
        ChaseUpEnemy
    }

    /// <summary>
    /// 状态基类
    /// </summary>
    public abstract class StateBase
    {
        protected FSMManager fsm;
        protected StateID stateID;
        protected Dictionary<Transition, StateID> transStateDic = new Dictionary<Transition, StateID>();

        public StateID StateID { get => stateID; }
        public StateBase(FSMManager fsm) { this.fsm = fsm; }


        public virtual void BeforeEnter() { }
        public virtual void BeforeLeave() { }
        public abstract void Act(Data data);
        public abstract void Reason(Data data);


        /// <summary>
        /// 添加转换条件, 当前状态才能转换到另一个状态
        /// </summary>
        public void AddTransition(Transition trans, StateID stateID)
        {
            if (trans == Transition.NullTransition)
            {
                //string typeName = this.GetType().ToString();//空间名.类名 
                Debug.LogError($"{this.GetType().Name } -> AddTransition() -> trans ==  Transition.NullTransition  ");
                return;
            }
            if (transStateDic.ContainsKey(trans))
            {
                Debug.LogError($"{this.GetType().Name } -> AddTransition() -> transStateDic.ContainsKey (trans) ");
                return;
            }

            transStateDic.Add(trans, stateID);
        }

        /// <summary>
        /// 移除转换条件
        /// </summary>
        public void RemoveTransition(Transition trans)
        {
            if (trans == Transition.NullTransition)
            {
                Debug.LogError($"{this.GetType().Name } -> RemoveTransition() -> trans ==  Transition.NullTransition  ");
                return;
            }
            if (!transStateDic.ContainsKey(trans))
            {
                Debug.LogError($"{this.GetType().Name } -> RemoveTransition() -> !transStateDic.ContainsKey (trans) ");
                return;
            }
            transStateDic.Remove(trans);
        }

        /// <summary>
        /// 获取状态ID通过转换条件
        /// </summary>
        public StateID GetStateIdByTrans(Transition trans)
        {
            if (trans == Transition.NullTransition)
            {
                Debug.LogError($"{this.GetType().Name } -> GetStateIdByTrans() -> trans ==  Transition.NullTransition  ");
                return StateID.NullStateID;
            }
            if (!transStateDic.ContainsKey(trans))
            {
                Debug.LogError($"{this.GetType().Name } -> GetStateIdByTrans() -> !transStateDic.ContainsKey (trans) ");
                return StateID.NullStateID;
            }
            return transStateDic[trans];
        }
    }
}