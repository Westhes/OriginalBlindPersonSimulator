using TMPro;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lookPercentageLabel;

    [HideInInspector] public float LookPercentage;

    private void Update()
    {
        lookPercentageLabel.text = LookPercentage.ToString("F3");
    }
}