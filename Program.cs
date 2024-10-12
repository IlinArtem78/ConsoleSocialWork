// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");


/* проверка имение или фамилии что не null. 
 
if (String.IsNullOrEmpty(lastName)) throw new ArgumentNullException();
 
 */


/* проверка пароля
string password = Console.ReadLine();

if (password.Length < 8)
    throw new ArgumentNullException(); */

using ConsoleSocialWork.BLL.Models;
using ConsoleSocialWork.BLL.services;
using System.ComponentModel.DataAnnotations;

class Program
{
    public static UserServices Services= new UserServices();
    public static void Main(string[] args)
    {
        while (true) { 
        Console.WriteLine("Добро пожаловать"); 
        Console.WriteLine();
        Console.WriteLine("Введите имя:"); 
        string firstName = Console.ReadLine();
        Console.WriteLine("Введите фамилию:");
        string lastName = Console.ReadLine();
        Console.WriteLine("Введите Email:");
        string email = Console.ReadLine();
        Console.WriteLine("Введите пароль:");
        string password = Console.ReadLine();
        var userRegistrationData = new UserRegistrationData()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            password = password

        };
        try
        {
            Services.Register(userRegistrationData);
            Console.WriteLine("Регистрация произошла успешно");
        }
        catch(ArgumentNullException)
        {
            Console.WriteLine("Введите корректные данные.");
        }
        catch (Exception ex) {
            Console.WriteLine("Произошла ошибка при регистрации");
        }
        Console.ReadLine();
        }
    }
}