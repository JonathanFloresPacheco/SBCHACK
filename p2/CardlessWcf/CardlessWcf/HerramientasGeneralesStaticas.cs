using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardlessWcf
{
    public class HerramientasGeneralesStaticas
    {
        public static String ResultadoBusqueda(String[] arregloabuscar, String DatoEncontrar)
        {
            string respuesta = null;
            //Nomeclatura del dato recibido code:00 separado por un split.
            for (int count = 0; count < arregloabuscar.Length; count++)
            {
                if (arregloabuscar[count].Equals(DatoEncontrar))
                {
                    try
                    {
                        respuesta = arregloabuscar[count + 2];
                        break;
                    }
                    catch
                    {
                        respuesta = null;
                        break;
                    }
                }
            }
            return respuesta;
        }

        public static String ResultadoBusquedaConDatosRepetidos(String[] arregloabuscar, String DatoEncontrar, int NumeroVeces)
        {
            string respuesta = null;
            int irepeticion = 0;
            //Nomeclatura del dato recibido code:00 separado por un split.
            for (int count = 0; count < arregloabuscar.Length; count++)
            {
                if (arregloabuscar[count].Equals(DatoEncontrar))
                {
                    irepeticion++;
                    if (irepeticion == NumeroVeces)
                    {
                        try
                        {
                            respuesta = arregloabuscar[count + 2];
                            break;
                        }
                        catch
                        {
                            respuesta = null;
                            break;
                        }
                    }
                }
            }
            return respuesta;
        }

        public static String[] ResultadoBusquedaSoloAmount(String[] arregloabuscar, String DatoEncontrar, int NumeroVeces)
        {
            List<string> respuesta = new List<string>();
            int irepeticion = 0;
            //Nomeclatura del dato recibido code:00 separado por un split.
            for (int count = 0; count < arregloabuscar.Length; count++)
            {
                if (arregloabuscar[count].Equals(DatoEncontrar))
                {
                    irepeticion++;
                    if (irepeticion == NumeroVeces)
                    {
                        try
                        {
                            respuesta.Add(arregloabuscar[count + 6]);
                            respuesta.Add(arregloabuscar[count + 10]);
                            break;
                        }
                        catch
                        {
                            respuesta = null;
                            break;
                        }
                    }
                }
            }
            return respuesta.ToArray();
        }

        public static String[] DivisionMessage(String Message)
        {
            String Resultado = Message;
            char[] CaracteresEspeciales = { '\\', '"', '{', '}' };
            return Resultado.Split(CaracteresEspeciales, StringSplitOptions.RemoveEmptyEntries);

        }

        public static String ResultadoBusqueda2(String[] arregloabuscar, String DatoEncontrar)
        {
            string respuesta = null;
            //Nomeclatura del dato recibido code:00 separado por un split.
            for (int count = 0; count < arregloabuscar.Length; count++)
            {
                char[] d = { ':', '\\', '"', '{', '}' };
                String[] cadenaTemp = arregloabuscar[count].Split(d, StringSplitOptions.RemoveEmptyEntries);
                if (cadenaTemp[0].Equals(DatoEncontrar))
                {
                    try
                    {
                        respuesta = (cadenaTemp[1]);
                        break;
                    }
                    catch
                    {
                        respuesta = null;
                        break;
                    }
                }
            }
            return respuesta;
        }
    }
}