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
    Vector3 move;
    bool isGrounded;

    public float LookSpeed = .01f;
    public float maximumY = 70, minimumY = -70;
    Vector3 smoothV, mouseLook;
    [SerializeField]
    public float sensitivity = 5.0f;
    [SerializeField]
    public float smoothing = 2.0f;

    public AudioSource audioSource;
    public AudioClip step1, step2, jump;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb.useGravity = false;
    }

    void Update()
    {
        //if (!audioSource.isPlaying && rb.velocity != Vector3.zero && isGrounded)
        //{
        //    if (audioSource.clip == step1)
        //        audioSource.clip = step2;
        //    else
        //        audioSource.clip = step1;
        //    audioSource.Play();
        //}
        //else if (rb.velocity == Vector3.zero)
        //    audioSource.Stop();

        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        // incrementally add to the camera look
        mouseLook += smoothV * LookSpeed;

        if (mouseLook.y > maximumY) mouseLook.y = maximumY;
        if (mouseLook.y < minimumY) mouseLook.y = minimumY;

        var lookX = Quaternion.AngleAxis(-mouseLook.x, Vector3.right).eulerAngles;
        var lookY = Quaternion.AngleAxis(mouseLook.y, Vector3.up).eulerAngles;

        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = Vector3.ClampMagnitude(move, 1);
        move.y = rb.velocity.y;
        //transform.Translate(move);
        move.x *= moveSpeed;
        move.z *= moveSpeed;
        move = transform.rotation * move;

        if (isGrounded)
            move.y = 0;
        else if (!isGrounded)
            move.y = rb.velocity.y - (gravity * Time.deltaTime);
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isGrounded = false;
            //audioSource.clip = jump;
            //audioSource.Play();
            move.y = jumpSpeed;
        }

        rb.velocity = move;

        cammy.transform.localRotation = Quaternion.Euler(-lookY.y, 0, 0);
        transform.localRotation = Quaternion.Euler(0, mouseLook.x, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = false;
    }
}