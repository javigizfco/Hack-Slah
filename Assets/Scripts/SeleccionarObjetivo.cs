using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SeleccionarObjetivo : MonoBehaviour {
	public List<Transform> objetivos;
	public Transform objetivoFijado;
	private Transform miTransform;

	// Se ejecuta cuando se iniciliaza
	void Start () {
		objetivos = new List<Transform>();
		//Se añaden todos los enemigos a la lista de Enemigos
		añadirTodosEnemigos();
		//EStablecemos el objetivo fijado como NULL
		objetivoFijado = null;
		//Guardamos la componente transform del Personaje
		miTransform = transform;
	}
	
	// Se ejecuta una vez cada frae
	void Update () {
		// Si se pulsa la tecla TAB se cambia de objetivo llamando a la funcion fijarEnemigo
		if(Input.GetKeyDown(KeyCode.Tab)){
			fijarEnemigo();
		}
	
	}

	//Crear un array donde almacen todos los GameObject de los enemigos
	public void añadirTodosEnemigos(){
		//Guardamos en un array de GameObjects aquellos GameObjects con el Tag "Enemigo"
		GameObject[] listaEnemigos = GameObject.FindGameObjectsWithTag("Enemigo");
		//Para cada enemigo, guardamos el componente transform en la lista de objetivos
		foreach (GameObject enemigo in listaEnemigos )
			//Para cada enemigo de la lista, guardamos su componente transform en la lista de objetivos
			añadirEnemigo(enemigo.transform);
	}

	//Añade la componente transform de un enemigo a la lista de objetivo
	public void añadirEnemigo(Transform enemigo){
		//Guardamos el componente transform de cada enemigo en la lista de objetivos
		objetivos.Add(enemigo);
	}

	//Fija al objetivo que se puede atacar
	private void fijarEnemigo(){
		//Si no hay ningun enemigo fijado
		if (objetivoFijado==null){
			//Se ordenan segun la distancia
			ordenarObjetivosPorDistancia();
			//Se fija el objetivo mas cercano
			objetivoFijado = objetivos[0];
		}
		else{
			//si hay un objetivo fijado, pasamos al siguiente
			int index = objetivos.IndexOf (objetivoFijado);
			if (index < objetivos.Count -1){
				//Siguiente objetivo
				index++;
			}
			else{
				//Si es el ultimo objetivo de la lista, pasamos al primero
				index = 0;
			}
			//desmarcamos el objetivo anterior
			desmarcarObjetivo();
			//fiajmos el objetivo adecuado
			objetivoFijado = objetivos[index];
		}
		//SE marca el objetivo seleccionado
		marcarObjetivo();
	}

	//Funcion que pinta de rojo el objetivoActual
	private void marcarObjetivo(){
		objetivoFijado.renderer.material.color = Color.red;
		//Fijamos como objetivo al que atacar al actual objetivo fiajod
		AtaquePersonaje ataque = (AtaquePersonaje)GetComponent("AtaquePersonaje");
		ataque.objetivo = objetivoFijado.gameObject;
	}

	//Se desmarca el objetivo y se pinta de azul.
	private void desmarcarObjetivo(){
		objetivoFijado.renderer.material.color = Color.blue;
		objetivoFijado = null;
	}

	//Se ordenan los objetivos segun la distanca
	private void ordenarObjetivosPorDistancia(){
		//El delegate permite definir una funcion como un parametro
		objetivos.Sort (delegate(Transform t1,Transform t2){
			//Calcula la distancia menor entre el jugador y un enemigo, o el jugador y otro enemigo
			return Vector3.Distance(t1.position,miTransform.position).CompareTo(Vector3.Distance(t2.position,miTransform.position));
			   	   });

	}
}
