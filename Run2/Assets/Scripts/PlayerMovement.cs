using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rigid; //리지드바디
    Animator anim; //애니메이터

    public float runSpeed = 5; // 달리기 속도
    public float jumpPower = 5; //점프 높이
    public float rightSpeed = 7; //좌우 움직임 스피드

    float hAxis, vAxis;

    public bool isJump = false; //점프관련 false여야 가능
    public bool isBump = false; //벽에 부딪혔나?
    public LayerMask layer;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Running();
        Jump();
    }

    private void FixedUpdate()
    {
        CheckGround();
        FreezeRotation();
    }

    //자동으로 앞으로 달리기
    void Running()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
        Movement();

        if (isBump)
        {
            anim.SetBool("isRun", false);
            runSpeed = 0;
        }
    }

    //좌우움직임
    void Movement()
    {
        hAxis = Input.GetAxisRaw("Horizontal");

        Vector3 dir = new Vector3(hAxis, 0, vAxis);

        transform.position += dir * rightSpeed * Time.deltaTime;
    }

    //점프
    public void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJump && !isBump)
        {//눌럿을때 + 점프상태가 아니면

            Vector3 jumpHight = Vector3.up * jumpPower;
            rigid.AddForce(jumpHight, ForceMode.Impulse);
            anim.SetTrigger("Jump");
        }
    }
    void CheckGround()
    {
        //다중 점프 방지용 땅체크
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, 0.8f, layer))
        {
            isJump = false;
        }
        else{ isJump = true; }
    }

    //충돌관련
    private void OnTriggerEnter(Collider other)
    {
        //벽일때
        if(other.transform.CompareTag("Wall"))
        {
            StartCoroutine("Bump");
        }
    }

    //부딪혔을때 살짝 뒤로 밀림 + 애니메이션 실행
    void BumpedWall()
    {
        transform.position -= new Vector3(0, 0, 0.5f);
        anim.SetTrigger("Bump");
    }

    //부딪혔을때 상태변화
    IEnumerator Bump()
    {
        isBump = true;
        BumpedWall();
        yield return new WaitForSeconds(3.2f);
        isBump = false;
        runSpeed = 10;
    }

    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }
}
