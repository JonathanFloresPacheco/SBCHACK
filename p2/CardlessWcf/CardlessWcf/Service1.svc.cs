using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System;
using System.IO;
using System.Security.Cryptography;

namespace CardlessWcf
{

    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
        SqlConnection con = new SqlConnection("Data Source=k4ch4th0nf1nt3ch.database.windows.net;Initial Catalog=Fintech;User ID=Admin2018;Password=Prubas_2018");

        public string Codigo, Token;
        public void ResSession(string scodigo, string sToken)
        {
            this.Codigo = scodigo;
            this.Token = sToken;
        }

        public string CrearSession()
        {
            var client = new RestClient("http://217.32.246.192:9091/ESBConnector/createSession");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "ca7f5519-b729-9dfa-3fb8-af4ba78cfe0d");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("undefined", "organizationID=COMPARTAMOS&channel=WEBSERVICE&application=CMPXA003&username=TCPIP", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            String[] Datatem = HerramientasGeneralesStaticas.DivisionMessage(response.Content);

            String Code = HerramientasGeneralesStaticas.ResultadoBusqueda(Datatem, "code");
            String Message = HerramientasGeneralesStaticas.ResultadoBusqueda(Datatem, "message");
            String Token = HerramientasGeneralesStaticas.ResultadoBusqueda(Datatem, "token");

            return "" + Code + "," + Message + "," + Token + "";
            // new ResSession(Code, Token);
        }

        public string SolicitudDelogin(String Token)
        {
            /*Recordar que, para usar este metodo es necesario haber realizado antes el uso del toekn.*/
            var client = new RestClient("http://217.32.246.192:9091/ESBConnector/service");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Postman-Token", "ec8e6b9f-836c-a253-f966-bbab19943bb2");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("x-access-token", Token);
            request.AddParameter("undefined", "username=TCPIP&password=12345678&action=LOGIN&=", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            String[] Datatem = HerramientasGeneralesStaticas.DivisionMessage(response.Content);

            String Code = HerramientasGeneralesStaticas.ResultadoBusqueda(Datatem, "code");
            String Username = HerramientasGeneralesStaticas.ResultadoBusqueda(Datatem, "username");
            String Status = HerramientasGeneralesStaticas.ResultadoBusqueda(Datatem, "status");
            return "" + Code + "," + Username + "," + Status + "";
            //return new ResLogin(Code, Username, Status);

        }

        public string ConsultaDeSaldo(string Token, string numCuenta)
        {
            try
            {
            var client = new RestClient("http://217.32.246.192:9091/ESBConnector/esb/core/transaction");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Postman-Token", "e86d185a-9141-bdaa-4b59-71166a0d51c4");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("x-access-token", Token);
            request.AddParameter("undefined", "trans_type=BENQ0002&account=01&custaccount=" + numCuenta, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            String[] Datatem = HerramientasGeneralesStaticas.DivisionMessage(response.Content);

            List<Saldo> GrupoSaldos = new List<Saldo>();
            for (int countObject = 1; countObject < 4; countObject++)
            {
                String[] Detallemonto = HerramientasGeneralesStaticas.ResultadoBusquedaSoloAmount(Datatem, "Amount", countObject);
                String Monto = Detallemonto[1];
                String Divisa = Detallemonto[0];
                String Operacion = HerramientasGeneralesStaticas.ResultadoBusquedaConDatosRepetidos(Datatem, "Name", countObject);
                String Descripcion = HerramientasGeneralesStaticas.ResultadoBusquedaConDatosRepetidos(Datatem, "Description", countObject);

                GrupoSaldos.Add(new Saldo(Operacion, Descripcion, Monto, Divisa));
            }
            return "" + GrupoSaldos[0] + "," + GrupoSaldos[1] + "," + GrupoSaldos[2] + "," + HerramientasGeneralesStaticas.ResultadoBusqueda(Datatem, "accountHolderName") + "";
                //return new CuentaSaldo(GrupoSaldos[0], GrupoSaldos[1], GrupoSaldos[2], HerramientasGeneralesStaticas.ResultadoBusqueda(Datatem, "accountHolderName"));
            }
            catch(Exception erc)
            {
                return "Usuario no Existente";
            }
        }

        public void estadoDeCuenta(String Token, String numCuenta)
        {
            var client = new RestClient("http://217.32.246.192:9091/ESBConnector/esb/core/transaction");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Postman-Token", "28aaf7e5-16e7-b1e2-d226-918c2423bab7");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("x-access-token", Token);
            request.AddParameter("undefined", "trans_type=STMNT0002&account=01&custaccount=" + numCuenta, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            String Datatem = response.Content;
            int DataChar = Datatem.IndexOf(',');
            String DataOne = Datatem.Substring(0, DataChar);
            int DataChar2 = Datatem.IndexOf(',', DataChar + 1);
            String DataTwo = Datatem.Substring(DataChar + 1, DataChar2 - DataChar);
            int DataChar3 = Datatem.LastIndexOf(',');
            String DataThree = Datatem.Substring(DataChar2 + 1, DataChar3 - DataChar2);

            String[] DataFileThree = DataThree.Split('[');
            List<FilasOperaciones> fooop = new List<FilasOperaciones>();
            String Code = HerramientasGeneralesStaticas.ResultadoBusqueda(DataFileThree, "code");
            String Message = HerramientasGeneralesStaticas.ResultadoBusqueda(DataFileThree, "message");
            String Account = HerramientasGeneralesStaticas.ResultadoBusqueda(DataFileThree, "accountHolderName");

            for (int conOperacion = 2; conOperacion < DataFileThree.Length; conOperacion++)
            {
                String Referencia = HerramientasGeneralesStaticas.ResultadoBusqueda2(DataFileThree[conOperacion].Split(','), "TrReference");
                String Type = HerramientasGeneralesStaticas.ResultadoBusqueda2(DataFileThree[conOperacion].Split(','), "TrType");
                String Status = HerramientasGeneralesStaticas.ResultadoBusqueda2(DataFileThree[conOperacion].Split(','), "TrStatus");
                String Description = HerramientasGeneralesStaticas.ResultadoBusqueda2(DataFileThree[conOperacion].Split(','), "TrDescription");
                String Fecha = HerramientasGeneralesStaticas.ResultadoBusqueda2(DataFileThree[conOperacion].Split(','), "TrDate");
                String Monto = HerramientasGeneralesStaticas.ResultadoBusqueda2(DataFileThree[conOperacion].Split(','), "TrAmount");
                String Divisa = HerramientasGeneralesStaticas.ResultadoBusqueda2(DataFileThree[conOperacion].Split(','), "Currency");

                fooop.Add(new FilasOperaciones(Referencia, Type, Status, Description, Fecha, Monto, Divisa));
            }
            String DataFour = Datatem.Substring(DataChar3 + 1);
            // String[] NumTransacciones = Datatem[2].Substring(6).Split(',');
            int ced = Datatem.Length;

        }

        public string Transferencia(String Token, String numCuentaDeRetiro, String NumCuentaDeDeposito, String Motivo, String Monto)
        {

            var resRetCnt=Encriptar(numCuentaDeRetiro);
            var client = new RestClient("http://217.32.246.192:9091/ESBConnector/esb/core/transaction");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "59d99c3e-1392-bf29-2894-17e9da2e8eec");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("x-access-token", Token);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("undefined", "trans_type=INTTFR0002&account=00&fromaccountnumber=" + resRetCnt + "&toaccountnumber=" + NumCuentaDeDeposito + "&amount=" + Monto + "&reference=" + Motivo.Trim() + "=", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            String[] Datatem = HerramientasGeneralesStaticas.DivisionMessage(response.Content);
            String Codigo = HerramientasGeneralesStaticas.ResultadoBusquedaConDatosRepetidos(Datatem, "code", 2);
            String Message = HerramientasGeneralesStaticas.ResultadoBusquedaConDatosRepetidos(Datatem, "message", 2);
            String IDAutorizacion = HerramientasGeneralesStaticas.ResultadoBusqueda(Datatem, "authorisationId");
            String IDTransaccion = HerramientasGeneralesStaticas.ResultadoBusqueda(Datatem, "transactionId");
            String EverestTrans = HerramientasGeneralesStaticas.ResultadoBusqueda(Datatem, "everest_ref");
            return "" + Codigo + "," + Message + "," + IDAutorizacion + "," + IDTransaccion + "," + EverestTrans;
            // new ResultadoTransaccion(Codigo, Message, IDAutorizacion, IDTransaccion, EverestTrans);

        }

        public string Encriptar(string original)
        {
            try
            {

                //original = "Here is some data to encrypt!";

                // Create a new instance of the Aes
                // class.  This generates a new key and initialization 
                // vector (IV).
                using (Aes myAes = Aes.Create())
                {
                    var resEncr = StrToByteArray(original);
                    // Encrypt the string to an array of bytes.
                    //byte[] encrypted = EncryptStringToBytes_Aes(original, myAes.Key, myAes.IV);

                    // Decrypt the bytes to a string.
                    string roundtrip = DecryptStringFromBytes_Aes(resEncr, myAes.Key, myAes.IV);
                    return roundtrip;
                    //Display the original data and the decrypted data.
                    //Console.WriteLine("Original:   {0}", original);
                    //Console.WriteLine("Round Trip: {0}", roundtrip);
                }

            }
            catch (Exception e)
            {
                //Console.WriteLine("Error: {0}", e.Message);
                return "";
            }
        }
        public static byte[] StrToByteArray(string str)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            return encoding.GetBytes(str);
        }

        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key,
byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }

        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key
, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key
, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt
, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(
csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream                                             // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }
        public string InsertMovtos(Movto customerInfo)
        {
            string strMessage = string.Empty;
            con.Open();
            SqlCommand cmd = new SqlCommand("AddMovto", con);
            cmd.Parameters.AddWithValue("@usuario", customerInfo.Usuario);
            cmd.Parameters.AddWithValue("@cuenta", customerInfo.Cuenta);
            cmd.Parameters.AddWithValue("@banco", customerInfo.Banco);
            cmd.Parameters.AddWithValue("@Nombre", customerInfo.Nombre);
            cmd.Parameters.AddWithValue("@Monto", customerInfo.Monto);
            cmd.Parameters.AddWithValue("@codigocobro", customerInfo.codigoCobro);
            cmd.Parameters.AddWithValue("@respuesta", customerInfo.Respuesta);
            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                strMessage = customerInfo.codigoCobro + " inserted successfully";
            }
            else
            {
                strMessage = customerInfo.codigoCobro + " not inserted successfully";
            }
            con.Close();
            return strMessage;
        }
        public List<Movto> GetMovtos(string CutomerName)
        {
            List<Movto> CustomerDetails = new List<Movto>();
            {
                string strMessage = string.Empty;
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from movimiento where usuario='@usuario'", con);
                cmd.Parameters.AddWithValue("@usuario", CutomerName);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Movto customerInfo = new Movto();
                        customerInfo.Usuario = dt.Rows[i]["Usuario"].ToString();
                        customerInfo.Cuenta = dt.Rows[i]["Cuenta"].ToString();
                        customerInfo.Banco = dt.Rows[i]["Banco"].ToString();
                        customerInfo.Nombre = dt.Rows[i]["Nombre"].ToString();
                        decimal.TryParse(dt.Rows[i]["Monto"].ToString(), out customerInfo.Monto);
                        customerInfo.codigoCobro = dt.Rows[i]["codigoCobro"].ToString();
                        customerInfo.Respuesta = dt.Rows[i]["Respuesta"].ToString();
                        CustomerDetails.Add(customerInfo);
                    }
                }
                con.Close();
            }
            return CustomerDetails;
        }
    }
    class Saldo
    {
        public string Operacion, Descripcion, Monto, Divisa;
        public Saldo(string soperacion, string sdescripción, string smonto, string sdivisa)
        {
            this.Operacion = soperacion;
            this.Descripcion = sdescripción;
            this.Monto = smonto;
            this.Divisa = sdivisa;
        }

    }
    class FilasOperaciones
    {
        String Referencia, Type, Status, Descripcion, Fecha, Monto, Divisa;
        public FilasOperaciones(String Referencia, String Type, String Status, String Descripcion, String Fecha, String Monto, String Divisa)
        {
            this.Referencia = Referencia;
            this.Type = Type;
            this.Status = Status;
            this.Descripcion = Descripcion;
            this.Fecha = Fecha;
            this.Monto = Monto;
            this.Divisa = Divisa;
        }
    }
    class ListaOperaciones
    {
        String Code, Msg, Account;
        List<FilasOperaciones> fOperaciones = new List<FilasOperaciones>();
        public ListaOperaciones(String Code, String Msg, String Account, List<FilasOperaciones> foop)
        {
            this.Code = Code;
            this.Msg = Msg;
            this.Account = Account;
            this.fOperaciones = foop;
        }


        class ResultadoTransaccion
        {
            public string Codigo, Message, Autorizacion, Transaccion, everest_ref;
            public ResultadoTransaccion(string scodigo, string sMesage, string sAutorizacion, string sTransaccion, string severest_ref)
            {
                this.Codigo = scodigo;
                this.Message = sMesage;
                this.Autorizacion = sAutorizacion;
                this.Transaccion = sTransaccion;
                this.everest_ref = severest_ref;
            }
        }

    }
}
