using FSMSystem;
using FSMSystem.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private FSMManager<GameObject> fsmManager;

    private void Start()
    {
        fsmManager = new FSMManager<GameObject>();

        StateBase<GameObject> patrolState = new  PatrolState (fsmManager);
        patrolState.AddTransition(Transition.SeeEnemy, StateID.ChaseState);

        StateBase<GameObject> chaseState = new ChaseState(fsmManager);
        chaseState.AddTransition(Transition.LostEnemy, StateID.PatrolState);
        chaseState.AddTransition(Transition.ChaseUpEnemy, StateID.AttackState);

        StateBase<GameObject> attackState = new AttackState(fsmManager);
        attackState.AddTransition(Transition.SeeEnemy, StateID.ChaseState);

        fsmManager.AddState(patrolState);
        fsmManager.AddState(chaseState);
        fsmManager.AddState(attackState);

        fsmManager.InitState(patrolState);
    }

    private void Update()
    {
        fsmManager.Update(this.gameObject);
    }
}
