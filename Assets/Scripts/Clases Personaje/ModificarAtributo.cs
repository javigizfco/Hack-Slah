using System.Collections.Generic;

public class ModificarAtributo: AtributoaBase {
	private List<AtributoModificado> _mods;     //Lista de mods que modifican este atributo
	private int _modValor;						//Cantidad añadida al valorBase por parte de los mods

	public ModificarAtributo(){
		_mods = new List<AtributoModificado>();
		_modValor = 0;
	}

	public void añadirModificador(AtributoModificado mod){
		_mods.Add(mod);
	}

	public void calcularModValor(){
		_modValor = 0;

		if(_mods.Count>0)
			foreach (AtributoModificado atri in _mods)
				_modValor += (int)(atri.atributo.ajustarNivel * atri.ratio);
	}

	public new int ajustarNivel{
		get{ return valorBase + valorBuff + _modValor;}
	}

	public void Update(){
		calcularModValor();
	}
}

public struct AtributoModificado{
	public Atributo atributo;
	public float ratio;
}