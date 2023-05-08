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
        string u = "Marte el cuarto planeta del sistema solar está en peligro. La humanidad quiere colonizarlo… Esta es la historia de la tribu " + tribu + " y su joven guardian llamado " + name + " quien ha decidido defender el planeta y a su tribu " + tribu + " de los invasores terrícolas que vienen a saquear todo el " + loQueCultiva + ". " + name + " espero que estés lo suficientemente preparado para defender tu mundo de los invasores humanos... Buena Suerte " + name + ".";
        mensaje.text = u;
    }
    
}
