﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform laserPoint;
    public GameObject impact;

    public GameObject Arm;
    public GameObject Player;

    private bool isFacingRight;
    public LineRenderer line;

    [SerializeField]
    private float damage;

    [SerializeField] private AudioClip[] flashclips;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;


        if (mousePosition.x > 0 && !isFacingRight)
        {
            Flip();
        }

        if(mousePosition.x < 0 && isFacingRight)
        {
            Flip();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Shoot());
            AudioClip clip = GetRandomClip();
            audioSource.PlayOneShot(clip);

        }
    }

    private AudioClip GetRandomClip()
    {
        return flashclips[UnityEngine.Random.Range(0, flashclips.Length)];
    }

    IEnumerator Shoot()
    {
        RaycastHit2D projectLaser = Physics2D.Raycast(laserPoint.position, laserPoint.right);

        if (projectLaser)
        {
            Roak roak = projectLaser.transform.GetComponent<Roak>();

            if(roak != null)
            {
                roak.takeDamage(damage);
            }

            Instantiate(impact, projectLaser.point, Quaternion.identity);

            line.SetPosition(0, laserPoint.position);
            line.SetPosition(1, projectLaser.point);
        }
        else
        {
            line.SetPosition(0, laserPoint.position);
            line.SetPosition(1, laserPoint.position + laserPoint.right * 100);
        }

        line.enabled = true;

        yield return new WaitForSeconds(0.02f);

        line.enabled = false;
       
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        if(isFacingRight)
        {
            Arm.transform.localScale = new Vector3 (.54f, -.53f, 0f);
        }

        if (!isFacingRight)
        {
            Arm.transform.localScale = new Vector3(.54f, .53f, 0f);
        }
    }
}
