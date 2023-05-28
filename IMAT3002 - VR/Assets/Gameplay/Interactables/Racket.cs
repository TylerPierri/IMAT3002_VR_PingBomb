using TMPro;
using UnityEngine;

public class Racket : MonoBehaviour
{
    [SerializeField] private BoxCollider netCollider;
    [SerializeField] private CapsuleCollider poleCollider;
    [SerializeField] private AudioClip bounceClip;
    private AudioSource source;

    [SerializeField] private TextMeshProUGUI scoreText;
    private int score;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        CharacterController playerCollider = GameObject.FindWithTag("Player").GetComponent<CharacterController>();
        
        Physics.IgnoreCollision(netCollider, playerCollider);
        Physics.IgnoreCollision(poleCollider, playerCollider);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            bounce();
            rewardPoints(1);
        }
    }

    private void bounce()
    {
        source.clip = bounceClip;
        source.Play();
    }

    public void rewardPoints(int add)
    {
        score += add;
        scoreText.text = score.ToString();
    }
}
