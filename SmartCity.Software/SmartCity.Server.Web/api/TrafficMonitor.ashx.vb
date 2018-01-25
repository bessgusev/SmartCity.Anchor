Imports System.Web
Imports System.Web.Services
Imports MySql.Data.MySqlClient

Public Class TrafficMonitor
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim id = context.Request.Params.Get("last_id")
        Dim act = context.Request.Params.Get("act")
        If act IsNot Nothing Then
            If act.ToLower.Equals("clear_base") Then
                Global_asax.SQL.GetJsonResult("TRUNCATE TABLE traffic_monitor;")
                context.Response.Write("OK")
            End If
        End If
        If id IsNot Nothing Then
            context.Response.Write(Global_asax.SQL.GetJsonResult("SELECT * FROM traffic_monitor where id>" + id + " LIMIT 0, 30;"))
        End If

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class