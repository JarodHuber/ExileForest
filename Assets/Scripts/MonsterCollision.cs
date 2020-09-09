using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCollision : MonoBehaviour
{
    public GameObject cam;
    public AudioClip gameOverClip;
    public Light endLight;

    Timer timer = new Timer(1f);
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ValueHolder.GameOver = true;
            cam.SetActive(true);

            endLight.enabled = true;

            audioSource.loop = false;
            audioSource.volume = 5;
            audioSource.priority = 1;
            audioSource.spatialBlend = 0;
            audioSource.Stop();
            audioSource.clip = gameOverClip;
            audioSource.Play();
        }
    }

    private void Update()
    {
        if (!ValueHolder.GameOver)
            return;

        if (timer.Check())
        {
            GameObject.FindGameObjectWithTag("SceneChanger").GetComponent<SceneChanger>().LoadScene(0);
        }
    }
}
