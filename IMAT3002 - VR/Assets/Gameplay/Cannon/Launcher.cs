
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] private GameObject ball_OBJ;
    public Transform ball_Origin;

    private Vector3 cannon_Target;
    private Vector3 cannon_Original;
    [SerializeField] private Vector3 cannon_Offset;

    [SerializeField] private Vector2 shootRecovery;
    [SerializeField] private Vector2 shootMean;
    [SerializeField] [Range(0,10)] private float cannonSpeed = 3;
    private float shootMeanRandom;
    private float timer, shootdelay;
  
    [SerializeField] private Vector2 BlastPower_Offset;
    private float blastPowerTarget;
    [HideInInspector] public float BlastPower = 5;

    private AudioSource source;
    [SerializeField] private AudioClip shootClip;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        cannon_Original = ball_Origin.parent.gameObject.transform.rotation.eulerAngles;
        cannon_Target = ball_Origin.parent.gameObject.transform.rotation.eulerAngles;
        blastPowerTarget = Random.Range(BlastPower_Offset.x, BlastPower_Offset.y);

        shootMeanRandom = Random.Range(shootMean.x, shootMean.y);

        GetComponent<Health>().deathAction += death;
    }

    private void FixedUpdate()
    {
        if (shootdelay > 0)
        {
            shootdelay -= Time.deltaTime;
            return;
        }

        if(timer >= shootMeanRandom)
        {
            timer = 0;
            fireBall();
        }
        else
        {
            timer += Time.deltaTime;
        }

        BlastPower = Mathf.MoveTowards(BlastPower, blastPowerTarget, cannonSpeed * Time.deltaTime);
        ball_Origin.parent.gameObject.transform.rotation = Quaternion.RotateTowards(ball_Origin.parent.gameObject.transform.rotation, Quaternion.Euler(cannon_Target), cannonSpeed * Time.deltaTime);
    }

    private void fireBall()
    {
        GameObject newBall = Instantiate(ball_OBJ, ball_Origin.position, Quaternion.identity);
        source.clip = shootClip;
        source.Play();

        Destroy(newBall.gameObject, 10);

        Rigidbody ballRB = newBall.GetComponent<Rigidbody>();

        ballRB.velocity = ball_Origin.up * BlastPower;

        Vector3 randomOffset = new Vector3(0, Random.Range(cannon_Offset.y * -1, cannon_Offset.y), Random.Range(cannon_Offset.z * -1, cannon_Offset.z));
        cannon_Target = cannon_Original + randomOffset;

        blastPowerTarget = Random.Range(BlastPower_Offset.x, BlastPower_Offset.y);

        shootMeanRandom = Random.Range(shootMean.x, shootMean.y);
        shootdelay = Random.Range(shootRecovery.x,shootRecovery.y);
    }

    private void death()
    {
        FindObjectOfType<Racket>().rewardPoints(50);
        Destroy(gameObject);
    }
}
