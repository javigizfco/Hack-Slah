public class AtributoaBase {
	private int _nivelAtributo;	//nivel del atributo
	private int _valorBuff;			//Cantidad de buff de este atributo
	private int _expSubirNivel; 	//Catidad de experirencia necesaria para subir el nivel del atributo
	private float _modExperiencia;	//Modificar de la cantidad de experiencia necesaria por nivel
	
#region gets y sets basicos
	public int valorBase{
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

	private int calcularExpSubirNivel(){
		return (int)(_expSubirNivel * _modExperiencia);
	}

	public void subirNivelAtributo(){
		_expSubirNivel = calcularExpSubirNivel();
		_nivelAtributo++;
	}

	public int ajustarNivel{
		get{ return _nivelAtributo + _valorBuff;}
	}

}
