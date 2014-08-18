using UnityEngine;
using System.Collections;

public class AtaqueEnemigo : MonoBehaviour {

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
		
		//Cuando el tiempo hasta el siguiente ataque sea 0, se puede atacar. El enemigo no tiene que pulsar una tecla para atacar
		if(temporizadorAtaque==0){
			Atacar();
			//despues de realizar el ataque, pones el tiempo hasta el siguiente ataque al valor del tiempo entre ataques
			temporizadorAtaque = tiempoEntreAtaques;
		}
	}
	
	
	private void Atacar(){
		//Calculamos la distancia eentre el el Objetivo del Enemigo y el Enemigo
		float distanciaObjetivo = Vector3.Distance (objetivo.transform.position,transform.position);
		
		//Calculamos el vector normal entre el el Objetivo del Enemigo y el Enemigo
		Vector3 dir = (objetivo.transform.position - transform.position).normalized;
		//Calculamos la direccion, si el jugador no esta en la direccion del Enemigo, no se le restara vida
		//Dot->Para vectores normalizados, devuelve:
		//		 1: si estan en la misma direccion
		//		-1: si estan en posicion contraria
		//		 0: si son perpendiculares
		float direccion = Vector3.Dot(dir,transform.forward);
		//Solo le restamos vida si esta a una distancia menor a 2.5
		if (distanciaObjetivo<2.5f && direccion>0){
			VidaPersonaje vidaPers = (VidaPersonaje)objetivo.GetComponent("VidaPersonaje");
			vidaPers.ajustarVida(-10);
		}
		
	}
}
