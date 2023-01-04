<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="tx_aspnet_webforms_editor._Default" %>
 	<%@ Register Assembly="TXTextControl.Web" Namespace="TXTextControl.Web" TagPrefix="cc1" %>
	
	<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	    <cc1:TextControl ID="TextControl1" runat="server" />
	
	    <input type="button" onclick="insertTable()" value="Insert Table" />
	    
	    <script>
	        function insertTable() {
	            TXTextControl.tables.add(5, 5, 10, function(e) {
	              if (e === true) { // if added
	                TXTextControl.tables.getItem(function(table) {
	                  table.cells.forEach(function(cell) {
	    
	                    cell.setText("Cell text");
	    
	                  });
	                }, null, 10);
	              }
	            })
	        }
        </script>
	
	</asp:Content>