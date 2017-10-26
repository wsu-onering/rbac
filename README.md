# OneRing
Dashboard software with extensible portlets

# Running this software

This software is meant to run on the Microsoft Azure platform. It is originally based on code found [in this Azure sample.](https://github.com/Azure-Samples/active-directory-dotnet-graphapi-web) If these instructions for configuring and deploying this software aren't enough, we advise you to look at and use the instructions from the [original Azure sample (the same one mentioned previously).](https://github.com/Azure-Samples/active-directory-dotnet-graphapi-web) The following instructions are copied from the README of that sample, as of 10/26/2017.

## Running OneRing locally with your own AAD tenant

### Register the OneRing app in your tenant

1. Sign in to the [Azure portal](https://portal.azure.com).
2. On the top right, click on your account and under the **Directory** list, choose an Active Directory tenant where you have admin permissions.
3. Type **App registrations** in the search filter.
4. Click on **App registrations** and choose **Add**.
5. Enter a friendly name for the application, for example 'OneRing' and select 'Web Application and/or Web API' as the Application Type. For the sign-on URL, enter the base URL for the application, which is by default `https://localhost:51716/` (the port value is modifiable within `OneRing.csproj`). **NOTE:** It is important, due to the way Azure AD matches URLs, to ensure there is a trailing slash on the end of this URL. If you don't include the trailing slash, you will receive an error when the application attempts to redeem an authorization code. Click on **Create** to create the application.
6. While still in the Azure portal, choose your application, click on **Settings** and choose **Properties**.
7. Find the Application ID value and copy it to the clipboard.
8. For the App ID URI, enter `https://<your_tenant_name>/OneRing`, replacing `<your_tenant_name>` with the domain name of your Azure AD tenant. For Example "https://contoso.com/OneRing".
9. In the Reply URL, add the reply URL address used to return the authorization code returned during Authorization code flow, eg https://localhost:>51716/". **NOTE:** If you see TLS error messages, try changing your reply URL to use http instead of http*s*.
10. From the Settings menu, choose **Keys** and add a key - select a key duration of either 1 year or 2 years. When you save this page, the key value will be displayed, copy and save the value in a safe location - you will need this key later to configure the project in Visual Studio - this key value will not be displayed again, nor retrievable by any other means, so please record it as soon as it is visible from the Azure Portal.
11. Configure Permissions for your application - in the Settings menu, choose the 'Required permissions' section, click on **Add**, then **Select an API**, and select 'Windows Azure Active Directory' (this is the AADGraph API). Then, click on  **Select Permissions** and select 'Access the directory as the signed-in user' and 'Sign in and read user profile'.

NOTE: the permission "Access the directory as the signed-in user" allows the application to access your organization's directory on behalf of the signed-in user - this is a delegation permission and must be consented by the Administrator for web apps (such as this demo app).
The permission "Sign in and read user profile' profiles" allows users to sign in to the application with their organizational accounts and lets the application read the profiles of signed-in users, such as their email address and contact information - this is a delegation permission, and can be consented to by the user.

### Configure OneRing to use your tenant

1. You will need to update the `web.config` file of OneRing. From Visual Studio, open the `web.config` file, and under the `<appSettings>` section, modify `"ida:ClientId"` and `"ida:AppKey"` with the values from the previous steps.  Also update the `"ida:Tenant"` with your Azure AD Tenant's domain name e.g. `contoso.onMicrosoft.com`, (or `contoso.com` if that domain is owned by your tenant).
2. Find your tenantID. Your tenantId can be discovered by opening the following metadata.xml document: https://login.microsoftonline.com/GraphDir1.onmicrosoft.com/FederationMetadata/2007-06/FederationMetadata.xml - replace "graphDir1.onMicrosoft.com", with your tenant's domain value (any domain that is owned by the tenant will work). The tenantId is a guid, that is part of the sts URL, returned in the first xml node's sts url ("EntityDescriptor"): e.g. "https://sts.windows.net/".
3. In `web.config` add this line in the `<system.web>` section: `<sessionState timeout="525600" />`.  This increases the ASP.Net session state timeout to it's maximum value so that access tokens and refresh tokens cache in session state aren't cleared after the default timeout of 20 minutes.
4. Build and run your application - you will need to authenticate with valid user credentials for your company when you run the application. **Note:** If you get "missing reference" errors, enable Nuget Package Restore and try building again.

## How To Deploy This Application to Azure

To deploy OneRing to Azure Web Sites, you will create a web site, publish the Visual Studio project to the site, and update your tenant to use the web site instead of IIS Express.

### Create and Publish OneRing to an Azure Web Site

1. Sign in to the [Azure portal](https://portal.azure.com).
2. Click New in the top left hand corner, select Web + Mobile --> Web App, select the hosting plan and region, and give your web site a name, e.g. onering-yourusernamehere.azurewebsites.net.  Click Create Web Site.
3. Once the web site is created, click on it to manage it.  For this set of steps, download the publish profile and save it.  Other deployment mechanisms, such as from source control, can also be used.
4. Switch to Visual Studio and go to the OneRing project.  Right click on the project in the Solution Explorer and select Publish.  Click Import, and import the publish profile that you just downloaded.
5. On the Connection tab, update the Destination URL so that it is https, for example https://onering-yourusernamehere.azurewebsites.net.  Click Next.
6. On the Settings tab, make sure Enable Organizational Authentication is NOT selected.  Click Publish.
7. Visual Studio will publish the project and automatically open a browser to the URL of the project.  If you see the default web page of the project, the publication was successful.

### Update the Application Configurations in the Directory Tenant

1. Navigate to the [Azure portal](https://portal.azure.com).
2. On the top bar, click on your account and under the **Directory** list, choose the Active Directory tenant where you wish to register your application.
3. On the applications tab, select the appropriate application.
4. From the Settings -> Properties and Settings -> Reply URLs menus, update the Sign-On URL and Reply URL fields to the address of your service, for example https://onering-yourusernamehere.azurewebsites.net/.  Save the configuration.
