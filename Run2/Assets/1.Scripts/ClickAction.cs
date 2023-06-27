using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class ClickAction : MonoBehaviour
{
    //걷기속도
    float finalSpeed = 0f;
    float walkSpeed = 2.5f;
    float runSpeed = 10f;

    float attackColltime = 0.8f;
    float dealTime;

    bool isMove = false;
    public bool isAttack = false;
    bool isStandAttack = false;

    [SerializeField]
    private Transform arrivalPoint;
    private RaycastHit hit;

    Coroutine standAttackCor;

    //필수컴포넌트
    NavMeshAgent agent;
    public Transform navMeshSurface;
    Animator anim;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        isStandAttack = CheckLeftShift();
        CheckDistance();
    }
    void LateUpdate()
    {
        ClickMoveToWorld();
    }

    void ClickMoveToWorld()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (isStandAttack) //왼쪽 쉬프트 눌려있음
            {
                if (Input.GetMouseButtonDown(0) && !isAttack) //마우스 클릭함
                {
                    isAttack = true;
                    isMove = false;
                    //standAttackCor = StartCoroutine(AttackInPlace(hit.point, isAttack));
                    AttackInPlace1(hit.point, isAttack);
                }
            }
            else if (Input.GetMouseButton(0))
            {
                if (!hit.transform.CompareTag("Ground"))
                {
                    ClickTaget target = hit.transform.GetComponent<ClickTaget>();
                    target._clickTagSwitch(hit);
                }
                if (!isAttack)
                {
                    isMove = true;
                    agent.SetDestination(hit.point);
                    agent.speed = SetSpeed();
                    anim.SetFloat("Speed", SetSpeed());
                }
            }

            if (agent.remainingDistance <= 0.1f)
            {
                isMove = false;
                agent.speed = 0;
                anim.SetFloat("Speed", SetSpeed());
                arrivalPoint.gameObject.SetActive(isMove);
            }
        }
    }
    IEnumerator AttackInPlace(Vector3 _hitPoint, bool _isAttack)
    {
        if (_isAttack)
        {
            agent.destination = transform.position;
            transform.rotation = Quaternion.LookRotation(_hitPoint - transform.position);
            anim.SetBool("isAttack", _isAttack);
            anim.SetFloat("Speed", SetSpeed());
        }
        yield return new WaitForSeconds(attackColltime);
        isAttack = false;
        yield return new WaitForSeconds(attackColltime/2);
        anim.SetBool("isAttack", isAttack);
    }

    void  AttackInPlace1(Vector3 _hitPoint, bool _isAttack)
    {
        if (_isAttack)
        {
            agent.destination = transform.position;
            transform.rotation = Quaternion.LookRotation(_hitPoint - transform.position);
            anim.SetBool("isAttack", _isAttack);
            anim.SetFloat("Speed", SetSpeed());
        }
        
    }
    float SetSpeed()
    {
        bool isRun = true;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            isRun = false;
        }
        finalSpeed = isMove ? (isRun ? runSpeed : walkSpeed) : 0;
        return finalSpeed;
    }

    bool CheckLeftShift()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isStandAttack = true;
        }
        else
        {
            isStandAttack = false;
        }

        return isStandAttack;
    }

    void CheckDistance()
    {
        if(Vector3.Distance(this.transform.position, navMeshSurface.position) > 10f)
        {
            navMeshSurface.position = this.transform.position;
            navMeshSurface.GetComponent<NavMeshSurface>().BuildNavMesh();
        }
    }

    public void StandAttackBooled()
    {
        isAttack= false;
        anim.SetBool("isAttack", isAttack);
    }
}
