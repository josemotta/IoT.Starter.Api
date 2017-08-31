# IO.Swagger.Api.CompaniesApi

All URIs are relative to *https://cadastro.restlet.io*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CompaniesCompanyidDelete**](CompaniesApi.md#companiescompanyiddelete) | **DELETE** /companies/{companyid} | Delete a Company
[**CompaniesCompanyidGet**](CompaniesApi.md#companiescompanyidget) | **GET** /companies/{companyid} | Load an individual Company
[**CompaniesCompanyidPut**](CompaniesApi.md#companiescompanyidput) | **PUT** /companies/{companyid} | Update a Company
[**CompaniesGet**](CompaniesApi.md#companiesget) | **GET** /companies/ | Load the list of Companies
[**CompaniesPost**](CompaniesApi.md#companiespost) | **POST** /companies/ | Create a new Company


<a name="companiescompanyiddelete"></a>
# **CompaniesCompanyidDelete**
> void CompaniesCompanyidDelete (string companyid)

Delete a Company

Deletes a Company

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CompaniesCompanyidDeleteExample
    {
        public void main()
        {
            
            // Configure HTTP basic authorization: HTTP_BASIC
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new CompaniesApi();
            var companyid = companyid_example;  // string | Identifier of the Company

            try
            {
                // Delete a Company
                apiInstance.CompaniesCompanyidDelete(companyid);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CompaniesApi.CompaniesCompanyidDelete: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **companyid** | **string**| Identifier of the Company | 

### Return type

void (empty response body)

### Authorization

[HTTP_BASIC](../README.md#HTTP_BASIC)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="companiescompanyidget"></a>
# **CompaniesCompanyidGet**
> Company CompaniesCompanyidGet (string companyid)

Load an individual Company

Loads a Company

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CompaniesCompanyidGetExample
    {
        public void main()
        {
            
            var apiInstance = new CompaniesApi();
            var companyid = companyid_example;  // string | Identifier of the Company

            try
            {
                // Load an individual Company
                Company result = apiInstance.CompaniesCompanyidGet(companyid);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CompaniesApi.CompaniesCompanyidGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **companyid** | **string**| Identifier of the Company | 

### Return type

[**Company**](Company.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="companiescompanyidput"></a>
# **CompaniesCompanyidPut**
> Company CompaniesCompanyidPut (string companyid, Company body)

Update a Company

Stores a Company

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CompaniesCompanyidPutExample
    {
        public void main()
        {
            
            // Configure HTTP basic authorization: HTTP_BASIC
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new CompaniesApi();
            var companyid = companyid_example;  // string | Identifier of the Company
            var body = new Company(); // Company | 

            try
            {
                // Update a Company
                Company result = apiInstance.CompaniesCompanyidPut(companyid, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CompaniesApi.CompaniesCompanyidPut: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **companyid** | **string**| Identifier of the Company | 
 **body** | [**Company**](Company.md)|  | 

### Return type

[**Company**](Company.md)

### Authorization

[HTTP_BASIC](../README.md#HTTP_BASIC)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="companiesget"></a>
# **CompaniesGet**
> List<Company> CompaniesGet (int? size = null, decimal? page = null, string sort = null, string name = null)

Load the list of Companies

Loads a list of Company

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CompaniesGetExample
    {
        public void main()
        {
            
            var apiInstance = new CompaniesApi();
            var size = 56;  // int? | Size of the page to retrieve. (optional) 
            var page = 3.4;  // decimal? | Number of the page to retrieve. (optional) 
            var sort = sort_example;  // string | Order in which to retrieve the results. Multiple sort criteria can be passed. Example: sort=age ASC,height DESC (optional) 
            var name = name_example;  // string | Allows to filter the collections of result by the value of field name (optional) 

            try
            {
                // Load the list of Companies
                List&lt;Company&gt; result = apiInstance.CompaniesGet(size, page, sort, name);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CompaniesApi.CompaniesGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **size** | **int?**| Size of the page to retrieve. | [optional] 
 **page** | **decimal?**| Number of the page to retrieve. | [optional] 
 **sort** | **string**| Order in which to retrieve the results. Multiple sort criteria can be passed. Example: sort&#x3D;age ASC,height DESC | [optional] 
 **name** | **string**| Allows to filter the collections of result by the value of field name | [optional] 

### Return type

[**List<Company>**](Company.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="companiespost"></a>
# **CompaniesPost**
> Company CompaniesPost (Company body)

Create a new Company

Adds a Company

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CompaniesPostExample
    {
        public void main()
        {
            
            // Configure HTTP basic authorization: HTTP_BASIC
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new CompaniesApi();
            var body = new Company(); // Company | 

            try
            {
                // Create a new Company
                Company result = apiInstance.CompaniesPost(body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CompaniesApi.CompaniesPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**Company**](Company.md)|  | 

### Return type

[**Company**](Company.md)

### Authorization

[HTTP_BASIC](../README.md#HTTP_BASIC)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

