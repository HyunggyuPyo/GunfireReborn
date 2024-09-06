using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemotePlayerMove : MonoBehaviour, IPunInstantiateMagicCallback
{
    #region 전역변수
    CharacterController cc;
    Animator animator;
    private float moveSpeed = 3f;

    bool isGrounded;
    LayerMask groundMask;
    Transform groundCheckPoint;
    float gravity = -4f;
    Coroutine jumpCoroutine;

    float mouseSensitivity = 200f;
    float dirX;
    #endregion

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        groundCheckPoint = transform;
        groundMask = (1 << LayerMask.NameToLayer("Ground"));
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector3 dir = Vector3.zero;

        dir.x = Input.GetAxisRaw("Horizontal");
        dir.z = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && jumpCoroutine == null)
        {

            jumpCoroutine = StartCoroutine(Jump());
            animator.SetTrigger("Jump");
        }

        cc.Move(transform.forward * dir.z * moveSpeed * Time.deltaTime);
        cc.Move(transform.right * dir.x * moveSpeed * Time.deltaTime);

        animator.SetFloat("Xpos", dir.x);
        animator.SetFloat("Ypos", dir.z);
        animator.SetFloat("Speed", dir.magnitude);

        isGrounded = Physics.Raycast(groundCheckPoint.position, Vector3.down, 0.2f, groundMask);

        if (!isGrounded)
        {
            cc.Move(transform.up * gravity * Time.deltaTime);
        }

        dirX += Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;

        transform.localRotation = Quaternion.Euler(0f, dirX * 0.4f, 0f);
    }

    IEnumerator Jump()
    {
        float time = 0;

        while (time < .4f)
        {
            cc.Move(cc.transform.up * 8f * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }

        gravity = -2f;
        yield return new WaitForSeconds(0.2f);
        gravity = -4f;

        jumpCoroutine = null;
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        print("Remote OnPhotonInstantiate 호출");
        
    }
}
