using UnityEngine;
using System.Collections;

public class IAEnemigo : MonoBehaviour {
//Clase que implementa la IA del enemigo. Basicamente el enemigo rotara hasta mirar de frente al jugador a la vez que se va desplazando hacia delante,
//como acaba mirando de frente al Personaje, al moverse hacia adelante, se estara acercando a el.

	public Transform objetivo;		//Propiedad Transform del objetivo
	public int velocidadMovimiento;	//Velocidad de movimiento
	public int velocidadRotacion;	//Velocidad de rotacion
	public int distanciaMax;		//Distancia max hasta la que se aproxima el enemigo, no se acerca mas de esta distancia

	private Transform miTransform;

	//se ejecuta antes de cargar la escena, antes de Start. Es util para inicializar variables o estados del juego antes de que empieze.
	void Awake(){
		//Guardamos la componente Transorm del enemigo en una variable
		miTransform = transform;
	}

	//La funcion 'Start' se ejecuta cuando se carga la escena
	void Start () {
		//Localizamos el gameObject Personaje
		GameObject go = GameObject.FindGameObjectWithTag("Personaje");
		//Se guarda en una variable el componente transforme del Personaje
		objetivo = go.transform;
		//Establecemos el valor de la distancia maxima, a partir de la cual el enemigo dejara de acercarse
		distanciaMax = 2;
	}
	
	//La funcion 'Update' se ejecuta una vez por cada frame
	void Update () {
		//Dibuja una linea entre el personaje y el objetivo. SE DEBE BORRAR
		Debug.DrawLine(objetivo.position,miTransform.position,Color.blue);
		//Mirar al objetivo. Rota hasta mirar al jugador
		miTransform.rotation = Quaternion.Slerp(miTransform.rotation, Quaternion.LookRotation(objetivo.position - miTransform.position),
		                                        velocidadRotacion*Time.deltaTime);
		//Solo se movera hasta la distanciaMax
		if (Vector3.Distance(objetivo.position,miTransform.position)>distanciaMax){
			//Moverse hacia el objetivo. Una vez que mira al objetivo, lo que hacemos es moverlo hacia delante
			miTransform.position += miTransform.forward * velocidadMovimiento * Time.deltaTime;
		}
	}
}
