﻿using System;
using Login;
using Menu;
using System.Runtime.CompilerServices;
using Students.Controller;

class Precence
{
    static void Main(string[] args)
    {
        Students.Controller.Code.Data();
        Students.Controller.Code.DataA();
        Students.Controller.Code.Clear();

        Console.WriteLine(@$"
        _____       _     _            _                         __
        / ____|     (_)   (_)          | |                       /_/
        | (___   __ _ _ ___ _  ___    __| | ___  ___   _ __  _ __ ___  ___  ___ _ __   ___ ___  ___
        \___ \ / _` | / __| |/ _ \  / _` |/ _ \/ __| | '_ \| '__/ _ \/ __|/ _ \ '_ \ / __/ _ \/ __|
        ____) | (_| | \__ \ |  __/ | (_| |  __/\__ \ | |_) | | |  __/\__ \  __/ | | | (_|  __/\__ \
        |_____/ \__,_|_|___/_|\___|  \__,_|\___||___/ | .__/|_|  \___||___/\___|_| |_|\___\___||___/
                                                    | |
                                                    |_|
        ");
        Console.WriteLine("---------------------------------------------------------");
        Login.Controller.Login.Auth();
        bool showMenu = true;
        while (showMenu)
        {
            showMenu = Menu.Controller.Code.MainMenu();

        }





    }
}










