using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerToEnergyBar : MonoBehaviour
{

	public Image bar;
	public Text energy;
	private PlayerController pc;

	private float ratioMaxEnergy;
	public Color regularColor;
	public Color redColor;

    void Start()
    {
		pc = GetComponent<PlayerController>();
		ratioMaxEnergy = 1 / (float) pc.MaxEnergy;

		regularColor = energy.color;
	}
	
    void Update()
    {
		bar.fillAmount = pc.Energy * ratioMaxEnergy;
		if (pc.Energy <= 15)
			energy.color = redColor;
		else
			energy.color = regularColor;
		energy.text = pc.Energy.ToString();
    }
}
