<%@ Page Language="c#" MasterPageFile="~/MasterPage.master" CodeFile="$UseCase.Name.Pascal$.aspx.cs" AutoEventWireup="false" Inherits="Forms_$UseCase.Name.Pascal$" Title="<%$ Resources: $UseCase.Name.Pascal$ %>"%>

<asp:Content ID="$UseCase.Name.Pascal$Content" ContentPlaceHolderID="UsecaseContent" runat="Server">

<Tobago.Loop(UseCase.Attributes, "Controls")>
</asp:Content>