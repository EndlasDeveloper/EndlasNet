#pragma checksum "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "91295ce2c9131c9d569ea42009330af584444400"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_PartsForAJob__DetailsAndDelete), @"mvc.1.0.view", @"/Views/PartsForAJob/_DetailsAndDelete.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"91295ce2c9131c9d569ea42009330af584444400", @"/Views/PartsForAJob/_DetailsAndDelete.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1b56846bc26cdbd87384895e8a386d772c1991b0", @"/Views/_ViewImports.cshtml")]
    public class Views_PartsForAJob__DetailsAndDelete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EndlasNet.Data.PartForJob>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/img/no_image.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("max-width:450px;max-height:450px"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n<div>\r\n    <h4>Part for a job</h4>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 9 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
       Write(Html.DisplayNameFor(model => model.WorkItem.Work));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 12 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
       Write(Html.DisplayFor(model => model.WorkItem.Work.WorkDescription));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 16 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
       Write(Html.DisplayNameFor(model => model.Suffix));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 19 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
       Write(Html.DisplayFor(model => model.Suffix));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 22 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
       Write(Html.DisplayNameFor(model => model.ConditionDescription));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 25 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
       Write(Html.DisplayFor(model => model.ConditionDescription));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 28 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
       Write(Html.DisplayNameFor(model => model.InitWeight));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 31 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
       Write(Html.DisplayFor(model => model.InitWeight));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 34 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
       Write(Html.DisplayNameFor(model => model.CladdedWeight));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 37 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
       Write(Html.DisplayFor(model => model.CladdedWeight));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 40 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
       Write(Html.DisplayNameFor(model => model.FinishedWeight));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 43 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
       Write(Html.DisplayFor(model => model.FinishedWeight));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 46 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
       Write(Html.DisplayNameFor(model => model.ProcessingNotes));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 49 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
       Write(Html.DisplayFor(model => model.ProcessingNotes));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        \r\n    </dl>\r\n    <br />\r\n    <br />\r\n\r\n    <h6 style=\"color:lightgray\" class=\"col-md-10 col-sm-10 col-lg-10\">\r\n\r\n        <em>\r\n            Machining image\r\n        </em>\r\n    </h6>\r\n\r\n    <div class=\"col-sm-2\">\r\n");
#nullable restore
#line 64 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
         if (Model.MachiningImageBytes == null)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "91295ce2c9131c9d569ea42009330af5844444009569", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 67 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"

        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <img");
            BeginWriteAttribute("src", " src=\"", 2066, "\"", 2122, 1);
#nullable restore
#line 71 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
WriteAttributeValue("", 2072, Html.DisplayFor(model => model.MachiningImageUrl), 2072, 50, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"max-width:450px;max-height:450px\" alt=\"Machining Image\" />\r\n");
#nullable restore
#line 72 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n    <br />\r\n    <h6 style=\"color:lightgray\" class=\"col-md-10 col-sm-10 col-lg-10\">\r\n        <em>\r\n            Cladding image\r\n        </em>\r\n    </h6>\r\n\r\n    <div class=\"col-sm-2\">\r\n");
#nullable restore
#line 82 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
         if (Model.CladdingImageBytes == null)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "91295ce2c9131c9d569ea42009330af58444440012201", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 85 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <img");
            BeginWriteAttribute("src", " src=\"", 2594, "\"", 2649, 1);
#nullable restore
#line 88 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
WriteAttributeValue("", 2600, Html.DisplayFor(model => model.CladdingImageUrl), 2600, 49, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"max-width:450px;max-height:450px\" alt=\"Cladding Image\" />\r\n");
#nullable restore
#line 89 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n    <br />\r\n    <h6 style=\"color:lightgray\" class=\"col-md-10 col-sm-10 col-lg-10\">\r\n\r\n        <em>\r\n            Finished image\r\n        </em>\r\n    </h6>\r\n\r\n    <div class=\"col-sm-2\">\r\n");
#nullable restore
#line 100 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
         if (Model.FinishedImageBytes == null)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "91295ce2c9131c9d569ea42009330af58444440014835", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 103 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <img");
            BeginWriteAttribute("src", " src=\"", 3122, "\"", 3177, 1);
#nullable restore
#line 106 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
WriteAttributeValue("", 3128, Html.DisplayFor(model => model.FinishedImageUrl), 3128, 49, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"max-width:450px;max-height:450px\" alt=\"Finished Image\" />\r\n");
#nullable restore
#line 107 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n    <br />\r\n    <h6 style=\"color:lightgray\" class=\"col-md-10 col-sm-10 col-lg-10\">\r\n        <em>\r\n            Used image\r\n        </em>\r\n    </h6>\r\n\r\n    <div class=\"col-sm-2\">\r\n");
#nullable restore
#line 117 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
         if (Model.UsedImageBytes == null)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "91295ce2c9131c9d569ea42009330af58444440017460", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" ");
#nullable restore
#line 119 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
                                                                                      }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <img");
            BeginWriteAttribute("src", " src=\"", 3631, "\"", 3682, 1);
#nullable restore
#line 122 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
WriteAttributeValue("", 3637, Html.DisplayFor(model => model.UsedImageUrl), 3637, 45, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"max-width:450px;max-height:450px\" alt=\"Used Image\" />\r\n");
#nullable restore
#line 123 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\_DetailsAndDelete.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n    \r\n    <br />\r\n    <br />\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EndlasNet.Data.PartForJob> Html { get; private set; }
    }
}
#pragma warning restore 1591
