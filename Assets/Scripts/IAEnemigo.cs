using UnityEngine;
using System.Collections;

public class IAEnemigo : MonoBehaviour {
	public Transform objetivo;
	public int velocidadMovimiento;
	public int velocidadRotacion;
	public int distanciaMax;

	private Transform miTransform;

	//se ejecuta antes de cargar la escena, antes de Start. Es util para inicializar variables o estados del juego antes de que empieze.
	void Awake(){
		miTransform = transform;
	}

	// Se ejecuta cuando se iniciliaza
	void Start () {
		//Localizamos el gameObject Personaje
		GameObject go = GameObject.FindGameObjectWithTag("Personaje");
		objetivo = go.transform;
		//Establecemos el valor de la distancia maxima, a partir de la cual el enemigo dejara de atacarse
		distanciaMax = 2;
	}
	
	// Se ejecuta una ver por frame
	void Update () {
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
