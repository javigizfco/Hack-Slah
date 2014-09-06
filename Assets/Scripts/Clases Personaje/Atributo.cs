public class Atributo : BaseAtributo {
//Clase hija de la clase baseAtributo. Define el constructor con el que establecemos los valores iniciales de la experiencia para subir nivel y el
//modificador de experiencia por nivel. Tambien define los atributos disponibles del personaje.

	//Constructor de la clase. Establece los valores iniciales para la exp para subir de nivel y el modificador de experiencia por nivel
	public Atributo(){
		expSubirNivel = 50;
		modExperiencia = 1.05f;
	}
}

//Tipo enumerado que define los atributos del personaje
public enum nombreAtributo{
	Fuerza,
	Poder,
	Destreza,
	Velocidad,
	Constitucion,
	Concentracion,
	Voluntad
}
