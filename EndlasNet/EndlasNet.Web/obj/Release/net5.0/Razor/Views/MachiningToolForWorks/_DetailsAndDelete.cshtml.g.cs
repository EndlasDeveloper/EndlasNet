#pragma checksum "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\MachiningToolForWorks\_DetailsAndDelete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b9f2972d2eb10c5dfb02c401a7e7182d2a63ae55"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_MachiningToolForWorks__DetailsAndDelete), @"mvc.1.0.view", @"/Views/MachiningToolForWorks/_DetailsAndDelete.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b9f2972d2eb10c5dfb02c401a7e7182d2a63ae55", @"/Views/MachiningToolForWorks/_DetailsAndDelete.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1b56846bc26cdbd87384895e8a386d772c1991b0", @"/Views/_ViewImports.cshtml")]
    public class Views_MachiningToolForWorks__DetailsAndDelete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EndlasNet.Data.MachiningToolForWork>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n<h4>Machining tool for work</h4>\r\n<hr />\r\n<dl class=\"row\">\r\n    <dt class=\"col-sm-2\">\r\n        ");
#nullable restore
#line 8 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\MachiningToolForWorks\_DetailsAndDelete.cshtml"
   Write(Html.DisplayNameFor(model => model.DateUsed));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
#nullable restore
#line 11 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\MachiningToolForWorks\_DetailsAndDelete.cshtml"
   Write(Html.DisplayFor(model => model.DateUsed));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
#nullable restore
#line 14 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\MachiningToolForWorks\_DetailsAndDelete.cshtml"
   Write(Html.DisplayNameFor(model => model.MachiningTool.VendorDescription));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
#nullable restore
#line 17 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\MachiningToolForWorks\_DetailsAndDelete.cshtml"
   Write(Html.DisplayFor(model => model.MachiningTool.VendorDescription));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
#nullable restore
#line 20 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\MachiningToolForWorks\_DetailsAndDelete.cshtml"
   Write(Html.DisplayNameFor(model => model.Work));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
#nullable restore
#line 23 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\MachiningToolForWorks\_DetailsAndDelete.cshtml"
   Write(Html.DisplayFor(model => model.Work.WorkDescription));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
#nullable restore
#line 26 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\MachiningToolForWorks\_DetailsAndDelete.cshtml"
   Write(Html.DisplayNameFor(model => model.MachiningType));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
#nullable restore
#line 29 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\MachiningToolForWorks\_DetailsAndDelete.cshtml"
   Write(Html.DisplayFor(model => model.MachiningType));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
#nullable restore
#line 32 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\MachiningToolForWorks\_DetailsAndDelete.cshtml"
   Write(Html.DisplayNameFor(model => model.Comment));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
#nullable restore
#line 35 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\MachiningToolForWorks\_DetailsAndDelete.cshtml"
   Write(Html.DisplayFor(model => model.Comment));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </dd>\r\n    <dt class=\"col-sm-2\">\r\n        ");
#nullable restore
#line 38 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\MachiningToolForWorks\_DetailsAndDelete.cshtml"
   Write(Html.DisplayNameFor(model => model.User));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </dt>\r\n    <dd class=\"col-sm-10\">\r\n        ");
#nullable restore
#line 41 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\MachiningToolForWorks\_DetailsAndDelete.cshtml"
   Write(Html.DisplayFor(model => model.User.EndlasEmail));

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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EndlasNet.Data.MachiningToolForWork> Html { get; private set; }
    }
}
#pragma warning restore 1591
