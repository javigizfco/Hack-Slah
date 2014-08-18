using UnityEngine;
using System.Collections;

public class AtaquePersonaje : MonoBehaviour {
	public GameObject objetivo;
	//El tiempoEntreAtaques es el tiempo que ha de transcurrir entre dos ataques
	public float tiempoEntreAtaques;
	//El temporizadorAtaque es el tiempo que falta hasta poder realizar el siguiente ataque
	public float temporizadorAtaque;

	// Use this for initialization
	void Start () {
		tiempoEntreAtaques = 2.0f;
		temporizadorAtaque = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//Si el tiempo hasta el siguiente Ataque es >0, entonces se resta tiempo
		if (temporizadorAtaque>0){
			temporizadorAtaque -= Time.deltaTime;
		}
		else{
			temporizadorAtaque = 0;
		}

		//Si se pulsa el boton 0, se llama a la funcion atacar. Cuando el tiempo hasta el siguiente ataque sea 0, se puede atacar
		if(Input.GetMouseButtonDown(0)&& temporizadorAtaque==0){
			Atacar();
			//despues de realizar el ataque, pones el tiempo hasta el siguiente ataque al valor del tiempo entre ataques
			temporizadorAtaque = tiempoEntreAtaques;
		}
	}


	private void Atacar(){
		//Calculamos la distancia entre la posicion del Enemigo y la del jugador
		float distanciaObjetivo = Vector3.Distance (objetivo.transform.position,transform.position);

		//Calculamos el vector normal entre el Objetivo y el Jugador
		Vector3 dir = (objetivo.transform.position - transform.position).normalized;
		//Calculamos la direccion, si el jugador no esta en la direccion del Enemigo, no se le restara vida
		//Dot->Para vectores normalizados, devuelve:
		//		 1: si estan en la misma direccion
		//		-1: si estan en posicion contraria
		//		 0: si son perpendiculares
		float direccion = Vector3.Dot(dir,transform.forward);
		//Solo le restamos vida si esta a una distancia menor a 2.5
		if (distanciaObjetivo<2.5f && direccion>0){
			VidaEnemigo vidaEnem = (VidaEnemigo)objetivo.GetComponent("VidaEnemigo");
			vidaEnem.ajustarVida(-10);
		}

	}
}
