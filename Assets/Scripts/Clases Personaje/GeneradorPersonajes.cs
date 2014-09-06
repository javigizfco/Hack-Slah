using UnityEngine;
using System.Collections;
using System;				//Usada para la clase Enum

public class GeneradorPersonajes : MonoBehaviour {
	private PersonajeJugador _miPersonaje;
	//puntos restantes por asignar
	private int puntosRestantes;
	//puntos de atributos iniciales para asignar
	private const int PUNTOS_INICIO = 350;
	//valores minimos de nivel de atributo
	private const int MIN_VALOR_INICIO_ATRIBUTOS = 10;
	//al empezar, ponemos los valores de los atributos a 50, pero se puede restar hasta llegar al minimo que es el valor anteiror (10)
	private int VALOR_INICIAL = 50;

	//constantes usadas para ubicar las etiquetas y botones en la pantalla
	private const int OFFSET = 5;							//valor de offset usado
	private const int ALTO_LINEA = 20;						//numero de pixel de alto de cada linea
	private const int ETIQUETA_ATRIBUTO_ANCHO = 100;		//ancho de la etiqueta para el nombre del atributo
	private const int ETIQUETA_NIVELATRIBUTO_ANCHO = 30;	//ancho de la etiqueta para el nivel del atributo
	private const int BOTON_ALTO = 20;						//alto del boton
	private const int BOTON_ANCHO = 20;						//ancho del boton
	private const int POS_INICIAL_ATRIBUTO = 40;			//posicion del atributo

	//para personalizar elementos del interfaz podemos usar GUIstyle para definirlo para un elemento o definir un skin con GUISKIN
	//para definir todos los elementos de la interfaz
	public GUISkin miSkin;

	//Se ejecuta cuando se inicia la escena
	void Start () {
		//los puntos restantes por asignar se establece al valor de PUNTOS_INICIO
		puntosRestantes = PUNTOS_INICIO;
		_miPersonaje = new PersonajeJugador();
		_miPersonaje.Awake();
		//Ponemos cada atributo con 50 puntos
		for(int count=0; count<Enum.GetValues(typeof(nombreAtributo)).Length; count++){
			_miPersonaje.obtenerAtributoPrimario(count).nivelAtributo = VALOR_INICIAL;
			puntosRestantes-= (VALOR_INICIAL - MIN_VALOR_INICIO_ATRIBUTOS);	//cada 50 puntos que pones a un atributo, tenemos que 
																			//restarlos del total de atributos por asignar
		}
		//actualizamos los atributos del personaje
		_miPersonaje.actualizarAtributos();

	}


	// Se ejecuta una vez por frame
	void Update () {
	}

	//Funcion para dibujar nuestra interfaz en la pantalla
	void OnGUI(){
		GUI.skin = miSkin;		//Aplicamos nuestro skin	
		MostrarNombre();		//Muestra el nombre del personaje
		MostrarPuntosFaltan();	//Muestra los puntos de atributos que faltan por asignar
		MostrarAtributos();		//Muestra los atributos
		GUI.skin = null;		
		MostrarVitalidad();		//Muestra las los atributos de vitalidad
		GUI.skin = miSkin;
		MostrarHabilidades();	//Muestra los atributos de HAbilidades
	}

	//Escribe un cuadro con el nombre y el valor de este
	private void MostrarNombre(){
		GUI.Label(new Rect(10,10,50,25), "Nombre:");
		_miPersonaje.nombre = GUI.TextField(new Rect(65,10,100,25), _miPersonaje.nombre);
	}

	//Escribe los atributos y el valor de cada uno
	private void MostrarAtributos(){
		for (int count=0; count<Enum.GetValues(typeof(nombreAtributo)).Length;count++){
			//nombreAtributo es un valor Enum, haciendo ((nombreAtributo).count), accedemos a cada
			//elemento del valor Enum.
			//Se pone 40 + (count * 25) porque sino no salen todas a la misma altura, asi para cada
			//count vamos dejando un espacio entre cada texto
			//Esta etiqueta se usa para escribir el nombre del atributo
			GUI.Label(new Rect(OFFSET,										//x
			                   POS_INICIAL_ATRIBUTO + (count * ALTO_LINEA), //y
			                   ETIQUETA_ATRIBUTO_ANCHO,						//ancho
			                   ALTO_LINEA),									//alto
			          		   ((nombreAtributo)count).ToString());

			//Se escribe el valor de cada atributo a su lado en una etiqueta
			GUI.Label(new Rect(ETIQUETA_ATRIBUTO_ANCHO + OFFSET,			//x
			                   POS_INICIAL_ATRIBUTO + (count * ALTO_LINEA), //y
			                   ETIQUETA_NIVELATRIBUTO_ANCHO,				//ancho
			                   ALTO_LINEA),									//alto
			          		   _miPersonaje.obtenerAtributoPrimario(count).ajustarNivel.ToString ());

			//Se dibuja un boton para restar puntos a cada atributo. Dentro del if, es TRUE cuando se pulsa ese botoon
			if(GUI.Button (new Rect (OFFSET + ETIQUETA_ATRIBUTO_ANCHO + ETIQUETA_NIVELATRIBUTO_ANCHO, 	//x
			                         POS_INICIAL_ATRIBUTO + (count * BOTON_ALTO),					  	//Y
			                         BOTON_ANCHO,														//ancho
			                         BOTON_ALTO), 														//alto
			               			"-")){
				if(_miPersonaje.obtenerAtributoPrimario(count).nivelAtributo> MIN_VALOR_INICIO_ATRIBUTOS){
					_miPersonaje.obtenerAtributoPrimario(count).nivelAtributo--;//Si restamos valores, se lo restamos al nivle del atributo
					puntosRestantes++;	//Al restar puntos a un atributo, se lo sumamos al total de puntos disponibles
					_miPersonaje.actualizarAtributos();	//Se actualizan los valores de habilidad y vitalidad en funcion de los valores de los atributos generalses
				}
			}

			//SE dibuja un boton para sumar puntos a cada atributo
			if (GUI.Button (new Rect (OFFSET + ETIQUETA_ATRIBUTO_ANCHO + ETIQUETA_NIVELATRIBUTO_ANCHO + BOTON_ANCHO, 	//x
			                          POS_INICIAL_ATRIBUTO + (count * BOTON_ALTO),					  					//Y
			                          BOTON_ANCHO,																		//ancho
			                          BOTON_ALTO),																		//alto
			                		  "+")){
				if(puntosRestantes>0){
					_miPersonaje.obtenerAtributoPrimario(count).nivelAtributo++;
					puntosRestantes--;
					_miPersonaje.actualizarAtributos();
				}
			}
		}
	}

	//Muestran los atributos de tipo vitalidad
	private void MostrarVitalidad(){
		for (int count=0; count<Enum.GetValues(typeof(VitalidadNombres)).Length;count++){
			//Se dibuja una etiqueta para escribir el nombre del atributo
			GUI.Label(new Rect(OFFSET,																							//x
			                   POS_INICIAL_ATRIBUTO + ((count + Enum.GetValues(typeof(nombreAtributo)).Length) * ALTO_LINEA),	//y
			                   ETIQUETA_ATRIBUTO_ANCHO,																			//ancho
			                   ALTO_LINEA),																						//alto
			          		  ((VitalidadNombres)count).ToString());

			//Se escribe el valor de cada atributo a su lado
			GUI.Label(new Rect(OFFSET + ETIQUETA_ATRIBUTO_ANCHO,																//x
			                   POS_INICIAL_ATRIBUTO + ((count + Enum.GetValues(typeof(nombreAtributo)).Length) * ALTO_LINEA),	//y
			                   ETIQUETA_NIVELATRIBUTO_ANCHO,																	//ancho
			                   ALTO_LINEA),																						//alto
			          		   _miPersonaje.obtenerVitalidad(count).ajustarNivel.ToString ());
		}
	}

	//Muestra los atirbutos de tipo habilidad
	private void MostrarHabilidades(){
		for (int count=0; count<Enum.GetValues(typeof(HabilidadNombres)).Length;count++){
			//Etiqueta para escribir el nombre del atributo
			GUI.Label(new Rect(OFFSET + ETIQUETA_ATRIBUTO_ANCHO + ETIQUETA_NIVELATRIBUTO_ANCHO + BOTON_ANCHO * 2 + OFFSET * 2,	//x
			                   POS_INICIAL_ATRIBUTO + (count * ALTO_LINEA),														//y
			                   ETIQUETA_ATRIBUTO_ANCHO,																			//ancho
			                   ALTO_LINEA),																						//alto
			                  ((HabilidadNombres)count).ToString());

			//Se escribe el valor de cada atributo a su lado
			GUI.Label(new Rect(OFFSET + ETIQUETA_ATRIBUTO_ANCHO + ETIQUETA_NIVELATRIBUTO_ANCHO + BOTON_ANCHO * 2
			                   		  + OFFSET * 2 + ETIQUETA_ATRIBUTO_ANCHO,	//x
			                   POS_INICIAL_ATRIBUTO + (count * ALTO_LINEA),		//y
			                   ETIQUETA_ATRIBUTO_ANCHO,							//ancho
			                   ALTO_LINEA),										//alto			
			          		  _miPersonaje.obtenerHabilidad(count).ajustarNivel.ToString ());
		}
	}

	//Muestra los puntos de atributos que faltan por asignar
	private void MostrarPuntosFaltan(){
		GUI.Label(new Rect(250,10,200,25), "Puntos restantes: " + puntosRestantes.ToString());
	}
}
