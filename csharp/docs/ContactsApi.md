# IO.Swagger.Api.ContactsApi

All URIs are relative to *https://cadastro.restlet.io*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ContactsContactidDelete**](ContactsApi.md#contactscontactiddelete) | **DELETE** /contacts/{contactid} | Delete a Contact
[**ContactsContactidGet**](ContactsApi.md#contactscontactidget) | **GET** /contacts/{contactid} | Load an individual Contact
[**ContactsContactidPut**](ContactsApi.md#contactscontactidput) | **PUT** /contacts/{contactid} | Update a Contact
[**ContactsGet**](ContactsApi.md#contactsget) | **GET** /contacts/ | Get the list of Contacts
[**ContactsPost**](ContactsApi.md#contactspost) | **POST** /contacts/ | Create a Contact


<a name="contactscontactiddelete"></a>
# **ContactsContactidDelete**
> void ContactsContactidDelete (string contactid)

Delete a Contact

Deletes a Contact

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ContactsContactidDeleteExample
    {
        public void main()
        {
            
            // Configure HTTP basic authorization: HTTP_BASIC
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new ContactsApi();
            var contactid = contactid_example;  // string | Identifier of the Contact

            try
            {
                // Delete a Contact
                apiInstance.ContactsContactidDelete(contactid);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ContactsApi.ContactsContactidDelete: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contactid** | **string**| Identifier of the Contact | 

### Return type

void (empty response body)

### Authorization

[HTTP_BASIC](../README.md#HTTP_BASIC)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="contactscontactidget"></a>
# **ContactsContactidGet**
> Contact ContactsContactidGet (string contactid)

Load an individual Contact

Loads a Contact

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ContactsContactidGetExample
    {
        public void main()
        {
            
            var apiInstance = new ContactsApi();
            var contactid = contactid_example;  // string | Identifier of the Contact

            try
            {
                // Load an individual Contact
                Contact result = apiInstance.ContactsContactidGet(contactid);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ContactsApi.ContactsContactidGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contactid** | **string**| Identifier of the Contact | 

### Return type

[**Contact**](Contact.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="contactscontactidput"></a>
# **ContactsContactidPut**
> Contact ContactsContactidPut (string contactid, Contact body)

Update a Contact

Stores a Contact

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ContactsContactidPutExample
    {
        public void main()
        {
            
            // Configure HTTP basic authorization: HTTP_BASIC
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new ContactsApi();
            var contactid = contactid_example;  // string | Identifier of the Contact
            var body = new Contact(); // Contact | 

            try
            {
                // Update a Contact
                Contact result = apiInstance.ContactsContactidPut(contactid, body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ContactsApi.ContactsContactidPut: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **contactid** | **string**| Identifier of the Contact | 
 **body** | [**Contact**](Contact.md)|  | 

### Return type

[**Contact**](Contact.md)

### Authorization

[HTTP_BASIC](../README.md#HTTP_BASIC)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="contactsget"></a>
# **ContactsGet**
> List<Contact> ContactsGet (int? size = null, int? page = null, string sort = null, string firstName = null, string lastName = null, bool? active = null, string company = null)

Get the list of Contacts

Loads a list of Contact

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ContactsGetExample
    {
        public void main()
        {
            
            var apiInstance = new ContactsApi();
            var size = 56;  // int? | Size of the page to retrieve. (optional) 
            var page = 56;  // int? | Number of the page to retrieve. (optional) 
            var sort = sort_example;  // string | Order in which to retrieve the results. Multiple sort criteria can be passed. (optional) 
            var firstName = firstName_example;  // string | Allows to filter the collections of result by the value of field firstName (optional) 
            var lastName = lastName_example;  // string | Allows to filter the collections of result by the value of field lastName (optional) 
            var active = true;  // bool? | Allows to filter the collections of result by the value of field active (optional) 
            var company = company_example;  // string | Allows to filter the collections of result by the value of field company (optional) 

            try
            {
                // Get the list of Contacts
                List&lt;Contact&gt; result = apiInstance.ContactsGet(size, page, sort, firstName, lastName, active, company);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ContactsApi.ContactsGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **size** | **int?**| Size of the page to retrieve. | [optional] 
 **page** | **int?**| Number of the page to retrieve. | [optional] 
 **sort** | **string**| Order in which to retrieve the results. Multiple sort criteria can be passed. | [optional] 
 **firstName** | **string**| Allows to filter the collections of result by the value of field firstName | [optional] 
 **lastName** | **string**| Allows to filter the collections of result by the value of field lastName | [optional] 
 **active** | **bool?**| Allows to filter the collections of result by the value of field active | [optional] 
 **company** | **string**| Allows to filter the collections of result by the value of field company | [optional] 

### Return type

[**List<Contact>**](Contact.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="contactspost"></a>
# **ContactsPost**
> Contact ContactsPost (Contact body)

Create a Contact

Adds a Contact

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ContactsPostExample
    {
        public void main()
        {
            
            // Configure HTTP basic authorization: HTTP_BASIC
            Configuration.Default.Username = "YOUR_USERNAME";
            Configuration.Default.Password = "YOUR_PASSWORD";

            var apiInstance = new ContactsApi();
            var body = new Contact(); // Contact | 

            try
            {
                // Create a Contact
                Contact result = apiInstance.ContactsPost(body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ContactsApi.ContactsPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**Contact**](Contact.md)|  | 

### Return type

[**Contact**](Contact.md)

### Authorization

[HTTP_BASIC](../README.md#HTTP_BASIC)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

