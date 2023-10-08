﻿using System;
using Calendar;
using Login;
using Menu;
using System.Runtime.CompilerServices;

class Program
{
    static void Main(string[] args)
    {
        Database.Config.Database.EnsureTableExists();

        Console.WriteLine(@$"
        _   _                _____      _ _
        | \ | |              / ____|    | | |
        |  \| | _____      _| |     __ _| | |
        | . ` |/ _ \ \ /\ / / |    / _` | | |
        | |\  |  __/\ V  V /| |___| (_| | | |
        |_| \_|\___| \_/\_/  \_____\__,_|_|_|

        ");
        Console.WriteLine("---------------------------------------------------------");
        Login.Controller.LoginController.ValidationLogin();
        bool showMenu = true;
        while (showMenu)
        {
            showMenu = Menu.Controller.Code.MainMenu();

        }





    }
}










