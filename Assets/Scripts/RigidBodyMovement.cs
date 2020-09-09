using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
[RequireComponent(typeof(AudioSource))]
public class RigidBodyMovement : MonoBehaviour
{
    public GameObject cammy;
    public float jumpSpeed = 20, moveSpeed = 5, gravity = 20;
    public Rigidbody rb;
    public Collider coll;
    public LayerMask isGroundedMask;
    Vector3 move;

    public float LookSpeed = .01f;
    public float maximumY = 70, minimumY = -70;
    Vector3 smoothV, mouseLook;
    [SerializeField]
    public float sensitivity = 5.0f;
    [SerializeField]
    public float smoothing = 2.0f;

    public AudioSource audioSource;
    public AudioClip step1, step2, jump;

    Timer timer = new Timer(.1f);

    Timer sprintTimer = new Timer(5);
    Timer sprintDelay = new Timer(2);
    bool sprinting = false;
    float speedMultiplier = 1;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb.useGravity = false;

        sprintDelay.Reset(sprintDelay.delay);

        Invoke("Popup", 5);
    }

    void Popup()
    {
        GameObject.FindGameObjectWithTag("TextPopup").GetComponent<TextPopUp>().ShowText(0);
        Invoke("Popup2", 4.1f);
    }
    void Popup2()
    {
        GameObject.FindGameObjectWithTag("TextPopup").GetComponent<TextPopUp>().ShowText(1);
        Invoke("Popup3", 4.1f);
    }
    void Popup3()
    {
        GameObject.FindGameObjectWithTag("TextPopup").GetComponent<TextPopUp>().ShowText(5);
    }

    void Update()
    {
        Vector2 md = new Vector2();
        if (!ValueHolder.GameOver || !ValueHolder.GameWin)
        {
            md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            if (sprintDelay.Check(false) && Input.GetKey(KeyCode.LeftShift))
            {
                sprinting = true;
                speedMultiplier = 1.8f;
            }
            else
            {
                sprinting = false;
                speedMultiplier = 1f;
            }

            if (sprinting && sprintTimer.Check())
            {
                sprinting = false;
                speedMultiplier = 1f;
                sprintDelay.Reset();
            }
        }

        if (!audioSource.isPlaying && rb.velocity != Vector3.zero && IsGrounded()) 
        {
            if (((!sprinting) ? timer.Check() : true))
            {
                if (audioSource.clip == step1)
                    audioSource.clip = step2;
                else
                    audioSource.clip = step1;
                audioSource.Play();
            }
        }
        else if (rb.velocity == Vector3.zero)
            audioSource.Stop();

        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        // incrementally add to the camera look
        mouseLook += smoothV * LookSpeed;

        if (mouseLook.y > maximumY) mouseLook.y = maximumY;
        if (mouseLook.y < minimumY) mouseLook.y = minimumY;

        var lookX = Quaternion.AngleAxis(-mouseLook.x, Vector3.right).eulerAngles;
        var lookY = Quaternion.AngleAxis(mouseLook.y, Vector3.up).eulerAngles;

        move = Vector3.ClampMagnitude(move, 1);
        move.y = rb.velocity.y;
        //transform.Translate(move);
        move.x *= moveSpeed * speedMultiplier;
        move.z *= moveSpeed * speedMultiplier;
        move = transform.rotation * move;

        if (IsGrounded())
        {
            move.y = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                audioSource.clip = jump;
                audioSource.Play();
                move.y = jumpSpeed;
            }

        }
        else
        {
            move.y = rb.velocity.y - (gravity * Time.deltaTime);
        }

        rb.velocity = move;

        cammy.transform.localRotation = Quaternion.Euler(-lookY.y, 0, 0);
        transform.localRotation = Quaternion.Euler(0, mouseLook.x, 0);
    }

    bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.01f, isGroundedMask))
            return true;

        return false;
    }
}