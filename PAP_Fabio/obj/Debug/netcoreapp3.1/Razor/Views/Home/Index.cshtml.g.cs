#pragma checksum "/Users/fabio_oliveira_010/Documents/dev/PAP_Fabio/PAP_Fabio/Views/Home/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "92a10cba1684f2f6e23476c0e3be8cc856bba841"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/Users/fabio_oliveira_010/Documents/dev/PAP_Fabio/PAP_Fabio/Views/_ViewImports.cshtml"
using PAP_Fabio;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/fabio_oliveira_010/Documents/dev/PAP_Fabio/PAP_Fabio/Views/_ViewImports.cshtml"
using PAP_Fabio.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"92a10cba1684f2f6e23476c0e3be8cc856bba841", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1d6586b9f8415df63ff528c0460692d3581c8864", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PAP_Fabio.Models.Utilizador>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "/Users/fabio_oliveira_010/Documents/dev/PAP_Fabio/PAP_Fabio/Views/Home/Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""text-center"">
    <p>&nbsp</p>
    <h4 class=""display-4"">Escola Secundária Padre Benjamim Salgado</h4>
    <p>&nbsp</p>
    <p>Esta APP tem o objetivo de melhorar e facilitar a vida aos alunos substituindo assim o <br />cartão da Escola por um código QR único para todos.</p>

");
#nullable restore
#line 12 "/Users/fabio_oliveira_010/Documents/dev/PAP_Fabio/PAP_Fabio/Views/Home/Index.cshtml"
     if (User.Identity.IsAuthenticated)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <p>&nbsp</p>\n        <div class=\"text-center\">\n            <h4>Código QR de ");
#nullable restore
#line 16 "/Users/fabio_oliveira_010/Documents/dev/PAP_Fabio/PAP_Fabio/Views/Home/Index.cshtml"
                        Write(this.User.Claims.Where(c => c.Type.Contains("givenname")).First().Value);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\n        </div>\n");
            WriteLiteral("        <div class=\"text-center\">\n            <img");
            BeginWriteAttribute("src", " src=\"", 641, "\"", 662, 1);
#nullable restore
#line 20 "/Users/fabio_oliveira_010/Documents/dev/PAP_Fabio/PAP_Fabio/Views/Home/Index.cshtml"
WriteAttributeValue("", 647, Model.codigoQR, 647, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"width:70%\"/>\n        </div>\n");
#nullable restore
#line 22 "/Users/fabio_oliveira_010/Documents/dev/PAP_Fabio/PAP_Fabio/Views/Home/Index.cshtml"

    }

#line default
#line hidden
#nullable disable
            WriteLiteral(")\n</div>\n\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PAP_Fabio.Models.Utilizador> Html { get; private set; }
    }
}
#pragma warning restore 1591
