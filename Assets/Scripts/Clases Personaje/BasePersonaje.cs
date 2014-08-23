using UnityEngine;
using System.Collections;
using System;				//Para acceder a la clase enum

public class BasePersonaje : MonoBehaviour {
	private string _nombre;
	private int _nivel;
	private uint _expAcumulada;

	private Atributo[] _atributoPrimario;
	private Vitalidad[] _vitalidad;
	private Habilidad[] _habilidad;
	
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

	public void Awake(){
		_nombre = string.Empty;
		_nivel = 0;
		_expAcumulada = 0;

		_atributoPrimario = new Atributo[Enum.GetValues(typeof(nombreAtributo)).Length];
		_vitalidad = new Vitalidad[Enum.GetValues(typeof(Vitalidad)).Length];
		_habilidad = new Habilidad[Enum.GetValues(typeof(Habilidad)).Length];

		configurarAtributosPrimarios();
		configurarVitalidad();
		configurarHabilidad();
	}

	public void añadirExperiencia (uint exp){
		_expAcumulada += exp;
		calcularNivel();
	}

	//toma la media de todas las habilidades del jugador y la asigna como el nivel del jugador
	public void calcularNivel (){

	}

	public void configurarAtributosPrimarios(){
		for (int index=0; index<_atributoPrimario.Length; index ++)
			_atributoPrimario[index] = new Atributo();
	}

	public void configurarVitalidad(){
		for (int index=0; index<_vitalidad.Length; index ++)
			_vitalidad[index] = new Vitalidad();
	}

	public void configurarHabilidad(){
		for (int index=0; index<_habilidad.Length; index ++)
			_habilidad[index] = new Habilidad();
	}

	public Atributo obtenerAtributoPrimario(int index){
		return _atributoPrimario[index];
	}

	public Vitalidad obtenerVitalidad(int index){
		return _vitalidad[index];
	}

	public Habilidad obtenerHabilidad(int index){
		return _habilidad[index];
	}

	private void configurarVitalidadModificadores(){
		//vida
		AtributoModificado vidaModificador = new AtributoModificado();
		vidaModificador.atributo = obtenerAtributoPrimario((int) nombreAtributo.Constitucion);
		vidaModificador.ratio = 0.5f;
		obtenerVitalidad ((int)VitalidadNombres.Vida).añadirModificador(vidaModificador);

		//energia
		AtributoModificado energiaModificador = new AtributoModificado();
		energiaModificador.atributo = obtenerAtributoPrimario((int) nombreAtributo.Constitucion);
		energiaModificador.ratio = 1f;
		obtenerVitalidad ((int)VitalidadNombres.Energia).añadirModificador(energiaModificador);

		//mana
		AtributoModificado manaModificador = new AtributoModificado();
		manaModificador.atributo = obtenerAtributoPrimario((int) nombreAtributo.Voluntad);
		manaModificador.ratio = 1f;
		obtenerVitalidad ((int)VitalidadNombres.Mana).añadirModificador(manaModificador);
	}

	private void configurarHabilidadesModificadores(){
		//ataque cuerpo a cuerpo
		AtributoModificado cuerpoAtaqueModificador1 = new AtributoModificado();
		AtributoModificado cuerpoAtaqueModificador2 = new AtributoModificado();
		cuerpoAtaqueModificador1.atributo = obtenerAtributoPrimario((int) nombreAtributo.Poder);
		cuerpoAtaqueModificador1.ratio = 0.33f;
		obtenerHabilidad ((int)HabilidadNombre.CuerpoAtaque).añadirModificador(cuerpoAtaqueModificador1);
		cuerpoAtaqueModificador2.atributo = obtenerAtributoPrimario((int) nombreAtributo.Destreza);
		cuerpoAtaqueModificador2.ratio = 0.33f;
		obtenerHabilidad ((int)HabilidadNombre.CuerpoAtaque).añadirModificador(cuerpoAtaqueModificador2);
		
		//defensa cuerpo a cuerpo
		AtributoModificado cuerpoDefensaModificador1 = new AtributoModificado();
		AtributoModificado cuerpoDefensaModificador2 = new AtributoModificado();
		cuerpoDefensaModificador1.atributo = obtenerAtributoPrimario((int) nombreAtributo.Velocidad);
		cuerpoDefensaModificador1.ratio = 0.33f;
		obtenerHabilidad ((int)HabilidadNombre.CuerpoDefensa).añadirModificador(cuerpoDefensaModificador1);
		cuerpoDefensaModificador2.atributo = obtenerAtributoPrimario((int) nombreAtributo.Constitucion);
		cuerpoDefensaModificador2.ratio = 0.33f;
		obtenerHabilidad ((int)HabilidadNombre.CuerpoDefensa).añadirModificador(cuerpoDefensaModificador2);
		
		//ataque magia
		AtributoModificado magiaAtaqueModificador1 = new AtributoModificado();
		AtributoModificado magiaAtaqueModificador2 = new AtributoModificado();
		magiaAtaqueModificador1.atributo = obtenerAtributoPrimario((int) nombreAtributo.Concentracion);
		magiaAtaqueModificador1.ratio = 0.33f;
		obtenerHabilidad ((int)HabilidadNombre.MagiaAtaque).añadirModificador(magiaAtaqueModificador1);
		magiaAtaqueModificador2.atributo = obtenerAtributoPrimario((int) nombreAtributo.Voluntad);
		magiaAtaqueModificador2.ratio = 0.33f;
		obtenerHabilidad ((int)HabilidadNombre.MagiaAtaque).añadirModificador(cuerpoAtaqueModificador2);
		
		//defensa magia
		AtributoModificado magiaDefensaModificador1 = new AtributoModificado();
		AtributoModificado magiaDefensaModificador2 = new AtributoModificado();
		magiaDefensaModificador1.atributo = obtenerAtributoPrimario((int) nombreAtributo.Concentracion);
		magiaDefensaModificador1.ratio = 0.33f;
		obtenerHabilidad ((int)HabilidadNombre.MagiaDefensa).añadirModificador(magiaDefensaModificador1);
		magiaDefensaModificador2.atributo = obtenerAtributoPrimario((int) nombreAtributo.Voluntad);
		magiaDefensaModificador2.ratio = 0.33f;
		obtenerHabilidad ((int)HabilidadNombre.MagiaDefensa).añadirModificador(magiaDefensaModificador2);

		//ataque distancia
		AtributoModificado distanciaAtaqueModificador1 = new AtributoModificado();
		AtributoModificado distanciaAtaqueModificador2 = new AtributoModificado();
		distanciaAtaqueModificador1.atributo = obtenerAtributoPrimario((int) nombreAtributo.Concentracion);
		distanciaAtaqueModificador1.ratio = 0.33f;
		obtenerHabilidad ((int)HabilidadNombre.DistanciaAtaque).añadirModificador(distanciaAtaqueModificador1);
		distanciaAtaqueModificador2.atributo = obtenerAtributoPrimario((int) nombreAtributo.Velocidad);
		distanciaAtaqueModificador2.ratio = 0.33f;
		obtenerHabilidad ((int)HabilidadNombre.DistanciaAtaque).añadirModificador(distanciaAtaqueModificador2);
		
		//defensa distancia
		AtributoModificado distanciaDefensaModificador1 = new AtributoModificado();
		AtributoModificado distanciaDefensaModificador2 = new AtributoModificado();
		distanciaDefensaModificador1.atributo = obtenerAtributoPrimario((int) nombreAtributo.Velocidad);
		distanciaDefensaModificador1.ratio = 0.33f;
		obtenerHabilidad ((int)HabilidadNombre.DistanciaDefensa).añadirModificador(distanciaDefensaModificador1);
		distanciaDefensaModificador2.atributo = obtenerAtributoPrimario((int) nombreAtributo.Destreza);
		distanciaDefensaModificador2.ratio = 0.33f;
		obtenerHabilidad ((int)HabilidadNombre.DistanciaDefensa).añadirModificador(distanciaDefensaModificador2);
	}

	public void actualizarAtributos(){
		for (int index = 0;index <_vitalidad.Length;index++)
			_vitalidad[index].Update();
		for (int index = 0;index <_habilidad.Length;index++)
			_habilidad[index].Update();

	}
}
