using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovment : MonoBehaviour
{
    [Header(" = ���-�� ������ = ")]
    [SerializeField] private float _health; //HP
    [SerializeField] private float _startTimeGodMode; //��������� ���������� (const)
    private float _timeGodMode;  //(No const)
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _jumpForce;
    private Rigidbody _rigidbody;
    public bool IsGrounded;
    public int BoostInpulse = 5000;
    private float _directionX;

    [Header(" = ��-��� ������ = ")]
    public Image HpBar; 
    public Text CountHpBar; //���-�� �������� � ������ �� ������ 

    [Header(" = ����� ������ = ")]
    public GameObject CanvasDead;

    private void Start()
    {
        CanvasDead.gameObject.SetActive(false); //�� ������ �� �� ����� ����� ������
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_timeGodMode > 0) //����������� ������ ���������� ����������
            _timeGodMode -= Time.deltaTime;

        Jump();
        Boost();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //������������ ������
        var horizontalAxis = Input.GetAxis("Horizontal");
        _rigidbody.velocity = new Vector3(horizontalAxis * _speed, _rigidbody.velocity.y, 0);

        /*_rigidbody.velocity = new Vector3(Input.GetAxis("Horizontal") * _speed, _rigidbody.velocity.y,
            Input.GetAxis("Vertical") * _speed); //move */

        //������� ������
        if (_rigidbody.velocity.x < -.01) 
            transform.eulerAngles = new Vector3(0, 180, 0);
        else if (_rigidbody.velocity.x > .01)
            transform.eulerAngles = new Vector3(0, 0, 0);
    }

    private void Jump()
    {
        //������ ���� ����� � ����� _jumpForce
        if (Input.GetButtonDown("Jump") && IsGrounded) //jump
        {
            _rigidbody.AddForce(new Vector3(0, _jumpForce, 0));
        }
    }

    private void OnCollisionEnter()
    {
        IsGrounded = true; //�� �����
    }
    private void OnCollisionExit()
    {
        IsGrounded = false; //� �������
    }

    private void Boost()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed = 15f;
        }
        else
        {
            _speed = 10f;
        }

        _directionX = Input.GetAxis("Horizontal") * _speed;
    }

    public void TakeDamage(float damage) //����� ��������� ����� �� ������
    {
        if(_timeGodMode <= 0) //��������� ����������
        {
            _health -= damage;

            HpBar.fillAmount = _health / 100; //������ ������� �������� � UI
            CountHpBar.text = "" + _health; //������� �������������� �� � ������ :)

            _timeGodMode = _startTimeGodMode;
            
        }

        if(_health <= 0)
        {
            //Destroy(gameObject.GetComponent<PlayerMovment>()); //������� ������

            CanvasDead.gameObject.SetActive(true); //����� ������ �����������
            Destroy(gameObject);

        }
    }

}