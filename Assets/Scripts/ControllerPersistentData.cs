using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class ControllerPersistentData
{
    //-------------------Ruta del fichero------------------
    private static string dataPath = Application.persistentDataPath+"/points.txt";
    //-----Application.persistentDataPath --> Ruta que proporciona Unity para la persistencia
    //de datos multiplataforma.

    public static void savePoints(int points, DateTime date)
    {
        //---------Instanciamos la clase que vamos a serializar para guardar los puntos------
        PersistentData persistentData = new PersistentData(points, date);
        //-----------------------------------------------------------------------------------
        StreamWriter streamWriter = new StreamWriter(dataPath,true);
        //---------SERIALIZACION Y GUARDADO----------------------
        streamWriter.WriteLine(persistentData.formatoString());
        //---------CERRAMOS EL FICHERO-----------------------
        streamWriter.Close();
        //------------------------------------
        Debug.Log("Datos guardados " + dataPath);
    }

    public static string loadPoints()
    {
        //SI EL FICHERO EXISTE------------------------------------
        if (File.Exists(dataPath))
        {
            string aux="";
            //---------------------------------------------------
            StreamReader streamReader = new StreamReader(dataPath);
            //---------DESERIALIZAMOS---------------------------------
            string persistentData= streamReader.ReadLine();
            while (persistentData!= null)
            {
                aux = aux + persistentData + "\n";
                persistentData = streamReader.ReadLine();

            }

            //---------CERRAMOS EL FICHERO-----------------------
            streamReader.Close();
            //------------------------------------
            Debug.Log("Datos obtenidos " + dataPath);
            //-------------------------------------
            return aux;
        }
        else
        {
            Debug.Log("El fichero " + dataPath + " no existe.");
            return "No hay puntuaciones guardadas.";
        }
    }

    public static void resetPoints() {
        File.Delete(dataPath);
    }

}
