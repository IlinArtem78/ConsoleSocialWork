// See https://aka.ms/new-console-template for more information
using ConsoleSocialWork.BLL.Exceptions;
using ConsoleSocialWork.BLL.Models;
using ConsoleSocialWork.BLL.services;
using ConsoleSocialWork.PLL.Views;
using System.ComponentModel.DataAnnotations;

class Program
{
    static MessageServices messageService;
    static UserServices userService;
    public static MainView mainView;
    public static RegistrationView registrationView;
    public static AuthenticationView authenticationView;
    public static UserMenuView userMenuView;
    public static UserInfoView userInfoView;
    public static UserDataUpdateView userDataUpdateView;
    public static MessageSendingView messageSendingView;
    public static UserIncomingMessageView userIncomingMessageView;
    public static UserOutcomingMessageView userOutcomingMessageView;

    static void Main(string[] args)
    {
        userService = new UserServices();
        messageService = new MessageServices();

        mainView = new MainView();
        registrationView = new RegistrationView(userService);
        authenticationView = new AuthenticationView(userService);
        userMenuView = new UserMenuView(userService);
        userInfoView = new UserInfoView();
        userDataUpdateView = new UserDataUpdateView(userService);
        messageSendingView = new MessageSendingView(messageService, userService);
        userIncomingMessageView = new UserIncomingMessageView();
        userOutcomingMessageView = new UserOutcomingMessageView();

        while (true)
        {
            mainView.Show();
        }
    }

}

