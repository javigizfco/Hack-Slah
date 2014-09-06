using UnityEngine;
using System.Collections;

public class VidaEnemigo : MonoBehaviour {
//Clase que implementa la barra de vida del Enemigo. Dibujara una barra de vida en la pantalla y la ira disminuyendo en funcion
//de los ataques ejecutados por el Personaje;
	public int vidaMaxima = 100;	//La vida maxima se establece como 100
	public int vidaActual;
	public float longitudBarraVida;
	
	//La funcion 'Start' se ejecuta cuando se carga la escena
	void Start () {
		//Se calcula el ancho de la barra de vida del enemigo que se dibujara en la pantalla. Screen.width nos da el ancho de la pntalla
		//independiente del dispositivo.
		longitudBarraVida = Screen.width / 2;
		vidaActual = vidaMaxima;	//Fijamos la vidaActual inicial al valor de la vidaMaxima.
	}
	
	//La funcion 'Update' se ejecuta una vez por cada frame
	void Update () {
		//Llama a la funcion ajustaVida con el ajuste 0, para recalcular el tamño de la barra
		ajustarVida (0);
	}
	
	// Funcion que se ejecuta para dibujar sobre la GUI
	void OnGUI(){
		//Screen.width nos devuelve el ancho de la pantalla
		GUI.Box (new Rect(10,40,longitudBarraVida,20),vidaActual + "/" + vidaMaxima);
	}

	//ajusta la vida recalculando el valor añadiendo el ajuste de vida.
	public void ajustarVida(int ajuste){
		//Añade el valor de ajuste a la vidaActual
		vidaActual += ajuste;
		
		//se comprueba que el ajusto no sobrepase los limites de vida
		if(vidaActual <0)
			//Si es menor de 0, lo ajustamos a 0 para que no sea negativo
			vidaActual = 0;
		if(vidaActual > vidaMaxima)
			//Si es mayor que la vidaMaxima se ajusta a la vidaMaxima para que no la sobrepase
			vidaActual = vidaMaxima;
		if(vidaMaxima<1)
			vidaMaxima = 1;
		//Se ajusta la longitud de la barra de vida
		longitudBarraVida = (Screen.width / 2) * (vidaActual / (float) vidaMaxima);
	}
	
}
