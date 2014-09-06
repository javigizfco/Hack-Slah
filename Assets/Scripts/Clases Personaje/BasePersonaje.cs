using UnityEngine;
using System.Collections;
using System;				//Para acceder a la clase enum

public class BasePersonaje : MonoBehaviour {
//Clase que implementa la base del Personaje.

	private string _nombre; 	//Nombre del Personaje
	private int _nivel;			//Nivel del Personaje
	private uint _expAcumulada;	//Experiencia acumulada por el Personaje.

	private Atributo[] _atributoPrimario;	//Lista de atributos de tipo 'Atributo'
	private Vitalidad[] _vitalidad;			//Lista de atributos de tipo 'Vitalidad'
	private Habilidad[] _habilidad;			//Lista de atributos de tipo 'Habilidad'
	
#region sets y gets basico

	public string nombre{
		get { return _nombre;}
		set { _nombre = value;}
	}

	public int nivel{
		get { return _nivel;}
		set {_nivel = value;}
	}

	public uint expAcumulada{
		get { return _expAcumulada;}
		set { _expAcumulada = value;}
	}
#endregion

	//La funcion Awake se ejecuta antes de la funcion Start
	public void Awake(){
		//Se establecen los valores iniciales del personaje
		_nombre = string.Empty;
		_nivel = 0;
		_expAcumulada = 0;

		//Cada personaje va tener 3 listas, con cada uno de los tipos de Atributos.
		//Asi tiene una lista con todos los Atributos primarios. Otra con todos los atributos de vitalidad
		//y una ultima con todos los atributos de habilidad.
		_atributoPrimario = new Atributo[Enum.GetValues(typeof(nombreAtributo)).Length];
		_vitalidad = new Vitalidad[Enum.GetValues(typeof(VitalidadNombres)).Length];
		_habilidad = new Habilidad[Enum.GetValues(typeof(HabilidadNombres)).Length];

		//Estas funciones inician todos los objetos de la lista
		configurarAtributosPrimarios();
		configurarVitalidad();
		configurarHabilidad();
	}

	//Añade la experiencia pasada como parametro y calcula el nivel.
	public void añadirExperiencia (uint exp){
		_expAcumulada += exp;
		calcularNivel();
	}

	//Toma la media de todas las habilidades del jugador y la asigna como el nivel del jugador
	public void calcularNivel (){

	}

	//En cada posicion de la lista de atributos primarios, crea un nuevo objeto 'Atributo'
	public void configurarAtributosPrimarios(){
		for (int index=0; index<_atributoPrimario.Length; index ++)
			_atributoPrimario[index] = new Atributo();
	}

	//En cada posicion de la lista de atributos de 'Vitalidad', crea un nuevo objeto 'Vitalidad'
	public void configurarVitalidad(){
		for (int index=0; index<_vitalidad.Length; index ++)
			_vitalidad[index] = new Vitalidad();
		configurarVitalidadModificadores();
	}

	//En cada posicion de la lista de atributos de 'Habilidad', crea un nuevo objeto 'Habilidad'
	public void configurarHabilidad(){
		for (int index=0; index<_habilidad.Length; index ++)
			_habilidad[index] = new Habilidad();
		configurarHabilidadesModificadores();
	}

	//Devuelve el objeto 'atributoPrimario' de la lista de Atributos Primarios de la posicion que se pasa como parametro
	public Atributo obtenerAtributoPrimario(int index){
		return _atributoPrimario[index];
	}

	//Devuelve el objeto 'vitalidad' de la lista de Atributos Vitalidad de la posicion que se pasa como parametro
	public Vitalidad obtenerVitalidad(int index){
		return _vitalidad[index];
	}

	//Devuelve el objeto 'habilidad' de la lista de Atributos Habilidad de la posicion que se pasa como parametro
	public Habilidad obtenerHabilidad(int index){
		return _habilidad[index];
	}

	//Configura los atributos de tipo 'Vitalidad'. Para ello crea un objeto 'vidaModificador' para cada uno de los atributos
	//de tipo 'Vitalidad'.
	private void configurarVitalidadModificadores(){
		//El atributo 'Constitucion' se ve modificado por los atributos de 'Vitalidad' vida y energia.
		//vida
		modificadorAtributo vidaModificador = new modificadorAtributo();
		vidaModificador.atributo = obtenerAtributoPrimario((int) nombreAtributo.Constitucion);
		vidaModificador.ratio = 0.5f;
		obtenerVitalidad ((int)VitalidadNombres.Vida).añadirModificador(vidaModificador);
		//energia
		modificadorAtributo energiaModificador = new modificadorAtributo();
		energiaModificador.atributo = obtenerAtributoPrimario((int) nombreAtributo.Constitucion);
		energiaModificador.ratio = 1f;
		obtenerVitalidad ((int)VitalidadNombres.Energia).añadirModificador(energiaModificador);

		//El atributo 'Voluntad' se ve modificado por el atributo de 'Vitalidad' mana.
		//mana
		modificadorAtributo manaModificador = new modificadorAtributo();
		manaModificador.atributo = obtenerAtributoPrimario((int) nombreAtributo.Voluntad);
		manaModificador.ratio = 1f;
		obtenerVitalidad ((int)VitalidadNombres.Mana).añadirModificador(manaModificador);
	}

	//Configura los atributos de tipo 'Habilidad'. Para ello crea un objeto 'habilidadModificador' para cada uno de los atributos
	//de tipo 'Habilidad'.
	private void configurarHabilidadesModificadores(){
		//El ataque cuerpo a cuerpo modifica los atirbutos 'Poder' y 'Destreza'
		modificadorAtributo cuerpoAtaqueModificador1 = new modificadorAtributo();
		modificadorAtributo cuerpoAtaqueModificador2 = new modificadorAtributo();
		cuerpoAtaqueModificador1.atributo = obtenerAtributoPrimario((int) nombreAtributo.Poder);
		cuerpoAtaqueModificador1.ratio = 0.33f;
		obtenerHabilidad ((int)HabilidadNombres.CuerpoAtaque).añadirModificador(cuerpoAtaqueModificador1);
		cuerpoAtaqueModificador2.atributo = obtenerAtributoPrimario((int) nombreAtributo.Destreza);
		cuerpoAtaqueModificador2.ratio = 0.33f;
		obtenerHabilidad ((int)HabilidadNombres.CuerpoAtaque).añadirModificador(cuerpoAtaqueModificador2);
		
		//La defensa cuerpo a cuerpo modfica los atributos 'Velocidad' y 'Constitucion'
		modificadorAtributo cuerpoDefensaModificador1 = new modificadorAtributo();
		modificadorAtributo cuerpoDefensaModificador2 = new modificadorAtributo();
		cuerpoDefensaModificador1.atributo = obtenerAtributoPrimario((int) nombreAtributo.Velocidad);
		cuerpoDefensaModificador1.ratio = 0.33f;
		obtenerHabilidad ((int)HabilidadNombres.CuerpoDefensa).añadirModificador(cuerpoDefensaModificador1);
		cuerpoDefensaModificador2.atributo = obtenerAtributoPrimario((int) nombreAtributo.Constitucion);
		cuerpoDefensaModificador2.ratio = 0.33f;
		obtenerHabilidad ((int)HabilidadNombres.CuerpoDefensa).añadirModificador(cuerpoDefensaModificador2);
		
		//Los ataques de magia modifican los atributos 'Concentracion' y 'Voluntad'
		modificadorAtributo magiaAtaqueModificador1 = new modificadorAtributo();
		modificadorAtributo magiaAtaqueModificador2 = new modificadorAtributo();
		magiaAtaqueModificador1.atributo = obtenerAtributoPrimario((int) nombreAtributo.Concentracion);
		magiaAtaqueModificador1.ratio = 0.33f;
		obtenerHabilidad ((int)HabilidadNombres.MagiaAtaque).añadirModificador(magiaAtaqueModificador1);
		magiaAtaqueModificador2.atributo = obtenerAtributoPrimario((int) nombreAtributo.Voluntad);
		magiaAtaqueModificador2.ratio = 0.33f;
		obtenerHabilidad ((int)HabilidadNombres.MagiaAtaque).añadirModificador(cuerpoAtaqueModificador2);
		
		//La defensa de magia modifica los atributos 'Concentracion' y 'Voluntad'
		modificadorAtributo magiaDefensaModificador1 = new modificadorAtributo();
		modificadorAtributo magiaDefensaModificador2 = new modificadorAtributo();
		magiaDefensaModificador1.atributo = obtenerAtributoPrimario((int) nombreAtributo.Concentracion);
		magiaDefensaModificador1.ratio = 0.33f;
		obtenerHabilidad ((int)HabilidadNombres.MagiaDefensa).añadirModificador(magiaDefensaModificador1);
		magiaDefensaModificador2.atributo = obtenerAtributoPrimario((int) nombreAtributo.Voluntad);
		magiaDefensaModificador2.ratio = 0.33f;
		obtenerHabilidad ((int)HabilidadNombres.MagiaDefensa).añadirModificador(magiaDefensaModificador2);

		//El ataque a distancia modifica los atributos 'Concentracion' y 'Velocidad'
		modificadorAtributo distanciaAtaqueModificador1 = new modificadorAtributo();
		modificadorAtributo distanciaAtaqueModificador2 = new modificadorAtributo();
		distanciaAtaqueModificador1.atributo = obtenerAtributoPrimario((int) nombreAtributo.Concentracion);
		distanciaAtaqueModificador1.ratio = 0.33f;
		obtenerHabilidad ((int)HabilidadNombres.DistanciaAtaque).añadirModificador(distanciaAtaqueModificador1);
		distanciaAtaqueModificador2.atributo = obtenerAtributoPrimario((int) nombreAtributo.Velocidad);
		distanciaAtaqueModificador2.ratio = 0.33f;
		obtenerHabilidad ((int)HabilidadNombres.DistanciaAtaque).añadirModificador(distanciaAtaqueModificador2);
		
		//La defensa distancia modifica los atributos 'Velocidad' y 'Destreza'
		modificadorAtributo distanciaDefensaModificador1 = new modificadorAtributo();
		modificadorAtributo distanciaDefensaModificador2 = new modificadorAtributo();
		distanciaDefensaModificador1.atributo = obtenerAtributoPrimario((int) nombreAtributo.Velocidad);
		distanciaDefensaModificador1.ratio = 0.33f;
		obtenerHabilidad ((int)HabilidadNombres.DistanciaDefensa).añadirModificador(distanciaDefensaModificador1);
		distanciaDefensaModificador2.atributo = obtenerAtributoPrimario((int) nombreAtributo.Destreza);
		distanciaDefensaModificador2.ratio = 0.33f;
		obtenerHabilidad ((int)HabilidadNombres.DistanciaDefensa).añadirModificador(distanciaDefensaModificador2);
	}

	//Actualiza los atributos de las listas de habilidad como vitalidad
	public void actualizarAtributos(){
		for (int index = 0;index <_vitalidad.Length;index++)
			_vitalidad[index].Update();
		for (int index = 0;index <_habilidad.Length;index++)
			_habilidad[index].Update();

	}
}
