using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    enum State
    {
        Patrolling,
        Chasing,
        Attacking
    }
    State _currentState;

    
    NavMeshAgent _enemy;
    [SerializeField] Transform _mc;
    
    [SerializeField] Transform _patrolAreaCenter;
    [SerializeField] Vector2 _patrollAreaSize;

    [SerializeField] float _visionRange;

    [SerializeField] float _attackArea;

    void Awake()
    {
        _enemy = GetComponent<NavMeshAgent>();

    }

    void Start()
    {
        _currentState = State.Patrolling;
    }

    void Update()
    {
        switch (_currentState)
        {
            case State.Patrolling:
                Patroll();            
            break;

            case State.Chasing:
                Chase();            
            break;

            case State.Attacking:
                Attack();            
            break;
            
        }          
    }

    void Patroll()
    {
        Debug.Log ("Patroll, patroll, patroll. I am PA-TROL-LING");
        if (OnRange() == true)
        {
            _currentState = State.Chasing;
        }

        if (_enemy.remainingDistance < 0.5f)
        {
            SetRandomPoint();
        }

    }

    void Chase()
    {
        Debug.Log ("COME HERE YOU *****");

        _enemy.destination = _mc.position;

        if (Vector3.Distance(transform.position, _mc.position) <= _attackArea)
        {
            _currentState = State.Attacking;
        }

        if (OnRange() == false)
        {
            _currentState = State.Patrolling;
        }
    }

    void Attack()
    {
        Debug.Log ("I AM ATTACKING YOU!!!!");

        _currentState = State.Chasing;
    }

    bool OnRange()
    {
        if (Vector3.Distance(transform.position, _mc.position) <= _visionRange)
        {
            return true;
        }

        return false;

    }

    void SetRandomPoint()
    {
        float _randomX = Random.Range(-_patrollAreaSize.x/2,_patrollAreaSize.x/2);
        float _randomZ = Random.Range(-_patrollAreaSize.y/2,_patrollAreaSize.y/2);

        Vector3 _randomPoint = new Vector3 (_randomX, 0, _randomZ) + _patrolAreaCenter.position;
        _enemy.destination = _randomPoint;

    }


}
