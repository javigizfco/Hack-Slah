using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SeleccionarObjetivo : MonoBehaviour {
//Clase que implmeneta la seleccion y fijacion de un objetivo. Crea una lista con las componentes transform de todos los elementos
//con Tag 'Enemigo'. Cuando se pulsa la tecla TAB, sino hay enemigo seleccionado, ordena la lista por distancia y selecciona el mas
//cercano. Si hay un enemigo seleccionado, pasa al siguiente en la lista. Ademas, cuando selecciona un objetivo lo marca en rojo
//y al deseleccionarlo lo pone en azul.

	public List<Transform> objetivos;  	//Lista de las componentes Transform de todos los enemigos
	public Transform objetivoFijado;   	//Componente Transform del objetivo fijado actualmente
	private Transform miTransform;		//Componente Transform del Personaje

	//La funcion 'Start' se ejecuta cuando se carga la escena
	void Start () {
		objetivos = new List<Transform>();
		//Se añaden todos los enemigos a la lista de Enemigos
		añadirTodosEnemigos();
		//Inicialmente no hay un objetivo fijado, por lo que lo fijamos como NULL
		objetivoFijado = null;
		//Guardamos la componente transform del Personaje
		miTransform = transform;
	}
	
	//La funcion 'Update' se ejecuta una vez por cada frame
	void Update () {
		// Si se pulsa la tecla TAB se cambia de objetivo llamando a la funcion fijarEnemigo
		if(Input.GetKeyDown(KeyCode.Tab)){
			fijarEnemigo();
		}
	
	}

	//Guarda el componente transforme de los enemigos en la lista de objetivos. Para ello crea una lista con todos los GameObjects
	//con el TAG 'Enemigo' y luego, para cada de uno de ellos guarda la componente transform en la lista de objetivos.
	public void añadirTodosEnemigos(){
		//Guardamos en un array de GameObjects aquellos GameObjects con el Tag "Enemigo"
		GameObject[] listaEnemigos = GameObject.FindGameObjectsWithTag("Enemigo");
		//Para cada enemigo, guardamos el componente transform en la lista de objetivos
		foreach (GameObject enemigo in listaEnemigos )
			añadirEnemigo(enemigo.transform);
	}

	//Añade la componente transform de un enemigo a la lista de objetivos
	public void añadirEnemigo(Transform enemigo){
		objetivos.Add(enemigo);
	}

	//Fija al objetivo que se puede atacar. Sino hay ningun ojetivo gijado, ordena los objetivos por distancia y fija el mas cercano.
	//Si ya hay un objetivo Fijado, pasa al siguiente en la lista. Si es el ultimo objetivo de la lista, tendra que pasar al primero.
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
		//Se marca el objetivo seleccionado
		marcarObjetivo();
	}

	//Marca el objetivo seleccionado. Lo pinta de color rojo y lo pone como objetivo del ataque del personaje
	private void marcarObjetivo(){
		objetivoFijado.renderer.material.color = Color.red;
		//Fijamos como objetivo al que atacar por parte del personaje
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
