# ASP.NET Pager ([Demo](http://webformspagernet.azurewebsites.net/default.aspx))
A very simple ASP.NET Pager Control to be used with WebForms.

### Available in [Nuget](https://www.nuget.org/packages/NPager/)
##### Install-Package NPager

###Features
- No setup required. Install and start using it.
- Made on Bootstrap.

###How does it work ?
- Install, Place it in your markup and thats it.
- There needs to be a \<controls> element inside the \<pages> element inside \<system.web> inside \<configuration>. Because Nuget installation automatically inserts the necessary Web.Config settings to register the pager control.
```XML
<system.web>
    <compilation debug="true" targetFramework="4.5.2"/>
    <httpRuntime targetFramework="4.5.2"/>
    <pages>
      <controls>
        <add tagPrefix="NPager" namespace="NPager" assembly="NPager"/> <-- This is automatically inserted. 
      </controls>
    </pages>
  </system.web>
```

###Code Example

This could be the markup on your .aspx page.
```HTML
<!-- You need a reference to Bootstrap since the library currently uses bootstrap classes for styling-->

    <head runat="server">
            <link href="Content/bootstrap.min.css" rel="stylesheet" />
    </head>

    
   <div class="row">
        <asp:GridView runat="server" ID="CustomerGrid"></asp:GridView>
        
        <!-- This is where the pager will be rendered-->
        <NPager:PagerControl runat="server" ID="Pager"></NPager:PagerControl>

    </div>
        
```
Now in the code behind we can have
```C#
//Gridview is being databound at Page_Load
    protected void Page_Load(object sender, EventArgs e)
          {

            CustomerGrid.DataSource = GetPagedCustomer(Pager.PageSize, Pager.Offset);//Get Data From Database or somewhere
            CustomerGrid.DataBind();
            Pager.RecordCount = _customers.Count;
        }
```
  Thats it !
  ##Enjoy Paging.
