using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication6
{
    class ResSession
    {
        public string Codigo, Token;
        public ResSession(string scodigo, string sToken)
        {
            this.Codigo = scodigo;
            this.Token = sToken;
        }
    }
    class ResLogin
    {
        public string Codigo, Username, Status;
        public ResLogin(string scodigo, string susername, string sstatus)
        {
            this.Codigo = scodigo;
            this.Username = susername;
            this.Status = sstatus;
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

    class CuentaSaldo
    {
        public Saldo saldo1,saldo2,saldo3;
        public String NombreDelaCuenta;
        public CuentaSaldo(Saldo ssaldo1, Saldo ssaldo2, Saldo ssaldo3, String SnombreDeLaCuenta)
        {
            this.saldo1 = ssaldo1;
            this.saldo2 = ssaldo2;
            this.saldo3 = ssaldo3;
            this.NombreDelaCuenta = SnombreDeLaCuenta;
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
        public ListaOperaciones(String Code,String Msg, String Account, List<FilasOperaciones> foop)
        {
            this.Code = Code;
            this.Msg = Msg;
            this.Account = Account;
            this.fOperaciones = foop;
        }
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
    class ConsumirServicio
    {
        public ResSession CrearSession()
        {
            var client = new RestClient("http://217.32.246.192:9091/ESBConnector/createSession");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "ca7f5519-b729-9dfa-3fb8-af4ba78cfe0d");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("undefined", "organizationID=COMPARTAMOS&channel=WEBSERVICE&application=CMPXA003&username=TCPIP", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            String[] Datatem = HerramientasGeneralesStaticas.DivisionMessage(response.Content);

            String Code = HerramientasGeneralesStaticas.ResultadoBusqueda(Datatem,"code");
            String Message = HerramientasGeneralesStaticas.ResultadoBusqueda(Datatem,"message");
            String Token = HerramientasGeneralesStaticas.ResultadoBusqueda(Datatem,"token");

            return new ResSession(Code,Token);
            
        }

        public ResLogin SolicitudDelogin(String Token)
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

            return new ResLogin(Code,Username,Status);

        }

        public CuentaSaldo ConsultaDeSaldo(String Token, String numCuenta)
        {
            var client = new RestClient("http://217.32.246.192:9091/ESBConnector/esb/core/transaction");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Postman-Token", "e86d185a-9141-bdaa-4b59-71166a0d51c4");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("x-access-token",Token );
            request.AddParameter("undefined", "trans_type=BENQ0002&account=01&custaccount="+numCuenta, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            String[] Datatem = HerramientasGeneralesStaticas.DivisionMessage(response.Content); 
                
            List<Saldo> GrupoSaldos = new List<Saldo>();
            for (int countObject = 1; countObject < 4; countObject++)
            {
                String[] Detallemonto = HerramientasGeneralesStaticas.ResultadoBusquedaSoloAmount(Datatem, "Amount",countObject);
                String Monto = Detallemonto[1];
                String Divisa = Detallemonto[0];
                String Operacion = HerramientasGeneralesStaticas.ResultadoBusquedaConDatosRepetidos(Datatem, "Name",countObject);
                String Descripcion = HerramientasGeneralesStaticas.ResultadoBusquedaConDatosRepetidos(Datatem, "Description",countObject);

                GrupoSaldos.Add(new Saldo(Operacion, Descripcion, Monto, Divisa));
            }

            return new CuentaSaldo(GrupoSaldos[0], GrupoSaldos[1], GrupoSaldos[2],HerramientasGeneralesStaticas.ResultadoBusqueda(Datatem, "accountHolderName"));
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
            String DataOne = Datatem.Substring(0,DataChar);
            int DataChar2 = Datatem.IndexOf(',', DataChar+1);
            String DataTwo = Datatem.Substring(DataChar+1,DataChar2-DataChar);
            int DataChar3 = Datatem.LastIndexOf(',');
            String DataThree = Datatem.Substring(DataChar2+1,DataChar3-DataChar2);

            String[] DataFileThree = DataThree.Split('[');
            List<FilasOperaciones> fooop = new List<FilasOperaciones>();
            String Code = HerramientasGeneralesStaticas.ResultadoBusqueda(DataFileThree, "code");
            String Message = HerramientasGeneralesStaticas.ResultadoBusqueda(DataFileThree, "message");
            String Account = HerramientasGeneralesStaticas.ResultadoBusqueda(DataFileThree, "accountHolderName");
           
            for (int conOperacion=2; conOperacion<DataFileThree.Length; conOperacion++)
            {
                String Referencia = HerramientasGeneralesStaticas.ResultadoBusqueda2(DataFileThree[conOperacion].Split(','), "TrReference");
                String Type = HerramientasGeneralesStaticas.ResultadoBusqueda2(DataFileThree[conOperacion].Split(','), "TrType");
                String Status = HerramientasGeneralesStaticas.ResultadoBusqueda2(DataFileThree[conOperacion].Split(','), "TrStatus");
                String Description = HerramientasGeneralesStaticas.ResultadoBusqueda2(DataFileThree[conOperacion].Split(','), "TrDescription");
                String Fecha = HerramientasGeneralesStaticas.ResultadoBusqueda2(DataFileThree[conOperacion].Split(','), "TrDate");
                String Monto = HerramientasGeneralesStaticas.ResultadoBusqueda2(DataFileThree[conOperacion].Split(','), "TrAmount");
                String Divisa = HerramientasGeneralesStaticas.ResultadoBusqueda2(DataFileThree[conOperacion].Split(','), "Currency");
              
                fooop.Add(new FilasOperaciones(Referencia,Type,Status,Description,Fecha,Monto,Divisa));
            }
            String DataFour = Datatem.Substring(DataChar3 + 1);
           // String[] NumTransacciones = Datatem[2].Substring(6).Split(',');
            int ced=Datatem.Length;

        }

        public ResultadoTransaccion Transferencia(String Token, String numCuentaDeRetiro, String NumCuentaDeDeposito,String Motivo,String Monto)
        {
            var client = new RestClient("http://217.32.246.192:9091/ESBConnector/esb/core/transaction");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "59d99c3e-1392-bf29-2894-17e9da2e8eec");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("x-access-token", Token);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("undefined", "trans_type=INTTFR0002&account=00&fromaccountnumber="+numCuentaDeRetiro+"&toaccountnumber="+NumCuentaDeDeposito+"&amount="+Monto+"&reference="+Motivo.Trim()+"=", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            String[] Datatem = HerramientasGeneralesStaticas.DivisionMessage(response.Content);
            String Codigo = HerramientasGeneralesStaticas.ResultadoBusquedaConDatosRepetidos(Datatem, "code", 2);
            String Message = HerramientasGeneralesStaticas.ResultadoBusquedaConDatosRepetidos(Datatem, "message", 2);
            String IDAutorizacion = HerramientasGeneralesStaticas.ResultadoBusqueda(Datatem, "authorisationId");
            String IDTransaccion = HerramientasGeneralesStaticas.ResultadoBusqueda(Datatem, "transactionId");
            String EverestTrans= HerramientasGeneralesStaticas.ResultadoBusqueda(Datatem, "everest_ref");
            return new ResultadoTransaccion(Codigo, Message, IDAutorizacion, IDTransaccion, EverestTrans);

        }
    }
}
