using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController cc;
    private float moveSpeed = 3f;

    bool isGrounded;
    LayerMask groundMask;
    Transform groundCheckPoint;
    float gravity = -1.5f;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        groundCheckPoint = transform;
        groundMask = (1 << LayerMask.NameToLayer("Ground"));
    }

    void Update()
    {
        Vector3 dir = Vector3.zero;

        dir.x = Input.GetAxisRaw("Horizontal");
        dir.z = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Jump());
        }

        //dir.y -= gravity * Time.deltaTime;

        //cc.Move(dir * moveSpeed * Time.deltaTime);
        //cc.Move(transform.forward * dir.z * moveSpeed * Time.deltaTime);
        //cc.Move(transform.right * dir.x * moveSpeed * Time.deltaTime);

        isGrounded = Physics.Raycast(groundCheckPoint.position, Vector3.down, 0.2f, groundMask);

        if (!isGrounded)
        {
            //cc.Move(transform.up * gravity * Time.deltaTime);
        }

        
    }

    IEnumerator Jump()
    {
        float time = 0;

        while (time < 0.5f)
        {
            cc.Move(cc.transform.up * 5f * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
