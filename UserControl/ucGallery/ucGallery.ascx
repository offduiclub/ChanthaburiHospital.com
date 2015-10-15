<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucGallery.ascx.cs" Inherits="ucGallery" %>

<%@ Register src="../ucColorBox/ucColorBox.ascx" tagname="ucColorBox" tagprefix="uc1" %>

<style type="text/css">
    .dvPhotoFrame
    {
        border:1px solid #ddd;
        margin:7px;
        float:left;
        display:block;
        width:<%=PreviewWidth%>;
        
        -moz-border-radius:0 0 7px 7px;
	    -webkit-border-radius:0 0 7px 7px;
	    border-radius:0 0 7px 7px;
    }
    .dvPhotoFrame:hover
    {
        border:1px solid #D2D2D2;
        box-shadow:0 0 7px #D2D2D2;
	    -moz-box-shadow:0 0 7px #D2D2D2;
	    -webkit-box-shadow:0 0 7px #D2D2D2;
    }
    .dvPhotoFrame:hover .dvPhotoFrameName
    {
        background-color:#F2F2F2;
    }
    .dvPhotoFramePhoto
    {
        border-bottom:1px solid #E6E6E6;
        width:<%=PreviewWidth%>;
        height:<%=PreviewHeight%>;
        overflow:hidden;float:left;
        background-color:#fff;
    }
    .dvPhotoFramePhoto img
    {
        filter:alpha(opacity=80);
        -moz-opacity:.80;opacity:.80;
    }
    .dvPhotoFramePhoto:hover img
    {
        filter:alpha(opacity=100);
        -moz-opacity:1;opacity:1;
    }
    .dvPhotoFrameName
    {
        width:inherit;
        height:<%=LabelHeight%>;
        overflow:hidden;
        background-color:#fafafa;
        display:table-cell;
        vertical-align:middle;
        text-align:center;
        padding:5px;
        -moz-border-radius:0 0 7px 7px;
	    -webkit-border-radius:0 0 7px 7px;
	    border-radius:0 0 7px 7px;
    }
    .dvGallery
    {
        width:<%=Width%>;
        height:<%=Height%>;
        overflow:auto;
    }
</style>

<uc1:ucColorBox ID="ucColorBox1" runat="server" ColorBoxPhotoName="cbPhoto"/>

<div class="dvGallery">
    <asp:Label ID="lblGallery" runat="server" />
    <asp:DataList ID="dlGallery" runat="server" BorderStyle="None" CellPadding="0" 
        RepeatDirection="Horizontal" ShowFooter="False" ShowHeader="False" 
        RepeatColumns="4" RepeatLayout="Flow" Visible="true" HorizontalAlign="Justify">
        <ItemStyle Wrap="True" />
        <ItemTemplate>
            <div class="dvPhotoFrame">
                <div class="dvPhotoFramePhoto">
                    <a href='<%#DataBinder.Eval(Container.DataItem,SourceFieldPhoto) %>' class="cbPhoto">
                        <img src="<%#PathPhoto %><%#DataBinder.Eval(Container.DataItem, SourceFieldPhotoPreview)%>" alt="<%#DataBinder.Eval(Container.DataItem,SourceFieldName) %>" title="<%#DataBinder.Eval(Container.DataItem,SourceFieldDetail) %>"/>
                    </a>
                </div>
                <div class="dvPhotoFrameName">
                    <%#DataBinder.Eval(Container.DataItem, SourceFieldName)%>
                </div>
            </div>
        </ItemTemplate>
    </asp:DataList>
</div>