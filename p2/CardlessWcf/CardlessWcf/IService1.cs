using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CardlessWcf
{
[ServiceContract]
    public interface IService1
    {
    [OperationContract]
    List<Movto> GetMovtos(string CutomerName);
    [OperationContract]
    string InsertMovtos(Movto customerInfo);
    [OperationContract]
     string CrearSession();
        [OperationContract]
        string SolicitudDelogin(String Token);
        [OperationContract]
        string ConsultaDeSaldo(string Token, string numCuenta);
        [OperationContract]
        string Transferencia(String Token, String numCuentaDeRetiro, String NumCuentaDeDeposito, String Motivo, String Monto);
        // TODO: agregue aquí sus operaciones de servicio
    }
    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    [DataContract]
    public class Movto
    {
            [DataMember]
            public string Usuario;
            [DataMember]
            public string Cuenta;
            [DataMember]
            public string Banco;
            [DataMember]
            public string Nombre;
            [DataMember]
            public decimal Monto;
            [DataMember]
            public string codigoCobro;
            [DataMember]
            public string Respuesta;
    }
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
