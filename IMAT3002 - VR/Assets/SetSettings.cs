using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetSettings : MonoBehaviour
{
    [SerializeField] private Slider moveSlider;
    [SerializeField] private Text moveText;
    [SerializeField] private float maxMoveSpeed = 10;

    [SerializeField] private Slider rotateSlider;
    [SerializeField] private Text rotateText;
    [SerializeField] private float maxRotateSpeed = 10;

    [SerializeField] private PlayerSettings setting;

    private void Awake()
    {
        moveSlider.maxValue = maxMoveSpeed;
        moveSlider.value = setting.moveSpeed;

        rotateSlider.maxValue = maxRotateSpeed;
        rotateSlider.value = setting.rotateSpeed;
    }
    private void FixedUpdate()
    {
        moveText.text = moveSlider.value.ToString("F0");
        rotateText.text = rotateSlider.value.ToString("F0");
    }

    public void SetValues()
    {
        setting.moveSpeed = moveSlider.value;
        setting.rotateSpeed = rotateSlider.value;
    }
}
