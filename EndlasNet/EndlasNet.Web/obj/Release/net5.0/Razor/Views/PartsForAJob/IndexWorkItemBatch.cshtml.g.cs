#pragma checksum "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\IndexWorkItemBatch.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "19b547f9ea3b00f5d8f5a7909c78cec190fd3b5b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_PartsForAJob_IndexWorkItemBatch), @"mvc.1.0.view", @"/Views/PartsForAJob/IndexWorkItemBatch.cshtml")]
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
#nullable restore
#line 1 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\IndexWorkItemBatch.cshtml"
using EndlasNet.Data;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"19b547f9ea3b00f5d8f5a7909c78cec190fd3b5b", @"/Views/PartsForAJob/IndexWorkItemBatch.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1b56846bc26cdbd87384895e8a386d772c1991b0", @"/Views/_ViewImports.cshtml")]
    public class Views_PartsForAJob_IndexWorkItemBatch : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<EndlasNet.Data.PartForWork>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/img/up_arrow.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("max-width:14px;max-height:18px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/img/down_arrow.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 5 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\IndexWorkItemBatch.cshtml"
  
    ViewData["Title"] = "Part batch";
    var displayString = "none - none";
    PartForWork element = null;
    if (Model != null)
    {
        element = Model.ToList().FirstOrDefault();
        displayString = element.WorkItem.StaticPartInfo.DrawingNumber + " - " + element.WorkItem.Work.EndlasNumber;
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h3>\r\n    Work item part batch\r\n</h3>\r\n<br />\r\n<h4>\r\n    ");
#nullable restore
#line 21 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\IndexWorkItemBatch.cshtml"
Write(displayString);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</h4>\r\n\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                <div>\r\n                    Suffix\r\n                </div>\r\n                <span style=\"color:firebrick\">\r\n                    <a");
            BeginWriteAttribute("href", " href=\"", 699, "\"", 835, 1);
#nullable restore
#line 32 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\IndexWorkItemBatch.cshtml"
WriteAttributeValue("", 706, Url.Action("IndexWorkItemBatch", "PartsForAJob", new { workItemId = element.WorkItemId, sortOrder = ViewBag.SuffixAscSortParm }), 706, 129, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" title=\"Ascending\">\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "19b547f9ea3b00f5d8f5a7909c78cec190fd3b5b6964", async() => {
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
            WriteLiteral("\r\n                    </a>\r\n                    <a");
            BeginWriteAttribute("href", " href=\"", 1003, "\"", 1140, 1);
#nullable restore
#line 35 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\IndexWorkItemBatch.cshtml"
WriteAttributeValue("", 1010, Url.Action("IndexWorkItemBatch", "PartsForAJob", new { workItemId = element.WorkItemId, sortOrder = ViewBag.SuffixDescSortParm }), 1010, 130, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" title=\"Descending\">\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "19b547f9ea3b00f5d8f5a7909c78cec190fd3b5b8689", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    </a>\r\n                </span>\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 41 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\IndexWorkItemBatch.cshtml"
           Write(Html.DisplayNameFor(model => model.ConditionDescription));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 44 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\IndexWorkItemBatch.cshtml"
           Write(Html.DisplayNameFor(model => model.InitWeight));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 47 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\IndexWorkItemBatch.cshtml"
           Write(Html.DisplayNameFor(model => model.CladdedWeight));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 50 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\IndexWorkItemBatch.cshtml"
           Write(Html.DisplayNameFor(model => model.FinishedWeight));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 53 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\IndexWorkItemBatch.cshtml"
           Write(Html.DisplayNameFor(model => model.ProcessingNotes));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 60 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\IndexWorkItemBatch.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 64 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\IndexWorkItemBatch.cshtml"
               Write(Html.DisplayFor(modelItem => item.Suffix));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n\r\n                <td>\r\n                    ");
#nullable restore
#line 68 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\IndexWorkItemBatch.cshtml"
               Write(Html.DisplayFor(modelItem => item.ConditionDescription));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 71 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\IndexWorkItemBatch.cshtml"
               Write(Html.DisplayFor(modelItem => item.InitWeight));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 74 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\IndexWorkItemBatch.cshtml"
               Write(Html.DisplayFor(modelItem => item.CladdedWeight));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 77 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\IndexWorkItemBatch.cshtml"
               Write(Html.DisplayFor(modelItem => item.FinishedWeight));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 80 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\IndexWorkItemBatch.cshtml"
               Write(Html.DisplayFor(modelItem => item.ProcessingNotes));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n\r\n                <td>\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "19b547f9ea3b00f5d8f5a7909c78cec190fd3b5b14278", async() => {
                WriteLiteral("Edit");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 84 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\IndexWorkItemBatch.cshtml"
                                           WriteLiteral(item.PartForWorkId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" |\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "19b547f9ea3b00f5d8f5a7909c78cec190fd3b5b16493", async() => {
                WriteLiteral("Details");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 85 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\IndexWorkItemBatch.cshtml"
                                              WriteLiteral(item.PartForWorkId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" \r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 88 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\PartsForAJob\IndexWorkItemBatch.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("</table>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<EndlasNet.Data.PartForWork>> Html { get; private set; }
    }
}
#pragma warning restore 1591