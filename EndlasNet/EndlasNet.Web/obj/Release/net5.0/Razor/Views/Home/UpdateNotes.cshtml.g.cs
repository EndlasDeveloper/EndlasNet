#pragma checksum "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\Home\UpdateNotes.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f9a1625aca5911819bf5677917a2eb2456b43aa2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_UpdateNotes), @"mvc.1.0.view", @"/Views/Home/UpdateNotes.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f9a1625aca5911819bf5677917a2eb2456b43aa2", @"/Views/Home/UpdateNotes.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1b56846bc26cdbd87384895e8a386d772c1991b0", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_UpdateNotes : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/custom-style.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "f9a1625aca5911819bf5677917a2eb2456b43aa24177", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n<style type=\"text/css\" media=\"screen\">\r\n\r\n    #update-notes-li{\r\n        margin-top:5px\r\n    }\r\n\r\n</style>\r\n");
#nullable restore
#line 9 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\Home\UpdateNotes.cshtml"
  
    ViewData["Title"] = "Update notes";

#line default
#line hidden
#nullable disable
            WriteLiteral("<h1 class=\"display-4\">");
#nullable restore
#line 12 "C:\Users\EndlasLaptop2\Desktop\EndlasNet\EndlasNet\EndlasNet.Web\Views\Home\UpdateNotes.cshtml"
                 Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n\r\n<p>\r\n");
            WriteLiteral(@"    <div style=""color:white;font-size:18px;"">Finished tasks</div>
    <ul style=""font-size:14px;line-height:200%;"">
        <li class=""update-notes-li"">
            In MachiningToolForWork: added dropdown / checkbox to select blanking or finishing
            (Discriminator to differentiate whether a tool was used for blanking or finishing).
        </li>
        <li class=""update-notes-li"">
            Removed IRepository interface because of problems it was causing with the need for multiple type casts.
        </li>
        <li class=""update-notes-li"">
            All Powder -> Added Asc - Desc: PowderName
        </li>
        <li class=""update-notes-li"">
            Now checks for initial weight >= weight for powder bottles on edit submit. If weight condition isn't met,
            the user is forwarded back to the create page with a warning message under the Weight form.
        </li>
        <li class=""update-notes-li"">
            Removed Edit from MachiningToolForWork; On delete: now");
            WriteLiteral(@" increments the tool count back for
            the machining tool it was used from (because it is decremented upon row creation).
        </li>
        <li class=""update-notes-li"">
            Put predictable dropdown enforcement inside all repositories (sort IEnumerables before returning them). Fixed
            leftover unnecessary typecasts and parameter over abstraction.
        </li>
        <li class=""update-notes-li"">
            Fixed issue in line items where on edit submit, the row would uninitialize rather than update.
        </li>
        <li class=""update-notes-li"">
            Fixed issue where customer wasn't displaying under job and work order idex.
        </li>
        <li class=""update-notes-li"">
            Referenced repositories as much as possible in the controllers (there are unfinished ones that still use context).
        </li>
        <li class=""update-notes-li"">
            Endlas email is now case insensitive for the user input.
        </li>
    </ul>

");
            WriteLiteral(@"    <div style=""color:white;font-size:18px;"">Todo</div>
    <ul style=""font-size:14px;line-height:200%;"">
        <li class=""update-notes-li"">
            Put deploy date in update notes for reference to when the last update was.
        </li>

        <li class=""update-notes-li"">
            Powder for Part -> Add job select. Aka Powder for part, Powder for part batch, then add an option for something
            in between. Want to be able to distribute powder evenly across user defined set of powder bottles.
            <div>
                <br />



                1. Choose Job/WO <br />
                2. On job submit, populate a check box list with all PartsForWork that belong to that job. <br />
                3. Set powder <br />
                4. Set weight. On submit, the weight will be distributed evenly between all selected PartsForWork. <br />
                <br />
            </div>
        </li>
        <li class=""update-notes-li"">
            Start looking at making");
            WriteLiteral(@" reports from database data.
        </li>
        <li class=""update-notes-li"">
            Redo Tools for work -> Tools for part???
        </li>
        <li class=""update-notes-li"">
            Write unit tests for all of the Utility methods. Write unit tests for the controllers.
        </li>
        <li class=""update-notes-li"">
            Implement repository pattern for more entities and replace the direct context calls in the controllers.
        </li>
    </ul>

");
            WriteLiteral(@"    <div style=""color:white;font-size:18px;"">Bugs and known issues</div>
        <ul style=""font-size:14px;line-height:200%;"">
            <li class=""update-notes-li"">
                Part count is wrong after adding 2 PartsForWork (they both display the 2nd count).
            </li>
            <li class=""update-notes-li"">
                Some delete views have a UI flaw with the delete button not fitting on one line with action links.
            </li>
            <li class=""update-notes-li"">
                From LineItem, when the user clicks manage powders, the bottle powder name is correctly set. If the user
                navigates to a child view, and is forwarded back to index, the bottle powder name is no longer set.
            </li>
            <li class=""update-notes-li"">
                User isn't set for PowderOrders.
            </li>
            <li class=""update-notes-li"">
                Not all delete views have an indication whether other db items will be deleted as a resu");
            WriteLiteral(@"lt of the delete.
            </li>
            <li class=""update-notes-li"">
                When a static part info is created (it has a customer fk reference), and then the customer fk entry is deleted,
                an exception is thrown.
            </li>
            <li class=""update-notes-li"">
                Some views do not have login protection implemented yet.
            </li>
        </ul>


");
            WriteLiteral(@"    <div style=""color:white;font-size:18px;"">Other considerations</div>
    <ul style=""font-size:14px;line-height:200%;"">
        <li class=""update-notes-li"">
            Under create and edit view, mark elements that are optional/required.
        </li>
        <li class=""update-notes-li"">
            Implement start suffix for part batch initialization?
        </li>
        <li class=""update-notes-li"">
            Put a '?' icon on all data access level views (any view that allows user to interact with the db in any way). When clicked,
            show a description of the view's purpose within the context of data access and work flow.
        </li>
        <li class=""update-notes-li"">
            Look at activity logging (or just logging) in ASP.NET EF Core.
        </li>
        <li class=""update-notes-li"">
            Look for a coded UI test library.
        </li>
        <li class=""update-notes-li"">
            Consider paging and which views to implement paging with.
        </li>");
            WriteLiteral(@"
        <li class=""update-notes-li"">
            Consider search bar on views with many rows to filter.
        </li>
        <li class=""update-notes-li"">
            Help tab development.
        </li>
        <li class=""update-notes-li"">
            Style upload buttons and enum referenced dropdowns.
        </li>
    </ul>
</p>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
