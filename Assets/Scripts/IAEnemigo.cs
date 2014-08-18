using UnityEngine;
using System.Collections;

public class IAEnemigo : MonoBehaviour {
	public Transform objetivo;
	public int velocidadMovimiento;
	public int velocidadRotacion;

	private Transform miTransform;

	//se ejecuta antes de cargar la escena, antes de Start. Es util para inicializar variables o estados del juego antes de que empieze.
	void Awake(){
		miTransform = transform;
	}

	// Se ejecuta cuando se iniciliaza
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag("Personaje");
		objetivo = go.transform;
	}
	
	// Se ejecuta una ver por frame
	void Update () {
		Debug.DrawLine(objetivo.position,miTransform.position,Color.blue);
		//Mirar al objetivo. Rota hasta mirar al jugador
		miTransform.rotation = Quaternion.Slerp(miTransform.rotation, Quaternion.LookRotation(objetivo.position - miTransform.position),
		                                        velocidadRotacion*Time.deltaTime);
		//Moverse hacia el objetivo. Una vez que mira al objetivo, lo que hacemos es moverlo hacia delante
		miTransform.position += miTransform.forward * velocidadMovimiento * Time.deltaTime;
	}
}
