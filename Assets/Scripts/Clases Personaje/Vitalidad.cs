public class Vitalidad : ModificacionDeAtributo {
//Clase que implementa los atributos de tipo 'Vitalidad'. Tiene un constructor que establece los valores iniciales 
//para los atributos de tipo 'Vitalidad' y una funcion que nos devuelve el el valor actual del atributo.

	private int _valorActual;

	//Constructor de la clase. Establece los valores iniciales para los Atributos de Tipo 'Vitalidad'.
	public Vitalidad(){
		_valorActual = 0;
		expSubirNivel = 50;
		modExperiencia = 1.1f;
	}

	//Funcion que devuelve el valor actual del atributo
	public int valorActual{
		get{
			if (_valorActual > ajustarNivel)
				_valorActual = ajustarNivel;
			return _valorActual;
		}

		set{ _valorActual = value;}
	}

}

//Enum que define los atribtutos de tipo 'Vitalidad'.
public enum VitalidadNombres{
	Vida,
	Energia,
	Mana
}
