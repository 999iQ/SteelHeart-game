using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefault : MonoBehaviour
{
    [Header("���-�� �����")]
    [SerializeField] private float _enemyHealth; //HP
    [SerializeField] private float _collisionDamage; 


    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player") 
        {
            PlayerMovment Player = collision.gameObject.GetComponent<PlayerMovment>(); //������ �� ������
            Player.TakeDamage(_collisionDamage); //��������� ����� ������
        }
    }
}
