
using System;

public static class sesionManager 
{
    public static string UserName { get; private set; }
    public static string Password { get; private set; }
    public static DateTime SessionExpiration { get; private set; } // Nueva variable para almacenar la fecha de expiración


    public static void SetCredentials(string userName, string password)
    {
        UserName = userName;
        Password = password;
        SessionExpiration = DateTime.Now.AddDays(30); // Establecer la fecha de expiración de la sesión a 30 días en el futuro
    }
}
