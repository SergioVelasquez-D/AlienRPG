using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class Historia : MonoBehaviour
{
    private string name;
    private string tribu;
    private string loQueCultiva;
    private string u;
    
    public TMP_Text nombre;
    public TMP_Text clan;
    public TMP_Text material;
    public TMP_Text mensaje;
    // Start is called before the first frame update
    

    public void Boton()
    {
        string name = nombre.text;
        string tribu = clan.text;
        string loQueCultiva = material.text;
        string u ="Marte el cuarto planeta y el sueño para ser colonizado por la humanidad… Esta es la historia de la tribu " + tribu + " y su joven astronauta llamado " + name + " quien se encarga de recolectar el preciado material " + loQueCultiva + " que permite crear infinidad de cosas y que es demasiado valioso para cualquier civilización. " + name + " serás el encargado de defender el material " + loQueCultiva + " y a la tribu " + tribu + " de los invasores alienígenas, espero que estés lo suficientemente preparado para defender tu mundo... Buena Suerte " + name + ".";
        mensaje.text = u;
    }
    
}
