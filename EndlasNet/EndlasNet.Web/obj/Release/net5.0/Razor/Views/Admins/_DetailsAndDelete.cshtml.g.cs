#pragma checksum "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\Admins\_DetailsAndDelete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1c2ffc8338aa2c014ea54857714e9269dce0231f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admins__DetailsAndDelete), @"mvc.1.0.view", @"/Views/Admins/_DetailsAndDelete.cshtml")]
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
#line 1 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\_ViewImports.cshtml"
using EndlasNet.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\_ViewImports.cshtml"
using EndlasNet.Web.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1c2ffc8338aa2c014ea54857714e9269dce0231f", @"/Views/Admins/_DetailsAndDelete.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1b56846bc26cdbd87384895e8a386d772c1991b0", @"/Views/_ViewImports.cshtml")]
    public class Views_Admins__DetailsAndDelete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EndlasNet.Data.Admin>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<h4>Admin</h4>\r\n<hr />\r\n<dl class=\"row\">\r\n    <dt class=\"col-sm-2\">\r\n        First name\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
#nullable restore
#line 10 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\Admins\_DetailsAndDelete.cshtml"
   Write(Html.DisplayFor(model => model.FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        Last name\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
#nullable restore
#line 16 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\Admins\_DetailsAndDelete.cshtml"
   Write(Html.DisplayFor(model => model.LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        Endlas email\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
#nullable restore
#line 22 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\Admins\_DetailsAndDelete.cshtml"
   Write(Html.DisplayFor(model => model.EndlasEmail));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </dd>\r\n</dl>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EndlasNet.Data.Admin> Html { get; private set; }
    }
}
#pragma warning restore 1591
