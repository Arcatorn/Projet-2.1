using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiculeFB : MonoBehaviour {

public static int vehiculeState = 0;
public GameObject[] particuleFenetreGO;
public CameraShake cameraShake;
public GameObject[] tempestGO;
public GameObject[] particuleMoteur;
	
	private void Start() {
		EtatSurregime();
	}

				// ETAT STOP
				// • Stop ambiance déplacement

				// ETAT DEPLACEMENT
				// • Play son d'ambiance

				// ETAT SURREGIME
				// • Accéléré speed particule devant fenêtres
				// • Play son surrégime
				// • Augmente camera shake
				// • Anim gros moteur Killian

				// ETAT TEMPETE
				// • Activer gameObject Tempest
				// • Coordonner son eclair avec anim eclair
				// • Son tempest ambiance
				// • Stop particules devant fenêtres
				// • Randomisation anim eclairs

	public void EtatStop()
	{
		for (int i = 0; i < particuleFenetreGO.Length; i++)
		{
			particuleFenetreGO[i].GetComponent<ParticleSystem>().Pause();
		}
		cameraShake.Shake(0,0,0);
	}

	public void EtatDeplacement()
	{
		for (int i = 0; i < particuleFenetreGO.Length; i++)
		{
			particuleFenetreGO[i].GetComponent<ParticleSystem>().Play();
		}
		cameraShake.Shake(0.05f, 99999, 1);

	}

	public void EtatSurregime()
	{
		for (int i = 0; i < particuleFenetreGO.Length; i++)
		{
			var main = particuleFenetreGO[i].GetComponent<ParticleSystem>().main;
			var maBITE = main.startSpeed.constant;
			maBITE = 5;
		}
		cameraShake.Shake(0.15f, 9999, 1);
	}

	public void EtatTempete()
	{

	}
}
