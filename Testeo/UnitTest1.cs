using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.IO;
using ParaProbar;

namespace Testeo
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDeJson()
        {

            File.Delete("Configuracion.config");

            ConfiguracionCliente configuracionPersonalizada = new ConfiguracionCliente();
            configuracionPersonalizada.RutaCertificado = @"c:\FE\basura\30707791973\30707791973.pfx";
            configuracionPersonalizada.TimeOut = 10000;
            configuracionPersonalizada.NombreServicio = "wsfe";
            configuracionPersonalizada.ServidorAutorizacion = "https://wsaahomo.afip.gov.ar/ws/services/LoginCms";
            configuracionPersonalizada.Cuit = 30707791973;

            Newtonsoft.Json.JsonSerializer seriali = new JsonSerializer();



            StreamWriter writer = File.CreateText("Configuracion.config");
            JsonTextWriter text = new JsonTextWriter(writer);

            
            string resultado = JsonConvert.SerializeObject(configuracionPersonalizada, Formatting.Indented);

            writer.WriteLine(resultado);

            writer.Close();
      //      seriali.Serialize(writer, configuracionPersonalizada);
           

            Assert.AreEqual(true, File.Exists("Configuracion.config"));

            string otroresultado = File.ReadAllText("Configuracion.config" );

            configuracion config = JsonConvert.DeserializeObject<configuracion>(otroresultado);

            Assert.AreEqual(@"c:\FE\basura\30707791973\30707791973.pfx", config.RutaCertificado);
           
        }

        [TestMethod]
        public void ObtenerFechaBien()
        {





            Assert.AreEqual("20150723", DateTime.Today.ToString("yyyyMMdd"));

        }


    }

    public class configuracion
    {
         [JsonProperty(PropertyName = "RutaCertificado")]
        public string RutaCertificado { get; set; }




    }
}
