Imports System.Web
Imports System.Web.Services
Imports MySql.Data.MySqlClient

Public Class AccessPointsStates
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.Write(Global_asax.SQL.GetJsonResult("SELECT * FROM access_points;"))
    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class