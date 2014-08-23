public class Vitalidad : ModificarAtributo {
	private int _valorActual;

	public Vitalidad(){
		_valorActual = 0;
		expSubirNivel = 50;
		modExperiencia = 1.1f;
	}

	public int valorActual{
		get{
			if (_valorActual > ajustarNivel)
				_valorActual = ajustarNivel;
			return _valorActual;
		}

		set{ _valorActual = value;}
	}

}

public enum VitalidadNombres{
	Vida,
	Energia,
	Mana
}
