if (Test-Path "IIS:\Sites\WebRoleA4C_IN_0_Web\aspnet_client") 
{ 
    echo "Virtual Directory exists"; 
} 
else 
{
    New-WebVirtualDirectory -Site "WebRoleA4C_IN_0_Web" -Name aspnet_client -PhysicalPath D:\inetpub\wwwroot\aspnet_client; 
    echo "Virtual Directory created"; 
}