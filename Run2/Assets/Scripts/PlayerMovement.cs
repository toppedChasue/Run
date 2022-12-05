using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rigid; //������ٵ�
    Animator anim; //�ִϸ�����

    public float runSpeed = 5; // �޸��� �ӵ�
    public float jumpPower = 5; //���� ����
    public float rightSpeed = 7; //�¿� ������ ���ǵ�

    float hAxis, vAxis;

    public bool isJump = false; //�������� false���� ����
    public bool isBump = false; //���� �ε�����?
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

    //�ڵ����� ������ �޸���
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

    //�¿������
    void Movement()
    {
        hAxis = Input.GetAxisRaw("Horizontal");

        Vector3 dir = new Vector3(hAxis, 0, vAxis);

        transform.position += dir * rightSpeed * Time.deltaTime;
    }

    //����
    public void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJump && !isBump)
        {//�������� + �������°� �ƴϸ�

            Vector3 jumpHight = Vector3.up * jumpPower;
            rigid.AddForce(jumpHight, ForceMode.Impulse);
            anim.SetTrigger("Jump");
        }
    }
    void CheckGround()
    {
        //���� ���� ������ ��üũ
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, 0.8f, layer))
        {
            isJump = false;
        }
        else{ isJump = true; }
    }

    //�浹����
    private void OnTriggerEnter(Collider other)
    {
        //���϶�
        if(other.transform.CompareTag("Wall"))
        {
            StartCoroutine("Bump");
        }
    }

    //�ε������� ��¦ �ڷ� �и� + �ִϸ��̼� ����
    void BumpedWall()
    {
        transform.position -= new Vector3(0, 0, 0.5f);
        anim.SetTrigger("Bump");
    }

    //�ε������� ���º�ȭ
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
