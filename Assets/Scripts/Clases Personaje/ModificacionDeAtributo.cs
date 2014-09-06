using System.Collections.Generic;
//Clase que implementa las funciones para modificar los valores de los atribuos.

public class ModificacionDeAtributo: BaseAtributo {
	private List<modificadorAtributo> _mods;     //Lista de mods que modifican este atributo
	private int _modValor;						 //Cantidad añadida al valorBase por parte de los mods

	//Constructor de la Clase
	public ModificacionDeAtributo(){
		//Se inicializa la lista de modificadores de atributos
		_mods = new List<modificadorAtributo>();
		//El valor modficado inicial es 0
		_modValor = 0;
	}

	//Añade un modificador a la lista.
	public void añadirModificador(modificadorAtributo mod){
		_mods.Add(mod);
	}

	//Calcula el valor de _modValor como el el sumatorio de todos los productors entre el valor 'ajustarNivel' de un atributo por el
	//valor ratio, de todos los elementos de la lista de modificadores
	public void calcularModValor(){
		_modValor = 0;

		if(_mods.Count>0)
			foreach (modificadorAtributo atrib in _mods)
				//_modValor sera el sumatorio del producto entre el valor que devuelve ajustarNivel y el valor ratio
				//de todos los modificadores de la lista
				_modValor += (int)(atrib.atributo.ajustarNivel * atrib.ratio);
	}

	//Reescribe la funcion ajustarNivel de la Clase Padre 'Base Atributo'. El valor que usa para ajustar el nivel es valor del 
	//nivel del atributo, el valro de _buff y el valor de _modValor calculado en esta clase.
	public new int ajustarNivel{
		get{ return nivelAtributo + valorBuff + _modValor;}
	}

	//Se ejecta una vez por frame. Cada frame ejecuta la funcion 'calcularModValor', que se encarga de calcular el valor de _modValor.
	public void Update(){
		calcularModValor();
	}
}

//Estructura que define el modificador de un atributo. Esta formado por un objeto atributo, que es el que se va modificar de nivle
//de experiencia, y un valor ratio, que es el valor que vamos añadir a la experiencia de ese atributo.
public struct modificadorAtributo{
	public Atributo atributo;
	public float ratio;
}