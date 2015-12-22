using UnityEngine;
using System.Collections;

public class PuntajeVO{
	private int posicion;
	private string nombreJugador; 
	private string tiempo;

	public PuntajeVO(){
	
	}
	public PuntajeVO(int posicion,string nombreJugador){
		this.posicion = posicion;
		this.nombreJugador = nombreJugador;
	}

	public void setNombreJugador(string nombreJugador){
		this.nombreJugador = nombreJugador;
	}
	public void setTiempo(string tiempo){
		this.tiempo = tiempo;
	}
	public void setPosicion(int posicion){
		this.posicion = posicion;
	}
	public int getPosicion(){
		return this.posicion;
	}
	public string getNombreJugador(){
		return this.nombreJugador;
	}
	public string getTiempo(){
		return this.tiempo;
	}

}