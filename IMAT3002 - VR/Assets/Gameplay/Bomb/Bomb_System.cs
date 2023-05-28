
using UnityEngine;

public class Bomb_System : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] private AudioClip fuseClip, explodeClip;

    [SerializeField] [Range(0.1f, 10)] private float radius = 3f;
    [SerializeField] [Range(0, 1000)] private float damage = 30f;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = fuseClip;
        source.loop = true;
        source.Play();

        GetComponent<SphereCollider>().enabled = false;
        Invoke("enableExplosions", 0.5f);
    }

    private void enableExplosions()
    {
        GetComponent<SphereCollider>().enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Racket")
        {
            Explosion();
        }
    }

    private void Explosion()
    {
        ParticleSystem explosion = GetComponentInChildren<ParticleSystem>();
        explosion.Play();

        source.clip = explodeClip;
        source.loop = false;
        source.Play();

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;

        damageRadius();

        Destroy(gameObject, 0.3f);
    }

    private void damageRadius()
    {
        Health[] healthSystems = FindObjectsOfType<Health>();

        foreach(Health health in healthSystems)
        {
            if(Vector3.Distance(transform.position, health.gameObject.transform.position) < radius)
            {
                float falloff = Mathf.Clamp((Vector3.Distance(transform.position, health.gameObject.transform.position) / 10) + 1, 0, 5);
                health.takeDamage(damage * falloff);
            }
        }
    }

}
