using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetSettings : MonoBehaviour
{
    [SerializeField] private PlayerSettings settings;
    [SerializeField] private ActionBasedContinuousMoveProvider action;
    [SerializeField] private ActionBasedContinuousTurnProvider turn;

    private void Start()
    {
        GetComponent<Health>().deathAction += onDeath;
    }
    private void Update()
    {
        action.moveSpeed = settings.moveSpeed;
        turn.turnSpeed = settings.rotateSpeed;
    }

    private void onDeath()
    {
        SceneManager.LoadScene("Start Scene");
    }

}
