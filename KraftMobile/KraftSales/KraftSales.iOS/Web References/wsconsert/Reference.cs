﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace KraftSales.iOS.wsconsert {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="wsConsertSoap", Namespace="http://tempuri.org/")]
    public partial class wsConsert : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ListarClientesOperationCompleted;
        
        private System.Threading.SendOrPostCallback ListarProdutosOperationCompleted;
        
        private System.Threading.SendOrPostCallback ListarVendedoresOperationCompleted;
        
        private System.Threading.SendOrPostCallback ListarGruposOperationCompleted;
        
        private System.Threading.SendOrPostCallback ListarDetalhesOperationCompleted;
        
        private System.Threading.SendOrPostCallback EnviarPedidoOperationCompleted;
        
        private System.Threading.SendOrPostCallback ConsultarSaldoOperationCompleted;
        
        private System.Threading.SendOrPostCallback EnviarClienteOperationCompleted;
        
        private System.Threading.SendOrPostCallback ConsultarUltimaCompraOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public wsConsert() {
            this.Url = "http://201.55.169.17/wsconsert/wsconsert.asmx";
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event ListarClientesCompletedEventHandler ListarClientesCompleted;
        
        /// <remarks/>
        public event ListarProdutosCompletedEventHandler ListarProdutosCompleted;
        
        /// <remarks/>
        public event ListarVendedoresCompletedEventHandler ListarVendedoresCompleted;
        
        /// <remarks/>
        public event ListarGruposCompletedEventHandler ListarGruposCompleted;
        
        /// <remarks/>
        public event ListarDetalhesCompletedEventHandler ListarDetalhesCompleted;
        
        /// <remarks/>
        public event EnviarPedidoCompletedEventHandler EnviarPedidoCompleted;
        
        /// <remarks/>
        public event ConsultarSaldoCompletedEventHandler ConsultarSaldoCompleted;
        
        /// <remarks/>
        public event EnviarClienteCompletedEventHandler EnviarClienteCompleted;
        
        /// <remarks/>
        public event ConsultarUltimaCompraCompletedEventHandler ConsultarUltimaCompraCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ListarClientes", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ListarClientes(string DataUltAlteracao) {
            object[] results = this.Invoke("ListarClientes", new object[] {
                        DataUltAlteracao});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ListarClientesAsync(string DataUltAlteracao) {
            this.ListarClientesAsync(DataUltAlteracao, null);
        }
        
        /// <remarks/>
        public void ListarClientesAsync(string DataUltAlteracao, object userState) {
            if ((this.ListarClientesOperationCompleted == null)) {
                this.ListarClientesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnListarClientesOperationCompleted);
            }
            this.InvokeAsync("ListarClientes", new object[] {
                        DataUltAlteracao}, this.ListarClientesOperationCompleted, userState);
        }
        
        private void OnListarClientesOperationCompleted(object arg) {
            if ((this.ListarClientesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ListarClientesCompleted(this, new ListarClientesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ListarProdutos", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ListarProdutos(string DataUltAlteracao) {
            object[] results = this.Invoke("ListarProdutos", new object[] {
                        DataUltAlteracao});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ListarProdutosAsync(string DataUltAlteracao) {
            this.ListarProdutosAsync(DataUltAlteracao, null);
        }
        
        /// <remarks/>
        public void ListarProdutosAsync(string DataUltAlteracao, object userState) {
            if ((this.ListarProdutosOperationCompleted == null)) {
                this.ListarProdutosOperationCompleted = new System.Threading.SendOrPostCallback(this.OnListarProdutosOperationCompleted);
            }
            this.InvokeAsync("ListarProdutos", new object[] {
                        DataUltAlteracao}, this.ListarProdutosOperationCompleted, userState);
        }
        
        private void OnListarProdutosOperationCompleted(object arg) {
            if ((this.ListarProdutosCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ListarProdutosCompleted(this, new ListarProdutosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ListarVendedores", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ListarVendedores() {
            object[] results = this.Invoke("ListarVendedores", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ListarVendedoresAsync() {
            this.ListarVendedoresAsync(null);
        }
        
        /// <remarks/>
        public void ListarVendedoresAsync(object userState) {
            if ((this.ListarVendedoresOperationCompleted == null)) {
                this.ListarVendedoresOperationCompleted = new System.Threading.SendOrPostCallback(this.OnListarVendedoresOperationCompleted);
            }
            this.InvokeAsync("ListarVendedores", new object[0], this.ListarVendedoresOperationCompleted, userState);
        }
        
        private void OnListarVendedoresOperationCompleted(object arg) {
            if ((this.ListarVendedoresCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ListarVendedoresCompleted(this, new ListarVendedoresCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ListarGrupos", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ListarGrupos() {
            object[] results = this.Invoke("ListarGrupos", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ListarGruposAsync() {
            this.ListarGruposAsync(null);
        }
        
        /// <remarks/>
        public void ListarGruposAsync(object userState) {
            if ((this.ListarGruposOperationCompleted == null)) {
                this.ListarGruposOperationCompleted = new System.Threading.SendOrPostCallback(this.OnListarGruposOperationCompleted);
            }
            this.InvokeAsync("ListarGrupos", new object[0], this.ListarGruposOperationCompleted, userState);
        }
        
        private void OnListarGruposOperationCompleted(object arg) {
            if ((this.ListarGruposCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ListarGruposCompleted(this, new ListarGruposCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ListarDetalhes", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ListarDetalhes() {
            object[] results = this.Invoke("ListarDetalhes", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ListarDetalhesAsync() {
            this.ListarDetalhesAsync(null);
        }
        
        /// <remarks/>
        public void ListarDetalhesAsync(object userState) {
            if ((this.ListarDetalhesOperationCompleted == null)) {
                this.ListarDetalhesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnListarDetalhesOperationCompleted);
            }
            this.InvokeAsync("ListarDetalhes", new object[0], this.ListarDetalhesOperationCompleted, userState);
        }
        
        private void OnListarDetalhesOperationCompleted(object arg) {
            if ((this.ListarDetalhesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ListarDetalhesCompleted(this, new ListarDetalhesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/EnviarPedido", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string EnviarPedido(string json) {
            object[] results = this.Invoke("EnviarPedido", new object[] {
                        json});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void EnviarPedidoAsync(string json) {
            this.EnviarPedidoAsync(json, null);
        }
        
        /// <remarks/>
        public void EnviarPedidoAsync(string json, object userState) {
            if ((this.EnviarPedidoOperationCompleted == null)) {
                this.EnviarPedidoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnEnviarPedidoOperationCompleted);
            }
            this.InvokeAsync("EnviarPedido", new object[] {
                        json}, this.EnviarPedidoOperationCompleted, userState);
        }
        
        private void OnEnviarPedidoOperationCompleted(object arg) {
            if ((this.EnviarPedidoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.EnviarPedidoCompleted(this, new EnviarPedidoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ConsultarSaldo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ConsultarSaldo(string Referencia) {
            object[] results = this.Invoke("ConsultarSaldo", new object[] {
                        Referencia});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ConsultarSaldoAsync(string Referencia) {
            this.ConsultarSaldoAsync(Referencia, null);
        }
        
        /// <remarks/>
        public void ConsultarSaldoAsync(string Referencia, object userState) {
            if ((this.ConsultarSaldoOperationCompleted == null)) {
                this.ConsultarSaldoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConsultarSaldoOperationCompleted);
            }
            this.InvokeAsync("ConsultarSaldo", new object[] {
                        Referencia}, this.ConsultarSaldoOperationCompleted, userState);
        }
        
        private void OnConsultarSaldoOperationCompleted(object arg) {
            if ((this.ConsultarSaldoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConsultarSaldoCompleted(this, new ConsultarSaldoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/EnviarCliente", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string EnviarCliente(string json) {
            object[] results = this.Invoke("EnviarCliente", new object[] {
                        json});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void EnviarClienteAsync(string json) {
            this.EnviarClienteAsync(json, null);
        }
        
        /// <remarks/>
        public void EnviarClienteAsync(string json, object userState) {
            if ((this.EnviarClienteOperationCompleted == null)) {
                this.EnviarClienteOperationCompleted = new System.Threading.SendOrPostCallback(this.OnEnviarClienteOperationCompleted);
            }
            this.InvokeAsync("EnviarCliente", new object[] {
                        json}, this.EnviarClienteOperationCompleted, userState);
        }
        
        private void OnEnviarClienteOperationCompleted(object arg) {
            if ((this.EnviarClienteCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.EnviarClienteCompleted(this, new EnviarClienteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ConsultarUltimaCompra", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ConsultarUltimaCompra(string Cnpj) {
            object[] results = this.Invoke("ConsultarUltimaCompra", new object[] {
                        Cnpj});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ConsultarUltimaCompraAsync(string Cnpj) {
            this.ConsultarUltimaCompraAsync(Cnpj, null);
        }
        
        /// <remarks/>
        public void ConsultarUltimaCompraAsync(string Cnpj, object userState) {
            if ((this.ConsultarUltimaCompraOperationCompleted == null)) {
                this.ConsultarUltimaCompraOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConsultarUltimaCompraOperationCompleted);
            }
            this.InvokeAsync("ConsultarUltimaCompra", new object[] {
                        Cnpj}, this.ConsultarUltimaCompraOperationCompleted, userState);
        }
        
        private void OnConsultarUltimaCompraOperationCompleted(object arg) {
            if ((this.ConsultarUltimaCompraCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConsultarUltimaCompraCompleted(this, new ConsultarUltimaCompraCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void ListarClientesCompletedEventHandler(object sender, ListarClientesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ListarClientesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ListarClientesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void ListarProdutosCompletedEventHandler(object sender, ListarProdutosCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ListarProdutosCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ListarProdutosCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void ListarVendedoresCompletedEventHandler(object sender, ListarVendedoresCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ListarVendedoresCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ListarVendedoresCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void ListarGruposCompletedEventHandler(object sender, ListarGruposCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ListarGruposCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ListarGruposCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void ListarDetalhesCompletedEventHandler(object sender, ListarDetalhesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ListarDetalhesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ListarDetalhesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void EnviarPedidoCompletedEventHandler(object sender, EnviarPedidoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class EnviarPedidoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal EnviarPedidoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void ConsultarSaldoCompletedEventHandler(object sender, ConsultarSaldoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConsultarSaldoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ConsultarSaldoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void EnviarClienteCompletedEventHandler(object sender, EnviarClienteCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class EnviarClienteCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal EnviarClienteCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void ConsultarUltimaCompraCompletedEventHandler(object sender, ConsultarUltimaCompraCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConsultarUltimaCompraCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ConsultarUltimaCompraCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
}

#pragma warning restore 1591