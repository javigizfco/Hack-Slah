public class Habilidad : ModificarAtributo {
	private bool _conocido;

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

public enum HabilidadNombre{
	CuerpoAtaque,
	CuerpoDefensa,
	DistanciaAtaque,
	DistanciaDefensa,
	MagiaAtaque,
	MagiaDefensa
}
