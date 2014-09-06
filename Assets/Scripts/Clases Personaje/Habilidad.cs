public class Habilidad : ModificacionDeAtributo {
//Clase que implementa los atributos de tipo 'Habilidad'. Tiene un constructor que establece los valores iniciales 
//para los atributos de tipo 'Habilidad' y una funcion que nos devuelve el el valor actual del atributo.

	private bool _conocido;

	//Constructor de la clase. Establece los valores iniciales para los Atributos de Tipo 'Habilidad'.
	public Habilidad(){
		_conocido = false;
		expSubirNivel = 25;
		modExperiencia = 1.1f;
	}

	public bool conocido{
		get {return _conocido;}
		set {_conocido = value;}
	}
}

//Enum que define los atribtutos de tipo 'Habilidad'.
public enum HabilidadNombres{
	CuerpoAtaque,
	CuerpoDefensa,
	DistanciaAtaque,
	DistanciaDefensa,
	MagiaAtaque,
	MagiaDefensa
}
