﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión del motor en tiempo de ejecución:2.0.50727.3603
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

// 
// This source code was auto-generated by wsdl, Version=2.0.50727.3038.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name="LoginCmsSoapBinding", Namespace="https://wsaa.afip.gov.ar/ws/services/LoginCms")]
public partial class WSAA : System.Web.Services.Protocols.SoapHttpClientProtocol {
    
    private System.Threading.SendOrPostCallback loginCmsOperationCompleted;
    
    /// <remarks/>
    public WSAA() 
    {
        this.Url = "https://wsaa.afip.gov.ar/ws/services/LoginCms";
        this.Timeout = 60000;
    }
    
    /// <remarks/>
    public event loginCmsCompletedEventHandler loginCmsCompleted;
    
    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://wsaa.view.sua.dvadac.desein.afip.gov", ResponseNamespace="http://wsaa.view.sua.dvadac.desein.afip.gov", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("loginCmsReturn")]
    public string loginCms(string in0) {
        object[] results = this.Invoke("loginCms", new object[] {
                    in0});
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public System.IAsyncResult BeginloginCms(string in0, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("loginCms", new object[] {
                    in0}, callback, asyncState);
    }
    
    /// <remarks/>
    public string EndloginCms(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    /// <remarks/>
    public void loginCmsAsync(string in0) {
        this.loginCmsAsync(in0, null);
    }
    
    /// <remarks/>
    public void loginCmsAsync(string in0, object userState) {
        if ((this.loginCmsOperationCompleted == null)) {
            this.loginCmsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnloginCmsOperationCompleted);
        }
        this.InvokeAsync("loginCms", new object[] {
                    in0}, this.loginCmsOperationCompleted, userState);
    }
    
    private void OnloginCmsOperationCompleted(object arg) {
        if ((this.loginCmsCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.loginCmsCompleted(this, new loginCmsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    /// <remarks/>
    public new void CancelAsync(object userState) {
        base.CancelAsync(userState);
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
public delegate void loginCmsCompletedEventHandler(object sender, loginCmsCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class loginCmsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal loginCmsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    /// <remarks/>
    public string Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((string)(this.results[0]));
        }
    }
}
