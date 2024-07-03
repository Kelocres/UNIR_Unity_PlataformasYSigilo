using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowHealth : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI showLife;
    [SerializeField] private HealthSystem healthSystem;
    // Start is called before the first frame update
    void Start()
    {
        if (showLife == null) showLife = GetComponent<TextMeshProUGUI>();

        healthSystem.delSetLife += SetLife;
    }

    // Update is called once per frame
    public void SetLife(float currentLive)
    {
        if (showLife == null) return;
        if (currentLive > 0)
            showLife.text = "VIDA: " + currentLive;
        else
            showLife.enabled = false;
    }
}
