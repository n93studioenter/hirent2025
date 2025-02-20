<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NivoSlider.ascx.cs" Inherits="CMSTemplates_DevCms_controls_NivoSlider" %>

<link href="/App_Themes/DevCms/js/slick/slick.css" rel="stylesheet" />

<div  class="autoplay2"> 
            <cms:CMSRepeater runat="server" ID="rp_Img" Path="/he-thong/slideshow/%" ClassNames="CMS.File" EnableViewState="false">
                <ItemTemplate>
		<div>
                   <img src="/getattachment/<%#Eval("FileAttachment","{0}") %><%#Eval("NodeAliasPath") %>.aspx" />
		</div>
                </ItemTemplate>
            </cms:CMSRepeater>
</div>
    
<script src="/App_Themes/DevCms/js/slick/slick.min.js"></script>
<script src="/App_Themes/DevCms/js/slick/truot.js"></script>

