using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Turret_Controller : MonoBehaviour
{
    scr_DeathSproutReset_Controller s_DeathSproutReset_Controller;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float cooldown;
    [SerializeField] private int maxShots;
    [SerializeField] private GameObject shootingPos;
    private LineRenderer line;
    [SerializeField] private AnimationCurve animCurve;
    private float lerper;
    private AudioSource audio;
    [SerializeField] private AudioClip deathAudio, shootAudio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        s_DeathSproutReset_Controller = GetComponent<scr_DeathSproutReset_Controller>();
        StartCoroutine(StartShooting());
        transform.localScale = transform.position.x > .1f ? new Vector2(-1, 1) : new Vector2(1, 1);
    }
    IEnumerator StartShooting()
    {
        yield return new WaitForSeconds(1);
        ShootRay();
        //StartCoroutine(Cooldown());
    }

    private void Update()
    {
        if (line.enabled)
        {
            line.SetPosition(0, new Vector3(shootingPos.transform.position.x, shootingPos.transform.position.y, -3));
            lerper += 1 * Time.deltaTime;
            line.widthMultiplier = Mathf.Lerp(0, 1, animCurve.Evaluate(lerper));
            RaycastHit2D ray = Physics2D.Raycast(shootingPos.transform.position, shootingPos.transform.up, 200, LayerMask.GetMask("SproutObstacle") + LayerMask.GetMask("Flower") + LayerMask.GetMask("BulletDestroyer") + LayerMask.GetMask("Player"));
            if (ray)
            {
                line.SetPosition(1, new Vector3(ray.point.x, ray.point.y, -3));
                if (ray.collider.gameObject.layer == LayerMask.NameToLayer("Flower"))
                { ray.collider.GetComponent<scr_Flower_Controller>().TakeDamage();   }
                if (ray.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                { ray.collider.GetComponent<scr_Player_Controller>().Stunned(); }
                if (ray.collider.gameObject.layer == LayerMask.NameToLayer("SproutObstacle"))
                { ray.collider.GetComponent<scr_SproutObstacle_Controller>().takeDamage(); }
            }
        }
    }
    void ShootRay()
    {
        audio.PlayOneShot(shootAudio);
        lerper = 0;
        line.widthMultiplier = 1;
        line.enabled = true;
        StartCoroutine(turnOffRay());
    }
    IEnumerator turnOffRay()
    {
        yield return new WaitForSeconds(1f);
        line.enabled = false;
        audio.PlayOneShot(deathAudio);
        s_DeathSproutReset_Controller.Die();
    }


}
