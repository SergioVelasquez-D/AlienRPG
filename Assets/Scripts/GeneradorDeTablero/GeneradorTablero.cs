using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GeneradorTablero : MonoBehaviour
{
    /*//Dimensiones del tablero
    static int ancho = 10;
    static int largo = 10;

    //Posicion de la casilla
    static float casillaOffSetX = 2;
    static float casillaOffSetZ = 2;
    
    //Coloca una pestaña en el menu para crear desde el tablero desde Unity
    [MenuItem("Custom/CrearTablero")]

    //Metodo para buscar objetos en jerarquia e instanciar casillas
    static void CrearTablero()
    {
        GameObject casillaPrefab = GameObject.FindGameObjectWithTag("CasillaPrefab");
        GameObject contenedorCasillas = GameObject.FindGameObjectWithTag("Contenedor");
    
        // Crear casillas desde Prefabs, colocarles nombre de "lugar en el tablero"
        for (int i = 0; i < ancho; i++)
        {
            for (int j = 0; j < largo; j++)
            {
                GameObject goCasilla = Instantiate(casillaPrefab, tablero.transform);
                goCasilla.name = $"{i}, {j}";

                goCasilla.transform.position = new Vector3(i*casillaOffSetX, 0.01f, j*casillaOffSetZ);
            }  

        }
    }*/
}