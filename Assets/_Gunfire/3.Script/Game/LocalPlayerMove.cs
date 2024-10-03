using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayerMove : MonoBehaviour, IPunInstantiateMagicCallback
{
    #region 전역변수
    CharacterController cc;
    Animator animator;
    private float moveSpeed;

    bool isGrounded;
    bool isDash = false;
    bool dashDelay = true;
    LayerMask groundMask;
    Transform groundCheckPoint;
    float gravity = -3f;
    Coroutine jumpCoroutine, dashCoroutine;

    float mouseSensitivity = 200f;
    float dirX, dirY;
    public GameObject body;
    public GameObject dushEff;
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
        moveSpeed = gameObject.GetComponent<CharacterManager>().Data.speed;
    }

    void Update()
    {
        Vector3 dir = Vector3.zero;

        dir.x = Input.GetAxisRaw("Horizontal");
        dir.z = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && jumpCoroutine == null)
        {

            jumpCoroutine = StartCoroutine(Jump());
        }

        if(isDash == false)
        {
            cc.Move(transform.forward * dir.z * moveSpeed * Time.deltaTime);
            cc.Move(transform.right * dir.x * moveSpeed * Time.deltaTime);
        }        

        if (dashDelay && Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashCoroutine = StartCoroutine(Dash());
        }

        isGrounded = Physics.Raycast(groundCheckPoint.position, Vector3.down, 0.2f, groundMask);

        if (!isGrounded)
        {
            cc.Move(transform.up * gravity * Time.deltaTime);
        }        

        dirX += Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        dirY -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        dirY = Mathf.Clamp(dirY, -180f, 180f);

        body.transform.localRotation = Quaternion.Euler(dirY * 0.4f, 0f, 0f);

        transform.localRotation = Quaternion.Euler(0f, dirX * 0.4f, 0f);
    }

    IEnumerator Jump()
    {
        float time = 0;
     
        while (time < .4f) 
        {
            cc.Move(cc.transform.up * 9f * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }

        if(!isDash)
            gravity = -2f;
        yield return new WaitForSeconds(0.3f);
        if (!isDash)
            gravity = -3f;

        jumpCoroutine = null;
    }

    IEnumerator Dash()
    {
        StartCoroutine(PlayerWeaponUI.Instance.CoolTime());
        isDash = true;
        dashDelay = false;
        float time = 0;
        gravity = 0;
        dushEff.SetActive(true);

        while (time < .5f)
        {
            cc.Move(transform.forward * 8f * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }

        gravity = -3f;
        isDash = false;
        dushEff.SetActive(false);

        yield return new WaitForSeconds(2f);
        dashDelay = true;
        dashCoroutine = null;
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        print("Local OnPhotonInstantiate 호출");

        if (GetComponent<PhotonView>().IsMine)
        {
            CameraMove cm = FindObjectOfType<CameraMove>();
            cm.enabled = true;
            MiniMapCamera mcm = FindObjectOfType<MiniMapCamera>();
            mcm.enabled = true;
        }
        else
        {
            GameObject newprefab = PhotonNetwork.Instantiate("RemoteFox", transform.position, transform.rotation);
            Destroy(gameObject);
        }

        //EnemyUIController.Instance.inConnect = true;
        GameManager.Instance.isConnect = true;
        PlayerWeaponManager.Instance.InitSetting();
    }
}
