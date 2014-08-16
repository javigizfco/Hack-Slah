using UnityEngine;
using System.Collections;

public class VidaPersonaje : MonoBehaviour {
	public int vidaMaxima = 100;
	public int vidaActual = 100;
		
	public float longitudBarraVida;

	// Se ejecuta cuando se inicializa
	void Start () {
		longitudBarraVida = Screen.width / 2;
	}
	
	// Se ejecuta una vez por cada frame
	void Update () {
		ajustarVida (0);
	}

	// Funcion que se ejecuta para dibujar sobre la GUI
	void OnGUI(){
		//Screen.width nos devuelve el ancho de la pantalla
		GUI.Box (new Rect(10,10,longitudBarraVida,20),vidaActual + "/" + vidaMaxima);
	}

	public void ajustarVida(int ajuste){
		vidaActual += ajuste;

		//se comprueba que el ajusto no sobrepase los limites de vida
		if(vidaActual <0)
			vidaActual = 0;
		if(vidaActual > vidaMaxima)
			vidaActual = vidaMaxima;
		if(vidaMaxima<1)
			vidaMaxima = 1;

		longitudBarraVida = (Screen.width / 2) * (vidaActual / (float) vidaMaxima);
	}

}
