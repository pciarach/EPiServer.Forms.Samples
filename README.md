# EPiServer.Forms.Samples

Open source of EPiServer.Forms.Samples package (which is published here http://nuget.episerver.com/en/OtherPages/Package/?packageId=EPiServer.Forms.Samples)
This compatibles with Forms 4.x API.

This is not intended to be a starter solution but provides the ability to showcase a number of features that may be needed for developing Forms extensions.
This package contains extra Form Elements, which required more dependency on complex JS libraries.

We will push code here after every release.
From 2017, we only maintain the source-code here without building the package.



Release note
============
v3.3.0

- EPiServerForm 4.3 improves the way resources (scripts, CSS) are loaded. If a form element needs its own resource, it must implement the IElementRequireClientResources interface. 
- The Forms element resources are loaded after Forms and external resources. 
- For example, the DateTime element requires jquery daytime to work but it is not loaded until the element is dragged into the form.
