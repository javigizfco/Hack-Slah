public class BaseAtributo {
//Clase base para los atributos que implementa los atributos basicos (nivel, experiencia para subir nivel, ...) y las aplicaciones bascias

	private int _nivelAtributo;		//nivel del atributo
	private int _valorBuff;			//Cantidad de buff de este atributo
	private int _expSubirNivel; 	//Catidad de experirencia necesaria para subir el nivel del atributo
	private float _modExperiencia;	//Modificar de la cantidad de experiencia necesaria para subir de nivel
	
#region gets y sets basicos
	public int nivelAtributo{
		get {return _nivelAtributo;}
		set {_nivelAtributo = value;}
	}

	public int valorBuff{
		get {return _valorBuff;}
		set {_valorBuff = value;}
	}

	public int expSubirNivel{
		get {return _expSubirNivel;}
		set {_expSubirNivel = value;}
	}

	public float modExperiencia{
		get {return _modExperiencia;}
		set {_modExperiencia = value;}
	}

#endregion
	
	//Calcula la experiencia necesaria para subir al siguiente nivel del atributo
	private int calcularExpSubirNivel(){
		//Se calcula la experiencia necesaria para subir al siguiente nivel. Sera el valor de la experiencia
		//para subir de nivel * el modExperiencia por nivel.
		return (int)(_expSubirNivel * _modExperiencia);
	}

	//Sube el nivel del atributo. Para ello suma 1 al nivel del atributo y calcula la experiencia necearia para subir al siguiente nivel.
	public void subirNivelAtributo(){
		//Subimos un nivel en el atributo
		_nivelAtributo++;
		//Calculamos la experiencia necesaria para subir al nivel siguiente
		_expSubirNivel = calcularExpSubirNivel();
	}

	//??? no se exactamente para que se usa
	public int ajustarNivel{
		get{ return _nivelAtributo + _valorBuff;}
	}

}
